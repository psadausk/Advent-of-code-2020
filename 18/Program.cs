using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace _18 {
    class Program {

        public static Regex regex = new Regex("\\((\\d|\\+|\\*|\\s)*\\)");

        public static Regex addRegex = new Regex("(\\d+ \\+ \\d+)");
        public static Regex multRegex = new Regex("(\\d+ \\* \\d+)");
        static void Main(string[] args) {
            Part1();
            //Console.WriteLine(Calc("6 + 6 + 9 + 7 * (8 * 2 * (7 * 7 + 9) + 2 * 4) + 6"));
            
        }

        static void Part1(){
            var sum = 0.0;
            var lines = File.ReadAllLines("input.txt");

            foreach(var line in lines){
                sum += Calc(line);
            }

            Console.WriteLine("sum is " + sum);
        }

        static double Calc(string input){
            //Console.WriteLine($"before expansion {input} = ");
            while(regex.IsMatch(input)){
                
                foreach(Match expression in regex.Match(input).Captures){
                    //Console.WriteLine("Calcing " + expression);
                    var val = CalcBlock(expression.Value.Substring(1, expression.Value.Length - 2));
                    //input = regex.Replace(input, sum.ToString());
                    input = input.Replace(expression.Value, val.ToString());
                }
                //Console.WriteLine("input is now " + input);
            }
            //Console.WriteLine("expanded expression is " + input);
            var sum =  CalcBlock(input);
            //Console.WriteLine(sum);
            return sum;
        }

        static double CalcBlock(string input){
            // do adds
            while(addRegex.IsMatch(input)){
                foreach(Match expression in addRegex.Match(input).Captures){
                    var tokens = expression.Value.Split(' ');
                    var val = double.Parse(tokens[0]) + double.Parse(tokens[2]);
                    var startIndex = input.IndexOf(expression.Value);
                    input = input.Substring(0, startIndex) + val + input.Substring(startIndex + expression.Value.Length);
                    //Console.WriteLine("after addition " + input);
                }
            }        

            while(multRegex.IsMatch(input)){
                foreach(Match expression in multRegex.Match(input).Captures){
                    var tokens = expression.Value.Split(' ');
                    var val = double.Parse(tokens[0]) * double.Parse(tokens[2]);
                    var startIndex = input.IndexOf(expression.Value);
                    input = input.Substring(0, startIndex) + val + input.Substring(startIndex + expression.Value.Length);
                    //Console.WriteLine("After mult " + input);
                }
            }
            
            return double.Parse(input);
        }
    }
    enum Operator {
        Add,
        Mult
    }
}
