using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace _19 {
    class Program {
        static void Main(string[] args) {
            Part1();
        }

        static Dictionary<int, Lazy<string>> knownRules = new Dictionary<int, Lazy<string>>();

        static void Part1(){
            var rules = File.ReadAllLines("rules.txt");
            foreach(var rule in rules){
                var tokens = rule.Split(':');
                knownRules[int.Parse(tokens[0])] = new Lazy<string>(() => SolveExpression(tokens[1].Trim(), 0));
            }

            var inputs = File.ReadAllLines("input.txt");

            var regexes = knownRules.Select(kvp => new Regex($"^{kvp.Value.Value}$"));

            var rule0 = new Regex($"^{knownRules[0].Value}$");
            Console.WriteLine(rule0);

            var sum = 0;
            foreach(var line in inputs){
                //if(regexes.Any(r => r.IsMatch(line))){
                if(rule0.IsMatch(line)) {
                    sum++;
                    Console.WriteLine(line);
                }
            }

            Console.WriteLine(sum);
        }

        static string SolveExpression(string input, int stackSize){


            Console.WriteLine(input);
            if(input == "\"a\""){
                return "a";
            }

            if(input == "\"b\""){
                return "b";
            }
            
            StringBuilder sb = new StringBuilder();

            if(input.Contains("|")){
                var orExpressions = input.Split("|");
                sb.Append("(");
                sb.Append(SolveAndExpression(orExpressions[0], stackSize++));
                sb.Append("|");
                sb.Append(SolveAndExpression(orExpressions[1], stackSize++));
                sb.Append(")");

            }else {
                sb.Append(SolveAndExpression(input,stackSize++));
            }            
            
            return sb.ToString();
        }

        static string SolveAndExpression(string input, int stackSize){
            var sb = new StringBuilder();
            var tokens = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            foreach(var token in tokens){
                sb.Append(knownRules[int.Parse(token)].Value);                
            }
            return sb.ToString();
        }
    }
}
