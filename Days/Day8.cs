using System.Runtime.CompilerServices;

namespace Days {
    public class Day8 : DaySolverBase {
        public override string Example1 =>
            @"30373
25512
65332
33549
35390";

        int[][] Transform(string raw) {
            List<string> list = raw.Split("\n").ToList();
            int arrayHeight = list.Count;
            int arrayWidth = list[0].Length;
            int[][] doubleArray = new int[arrayHeight][];
            for(int i = 0; i < arrayHeight; i++) {
                doubleArray[i] = new int[arrayWidth];
                for(int j = 0; j < arrayWidth; j++) {
                    doubleArray[i][j] = int.Parse(list[i][j].ToString());
                }
            }
                
            return doubleArray;
        }

        public override object Solve1(string raw) {
            int[][] input = Transform(raw);

            int visibleTrees = 0;
            for(int heightIndex = 1; heightIndex < input.Length - 1; heightIndex++) {
                int[] row = input[heightIndex];
                for(int widthIndex = 1; widthIndex < row.Length - 1; widthIndex++) {
                    if(CheckTreeVisibility(input, heightIndex, widthIndex)) {
                        visibleTrees += 1;
                    }
                }
            }

            visibleTrees += input.Length + input.Length + input[0].Length + input[0].Length - 4;

            return -1;
        }

        public bool CheckTreeVisibility(int[][] doubleArray, int heightIndex, int widthIndex) {
            int currentTreeHeight = doubleArray[heightIndex][widthIndex];
            bool isVisibleFromDirection = true;
            for(int i = heightIndex + 1; i < doubleArray.Length; i++) {
                if(currentTreeHeight <= doubleArray[i][widthIndex]) {
                    isVisibleFromDirection = false;
                }
            }
            if(isVisibleFromDirection) {
                return true;
            }
            isVisibleFromDirection = true;
            for(int i = heightIndex - 1; i >= 0; i--) {
                if(currentTreeHeight <= doubleArray[i][widthIndex]) {
                    isVisibleFromDirection = false;
                }
            }
            if(isVisibleFromDirection) {
                return true;
            }
            
            
            
            
            
            isVisibleFromDirection = true;
            for(int i = widthIndex - 1; i >= 0; i--) {
                if(currentTreeHeight <= doubleArray[heightIndex][i]) {
                    isVisibleFromDirection = false;
                }
            }
            if(isVisibleFromDirection) {
                return true;
            }
            
            isVisibleFromDirection = true;
            for(int i = widthIndex + 1; i < doubleArray[0].Length; i++) {
                if(currentTreeHeight <= doubleArray[heightIndex][i]) {
                    isVisibleFromDirection = false;
                }
            }
            if(isVisibleFromDirection) {
                return true;
            }
            
            return false;
        }
        
        public override object Solve2(string raw) {
            int[][] input = Transform(raw);

            int highestScore = 0;
            for(int heightIndex = 1; heightIndex < input.Length - 1; heightIndex++) {
                int[] row = input[heightIndex];
                for(int widthIndex = 1; widthIndex < row.Length - 1; widthIndex++) {
                    int score = CheckTreeScore(input, heightIndex, widthIndex);
                    if(score > highestScore) {
                        highestScore = score;
                    }
                }
            }
            return highestScore;
        }

        public int CheckTreeScore(int[][] doubleArray, int heightIndex, int widthIndex) {
            int currentTreeHeight = doubleArray[heightIndex][widthIndex];
            int treesVisibleNorth = 0;
            for(int i = heightIndex + 1; i < doubleArray.Length; i++) {
                treesVisibleNorth += 1;
                if(currentTreeHeight <= doubleArray[i][widthIndex]) {
                    break;
                }
            }
            
            int treesVisibleSouth = 0;
            for(int i = heightIndex - 1; i >= 0; i--) {
                treesVisibleSouth += 1;
                if(currentTreeHeight <= doubleArray[i][widthIndex]) {
                    break;
                }
            }
            
            int treesVisibleEast = 0;
            for(int i = widthIndex + 1; i < doubleArray[0].Length; i++) {
                treesVisibleEast += 1;
                if(currentTreeHeight <= doubleArray[heightIndex][i]) {
                    break;
                }
            }
            
            int treesVisibleWest = 0;
            for(int i = widthIndex - 1; i >= 0; i--) {
                treesVisibleWest += 1;
                if(currentTreeHeight <= doubleArray[heightIndex][i]) {
                    break;
                }
            }
            
            
            return treesVisibleNorth * treesVisibleSouth * treesVisibleEast * treesVisibleWest;
        }
    }
}