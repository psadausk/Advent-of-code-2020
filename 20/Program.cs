using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace _20 {
    class Program {
        static void Main(string[] args) {
            Part1();
        }

        static void Part1() {
            var lines = File.ReadAllLines("input.txt");
            var baseTiles = new List<Tile>();
            var sb = new List<string>();
            var tileId = -1;
            foreach(var line in lines){                
                if(line.StartsWith("Tile")){
                    tileId = int.Parse(line.Substring(5, 4));
                    Console.WriteLine("Found id " + tileId);
                } else if (line == string.Empty){
                    Console.WriteLine("Found id " + tileId);
                    var row = sb.Count();
                    baseTiles.Add(new Tile(sb, tileId));
                    sb.Clear();                    
                } else {
                    sb.Add(line);    
                }
            }

            foreach(var tile in baseTiles){
                Console.WriteLine(tile.ToString());
            }
        }
    }

    class Tile {

        public int Id {get;set;}
        public List<string> Map {get;set;}

        public int Rotate  {get;set;}

        public bool FlipX {get;set;}
        public bool FlipY {get;set;}

        public Tile(List<string> map, int id){
            this.Map = map;
            this.Id = id;
        }

        public override string ToString() {
            var sb = new StringBuilder();

            sb.AppendLine("Tile: " + this.Id);
            foreach(var line in this.Map){
                sb.AppendLine(line);
            }

            return sb.ToString();
        }

        string GetSide(Side side){
            switch(side){
                case Side.Left:
                    return new string(this.Map.Select(s => s.First()).ToArray());
                case Side.Top:
                    return this.Map.First();
                case Side.Right:
                    return new string(this.Map.Select(s => s.Last()).ToArray());
                case Side.Bottom:
                    return this.Map.Last();
                default:
                    throw new Exception();
            }
        }
    }

    enum Side {
        Left,
        Top,
        Right,
        Bottom
    }
}
