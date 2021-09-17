using System;
using System.Collections.Generic;
using System.Linq;

namespace _17 {
    class Program {
        static void Main(string[] args) {
            Console.WriteLine("Hello World!");
        }
    }

    enum State {
        Inactive,
        Active,
    }

    class Map {
        public List<List<List<State>>> States {get; private set;}

        public Map(string input){
            var rows = input.Split(Environment.NewLine);


        var initialBoard = new List<List<State>>();
            foreach(var row in rows){
                initialBoard.Add(row.Select(r => r == '.' ? State.Active : State.Inactive).ToList());
            }

            //z then y then x;

            //this.States = new State[rows.Length, rows[0].Length];            
            this.States = new List<List<List<State>>>();
            //this.States.Add();

            //Console.WriteLine($"width: {this.Width}");

            for (var r = 0; r < rows.Length; r++) {
                //Console.WriteLine(rows[r]);
                for (var c = 0; c < rows[r].Length; c++) {
                    switch(rows[r][c]){
                        case 'L':
                            this.States[r,c] = State.Inactive;
                            break;
                        case '.':
                            this.States[r,c] = State.Floor;
                            break;
                        case '#':
                            this.States[r,c] = State.Active;
                            break;
                    }
                }
            }
        }

        public bool Step(){ 
            var newState = new State[this.States.GetLength(0), this.States.GetLength(1)];
            for(var r = 0; r < this.States.GetLength(0); r++){
                //var c = 0;
                for(var c = 0; c < this.States.GetLength(1); c++){
                    //Console.WriteLine($"Checking {r},{c}" );
                    switch(this.States[r,c]){
                        case State.Inactive:
                            if(this.OccupiedNeighbors(r,c) == 0 ){
                                newState[r,c] = State.Active;
                            } else{
                                newState[r,c] = State.Inactive;
                            }
                            break;
                        case State.Active:
                            var numOcc = this.OccupiedNeighbors(r,c);
                            //Console.WriteLine($"num occ at {r},{c} is {numOcc}");
                            if(numOcc > 3 ){
                                newState[r,c] = State.Inactive;
                            } else {
                                newState[r,c] = State.Active;
                            }
                            break;
                        case State.Floor:
                            newState[r,c] = State.Floor;
                            continue;
                    }                        
                }
            }

            if(this.Matches(newState)){
                return true;
            } else {
                this.States = newState;
                return false;
            }
        }

        // public bool Step2(){ 
        //     var newState = new State[this.States.GetLength(0), this.States.GetLength(1)];
        //     for(var r = 0; r < this.States.GetLength(0); r++){
        //         //var c = 0;
        //         for(var c = 0; c < this.States.GetLength(1); c++){
        //             //Console.WriteLine($"Checking {r},{c}" );
        //             switch(this.States[r,c]){
        //                 case State.Inactive:
        //                     if(this.SeenOccupied(r,c) == 0 ){
        //                         newState[r,c] = State.Active;
        //                     } else{
        //                         newState[r,c] = State.Inactive;
        //                     }
        //                     break;
        //                 case State.Active:
        //                     var numOcc = this.SeenOccupied(r,c);
        //                     //Console.WriteLine($"num occ at {r},{c} is {numOcc}");
        //                     if(numOcc > 4 ){
        //                         newState[r,c] = State.Inactive;
        //                     } else {
        //                         newState[r,c] = State.Active;
        //                     }
        //                     break;
        //                 case State.Floor:
        //                     newState[r,c] = State.Floor;
        //                     continue;
        //             }                        
        //         }
        //     }

        //     if(this.Matches(newState)){
        //         return true;
        //     } else {
        //         this.States = newState;
        //         return false;
        //     }
        // }

        private int OccupiedNeighbors(int x, int y, int z){
            var numOcc = 0;
            //Console.WriteLine();
            for(var nr = x - 1; nr <= x + 1; nr++){
                if(nr < 0 || nr > this.Height - 1){
                    continue;
                }                

                for(var nc = y - 1; nc <= y + 1; nc++){
                    if(nc < 0 || nc > this.Width -1){
                        continue;
                    }

                    if(nc == y && nr == x){
                        continue;
                    }

                    //Console.WriteLine($"checking {nr},{nc}");
                    if(this.States[nr,nc] == State.Active){
                        numOcc++;
                    }

                }
            }
            return numOcc;
        }

