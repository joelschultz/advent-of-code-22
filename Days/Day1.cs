namespace Days {
    public class Day1 : DaySolverBase {
        public override string Example1 =>
            @"199
200
208
210
200
207
240
269
260
263";

        List<int> Transform(string raw) {
            return raw
                .Split("\n")
                .Select(int.Parse)
                .ToList();
        }

        public override object Solve1(string raw) {
            var input = Transform(raw);

            int count = 0;
            for(int i = 1; i < input.Count; i++) {
                if(input[i - 1] < input[i])
                    count++;
            }
            
            return count;
        }

        public override object Solve2(string raw) {
            var input = Transform(raw);

            int count = 0;
            for(int i = 3; i < input.Count; i++) {
                if(input[i - 3] < input[i - 0] )
                    count++;
            }
            
            return count;
        }
    }
}