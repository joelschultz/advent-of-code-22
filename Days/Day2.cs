namespace Days {
    public class Day2 : DaySolverBase {
        public override string Example1 =>
            @"A Y
B X
C Z";

        List<int[]> Transform(string raw) {

            List<int[]> intList = new List<int[]>();

            string[] stringArray = new string[2];

            List<string> strings = raw.Split("\n").ToList();

            foreach(var rowString in strings) {
                stringArray = rowString.Split(" ").ToArray();
                int[] intArray = new int[2];
                intArray[0] = GetValueFromString(stringArray[0]);
                intArray[1] = GetValueFromString(stringArray[1]);
                intList.Add(intArray);
            }
            
            return intList;
        }
        private static int GetValueFromString(string stringValue)
        {
            switch (stringValue)
            {
                case "X":
                case "A": 
                    return 0;
                    break;

                case "Y":
                case "B":
                    return 1;
                    break;

                case "Z":
                case "C":
                    return 2;
                    break;
            }
            return -1;
        }

        public override object Solve1(string raw) {
            var input = Transform(raw);

            int totalScore = 0;
            
            totalScore = GetScore1(input);

            
            
            return totalScore;
        }
        private static int GetScore1(List<int[]> input)
        {
            int scoreForType = 0;
            int scoreForResult = 0;
            foreach (var matchString in input)
            {
                switch (matchString[1])
                {
                    case 0:
                        scoreForType += 1;
                        switch(matchString[0]) {
                            case 0:
                                scoreForResult += 3;
                                break;
                            case 2:
                                scoreForResult += 6;
                                break;
                        }
                        break;
                    case 1:
                        scoreForType += 2;
                        switch(matchString[0]) {
                            case 0:
                                scoreForResult += 6;
                                break;
                            case 1:
                                scoreForResult += 3;
                                break;
                        }
                        break;
                    case 2:
                        scoreForType += 3;
                        switch(matchString[0]) {
                            case 1:
                                scoreForResult += 6;
                                break;
                            case 2:
                                scoreForResult += 3;
                                break;
                        }
                        break;
                }
                
            }
            return scoreForType + scoreForResult;
        }

        public override object Solve2(string raw) {
            var input = Transform(raw);

            int totalScore = 0;
            
            totalScore = GetScore1(GetAdjustedHandValues(input));

            
            
            return totalScore;
        }
        private List<int[]> GetAdjustedHandValues(List<int[]> input) {
            List<int[]> adjustedHandValues = new List<int[]>();
            foreach(var intArray in input) {
                int[] currentMatchValues = new int[2];
                currentMatchValues[0] = intArray[0];
                switch(intArray[1]) {
                    case 0:
                        // Get loose value
                        if(intArray[0] - 1 < 0) {
                            currentMatchValues[1] = 2;
                        }
                        else {
                            currentMatchValues[1] = intArray[0] - 1;
                        }
                        break;
                    case 1:
                        // Get Draw value
                        currentMatchValues[1] = intArray[0];
                        break;
                    case 2:
                        //Get win value
                        
                        if(intArray[0] + 1 > 2) {
                            currentMatchValues[1] = 0;
                        }
                        else {
                            currentMatchValues[1] = intArray[0] + 1;
                        }
                        break;
                    
                }
                adjustedHandValues.Add(currentMatchValues);
            }
            return adjustedHandValues;
        }
    }
}