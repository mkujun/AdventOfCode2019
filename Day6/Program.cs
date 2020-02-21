using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Unicode;

namespace Day6
{
    class Program
    {
        public class Interstellar
        {
            public Dictionary<string, List<string>> Planets;

            public Interstellar()
            {
                Planets = new Dictionary<string, List<string>>();
            }

            public void AddOrbitToPlanets(string planet, string orbit)
            {
                foreach (var item in Planets)
                {
                    if (item.Value.Contains(planet))
                    {
                        item.Value.Add(orbit);
                    }
                }
            }
            
            public void Add(string planet, string orbit)
            {
                List<string> orbits = new List<string>();

                // planet exists, but orbit does not
                if( Planets.ContainsKey(planet))
                {
                    Planets[planet].Add(orbit);
                    if (!Planets.ContainsKey(orbit))
                        Planets.Add(orbit, new List<string>());

                    AddOrbitToPlanets(planet, orbit);
                }

                // no planet
                if (!Planets.ContainsKey(planet))
                {
                    // no planet and no orbit
                    if (!Planets.ContainsKey(orbit) && !Planets.ContainsKey(planet))
                    {
                        Planets.Add(planet, new List<string>());
                        Planets[planet].Add(orbit);
                        Planets.Add(orbit, new List<string>());
                    }
                    // no planet but orbit
                    else
                    {
                        Planets.Add(planet, new List<string>());
                        orbits = Planets[orbit];
                        Planets[planet].Add(orbit);
                        Planets[planet].AddRange(orbits);
                    }
                }

            }

        }

        static void Main(string[] args)
        {
            Interstellar interstellar = new Interstellar();
            string line;
            System.IO.StreamReader file = new System.IO.StreamReader(@"C:\Users\markok\source\repos\Day6\input.txt");

            string log = @"C:\Users\markok\source\repos\AdventOfCode2019\Day6\log.txt";

            while((line = file.ReadLine()) != null)
            {
                string planet = line.Split(')')[0];
                string orbit = line.Split(')')[1];

                interstellar.Add(planet, orbit);

                using (StreamWriter sw = File.AppendText(log))
                {
                    sw.WriteLine("===========================");
                    sw.WriteLine("New planet : {0}", planet);
                    sw.WriteLine("New orbit : {0}", orbit);
                    sw.WriteLine("===========================");

                    StringBuilder stringBuilder = new StringBuilder();

                    foreach (var item in interstellar.Planets)
                    {
                        sw.WriteLine("Planet : {0}", planet);

                        foreach (var value in item.Value)
                        {
                            stringBuilder.Append(value);
                        }
                        sw.WriteLine("Orbits: {0}", stringBuilder);
                    }
                }
                
            }

            int count = 0;
            foreach (var stellar in interstellar.Planets)
            {
                count = count + (stellar.Value.Count);
            }

            Console.WriteLine("Hello World!");
        }
    }
}
