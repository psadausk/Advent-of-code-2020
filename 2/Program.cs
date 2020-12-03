using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace _2 {
    class Program {

        private static Regex _regex = new Regex("([0-9]+)-([0-9]+) ([a-z]): ([a-z]+)");
        static void Main(string[] args) {
            var file = "input.txt";

            var lines = File.ReadAllLines(file);
            Part1(lines);
            Part2(lines);
            
        }

        private static void Part1(IEnumerable<string> inputs){
            var valid = 0;
            foreach(var input in inputs){

                var match = _regex.Match(input);


                var minChar = int.Parse(match.Groups[1].Value);
                var maxChar = int.Parse(match.Groups[2].Value);
                var c = match.Groups[3].Value[0];
                var pw = match.Groups[4].Value;

                //Console.WriteLine($"min: {minChar}, max: {maxChar}, c:{c}, pw:{pw}");

                var numCharsInPw = pw.Count(pwc => pwc == c);

                if(numCharsInPw >= minChar && numCharsInPw <= maxChar){
                    valid++;
                }

            }

            Console.WriteLine($"{valid} valid passwords");
        }

        private static void Part2(IEnumerable<string> inputs){
            var valid = 0;
            foreach(var input in inputs){

                var match = _regex.Match(input);


                var firstPos = int.Parse(match.Groups[1].Value);
                var secondPos = int.Parse(match.Groups[2].Value);
                var c = match.Groups[3].Value[0];
                var pw = match.Groups[4].Value;

                if(pw[firstPos - 1] == c ^ pw[secondPos - 1] == c){
                    valid++;
                }

            }

            Console.WriteLine($"{valid} valid passwords");
        }
    }
}
