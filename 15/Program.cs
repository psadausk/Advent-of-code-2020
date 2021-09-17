using System;
using System.Collections.Generic;
using System.Linq;

namespace _15 {
    class Program {
        static void Main(string[] args) {

            var input = "0,13,1,16,6,17";
            var numbers = new Dictionary<int, int>();

            var inputs = input.Split(',').Select(i => int.Parse(i)).ToList();


            for(var i = 0; i < inputs.Count(); i ++){
                numbers[inputs[i]] = i + 1;
            }

            var lastNumber = inputs.Last();
            var spoken = inputs.Last();

            for(var i = inputs.Count() + 1; i < 30000001; i++){
                //Console.WriteLine("Last Number was  " + lastNumber);
                if(!numbers.ContainsKey(lastNumber) || numbers[lastNumber] == i - 1){
                    spoken = 0;
                    //Console.WriteLine(lastNumber + " was never said");
                    numbers[lastNumber] = i;
                } else {
                    //Console.WriteLine($"{lastNumber}  was last spoken on turn {numbers[lastNumber]}");
                    spoken = (i - 1) - numbers[lastNumber];

                    //Console.WriteLine($"{i} - 1 - {numbers[lastNumber]}");
                    
                }
                numbers[lastNumber] = i - 1;
                lastNumber = spoken;
                

                //Console.WriteLine($"Turn: {i}: spoke " + spoken);
                //Console.WriteLine();
            }
            Console.WriteLine($"Turn spoke " + spoken);
            Console.WriteLine(numbers.Count());
            
        }
    }
}
