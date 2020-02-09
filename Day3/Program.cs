using System;
using System.Collections.Generic;

namespace Day3
{
    class Program
    {
        public class Wire
        {
            public Wire()
            {
                Points = new List<Tuple<int, int>>();
            }

            private List<Tuple<int, int>> Points;

            public void SetPoint(int x, int y)
            {
                Points.Add(new Tuple<int, int>(x, y));
            }

            public List<Tuple<int, int>> GetPoints() => Points;
        }

        public class ManhattanGrid
        {
            public Wire Wire1 { get; set; }
            public Wire Wire2 { get; set; }
            public int SizeX { get; set; }
            public int SizeY { get; set; }
            public string[,] Grid { get; set; }
            public Tuple<int, int> CentralPort { get; set; }
            public Tuple<int, int> StartingPoint { get; set; }
            public List<Tuple<int, int>> Intersections { get; set; }

            public Tuple<int, int> GetCentralPort() => CentralPort;

            public ManhattanGrid()
            {

            }

            public void CreateWire(string nextPosition, Wire wire)
            {
                char direction = nextPosition[0];
                int distance = Int32.Parse(nextPosition.Substring(1, nextPosition.Length - 1));

                if (direction == 'R')
                {
                    for (int j = StartingPoint.Item2 + 1; j <= StartingPoint.Item2 + distance; j++)
                    {
                        wire.SetPoint(StartingPoint.Item1, j);

                        if ((Grid[StartingPoint.Item1, j] == "-") || (Grid[StartingPoint.Item1, j] == "|"))
                        {
                            Grid[StartingPoint.Item1, j] = "X";
                            Intersections.Add(new Tuple<int, int>(StartingPoint.Item1, j));
                        }
                        else
                        {
                            Grid[StartingPoint.Item1, j] = "-";
                        }
                    }
                    StartingPoint = new Tuple<int, int>(StartingPoint.Item1, StartingPoint.Item2 + distance);
                }
                if (direction == 'L')
                {
                    for (int j = StartingPoint.Item2 - 1; j >= StartingPoint.Item2 - distance; j--)
                    {
                        wire.SetPoint(StartingPoint.Item1, j);
                        if ((Grid[StartingPoint.Item1, j] == "-") || (Grid[StartingPoint.Item1, j] == "|"))
                        {
                            Grid[StartingPoint.Item1, j] = "X";
                            Intersections.Add(new Tuple<int, int>(StartingPoint.Item1, j));
                        }
                        else
                        {
                            Grid[StartingPoint.Item1, j] = "-";
                        }
                    }
                    StartingPoint = new Tuple<int, int>(StartingPoint.Item1, StartingPoint.Item2 - distance);
                }
                if (direction == 'U')
                {
                    for (int i = StartingPoint.Item1 - 1; i >= StartingPoint.Item1 - distance; i--)
                    {
                        wire.SetPoint(i, StartingPoint.Item2);

                        if ((Grid[i, StartingPoint.Item2] == "|") || (Grid[i, StartingPoint.Item2] == "-"))
                        {
                            Grid[i, StartingPoint.Item2] = "X";
                            Intersections.Add(new Tuple<int, int>(i, StartingPoint.Item2));
                        }
                        else
                        {
                            Grid[i, StartingPoint.Item2] = "|";
                        }
                    }
                    StartingPoint = new Tuple<int, int>(StartingPoint.Item1 - distance, StartingPoint.Item2);
                }
                if (direction == 'D')
                {
                    for (int i = StartingPoint.Item1 + 1; i <= StartingPoint.Item1 + distance; i++)
                    {
                        wire.SetPoint(i, StartingPoint.Item2);

                        if ((Grid[i, StartingPoint.Item2] == "|") || (Grid[i, StartingPoint.Item2] == "-"))
                        {
                            Grid[i, StartingPoint.Item2] = "X";
                            Intersections.Add(new Tuple<int, int>(i, StartingPoint.Item2));
                        }
                        else
                        {
                            Grid[i, StartingPoint.Item2] = "|";
                        }
                    }
                    StartingPoint = new Tuple<int, int>(StartingPoint.Item1 + distance, StartingPoint.Item2);
                }
            }

            public int CalculateManhattanDistance(int p1, int q1, int p2, int q2)
            {
                return Math.Abs(p1 - q1) + Math.Abs(p2 - q2);
            }

