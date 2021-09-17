    using System;
using System.Collections.Generic;
using System.IO;

namespace _12 {
    class Program {
        static void Main(string[] args) {
            Part2(File.ReadAllLines("input.txt"));
        }

        static void Part1(IEnumerable<string> input){
            var vPos = 0.0;
            var hPos = 0.0;
            var curDir = Direction.East;

            foreach(var l in input){
                var command = l[0];
                var mag = int.Parse(l.Substring(1));

                Console.WriteLine($"Current pos {vPos}, {hPos}, {curDir}. Command: {l}");

                switch(command){
                    case 'N':
                        vPos += mag;
                        break;
                    case 'S':
                        vPos -= mag;
                        break;
                    case 'W':
                        hPos -= mag;
                        break;
                    case 'E':
                        hPos += mag;
                        break;
                    case 'F':
                        switch(curDir){
                            case Direction.North:
                                vPos += mag;
                                break;
                            case Direction.South:
                                vPos -= mag;
                                break;
                            case Direction.West:
                                hPos -= mag;
                                break;
                            case Direction.East:
                                hPos += mag;
                                break;
                        }
                        break;
                    case 'L':
                        var numTurnsL = mag / 90;
                        var newDir = ((int)curDir - numTurnsL);
                        if(newDir < 0){
                            newDir = 4 + newDir;
                        }
                        //Console.WriteLine($"Old direction {curDir}: turned left {mag} degrees. Number of left turns is {numTurnsL}. New dir int is {newDir}, which is {(Direction)newDir}");
                        curDir = (Direction)newDir;
                        break;
                    case 'R':                        
                        var numTurnsR = mag / 90;
                        var newDirR = ((int)curDir + numTurnsR) % 4;
                        //Console.WriteLine($"Old direction {curDir}: turned right {mag} degrees. Number of right turns is {numTurnsR}. New dir int is {newDirR}, which is {(Direction)newDirR}");
                        curDir = (Direction)newDirR;
                        break;
                    default:
                        Console.WriteLine("Unable to read command " + l);
                        break;
                }
            }
            Console.WriteLine($"Current pos {vPos}, {hPos}, {curDir}.");
            //Console.WriteLine($"vPos: {Math.Abs(vPos)}, hPos: {Math.Abs(hPos)}");
            
        }    

        static void Part2(IEnumerable<string> input){
            
            var vPos = 0;
            var hPos = 0;
            var curDir = Direction.East;
            var wpHOffset = 10;
            var wpVOffset = 1;

            foreach(var l in input){
                var command = l[0];
                var mag = int.Parse(l.Substring(1));
                Console.WriteLine($"Current pos {vPos}, {hPos}, wp {wpVOffset}, {wpHOffset}. Command: {l}");
                switch(command){
                    case 'N':
                        wpVOffset += mag;
                        break;
                    case 'S':
                        wpVOffset -= mag;
                        break;
                    case 'W':
                        wpHOffset -= mag;
                        break;
                    case 'E':
                        wpHOffset += mag;
                        break;
                    case 'F':
                        vPos += mag * wpVOffset;
                        hPos += mag * wpHOffset;
                        break;
                    case 'L':
                    case 'R':       
                        var degrees = command == 'L' 
                            ? mag
                            : mag * -1;

                        var radians = (degrees * Math.PI)/180;

                        var newHOffset = ((wpHOffset * (int)Math.Cos(radians)) - (wpVOffset * (int)Math.Sin(radians)));
                        //Console.WriteLine($"{wpHOffset} * {(int)Math.Cos(radians)} - {wpVOffset} * {(int)Math.Sin(radians)}");

                        var newVOffset = ((wpVOffset * (int)Math.Cos(radians)) + (wpHOffset *(int)Math.Sin(radians)));
                        //Console.WriteLine($"{wpVOffset} * {(int)Math.Cos(radians)} + {wpHOffset} * {(int)Math.Sin(radians)}");

                        wpHOffset = newHOffset;
                        wpVOffset = newVOffset;
                        break;
                    default:
                        Console.WriteLine("Unable to read command " + l);
                        break;
                }
            }
            Console.WriteLine($"Current pos {vPos}, {hPos}, wp {wpVOffset}, {wpHOffset}.");

            Console.WriteLine(Math.Abs(vPos) + Math.Abs(hPos));
            //Console.WriteLine($"vPos: {Math.Abs(vPos)}, hPos: {Math.Abs(hPos)}");
            
        }
    }

    enum Direction{
        North = 0,
        East = 1,
        South = 2,
        West = 3

    }
}
