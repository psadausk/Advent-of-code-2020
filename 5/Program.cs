using System;
using System.IO;
using System.Linq;

namespace _5 {
    class Program {
        private static int TotalRows = 127;
        private static int TotalCols = 7;
        static void Main(string[] args) {
            Part2();
        }

        private static void Part1(){
            var file = "input.txt";
            Console.WriteLine(File.ReadAllLines(file).Select(GetData).Max());
        }

        private static void Part2(){
            var file = "input.txt";
            var lines = File.ReadAllLines(file);

            var rows = lines
            .Select(l => new Tuple<int, int>(GetRow(l), GetCol(l)))
            .GroupBy(k => k.Item1);

            foreach(var row in rows.OrderBy(g => g.Key).Where(g => g.Count() != 8)){
                foreach(var col in row.OrderBy(r => r.Item2)){
                    Console.WriteLine(col);
                }
            }
        }

        private static int GetData(string input){
            var r = GetRow(input);
            var c = GetCol(input);
            var d = GetId(r,c);
            //Console.WriteLine($"r: {r}, c: {c}, d: {d}");
            return d;
        }

        private static int GetRow(string input){
            var min = 0;
            var max = 127;
            for(var i = 0; i < 7; i++){                                
                var mid = (int)((max - min + 1) / 2);                                
                if(input[i] == 'F'){
                    max -= mid;
                } else if(input[i] == 'B'){
                    min += mid;                    
                }
                
            }           
            return min;
        }

        private static int GetCol(string input){
            var min = 0;
            var max = 7;
            for(var i = 7; i < input.Length; i++){                                
                var mid = (int)((max - min + 1) / 2);                                
                if(input[i] == 'L'){
                    max -= mid;
                } else if(input[i] == 'R'){
                    min += mid;                    
                }
                
            }   
            return min;
        }

        private static int GetId(int row, int col){
            return 8 * row + col;
        }
    }
}
