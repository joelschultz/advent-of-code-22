namespace Days {
    public class Day9 : DaySolverBase {
        public override string Example1 =>
            @"R 4
U 4
L 3
D 1
R 4
D 1
L 5
R 2";

        List<int[]> Transform(string raw) {

            List<string> rows = raw.Split("\n").ToList();
            List<int[]> instructions = new List<int[]>();
            foreach(var row in rows) {
                int[] instruction = new int [] {0,0};
                switch(row[0]) {
                    case 'R':
                        instruction[0] = int.Parse(row[2].ToString());
                        break;
                    case 'L':
                        instruction[0] = -int.Parse(row[2].ToString());
                        break;
                    case 'U':
                        instruction[1] = int.Parse(row[2].ToString());
                        break;
                    case 'D':
                        instruction[1] = -int.Parse(row[2].ToString());
                        break;
                }
                instructions.Add(instruction);
            }
            
            return instructions;
        }

        struct Position {
            public Position(int anX, int aY) {
                x = anX;
                y = aY;
            }
            public int x;
            public int y;
        }
        public override object Solve1(string raw) {
            List<int[]> input = Transform(raw);

            HashSet<Position> visitedTiles = new HashSet<Position>();
            visitedTiles.Add(new Position(0,0));

            foreach(var instruction in input) {
                
            }
            
            return -1;
        }

        public override object Solve2(string raw) {
            var input = Transform(raw);

            return -1;
        }
    }
}