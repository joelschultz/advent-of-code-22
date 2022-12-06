namespace Days {
    public class Day6 : DaySolverBase {
        public override string Example1 =>
            @"mjqjpqmgbljsphdztnvjfqwrcgsmlb";

        string Transform(string raw) {
            return raw;
        }

        public override object Solve1(string raw) {
            var input = Transform(raw);

            for(int i = 3; i < input.Length; i++) {
                if(input[i] != input[i - 1] &&
                    input[i] != input[i - 2] &&
                    input[i] != input[i - 3] &&
                    input[i - 1] != input[i - 2] &&
                    input[i - 1] != input[i - 3] &&
                    input[i - 2] != input[i - 3]) {
                    return i + 1;
                }
            }
            
            return -1;
        }

        public override object Solve2(string raw) {
            var input = Transform(raw);

            for(int i = 13; i < input.Length; i++) {
                if(!GetIsMarkerAtLocation(input, i)) {
                    return i + 1;
                }
            }
            return -1;
        }
        private static bool GetIsMarkerAtLocation(string input, int startNumber) {
            int endNumber = startNumber - 12;
            int maxToCheck = 13;
            for(int index = startNumber; index > endNumber; index--) {
                for(int j = 1; j <= maxToCheck; j++) {
                    if(input[index] == input[index - j]) {
                        return true;
                    }
                }
                maxToCheck -= 1;
            }
            return false;
        }
    }
}