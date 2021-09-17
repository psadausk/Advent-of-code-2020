using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace _8 {
    class Program {
        static void Main(string[] args) {
            var file = "input.txt";
            var lines = File.ReadAllLines(file);
            Part1(lines.ToList());
            Part2(lines.ToList());

        }

        static bool Part1(List<string> input){
            var executedLines = new HashSet<int>();
            var currentLine = 0;
            var acc = 0;
            var repeated = false;
            var finished = false;
            while(!finished && !repeated){
                //Console.WriteLine("Checking " + currentLine + ": " + input[currentLine]);
                executedLines.Add(currentLine);
                var splitStr = input[currentLine].Split(' ');
                var instruction = splitStr[0];
                var val = int.Parse(splitStr[1]);
                
                switch(instruction) {
                    case "nop":
                        currentLine++;
                        break;
                    case "acc":
                        acc += val;
                        currentLine++;
                        break;
                    case "jmp":
                        currentLine += val;
                        break;
                }
                if(executedLines.Contains(currentLine)){
                    repeated = true;
                } else if(currentLine >= input.Count){
                    finished = true;
                }
            }
            Console.WriteLine("Acc: " + acc + " repeated " + repeated + " finished " + finished);
            return finished;
        }

        static void Part2(List<string> input){
            //brute force this bitch
            
            
            for (int i = 0; i < input.Count; i++) {
                var modifiedInput =  new List<string>(input);
                Console.WriteLine("i " + i);
                string line = input[i];
                var instruction = line.Split(' ')[0];
                if(instruction == "jmp"){
                    modifiedInput[i] = modifiedInput[i].Replace("jmp", "nop");
                    if(Part1(modifiedInput)){
                        Console.WriteLine("Matched");
                        break;
                    }
                } else if(instruction == "nop"){
                    modifiedInput[i] = modifiedInput[i].Replace("nop", "jmp");
                    if(Part1(modifiedInput)){
                        Console.WriteLine("Matched");
                        break;
                    }
                }
            }
            
        }
    }
}
