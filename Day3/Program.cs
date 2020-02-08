﻿using System;
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

            private Wire Wire1 { get; set; }
            private Wire Wire2 { get; set; }

            private int SizeX { get; set; }
            private int SizeY { get; set; }
            private string[,] Grid { get; set; }

            private Tuple<int, int> StartingPoint { get; set; }
            public Tuple<int, int> CentralPort { get; set; }
            public List<Tuple<int, int>> Intersections { get; set; }

            public void SetStartingPosition(int x, int y)
            {
                CentralPort = new Tuple<int, int>(x, y);
                StartingPoint = new Tuple<int, int>(x, y);
            }

            // todo: add builder design pattern here
            public ManhattanGrid(int sizeX, int sizeY, Wire wire1, Wire wire2)
            {
                SizeX = sizeX;
                SizeY = sizeY;
                Wire1 = wire1;
                Wire2 = wire2;
                Grid = new string[SizeX, SizeY];
                Intersections = new List<Tuple<int, int>>();
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
        }


        static void Main(string[] args)
        {
            List<string> Wires1 = new List<string>();
            List<string> Wires2 = new List<string>();

            //ManhattanGrid manhattanGrid = new ManhattanGrid(60000, 60000);
            Wire wireMG1 = new Wire();
            Wire wireMG2 = new Wire();
            ManhattanGrid manhattanGrid = new ManhattanGrid(1000, 1000, wireMG1, wireMG2);

            //string wire1Dataset = "R1009,U286,L371,U985,R372,D887,R311,U609,L180,D986,L901,D592,R298,U955,R681,D68,R453,U654,L898,U498,R365,D863,L974,U333,L267,D230,R706,D67,L814,D280,R931,D539,R217,U384,L314,D162,L280,U484,L915,D512,L974,D220,R292,U465,L976,U837,R28,U68,L98,D177,L780,U732,R696,D412,L715,U993,L617,U999,R304,D277,R889,D604,R199,U498,R302,U958,R443,U957,R453,U362,R704,U301,R813,U404,L150,D673,L407,D233,L901,D965,R602,U615,R496,U467,R849,U530,L205,D43,R709,U127,L35,U801,L565,D890,R90,D763,R95,D542,R84,D421,L298,D58,R794,U722,R205,U830,L149,D759,L950,D708,L727,U401,L187,D598,L390,D469,R375,U985,L723,U63,L983,D39,L160,U276,R822,D504,L298,D484,L425,U228,L984,D623,L936,U624,L851,D748,L266,D576,L898,U783,L374,D276,R757,U89,L649,U73,L447,D11,L539,U291,L507,U208,R167,D874,L596,D235,R334,U328,R41,D212,L544,D72,L972,D790,L282,U662,R452,U892,L830,D86,L252,U701,L215,U179,L480,U963,L897,U489,R223,U757,R804,U373,R844,D518,R145,U304,L24,D988,R605,D644,R415,U34,L889,D827,R854,U836,R837,D334,L664,D883,L900,U448,R152,U473,R243,D147,L711,U642,R757,U272,R192,U741,L522,U785,L872,D128,L161,D347,L967,D295,R831,U535,R329,D752,R720,D806,R897,D320,R391,D737,L719,U652,L54,D271,L855,D112,R382,U959,R909,D687,L699,U892,L96,D537,L365,D182,R886,U566,R929,U532,L255,U823,R833,U542,R234,D339,R409,U100,L466,U572,L162,U843,L635,D153,L704,D317,L534,U205,R611,D672,L462,D506,L243,U509,L819,D787,R448,D353,R162,U108,R850,D919,R259,U877,R50,D733,L875,U106,L890,D275,L904,U849,L855,U314,L291,U170,L627,U608,R783,U404,R294";
            string wire1Dataset = "R98,U47,R26,D63,R33,U87,L62,D20,R33,U53,R51";
            //string wire2Dataset = "L1010,D347,R554,U465,L30,D816,R891,D778,R184,U253,R694,U346,L743,D298,L956,U703,R528,D16,L404,D818,L640,D50,R534,D99,L555,U974,L779,D774,L690,U19,R973,D588,L631,U35,L410,D332,L74,D858,R213,U889,R977,U803,L624,U627,R601,U499,L213,U692,L234,U401,L894,U733,R414,D431,R712,D284,R965,D624,R848,D17,R86,D285,R502,U516,L709,U343,L558,D615,L150,D590,R113,D887,R469,U584,L434,D9,L994,D704,R740,D541,R95,U219,L634,D184,R714,U81,L426,D437,R927,U232,L361,D756,R685,D206,R116,U844,R807,U811,L382,D338,L660,D997,L551,D294,L895,D208,R37,D90,R44,D131,R77,U883,R449,D24,R441,U659,R826,U259,R98,D548,R118,D470,L259,U170,R518,U731,L287,U191,L45,D672,L691,U117,R156,U308,R230,U112,L938,U644,R911,U110,L1,U162,R943,U433,R98,U610,R428,U231,R35,U590,R554,U612,R191,U261,R793,U3,R507,U632,L571,D535,R30,U281,L613,U199,R168,D948,R486,U913,R534,U131,R974,U399,L525,D174,L595,D567,L394,D969,L779,U346,L969,D943,L845,D727,R128,U241,L616,U117,R791,D419,L913,D949,R628,D738,R776,D294,L175,D708,R568,U484,R589,D930,L416,D114,L823,U16,R260,U450,R534,D94,R695,D982,R186,D422,L789,D886,L761,U30,R182,U930,L483,U863,L318,U343,L380,U650,R542,U92,L339,D390,L55,U343,L641,D556,R616,U936,R118,D997,R936,D979,L594,U326,L975,U52,L89,U679,L91,D969,R878,D798,R193,D858,R95,D989,R389,U960,R106,D564,R48,D151,L121,D241,L369,D476,L24,D229,R601,U849,L632,U894,R27,U200,L698,U788,L330,D73,R405,D526,L154,U942,L504,D579,L815,D643,L81,U172,R879,U28,R715,U367,L366,D964,R16,D415,L501,D176,R641,U523,L979,D556,R831";
            string wire2Dataset = "U98,R91,D20,R16,D67,R40,U7,R15,U6,R7";

            string[] wire1 = wire1Dataset.Split(',');
            string[] wire2 = wire2Dataset.Split(',');

            for (int i = 0; i < wire1.Length; i++)
            {
                Wires1.Add(wire1[i]);
            }

            for (int i = 0; i < wire2.Length; i++)
            {
                Wires2.Add(wire2[i]);
            }

            //manhattanGrid.SetStartingPosition(30000, 30000);
            manhattanGrid.SetStartingPosition(250, 250);

            foreach (var wire in Wires1)
            {
                manhattanGrid.CreateWire(wire, wireMG1);
            }

            //manhattanGrid.SetStartingPosition(30000, 30000);
            manhattanGrid.SetStartingPosition(250, 250);
            foreach (var wire in Wires2)
            {
                manhattanGrid.CreateWire(wire, wireMG2);
            }

            //testing part 1
            int shortest = 500;
            foreach (var intersection in manhattanGrid.Intersections)
            {
                int current = manhattanGrid.CalculateManhattanDistance(manhattanGrid.CentralPort.Item1, intersection.Item1, manhattanGrid.CentralPort.Item2, intersection.Item2);

                if (shortest > current)
                {
                    shortest = current;
                }
            }
            Console.WriteLine("This is the shortest one:");
            Console.WriteLine(shortest);

            //testing part 2
            int fewestSteps = 1000;
            foreach (var intersection in manhattanGrid.Intersections)
            {
                int current = manhattanGrid.CalculateWireDistance(wireMG1, intersection) + manhattanGrid.CalculateWireDistance(wireMG2, intersection);

                if (fewestSteps > current)
                {
                    fewestSteps = current;
                }
            }

            Console.WriteLine("This is the fewest steps:");
            Console.WriteLine(fewestSteps);
        }
    }
}
