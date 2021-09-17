using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace _16 {
    class Program {
        static void Main(string[] args) {
            Part2();
        }

        static void Part1(){
            var rules = CreateRules().ToList();
            var inputs = File.ReadAllLines("tickets.txt");
            var sum = 0;
            foreach(var input in inputs){
                var values = input.Split(',').Select(s => int.Parse(s));
                foreach(var value in values){
                    if(!rules.Any(r => r.IsVald(value))) {
                        Console.WriteLine(value + " was not valid");
                        sum += value;
                    }
                }
            }
            Console.WriteLine("sum is " + sum);
        }

        static Dictionary<string, List<bool>> possibleMatches;

        static void Part2(){
            var rules = CreateRules().ToList();
            var validTickets = GetValidTickets(rules).ToList();
            Console.WriteLine(validTickets.Count());

            possibleMatches = new Dictionary<string, List<bool>>();

            foreach(var rule in rules){
                possibleMatches[rule.Name] = Enumerable.Repeat(true, validTickets[0].Count()).ToList();
            }
            var ticketNum = 0;
            //var ticket = validTickets.First();
            foreach(var ticket in validTickets){
                for(var i = 0; i < ticket.Count(); i++){
                    foreach(var rule in rules){
                        //Console.WriteLine($"checking pos {0} ");
                        if(possibleMatches[rule.Name][i] == false){
                            continue;
                        }
                        if(!rule.IsVald(ticket[i])){
                            possibleMatches[rule.Name][i] = false;
                            Console.WriteLine($"pos {i} cannot be {rule.Name} because ticket {ticketNum} has value {ticket[i]}");

                            //If there's only one match now, we know this position must be the rule.
                            if(possibleMatches[rule.Name].Count(r => r == true) == 1){                                
                                SetRuleToFalse(rule, rules);
                            }
                        }
                    }
                }
                ticketNum++;
            }

            var myTicket = "97,61,53,101,131,163,79,103,67,127,71,109,89,107,83,73,113,59,137,139".Split(',').Select(i => int.Parse(i)).ToList();

            var product = 1.0;
            foreach(var kvp in possibleMatches.Where(r => r.Key.StartsWith("departure"))){
                Console.WriteLine($"{kvp.Key} is {kvp.Value.IndexOf(true)} my ticket is {myTicket[kvp.Value.IndexOf(true)]}");
                product = product * myTicket[kvp.Value.IndexOf(true)];
            }

            Console.WriteLine(product);
        }

        static void SetRuleToFalse(Rule rule, IEnumerable<Rule> rules){
            var matchedRule = possibleMatches[rule.Name].IndexOf(true);
            // if(matchedRule < 0){
            //     return;
            // }
            Console.WriteLine($"Setting {rule.Name} {matchedRule} to false" );
            foreach(var kvp in possibleMatches){
                if(kvp.Key != rule.Name && kvp.Value[matchedRule]){
                    kvp.Value[matchedRule] = false;

                    if(possibleMatches[kvp.Key].Count(r => r == true) == 1){
                        SetRuleToFalse(rules.First(r => r.Name == kvp.Key), rules);
                    }                    
                }
            }
        }

        static IEnumerable<List<int>> GetValidTickets(IList<Rule> rules){
            var inputs = File.ReadAllLines("tickets.txt");
            foreach(var input in inputs){
                var isValid = true;
                var values = input.Split(',').Select(s => int.Parse(s)).ToList();
                foreach(var value in values){
                    if(!rules.Any(r => r.IsVald(value))) {
                        isValid = false;
                        break;
                    }
                }
                if(isValid){
                    yield return values;
                }
            }
        }

        static IEnumerable<Rule> CreateRules(){
            var text = File.ReadAllLines("rules.txt");

            foreach(var l in text){
                var phrase = l.Split(':');
                var values = phrase[1].Split(" or ");
                var rule = new Rule();
                rule.Name = phrase[0];
                foreach(var range in values){
                    var r = range.Split('-');
                   rule.Ranges.Add(new Range {
                       Min = int.Parse(r[0]),
                       Max = int.Parse(r[1])
                   });
                }
                yield return rule;
            }

            yield break;
        }
    }

    class Rule {
        public string Name { get; set;}
        public IList<Range> Ranges { get; }

        public Rule(){
            this.Ranges = new List<Range>();
        }

        public bool IsVald(int i){
            return this.Ranges.Any(r => r.IsValid(i));
        }
    }

    class Range {
        public int Min {get; set; }
        public int Max {get; set; }

        public bool IsValid(int i){
            var ret =  i >= this.Min && i <= this.Max;
            if(!ret){
                //Console.WriteLine($"{i} is not between {this.Min} and {this.Max}");
            }
            return ret;
        }
    }
}