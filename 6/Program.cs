using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace _6 {
    class Program {
        static void Main(string[] args) {
            var file = "input.txt";

            var lines = File.ReadAllLines(file);
            Part2(lines);
        }

        private static void Part1(IEnumerable<string> inputs ){
            var sb = new StringBuilder();
            var answers = 0;
            foreach(var line in inputs){
                if(string.IsNullOrWhiteSpace(line)){                    
                    var hashSet = new HashSet<char>(sb.ToString());
                    Console.WriteLine("Line was " + sb.ToString());
                    Console.WriteLine($"Found {hashSet.Count()} characters");
                    answers += hashSet.Count();

                    sb.Clear();
                } else {
                    sb.Append(line);
                } 
            }

            if(sb.Length > 0){
                var line = sb.ToString();
                var hashSet = new HashSet<char>(line);
                answers += hashSet.Count();            
            }
            Console.WriteLine("total answers: " + answers);
        }

        private static void Part2(IEnumerable<string> inputs ){
            var sb = new StringBuilder();
            var count = 0;
            foreach(var line in inputs){
                if(string.IsNullOrWhiteSpace(line)){
                    var answers = sb.ToString().Split(' ', StringSplitOptions.RemoveEmptyEntries);

                    IEnumerable<char> intersect = new List<char>(answers[0]);

                    for(var i = 1; i < answers.Count(); i++ ){
                        intersect = intersect.Intersect(answers[i]);
                    }
                    
                    count += intersect.Count();
                    sb.Clear();
                    
                } else {
                    sb.Append(" " + line);
                } 
            }

            if(sb.Length > 0){
                var answers = sb.ToString().Split(' ', StringSplitOptions.RemoveEmptyEntries);        
                IEnumerable<char> intersect = new List<char>(answers[0]);

                for(var i = 1; i < answers.Count(); i++ ){
                    intersect = intersect.Intersect(answers[i]);
                }
                count += intersect.Count();
                    
            }
            Console.WriteLine("total answers: " + count);
        }
    }
}
