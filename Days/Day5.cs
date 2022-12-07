using System.Diagnostics;

namespace Days {
    public class Day5 : DaySolverBase {
        public override string Example1 =>
            @"    [D]    
[N] [C]    
[Z] [M] [P]
 1   2   3 

move 1 from 2 to 1
move 3 from 1 to 3
move 2 from 2 to 1
move 1 from 1 to 2";

       string GetInstructions(string raw) {
            List<string> list = new List<string>();
            string returnString = "";
            if(raw.Length > 300) {
                 returnString = raw.Substring(325);
            }
            return returnString;
        }
       
       List<char>[] GetCratesString(string raw) {
           List<char>[] listArray = new List<char>[9];
           if(raw.Length > 300) {
               string subString = raw.Substring(0, 287);
               List<string> rows = new List<string>();
               rows = subString.Split("\n").ToList();

               for(int i = 0; i < 9; i++) {
                   listArray[i] = new List<char>();
                   for(int j = 7; j > -1; j--) {
                       listArray[i].Add(rows[j][1 + (4 * i)]);
                   }
               }
               foreach(var charList in listArray) {
                   for(int i = 0; i < charList.Count; i++) {
                       if(charList[i] == ' ') {
                           charList.RemoveAt(i);
                           i -= 1;
                       }
                       
                   }
               }
           }

           return listArray;
       }

        public override object Solve1(string raw) {
            var instructions = GetInstructions(raw);

            int count = 0;
            List<List<int>> instructionList = new List<List<int>>(); 
            for(int i = 0; i < instructions.Length; i++) {
                if(Int32.TryParse(instructions[i].ToString(), out int instructionInt)) {
                    if(i < instructions.Length - 2 && Int32.TryParse(instructions[i+1].ToString(), out int instructionIntSecond)) {
                        Int32.TryParse(instructions[i].ToString() + instructions[i + 1].ToString(), out int combinedInt);
                        instructionInt = combinedInt;
                        i += 1;
                    }
                    if(count == 0) {
                        instructionList.Add(new List<int>());
                    }
                    instructionList[^1].Add(instructionInt);
                    count += 1;
                    if(count >= 3) {
                        count = 0;
                    }
                }
                
            }
            
            var crates = GetCratesString(raw);

            for(int j = 0; j < instructionList.Count; j++) {
                List<int> instructionSet = instructionList[j];
                int numberOfMoves = instructionSet[0];
                int startPos = instructionSet[1] - 1;
                int endPos = instructionSet[2] - 1;

                List<char> cratesToAdd = new List<char>();
                for(int i = 0; i < numberOfMoves; i++) {
                    if(crates[startPos].Count > 0) {
                        cratesToAdd.Add(crates[startPos][crates[startPos].Count - 1]);
                        crates[startPos].RemoveAt(crates[startPos].Count - 1);
                    }
                }
                for(int i = cratesToAdd.Count - 1; i >= 0; i--) {
                    crates[endPos].Add(cratesToAdd[i]);
                }
            }
            char[] topCrates = new char[9];
            for(int i = 0; i < crates.Length; i++) {
                if(crates.Length > i && crates[i].Count > 0) {
                topCrates[i] = crates[i][^1];
                    
                }
            }
            
            return topCrates;
        }

        public override object Solve2(string raw) {
            //var input = GetInstructions(raw);

            return -1;
        }
    }
}