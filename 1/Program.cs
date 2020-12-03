using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace _1 {
    class Program {
        static void Main(string[] args) {
            var file = "input.txt";

            var lines = File.ReadAllLines(file).Select(l => int.Parse(l)).ToList();
            Part1(lines);
            Part2(lines);
            
        }

        private static void Part1(List<int> input){
            for(var i = 0; i < input.Count(); i++){
                for(var j = 0; j < input.Count(); j++){
                    if(i == j){
                        continue;
                    }

                    if(input[i] + input[j] == 2020){
                        Console.WriteLine($"matched on i:{i}, j:{j}");
                        Console.WriteLine($"{input[i]} * {input[j]} = {input[i] * input[j]}");
                        Console.ReadLine();
                    }

                }
            }
        }

        private static void Part2(List<int> input){
            for(var i = 0; i < input.Count(); i++){
                for(var j = 0; j < input.Count(); j++){
                    if(input[i] + input[j] > 2020){
                        continue;
                    }
                    if(i == j ){
                            continue;
                        }

                    for(var k = 0; k < input.Count(); k++){
                        if(i == k ){
                            continue;
                        }

                        if(input[i] + input[j] + input[k] == 2020){
                            Console.WriteLine($"matched on i:{i}, j:{j}, k:{k}");
                            Console.WriteLine($"{input[i]} * {input[j]} * {input[k]} = {input[i] * input[j] * input[k]}");
                            Console.ReadLine();
                        }
                    }

                }
            }
        }
    }
}
