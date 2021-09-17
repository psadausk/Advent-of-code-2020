using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace _10 {
    class Program {

        static Dictionary<int,double> Cache = new Dictionary<int, double>();
        static void Main(string[] args) {
            var inputs = File.ReadAllLines("input.txt").Select(l => int.Parse(l)).ToList();
            Part2(inputs);
        }

        static void Part1(List<int> input){
            var currentJolt = 0;
            var num1Diff = 0;
            var num3Diff = 0;
            input.Sort();

            foreach(var i in input){
                var diff = i - currentJolt;
                if(diff == 1){
                    num1Diff++;
                } else if (diff == 3) {
                    num3Diff++;
                } else {
                    Console.WriteLine("Unexpected number of diff " + diff );
                }

                currentJolt = i;
            }

            num3Diff++;
            Console.WriteLine($"num1diff {num1Diff}");
            Console.WriteLine($"num3diff {num3Diff}");

        }

        static void Part2(List<int> input){       
            var sortedSet = new SortedSet<int>(input);     
            var arrangemments = CalcDiff(0, sortedSet);
            Console.WriteLine(arrangemments);
        }

         static double CalcDiff(int i, SortedSet<int> inputs){
             //Console.WriteLine($"checking " + i);
             if(i == inputs.Max()){                 
                 return 1;
             }
            var ret = 0.0;
            if(inputs.Contains(i + 1)){
                if(!Cache.ContainsKey(i + 1)){
                    Cache[i + 1] = CalcDiff(i + 1, inputs);
                }
                ret += Cache[i+1];
            }

            if(inputs.Contains(i + 2)){
                if(!Cache.ContainsKey(i + 2)){
                    Cache[i + 2] = CalcDiff(i + 2, inputs);
                }
                ret += Cache[i + 2];
            }
            
            if(inputs.Contains(i + 3)){
                if(!Cache.ContainsKey(i + 3)){
                    Cache[i + 3] = CalcDiff(i + 3, inputs);
                }
                ret += Cache[i + 3];
            }

            return ret;
         }
    }
}
