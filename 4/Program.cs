using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace _4 {
    class Program {

        private static string byrCheck = "(?<byr>19[2-9][0-9]|200[0-2](?:\\s|$))";
        private static string iyrCheck = "(?<iyr>201[0-9]|2020(?:\\s|$))";
        private static string eyrCheck = "(?<eyr>202[0-9]|2030(?:\\s|$))";
        private static string hgtCheck = "(?<hgt>(?:(?:59)|(?:6[0-9])|(?:7[0-6]))(?:in)|(?:(?:(?:1[5-8][0-9])|(?:19[0-3]))(?:cm))(\\s|$))";
         private static string hclCheck = "(?<hcl>#[0-9a-f]{6}(?:\\s|$))";
         private static string eclCheck = "(?<ecl>(?:amb)|(?:blu)|(?:brn)|(?:gry)|(?:grn)|(?:hzl)|(?:oth)(?:\\s|$))";
        private static string pidCheck = "(?<pid>[0-9]{9}(\\s|$))";
        private static Regex _part1 = new Regex("^(?=.*byr:)(?=.*iyr:)(?=.*eyr:)(?=.*hgt:)(?=.*hcl:)(?=.*ecl:)(?=.*pid:).*$");
        private static Regex _part2 = new Regex($"^(?=.*byr:{byrCheck})(?=.*iyr:{iyrCheck})(?=.*eyr:{eyrCheck})(?=.*hgt:{hgtCheck})(?=.*hcl:{hclCheck})(?=.*ecl:{eclCheck})(?=.*pid:{pidCheck}).*$");

        static void Main(string[] args) {
            var file = "input.txt";

            var lines = File.ReadAllLines(file);
            Validate(lines, _part1);
            Validate(lines, _part2);
            
        }

        private static void Validate(IEnumerable<string> inputs, Regex regex){
            var sb = new StringBuilder();
            var validPassports = 0;
            foreach(var line in inputs){
                if(string.IsNullOrWhiteSpace(line)){                    
                    var passport = sb.ToString();
                    if(regex.IsMatch(passport)){
                        validPassports++;
                    }
                    sb.Clear();
                } else {
                    sb.Append(" " + line);
                } 
            }

            if(sb.Length > 0){
                var passport = sb.ToString();
                if(regex.IsMatch(passport)){
                    validPassports++;                    
                }
            }
            Console.WriteLine("Valid Passports: " + validPassports);
        }

        private static void Part2(IEnumerable<string> inputs){

        }
    }    
}
