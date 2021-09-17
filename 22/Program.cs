using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace _22
{
    class Program
    {
        static void Main(string[] args)
        {
            Part1();
        }

        static void Part1(){

            var player1 = new Queue<int>();
            var player2 = new Queue<int>();

            var player1Input = File.ReadAllLines("player1.txt").Select(i => int.Parse(i));
            foreach(var i in player1Input){
                player1.Enqueue(i);
            }

            var player2Input = File.ReadAllLines("player2.txt").Select(i => int.Parse(i));
            foreach(var i in player2Input){
                player2.Enqueue(i);
            }


            while(player1.Any() && player2.Any()){
                var player1Card = player1.Dequeue();
                var player2Card = player2.Dequeue();

                Console.WriteLine($"{player1Card} vs {player2Card}");
                if(player1Card > player2Card){
                    player1.Enqueue(player1Card);
                    player1.Enqueue(player2Card);
                    Console.WriteLine("Player 1 won");
                } else {
                    player2.Enqueue(player2Card);
                    player2.Enqueue(player1Card);
                    Console.WriteLine("Player 2 won");               
                }
            }
            Console.WriteLine("Done");

            var winningDeck = player1.Any() 
                ? player1 
                : player2;

            if(player1.Any()){
                Console.WriteLine("Player 1 won the game");
            } else {
                Console.WriteLine("Player 2 won the game");
            }

            var index = 0;
            var sum = winningDeck.Reverse().Sum((value) => {
                index++;
                return value * index;
            });

            Console.WriteLine("Sum is " + sum);

            
        }
    }
}
