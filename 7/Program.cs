using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace _7 {
    class Program {
        private static Regex InputRegex = new Regex("([\\w]+ [\\w]+) bags");
        private static Regex OutputRegex = new Regex("([0-9]+) ([\\w]+ [\\w]+) bags?");
        static void Main(string[] args) {
            var file = "input.txt";

            var lines = File.ReadAllLines(file);

            var bagDict = CreateBagDict(lines);
            Part2(bagDict);


        }

        private static void Part1(Dictionary<string, List<BagContents>> dict){
            var colorQueue = new Queue<string>();
            var searchedColors = new HashSet<string>();
            colorQueue.Enqueue("shiny gold");

            while(colorQueue.Any()){
                var currentColor = colorQueue.Dequeue();
                //Console.WriteLine("Dequeued " + currentColor);
                foreach(var kvp in dict){
                    if(kvp.Value.Any(v => v.Color == currentColor) && !searchedColors.Contains(kvp.Key) && !colorQueue.Contains(kvp.Key)){
                        searchedColors.Add(kvp.Key);
                        colorQueue.Enqueue(kvp.Key);
                    }
                }
            }

            Console.WriteLine("Found " + searchedColors.Count() + " colors");
        }

        private static void Part2(Dictionary<string, List<BagContents>> dict) {
            var colorQueue = new Queue<BagContents>();
            var bc = new BagContents{
                Color = "shiny gold",
                Count = 1
            };
            var sum =  GetNumberOfBags(dict, bc);

            Console.WriteLine(sum);
        }

        private static int GetNumberOfBags(Dictionary<string, List<BagContents>> dict, BagContents currentBag){
            if(!dict.ContainsKey(currentBag.Color)){
                return 0;
            }
            var sum = 0;
            foreach(var bc in dict[currentBag.Color]){
                var innerBags = GetNumberOfBags(dict, bc);
                sum = sum + bc.Count + (bc.Count * innerBags);
            }
            return sum;
        }

        static Dictionary<string, List<BagContents>> CreateBagDict(IEnumerable<string> lines){
            var dict = new Dictionary<string, List<BagContents>>(); 
            foreach(var line in lines){
                var containsSplit = line.Split("contain");
                var input = containsSplit[0];
                var output = containsSplit[1];

                if(output.Trim() == "no other bags."){
                    //Do something
                } else {                                    
                    var inputColor = InputRegex.Match(input).Groups[1].Value;
                    var outputs = output
                        .Split(',', StringSplitOptions.RemoveEmptyEntries)
                        .Select(o => {
                            var matchedOutput = OutputRegex.Match(o);
                            return new BagContents{
                                Count = int.Parse(matchedOutput.Groups[1].Value),
                                Color = matchedOutput.Groups[2].Value
                            };
                        });

                    foreach(var o in outputs){
                        //Console.WriteLine($"\tOutput, color: {o.Color}, count: {o.Count}");
                    }
                    

                    foreach(var o in outputs){
                        if(!dict.ContainsKey(inputColor)){
                            dict[inputColor] = new List<BagContents>();
                        }
                        dict[inputColor].Add(o);                        
                    }
                }
            }
            return dict;
        }
    }

    class BagContents {
        public string Color {get;set;}
        public int Count {get;set;}

    }
}
