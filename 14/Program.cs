using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace _14 {
    class Program {
        static void Main(string[] args) {
            Part1();
            Part2();
        }

        static void Part1(){
            var mem = new Dictionary<string, long>();
            var mask = new BitMask("0101XX01X00X1X1011X1X000000101X10001");
            var inputs = File.ReadAllLines("input.txt");
            var maskRegex = new Regex("mask = (\\w+)");
            var memRegex = new Regex("mem\\[(\\d+)\\] = (\\d+)");
            foreach(var l in inputs){
                if(maskRegex.IsMatch(l)){
                    mask.UpdateMask(maskRegex.Match(l).Groups[1].Value);
                } else if(memRegex.IsMatch(l)){
                    var match = memRegex.Match(l);
                    var loc = match.Groups[1].Value;
                    var val = long.Parse(match.Groups[2].Value);

                    mem[loc] = mask.Mask(val);

                } else {
                    Console.WriteLine("Unable to parse " + l);
                }
            }
            Console.WriteLine(mem.Keys.Count);
            Console.WriteLine(mem.Values.Sum());
        }

        static void Part2(){
            var mem = new Dictionary<long, long>();
            var mask = new BitMask("0101XX01X00X1X1011X1X000000101X10001");
            var inputs = File.ReadAllLines("input.txt");
            var maskRegex = new Regex("mask = (\\w+)");
            var memRegex = new Regex("mem\\[(\\d+)\\] = (\\d+)");

            foreach(var l in inputs){
                if(maskRegex.IsMatch(l)){
                    mask.UpdateMask(maskRegex.Match(l).Groups[1].Value);
                } else if(memRegex.IsMatch(l)){
                    var match = memRegex.Match(l);
                    var locationAddress = match.Groups[1].Value;
                    var locations = mask.MaskAddress(Int64.Parse(locationAddress));

                    var val = long.Parse(match.Groups[2].Value);

                    foreach(var loc in locations){
                        mem[loc] = val;//mask.Mask(val);
                    }

                } else {
                    Console.WriteLine("Unable to parse " + l);
                }
            }
            Console.WriteLine(mem.Values.Sum());
        }

        class BitMask {
        
            private class BitOperation {
                public int Pos {get;set;}
                public char Value {get;set;}
            }

            List<BitOperation> _mask = new List<BitOperation>();
            public BitMask(string input){
                this.UpdateMask(input);
            }

            public void UpdateMask(string input){
                this._mask.Clear();
                var i = 0;
                
                foreach(var c in input){
                    //if(c != 'X'){
                        this._mask.Add(new BitOperation{
                            Pos = i,
                            Value = c
                        });
                    //}
                    i++;
                }
            }

            public List<long> MaskAddress(long i){
                var ret = new List<StringBuilder>();
                ret.Add(new StringBuilder());
                var input = new StringBuilder(Convert.ToString(i, 2).PadLeft(36, '0'));
                var idx = 0;
                foreach(var o in this._mask){
                    if(o.Value == '1'){
                        foreach(var sb in ret){
                            sb.Append('1');
                        }
                    } else if(o.Value =='0'){
                        foreach(var sb in ret){
                            sb.Append(input[idx]);
                        }
                    } else {
                            var count = ret.Count();
                            for (int i1 = 0; i1 < count; i1++){
                                var sb = ret[i1];
                                ret.Add(new StringBuilder(sb.ToString() + "0"));
                            sb.Append("1");
                        }
                    }
                    idx++;
                }

                return ret.Select(sb => Convert.ToInt64(sb.ToString(), 2)).ToList();
            }

            public long Mask(long i){
                var input = new StringBuilder(Convert.ToString(i, 2).PadLeft(36, '0'));
                //Console.WriteLine(input);
                foreach(var o in this._mask){
                    if(o.Value == 'X'){
                        continue;
                    }
                    input[o.Pos] = o.Value;
                }
                //Console.WriteLine(input.ToString());
                return Convert.ToInt64(input.ToString(), 2);
            }
        }
    }
}
