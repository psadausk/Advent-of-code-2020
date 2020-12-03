using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace _3 {
    class Program {
                static void Main(string[] args) {
            var file = "input.txt";

            var lines = File.ReadAllLines(file).ToList();
            var length = lines[0].Length;

            var sum = CheckSlope(1, 1, lines);
            sum *= CheckSlope(1, 3, lines);
            sum *= CheckSlope(1, 5, lines);
            sum *= CheckSlope(1, 7, lines);
            sum *= CheckSlope(2, 1, lines);

            Console.WriteLine("sum: " + sum);


            
        }

        private static void Part1(List<string> inputs){
            var length = inputs[0].Length;
            //Right is increasing
            //Down is increasing;
            var x = 0;
            //var startY = 0;
            var trees = 0;

            for(var y = 0; y < inputs.Count(); y++, x+=3){
                if(inputs[y][x%length] == '#'){
                    trees++;
                }

            }
            Console.Write($"{trees} trees");
        }

        private static void Part2(IEnumerable<string> inputs){

        }

        private static int CheckSlope(int ySlope, int xSlope, List<string> inputs){
            var length = inputs[0].Length;
            //Right is increasing
            //Down is increasing;
            var x = 0;
            //var startY = 0;
            var trees = 0;

            for(var y = 0; y < inputs.Count(); y+=ySlope, x+=xSlope){
                if(inputs[y][x%length] == '#'){
                    trees++;
                }

            }
            Console.WriteLine($"({xSlope},{ySlope}) has {trees} trees");
            return trees;
        }
    }
}