            public int CalculateWireDistance(Wire wire, Tuple<int, int> intersection)
            {
                int counter = 0;

                foreach (var point in wire.GetPoints())
                {
                    if (point.Item1 == intersection.Item1 && point.Item2 == intersection.Item2)
                    {
                        counter++;
                        return counter;
                    }
                    else
                    {
                        counter++;
                    }
                }

                return counter;
            }
        }

        public class MGBuilder
        {
            protected ManhattanGrid manhattanGrid = new ManhattanGrid();

            public MGSizeBuilder Size => new MGSizeBuilder(manhattanGrid);
            public MGWireBuilder Wires => new MGWireBuilder(manhattanGrid);

            public static implicit operator ManhattanGrid(MGBuilder mg)
            {
                return mg.manhattanGrid;
            }
        }

        public class MGSizeBuilder : MGBuilder 
        {
            public MGSizeBuilder(ManhattanGrid manhattanGrid)
            {
                this.manhattanGrid = manhattanGrid;
            }

            public MGSizeBuilder HasSize(int x, int y)
            {
                manhattanGrid.SizeX = x;
                manhattanGrid.SizeY = y;
                manhattanGrid.Grid = new string[x, y];
                return this;
            }

            public MGSizeBuilder HasCentralPortAt(int x, int y)
            {
                manhattanGrid.CentralPort = new Tuple<int, int>(x, y);
                return this;
            }
        }

        public class MGWireBuilder : MGBuilder
        {
            public MGWireBuilder(ManhattanGrid manhattanGrid)
            {
                this.manhattanGrid = manhattanGrid;
            }

            public MGWireBuilder HasWires(Wire wire1, Wire wire2)
            {
                manhattanGrid.Wire1 = wire1;
                manhattanGrid.Wire2 = wire2;
                return this;
            }
            public MGWireBuilder HasStartingPositionAt(int x, int y)
            {
                manhattanGrid.StartingPoint = new Tuple<int, int>(x, y);
                return this;
            }

            public MGWireBuilder HaveIntersections()
            {
                manhattanGrid.Intersections = new List<Tuple<int, int>>();
                return this;
            }
            public MGWireBuilder HasWire1Directions(string wireDirection)
            {
                string[] wire1 = wireDirection.Split(',');

                List<string> Wires1 = new List<string>();

                for (int i = 0; i < wire1.Length; i++)
                {
                    Wires1.Add(wire1[i]);
                }
                foreach (var wire in Wires1)
                {
                    manhattanGrid.CreateWire(wire, manhattanGrid.Wire1);
                }

                return this;
            }
            public MGWireBuilder HasWire2Directions(string wireDirection)
            {
                string[] wire2 = wireDirection.Split(',');

                List<string> Wires2 = new List<string>();

                for (int i = 0; i < wire2.Length; i++)
                {
                    Wires2.Add(wire2[i]);
                }
                foreach (var wire in Wires2)
                {
                    manhattanGrid.CreateWire(wire, manhattanGrid.Wire2);
                }

                return this;
            }
        }

        static void Main(string[] args)
        {
            var mgBuilder = new MGBuilder();
            ManhattanGrid manhattanGrid = mgBuilder
                .Size
                  .HasSize(1000, 1000)
                  .HasCentralPortAt(250, 250)
                .Wires
                  .HasWires(new Wire(), new Wire())
                  .HaveIntersections()
                  .HasStartingPositionAt(250, 250)
                  .HasWire1Directions("R98,U47,R26,D63,R33,U87,L62,D20,R33,U53,R51")
                  .HasStartingPositionAt(250, 250)
                  .HasWire2Directions("U98,R91,D20,R16,D67,R40,U7,R15,U6,R7");

            // testing part 1
            int shortest = 500;
            foreach (var intersection in manhattanGrid.Intersections)
            {
                int current = manhattanGrid.CalculateManhattanDistance(manhattanGrid.GetCentralPort().Item1, intersection.Item1, manhattanGrid.GetCentralPort().Item2, intersection.Item2);

                if (shortest > current)
                {
                    shortest = current;
                }
            }

            Console.WriteLine("Shortest one:");
            Console.WriteLine(shortest);

            // testing part 2
            int fewestSteps = 1000;
            foreach (var intersection in manhattanGrid.Intersections)
            {
                int current = manhattanGrid.CalculateWireDistance(manhattanGrid.Wire1, intersection) + manhattanGrid.CalculateWireDistance(manhattanGrid.Wire2, intersection);

                if (fewestSteps > current)
                {
                    fewestSteps = current;
                }
            }

            Console.WriteLine("Fewest steps:");
            Console.WriteLine(fewestSteps);
        }
    }
}
