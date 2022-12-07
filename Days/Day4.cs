namespace Days {
    public class Day4 : DaySolverBase {
        public override string Example1 =>
            @"2-4,6-8
2-3,4-5
5-7,7-9
2-8,3-7
6-6,4-6
2-6,4-8";

        List<int[][]> Transform(string raw) {
            List<int[][]> totalInput = new List<int[][]>();


            List<string> listOfStringPairs = raw.Split("\n").ToList<string>();

            foreach (var stringPair in listOfStringPairs)
            {
            int[][] pairArray = new int[2][];

                string[] stringArray = stringPair.Split(",").ToArray<string>();

                string[] firstPairArrayString = stringArray[0].Split("-").ToArray<string>();
                string[] secondPairArrayString = stringArray[1].Split("-").ToArray<string>();

                int[] firstPairArray = new int[2];
                int[] secondPairArray = new int[2];


                firstPairArray[0] = int.Parse(firstPairArrayString[0]);
                firstPairArray[1] = int.Parse(firstPairArrayString[1]);


                secondPairArray[0] = int.Parse(secondPairArrayString[0]);
                secondPairArray[1] = int.Parse(secondPairArrayString[1]);

                pairArray[0] = firstPairArray;
                pairArray[1] = secondPairArray;

                totalInput.Add(pairArray);

            }
            return totalInput;
        }

        public override object Solve1(string raw) {
            List<int[][]> pairs = Transform(raw);

            int pairCount = 0;
            foreach (int[][] pair in pairs)
            {
                if (pair[0][0] <= pair[1][0] && pair[0][1] >= pair[1][1])
                {
                    pairCount += 1;
                }
                else if (pair[1][0] <= pair[0][0] && pair[1][1] >= pair[0][1])
                {
                    pairCount += 1;
                }
            }

            return pairCount;
        }

        public override object Solve2(string raw) {
            List<int[][]> pairs = Transform(raw);

            int pairCount = 0;
            foreach (int[][] pair in pairs)
            {
                if (pair[0][0] <= pair[1][0] && pair[0][1] >= pair[1][1])
                {
                    pairCount += 1;
                }
                else if (pair[1][0] <= pair[0][0] && pair[1][1] >= pair[0][1])
                {
                    pairCount += 1;
                }
                else if (pair[0][1] >= pair[1][0] && pair[0][1] <= pair[1][1])
                {
                    pairCount += 1;
                }
                else if (pair[1][1] >= pair[0][0] && pair[1][1] <= pair[0][1])
                {
                    pairCount += 1;
                }
            }

            return pairCount;

                
        }
    }
}