        // public int SeenOccupied(int r, int c){
        //     //Console.WriteLine("State is " + this.States[r,c]);
        //     var numOcc = 0;
        //     //Left
        //     for(var nc = c - 1; nc >= 0; nc--){
        //         //Console.WriteLine
        //         if(this.States[r,nc] == State.Active){
        //             numOcc++;
        //             break;
        //         } else if(this.States[r,nc] == State.Inactive) {
        //             break;
        //         }
        //     }

        //     //Right
        //     for(var nc = c + 1; nc < this.Width; nc++){
        //         if(this.States[r,nc] == State.Active){
        //             numOcc++;
        //             break;
        //         } else if(this.States[r,nc] == State.Inactive){
        //             break;
        //         }
        //     }

        //     //Up
        //     for(var nr = r - 1; nr >= 0; nr--){
        //         if(this.States[nr,c] == State.Active){
        //             numOcc++;
        //             break;
        //         } else if(this.States[nr,c] == State.Inactive){
        //             break;
        //         }
        //     }

        //     //Down
        //     for(var nr = r + 1; nr < this.Height; nr++){
        //         if(this.States[nr,c] == State.Active){
        //             numOcc++;
        //             break;
        //         } else if(this.States[nr,c] == State.Inactive){
        //             break;
        //         }
        //     }


        //     //UpLeft
        //     for(var i = 1; c - i >= 0 && r - i >= 0; i++){
        //         if(this.States[r - i,c - i] == State.Active){
        //             numOcc++;
        //             break;
        //         } else if(this.States[r - i,c - i] == State.Inactive){
        //             break;
        //         }
        //     }

        //     //UpRight
        //     for(var i = 1; c + i < this.Width && r - i >= 0; i++){
        //         //Console.WriteLine($"checking {r - i},{c + i}");
        //         if(this.States[r - i,c + i] == State.Active){
        //             numOcc++;
        //             break;
        //         } else if(this.States[r - i,c + i] == State.Inactive){
        //             break;
        //         }
        //     }

        //     //DownRight
        //     for(var i = 1; c + i < this.Width && r + i < this.Height; i++){
        //         if(this.States[r + i,c + i] == State.Active){
        //             numOcc++;
        //             break;
        //         } else if(this.States[r + i,c + i] == State.Inactive){
        //             break; 
        //         }
        //     }

        //     //DownLeft
        //     for(var i = 1; c - i >= 0 && r + i < this.Height; i++){
        //         //Console.WriteLine($"checking {r+i}, {c-i}");
        //         if(this.States[r + i,c - i] == State.Active){
        //             numOcc++;
        //             break;
        //         } else if(this.States[r + i,c - i] == State.Inactive){
        //             break;
        //         }
        //     }
            
        //     return numOcc;
        // }

        public override string ToString() {
            var sb = new StringBuilder();
            for(var i = 0; i < this.States.GetLength(0); i++){
                for(var j = 0; j < this.States.GetLength(1); j++){
                    switch(this.States[i,j]){
                        case State.Inactive:
                            sb.Append("L");
                            break;
                        case State.Active:
                            sb.Append("#");
                            break;
                            case State.Floor:
                            sb.Append(".");
                            break;
                    }
                }
                sb.AppendLine();
            }
            return sb.ToString();
        }
        
        public bool Matches(State[,] other){
            for(var i = 0; i < this.States.GetLength(0); i++){
                for(var j = 0; j < this.States.GetLength(1); j++){
                    if(this.States[i,j] != other[i,j]){
                        Console.WriteLine($"Failed on {i},{j}");
                        return false;
                    }
                }
            }
            return true;
        }

        public int NumnOccSeats(){
            var sum = 0;
            foreach(var s in this.States){
                if(s == State.Active){
                    sum++;
                }
            }
            return sum;
        }
    }
}
