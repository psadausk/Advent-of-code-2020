using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _23 {
    class Program {
        static void Main(string[] args) {
            part2();
        }

        static void Part1(){
            var cups = new List<int> {7,9,2,8,4,5,1,3,6};

            var currentIndex = 0;
            

            for(var i = 1; i <= 100; i++){
                var currentLabel = cups[currentIndex];
                //Console.WriteLine($"-- move {i} --");

                //Console.WriteLine("cups: " + string.Join(' ', cups.Select((label, idx) => idx == currentIndex ? $"({label})" : label + "")));

                var pickedUpCups = cups.RemoveAndReturn(currentIndex + 1, 3);

                //Console.WriteLine("pick ups: " + string.Join(',', pickedUpCups));

                var destination = currentLabel - 1;            

                while(true){
                    //Console.WriteLine("checking " + destination);
                    if(destination < 1){
                        destination = 9;
                        //Console.WriteLine("destiantion too high, setting to " + destination);
                    }
                    //Console.WriteLine("checking " + destination);
                    if(!pickedUpCups.Contains(destination)){
                        break;
                    }
                    destination = destination - 1;
                }

                //Console.WriteLine("destination: " + destination);                

                var destinationIndex = cups.IndexOf(destination);
                //Console.WriteLine("destinationIndex: " + destinationIndex);

                cups.InsertRange(destinationIndex + 1, pickedUpCups);
                //currentIndex = (currentIndex + 1) % cups.Count;
                currentIndex = (cups.IndexOf(currentLabel) + 1) % cups.Count();


                //Console.WriteLine();
            }
            //Console.WriteLine();
            //Console.WriteLine("-- final -- ");
            //Console.WriteLine("cups: " + string.Join(' ', cups.Select((label, idx) => idx == currentIndex ? $"({label})" : label + "")));

            var sb = new StringBuilder();
            var start = cups.IndexOf(1);
            for(var i = 0; i < cups.Count -1; i++){
                sb.Append(cups[(i + start + 1) % cups.Count()]);
            }
            Console.WriteLine(sb.ToString());


        }

                public static void part2()
        {
            int[] numbers = new int[1000001];
            numbers[0] = 0; // not used
            numbers[1] = 9;
            numbers[9] = 8;
            numbers[8] = 7;
            numbers[7] = 5;
            numbers[5] = 3;
            numbers[3] = 4;
            numbers[4] = 6;
            numbers[6] = 2;
            numbers[2] = 10;
            numbers[1000000] = 1;
            for(int i = 10; i < 1000000; ++i)
            {
                numbers[i] = i + 1;
            }

            int current = 1;

            for (int i = 0; i < 10000000; ++i)
            {
                int value = current;
                int next1 = numbers[current];
                int next2 = numbers[next1];
                int next3 = numbers[next2];

                do
                {
                    value--;
                    if (value == 0)
                        value = 1000000;
                }
                while (next1 == value || next2 == value || next3 == value);

                numbers[current] = numbers[next3];
                numbers[next3] = numbers[value];
                numbers[value] = next1;

                current = numbers[current];
            }

            long prod = numbers[1];
            prod *= numbers[numbers[1]];
            Console.Write(prod);
            Console.Read();
        }

        static void Part2(){
            var l = new List<int> {3,8,9,1,2,5,4,6,7};
            var cups = new LinkedList<int>();
            var node = new LinkedListNode<int>(3);
            //var nextNode = new LinkedListNode<int>(8);
            cups.AddFirst(node);
            //cups.AddAfter(node,nextNode);

            for(var i = 1; i < l.Count(); i++){
                var nextNode = new LinkedListNode<int>(l[i]);
                cups.AddAfter(node, nextNode);
                node = nextNode;
            }
            
            //cups.AddAfter()

            Console.WriteLine(cups.Count());

            var currentIndex = 0;
            var currentLabel = cups.First();

            for(var i = 1; i <= 100; i++){
                //var currentLabel = cups[currentIndex];
                Console.WriteLine($"-- move {i} --");

                //Console.WriteLine("cups: " + string.Join(' ', cups.Select((label, idx) => idx == currentIndex ? $"({label})" : label + "")));

                var pickedUpCups = cups.RemoveAndReturn(currentIndex + 1, 3);
                cups.)

                Console.WriteLine("pick ups: " + string.Join(',', pickedUpCups));

                var destination = currentLabel - 1;            

                while(true){
                    //Console.WriteLine("checking " + destination);
                    if(destination < 1){
                        destination = 1000001;
                        //Console.WriteLine("destiantion too high, setting to " + destination);
                    }
                    //Console.WriteLine("checking " + destination);
                    if(!pickedUpCups.Contains(destination)){
                        break;
                    }
                    destination = destination - 1;
                }

                //Console.WriteLine("destination: " + destination);                

                var destinationIndex = cups.IndexOf(destination);
                //Console.WriteLine("destinationIndex: " + destinationIndex);

                cups.InsertRange(destinationIndex + 1, pickedUpCups);
                //currentIndex = (currentIndex + 1) % cups.Count;
                currentIndex = (cups.IndexOf(currentLabel) + 1) % cups.Count();


                //Console.WriteLine();
            }
            //Console.WriteLine();
            //Console.WriteLine("-- final -- ");
            //Console.WriteLine("cups: " + string.Join(' ', cups.Select((label, idx) => idx == currentIndex ? $"({label})" : label + "")));

            // var sb = new StringBuilder();
            // var start = cups.IndexOf(1);
            // for(var i = 0; i < cups.Count -1; i++){
            //     sb.Append(cups[(i + start + 1) % cups.Count()]);
            // }
            // Console.WriteLine(sb.ToString());
            Console.WriteLine(cups[1]);
            Console.WriteLine(cups[2]);

        }
    }

    public static class ListExtensions {
        public static List<T> RemoveAndReturn<T>(this List<T> list, int index, int count){
             var ret = new List<T>();
            var c = list.Count;
             for(var i = 0; i < count; i++ ){

                 if(index  > list.Count){
                     index --;
                     //Console.WriteLine("subtracting");
                 }

                 var indexToRemove = (index ) % (list.Count);
                 //Console.WriteLine($"Index to remove is {indexToRemove } Math is {index} % {list.Count}");
                 ret.Add(list[indexToRemove]);
                 list.RemoveAt(indexToRemove);

                 //Console.WriteLine("Removed " + indexToRemove);

             }

             return ret;
        }
    }
}
