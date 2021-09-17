using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace _13 {
    class Program {
        static void Main(string[] args) {
            Part2();

            // var inputs = new List<Input>{
            //     new Input {Val = 17},
            //     new Input {Val = 13, Offset = 2},
            //     new Input {Val = 19, Offset = 3},
            //     //new Input {Val = 31, Offset = 6},
            //     //new Input {Val = 19, Offset = 7}
            // };




            // var inputs = new List<Input>{
            //     new Input {Val = 67},
            //     new Input {Val = 7, Offset = 1},
            //     new Input {Val = 59, Offset = 2},
            //     new Input {Val = 61, Offset = 3},
            //     //new Input {Val = 19, Offset = 7}
            // };

            var inputs = ParseInputs();
            foreach(var i in inputs){
                Console.WriteLine($"val: {i.Val}, offset: {i.Offset}");
            }

            FindCommonFactors(inputs);
            //FindCommonFactor(7, 13, 1);
            //FindCommonFactor(17, 13, 2);
            //FindCommonFactors(inputs);


        }


        static List<Input> ParseInputs(){
            var ret = new List<Input>();
            var input = File.ReadAllLines("input.txt")[0].Split(',');

            for(var i = 0; i < input.Length; i++){
                if(input[i] != "x"){
                    ret.Add(new Input{
                        Val = int.Parse(input[i]),
                        Offset = i
                    });
                }
            }
            return ret;


        }

        static void Part1(){
            var lines = File.ReadAllLines("Input.txt");
            var time = int.Parse(lines[0]);
            var buses = lines[1].Split(',').Where(l => l != "x").Select(l => int.Parse(l));

            var soonestTime = 0;
            var soonnestBus = 0;
            foreach(var bus in buses){
                var x = (int)Math.Ceiling((double)time/bus);
                var nextBusTime = x * bus;

                if(soonestTime == 0 || nextBusTime < soonestTime){
                    soonestTime = nextBusTime;
                    soonnestBus = bus;
                }
            }

            Console.WriteLine("soonest time " + soonestTime + " with bus " + soonnestBus + "gives answer " + (soonestTime - time ) * soonnestBus) ;
        }

        static void Part2(){
            for(var i = 0; i < 300; i++){
                var x = 17 * i;
                
                for(var j = 0; j < 300; j++){
                    var y = (13 * j) - 2;
                    
                    for(var k = 0; k < 300; k++){
                        var z = (19 * k) - 3;
                        if(x == y && y == z ){
                            Console.WriteLine($"i:{i}: j:{j}, k:{k}");
                        }
                    }
                }
            }


             var baseX = 0.0;
             var baseY = 0.0;

            for(var i = 0; i < 13; i++){
                var yIndex = baseY + (1 * i);
                var y = (i * 17);

                if(y % 13 == (13 - 2)){
                    Console.WriteLine($"1 Matched on {y}, {Math.Ceiling((double)y/13)}");
                    baseY = Math.Ceiling((double)y/13);
                    break;
                }
            }

            for(var i = 0; i < 19; i++){
                var yIndex = baseY + (17 * i);
                var y = (yIndex * 13) - 2;

                if(y % 19 == (19 - 3)){
                    Console.WriteLine($"2 Matched on {y}, {yIndex}");
                    break;
                }
            }
        }
        private static void FindCommonFactors(List<Input> inputs){
            var xInput = inputs[0];
            var yBase = 0L;

            var xIncrement = xInput.Val;
            var match = false;
            var y = 0.0;

            foreach(var yInput in inputs.Skip(1)){
                match = false;
                for(var i = 0.0; i < yInput.Val; i++){                    
                    y += xIncrement;
                    if((y + yInput.Offset) % yInput.Val == 0){
                        xIncrement *= yInput.Val;
                        xInput = yInput;
                        Console.WriteLine($"Matched on {y}, {yInput.Offset} {yInput.Val} ");
                        match = true;
                        break;
                    }
                }
                if(!match){
                    Console.WriteLine("did not find a match for " + yInput.Val);
                    break;
                }
            }
        }    
    }

    class Input {
        public long Val {get;set;}
        public long Offset {get;set;}
    }
}
