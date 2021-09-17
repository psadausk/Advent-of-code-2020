    using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace _21 {
    class Program {
        static void Main(string[] args) {
            Part1();
        }

        static void Part1(){
            var lines= File.ReadAllLines("input.txt");
            var knownAllegens = new List<Tuple<string,string>>();
            var possibleAllegens = new Dictionary<string, HashSet<string>>();
            var ingredientLines = new List<Line>();
            foreach(var l in lines){
                var segements = l.Split(" (contains ");
                //Console.WriteLine("reading " + l);
                var ingredients = segements[0].Split(" ");

                //Console.WriteLine("Segement[0] is " + segements[0]);

                var allergens = segements[1].Contains(", ")
                    ? segements[1].Split(", ").Select(s => s.Trim(')'))
                    : new [] { segements[1].Trim(')') };

                ingredientLines.Add(new Line(ingredients.ToList(), allergens.ToList()));
            }

            var shouldContinue = true;
            var exclusions = new HashSet<string>();
            while(shouldContinue){
                
                //Find the first line with a single allergen
                var currentLine = ingredientLines.FirstOrDefault(il => il.Allergens.Count == 1 && !exclusions.Contains(il.Allergens.First()));
                
                if(currentLine == null){
                    shouldContinue = false;
                    continue;
                }
                var allergen = currentLine.Allergens.First();

                //Find all other lines containing that allergen
                var matchingLines = ingredientLines.Where(il => il.Allergens.Contains(allergen));

                var ingredient = "";
                //Now find what ingredient is common in all of these linesz
                var matches = 0;
                foreach(var i in currentLine.Ingredients){
                    if(matchingLines.All(ml => ml.Ingredients.Contains(i))){
                        matches++;
                         if(matches > 1){
                             break;
                         }
                        ingredient = i;
                    }
                }

                if(matches == 1){
                    Console.WriteLine($"{ingredient} is {allergen}");
                    knownAllegens.Add(new Tuple<string, string>(ingredient, allergen));
                } else {
                    exclusions.Add(allergen);
                    continue;
                }

                exclusions.Clear();

                //Remove that ingredient and allergens from all remaining lines
                //Console.WriteLine("Removing " + allergen);
                //Console.WriteLine("Removing " + ingredient);
                foreach(var l in ingredientLines){
                    l.Ingredients.Remove(ingredient);
                    l.Allergens.Remove(allergen);
                    
                }                
            }

            // foreach(var il in ingredientLines){
            //     Console.WriteLine($"Ingredients: ");
            //     Console.WriteLine($"\t{string.Join(',', il.Ingredients)}");
            //     Console.WriteLine($"Allergens: ");
            //     Console.WriteLine($"\t{string.Join(',', il.Allergens)}");
            //     Console.WriteLine();

            // }

            Console.WriteLine($"There are {ingredientLines.Count} lines left");
            Console.WriteLine($"There are {ingredientLines.Sum(il => il.Ingredients.Count())} ingredients left");

            knownAllegens.Sort((a,b) => a.Item2.CompareTo(b.Item2));

            foreach(var ka in knownAllegens){
                Console.WriteLine($"{ka.Item1} is {ka.Item2}");
            }

            Console.WriteLine(string.Join(',', knownAllegens.Select(ka => ka.Item1)));
        }
    }

    class Line {
        public List<string> Ingredients {get;set;}
        public List <string> Allergens {get;set;}

        public Line() {
            this.Ingredients = new List<string>();
            this.Allergens = new List<string>();
        }

        public Line(List<string> ingredients, List<string> allergens) {
            this.Ingredients = ingredients;
            this.Allergens = allergens;
        }
    }
}
