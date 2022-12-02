namespace Days {
    public class Day1 : DaySolverBase {
        public override string Example1 =>
            @"1000
2000
3000

4000

5000
6000

7000
8000
9000

10000";

        List<int> Transform(string raw) {
            return raw
                .Split("\n")
                .Select(int.Parse)
                .ToList();
        }
        List<List<int>> TransformFromGroups(List<string> stringGroups) {
            List<List<int>> ints = new List<List<int>>();

            foreach(var stringGroup in stringGroups) {
                List<int> stringList = Transform(stringGroup);
                ints.Add(stringList);
            }
            
            
            return ints;
        }
        
        List<string> TransformToGroups(string raw) {
            return raw
                .Split("\n\n")
                .ToList();
        }

        public override object Solve1(string raw) {
            var inputGroups = TransformToGroups(raw);
            var input = TransformFromGroups(inputGroups);

            int maxCaloryAmount = -1;
            foreach(var intGroupList in input) {
                int totalInCurrent = 0;
                foreach(var caloryValue in intGroupList) {
                    totalInCurrent += caloryValue;
                }
                if(totalInCurrent > maxCaloryAmount) {
                    maxCaloryAmount = totalInCurrent;
                }
            }

            return maxCaloryAmount;
        }

        public override object Solve2(string raw) {
            
            var inputGroups = TransformToGroups(raw);
            var input = TransformFromGroups(inputGroups);

            List<int> totalList = new List<int>();
            foreach(var intGroupList in input) {
                int totalInCurrent = 0;
                foreach(var caloryValue in intGroupList) {
                    totalInCurrent += caloryValue;
                }
                totalList.Add(totalInCurrent);
            }
            totalList.Sort();
            int totalCalories = 0;

            totalCalories += totalList[^1];
            totalCalories += totalList[^2];
            totalCalories += totalList[^3];
            

            return totalCalories;
        }
    }
}