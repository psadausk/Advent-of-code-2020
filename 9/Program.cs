using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace _9 {
    class Program {
        static void Main(string[] args) {
            var input = File.ReadAllLines("input.txt").Select(i => double.Parse(i)).ToList();
            Part1(input);
            Part2(input);
        }

        static void Part1(List<double> inputs){
            var start = 0;
            var preambleLength = 25;                        
            for(var l = preambleLength; l < inputs.Count(); l++){
                var possibleSums = new List<double>();
                for(var i = l - preambleLength; i < l; i++){
                    //Console.WriteLine($"Preamble is {i ")
                    for(var j = i; j < l ; j++) {
                        possibleSums.Add(inputs[i] + inputs[j]);
                    }
                }
                //Console.WriteLine("L is " + inputs[l]);

                if(!possibleSums.Contains(inputs[l])){
                    Console.WriteLine(inputs[l] + " does not match");
                    break;
                }
            }
            Console.WriteLine("Done");
        }

        static void Part2(List<double> inputs){
            var sum = 69316178;

            for (int startIndex = 0; startIndex < inputs.Count; startIndex++){
                var found = false;
                var over = false;
                var endIndex = startIndex + 1;
                var currentSum = inputs[startIndex];

                if(found){
                    break;
                }
                //Console.WriteLine("checking at " + startIndex);
                while(!found || !over){
                    currentSum += inputs[endIndex];
                    //Console.WriteLine($"At {startIndex} - {endIndex}, current Sum is {currentSum}");
                    if(currentSum == sum){
                        Console.WriteLine($"Found sum from {startIndex} to {endIndex}");
                        var subList = new List<double>();
                        for(var i = startIndex; i < endIndex; i++){
                            subList.Add(inputs[i]);
                        }

                        var min = subList.Min();
                        var max = subList.Max();

                        Console.WriteLine($"{min} + {max} = {min+max}");
                        break;
                    } else if(currentSum > sum) {
                        //Console.WriteLine("Over");
                        break;
                    } else {
                        endIndex++;
                    }
                }
            }
        }
    }
}
