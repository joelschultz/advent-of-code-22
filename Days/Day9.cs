namespace Days
{
    public class Day9 : DaySolverBase
    {
        public override string Example1 =>
            @"R 5
U 8
L 8
D 3
R 17
D 10
L 25
U 20";

        List<int[]> Transform(string raw)
        {

            List<string> rows = raw.Split("\n").ToList();
            List<int[]> instructions = new List<int[]>();
            foreach (var row in rows)
            {
                int[] instruction = new int[] { 0, 0 };
                string[] rowInstruction = row.Split(" ").ToArray<string>();
                switch (rowInstruction[0])
                {
                    case "R":
                        instruction[0] = int.Parse(rowInstruction[1].ToString());
                        break;
                    case "L":
                        instruction[0] = -int.Parse(rowInstruction[1].ToString());
                        break;
                    case "U":
                        instruction[1] = int.Parse(rowInstruction[1].ToString());
                        break;
                    case "D":
                        instruction[1] = -int.Parse(rowInstruction[1].ToString());
                        break;
                }
                instructions.Add(instruction);
            }

            return instructions;
        }

        struct Position
        {
            public Position(int anX = 0, int aY = 0)
            {
                x = anX;
                y = aY;
            }

            public bool IsMorethanOneAway(Position otherPosition)
            {
                if (Math.Abs(x - otherPosition.x) > 1 || Math.Abs(y - otherPosition.y) > 1)
                {
                    return true;
                }
                return false;

            }
            public int x;
            public int y;
        }

        struct Position2
        {
            public Position2()
            {
                coordinates = new int[2];
            }

            public bool IsMorethanOneAway(Position2 otherPosition)
            {
                if (Math.Abs(coordinates[0] - otherPosition.coordinates[0]) > 1 || Math.Abs(coordinates[1] - otherPosition.coordinates[1]) > 1)
                {
                    return true;
                }
                return false;

            }
            public int[] coordinates;
        }

        public enum Direction
        {
           Right,
           Left,
           Up,
           Down
        };

        public override object Solve1(string raw)
        {
            List<int[]> input = Transform(raw);

            HashSet<Position> visitedTiles = new HashSet<Position>();
            visitedTiles.Add(new Position(0, 0));
            Position tailPos = new Position(0, 0);
            Position headPos = new Position(0, 0);
            foreach (var instruction in input)
            {
                Position nextHeadPos = new Position(headPos.x, headPos.y);

                if (instruction[0] > 0)
                {
                    for (int i = 0; i < Math.Abs(instruction[0]); i++)
                    {
                        nextHeadPos.x += 1;
                        if (nextHeadPos.IsMorethanOneAway(tailPos))
                        {
                            tailPos = headPos;
                            visitedTiles.Add(tailPos);
                        }
                        headPos = nextHeadPos;
                    }
                }
                else if (instruction[0] < 0)
                {
                    for (int i = 0; i < Math.Abs(instruction[0]); i++)
                    {
                        nextHeadPos.x -= 1;
                        if (nextHeadPos.IsMorethanOneAway(tailPos))
                        {
                            tailPos = headPos;
                            visitedTiles.Add(tailPos);
                        }
                        headPos = nextHeadPos;
                    }
                }
                else if (instruction[1] > 0)
                {
                    for (int i = 0; i < instruction[1]; i++)
                    {
                        nextHeadPos.y += 1;
                        if (nextHeadPos.IsMorethanOneAway(tailPos))
                        {
                            tailPos = headPos;
                            visitedTiles.Add(tailPos);
                        }
                        headPos = nextHeadPos;
                    }
                }
                else if (instruction[1] < 0)
                {
                    for (int i = 0; i < Math.Abs(instruction[1]); i++)
                    {
                        nextHeadPos.y -= 1;
                        if (nextHeadPos.IsMorethanOneAway(tailPos))
                        {
                            tailPos = headPos;
                            visitedTiles.Add(tailPos);
                        }
                        headPos = nextHeadPos;
                    }
                }

            }

            return visitedTiles.Count;
        }
        public override object Solve2(string raw)
        {
            List<int[]> input = Transform(raw);

            HashSet<Position2> visitedTiles = new HashSet<Position2>();
            Position2 startPos = new Position2();
            startPos.coordinates = new int[] {0,0};
            visitedTiles.Add(startPos);
            Position2[] knotPositions = new Position2[10];
            for (int i = 0; i < 10; i++)
            {
                knotPositions[i].coordinates = new int[] { 0, 0 }; 
            }

                Position2[] nextKnotPositions = new Position2[10];
            for (int i = 0; i < 10; i++)
            {
                nextKnotPositions[i].coordinates = new int[] { 0, 0 };
            }
            foreach (var instruction in input)
            {
                for (int i = 0; i < 10; i++)
                {
                    nextKnotPositions[i].coordinates[0] = knotPositions[i].coordinates[0];
                    nextKnotPositions[i].coordinates[1] = knotPositions[i].coordinates[1];
                }

                    NewMethod(ref visitedTiles, ref knotPositions, instruction, ref nextKnotPositions);
                System.Threading.Thread.Sleep(1000);
                Draw(knotPositions, visitedTiles);
            }

            return visitedTiles.Count;
        }

        private static void NewMethod(ref HashSet<Position2> visitedTiles, ref Position2[] knotPositions, int[] instruction, ref Position2[] nextKnotPositions) {
            int currentInstructionSize = Math.Abs(instruction[0]);
            if (Math.Abs(instruction[1]) > currentInstructionSize)
            {
                currentInstructionSize = instruction[1];
            }
            for(int i = 0; i < currentInstructionSize; i++) {

                if (instruction[0] > 0)
                {
                    nextKnotPositions[0].coordinates[0] += 1;
                }
                else if (instruction[0] < 0)
                {
                    nextKnotPositions[0].coordinates[0] -= 1;
                }
                else if (instruction[1] > 0)
                {
                    nextKnotPositions[0].coordinates[1] += 1;
                }
                else if (instruction[1] < 0)
                {
                    nextKnotPositions[0].coordinates[1] -= 1;
                }
                for (int j = 0; j < 9; j++) {
                    if(nextKnotPositions[j].IsMorethanOneAway(knotPositions[j + 1])) {
                        if ( Math.Abs(nextKnotPositions[j].coordinates[0] - knotPositions[j + 1].coordinates[0]) > 0 &&
                            Math.Abs(nextKnotPositions[j].coordinates[1] - knotPositions[j + 1].coordinates[1]) > 0)
                        {
                            nextKnotPositions[j + 1].coordinates[0] += nextKnotPositions[j].coordinates[0] - knotPositions[j].coordinates[0];
                            nextKnotPositions[j + 1].coordinates[1] += nextKnotPositions[j].coordinates[1] - knotPositions[j].coordinates[1];
                        }
                        else
                        {
                            nextKnotPositions[j + 1].coordinates[0] = knotPositions[j].coordinates[0];
                            nextKnotPositions[j + 1].coordinates[1] = knotPositions[j].coordinates[1];
                        }

                        if (j == 8) {
                            visitedTiles.Add(nextKnotPositions[j + 1]);
                        }
                    }
                }
                for(int j = 0; j < 10; j++) {
                    knotPositions[j].coordinates[0] = nextKnotPositions[j].coordinates[0];
                    knotPositions[j].coordinates[1] = nextKnotPositions[j].coordinates[1];
                }

                
            }
        }

        private static void Draw(Position2[] knotPositions, HashSet<Position2> visitedTiles) {

            Console.Clear();
            int largestX = 0;
            int largestY = 0;
            int smallestX = 0;
            int smallestY = 0;

            for (int i = 0; i < knotPositions.Length; i++)
            {
                if (knotPositions[i].coordinates[0] > largestX)
                {
                    largestX = knotPositions[i].coordinates[0];
                }
                else if (knotPositions[i].coordinates[0] < smallestX)
                {
                    smallestX = knotPositions[i].coordinates[0];
                }
                if (knotPositions[i].coordinates[1] > largestY)
                {
                    largestY = knotPositions[i].coordinates[1];
                }
                else if (knotPositions[i].coordinates[1] < smallestY)
                {
                    smallestY = knotPositions[i].coordinates[1];
                }
            }

            foreach (var visitedTile in visitedTiles)
            {
                if (visitedTile.coordinates[0] > largestX)
                {
                    largestX = visitedTile.coordinates[0];
                }
                else if (visitedTile.coordinates[0] < smallestX)
                {
                    smallestX = visitedTile.coordinates[0];
                }
                if (visitedTile.coordinates[1] > largestY)
                {
                    largestY = visitedTile.coordinates[1];
                }
                else if (visitedTile.coordinates[1] < smallestY)
                {
                    smallestY = visitedTile.coordinates[1];
                }
            }

            int boardWidth = Math.Abs(largestX - smallestX) + Math.Abs(smallestX) + 5;
            int boardHeight = Math.Abs(largestY - smallestY) + Math.Abs(smallestY) + 5;

            string[][] drawBoard = new string[boardWidth][];

            for (int i = 0; i < drawBoard.Length; i++)
            {
                drawBoard[i] = new string[boardHeight];
                for (int j = 0; j < boardHeight; j++)
                {
                    drawBoard[i][j] = ".";
                }
            }
            for (int i = 0; i < knotPositions.Length; i++)
            {
                Position2 pos = knotPositions[i];
                drawBoard[pos.coordinates[0] + Math.Abs(smallestX)][pos.coordinates[1] + Math.Abs(smallestY)] = i.ToString();
            }

            foreach (var visitedTile in visitedTiles)
            {
                if (drawBoard[visitedTile.coordinates[0]][visitedTile.coordinates[1]] != "9")
                {
                    drawBoard[visitedTile.coordinates[0]][visitedTile.coordinates[1]] = "#";
                }
            }
            for (int i = boardHeight - 1; i >= 0; i--)
            {
                for (int j = 0; j < boardWidth; j++)
                {
                    Console.Write(drawBoard[j][i]);
                }
                Console.Write("\n");
            }
        }
    }
}