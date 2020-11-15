using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Unicode;

namespace Day6
{
    class Program
    {
        public class PlanetNode
        {
            public string Name;
            public List<string> Orbits;

            public PlanetNode(string planet, string orbit)
            {
                Name = planet;
                Orbits = new List<string>();
                Orbits.Add(orbit);
            }
        }

        public class Map
        {
            public List<PlanetNode> PlanetNodes;
            public string[] inputData;

            public Map(string[] input)
            {
                PlanetNodes = new List<PlanetNode>();
                inputData = input;
            }

            public void AddToMap(string planet, string orbit)
            {
                bool isPlanetInList = false;

                // mapa je prazna
                if (PlanetNodes.Count == 0)
                {
                    PlanetNode planetNode = new PlanetNode(planet, orbit);
                    PlanetNodes.Add(planetNode);
                }
                // prodji kroz listu
                // gledaj svaki element ima li medju orbitama planet
                // ako ima, dodaj orbitu
                else
                {
                    for (int i = 0; i < PlanetNodes.Count; i++)
                    {
                        //
                        if (PlanetNodes[i].Orbits.Contains(planet))
                        {
                            PlanetNodes[i].Orbits.Add(orbit);
                        }

                        if (PlanetNodes[i].Name == planet)
                        {
                            isPlanetInList = true;
                            PlanetNodes[i].Orbits.Add(orbit);
                        }
                    }

                    // ako planet nije u listi dodaj ga
                    if(!isPlanetInList)
                    {
                        PlanetNode planetNode = new PlanetNode(planet, orbit);
                        PlanetNodes.Add(planetNode);
                    }

                }

            }

            internal void FindCOM()
            {
                for (int i = 0; i < inputData.Length; i++)
                {
                    string planet = inputData[i].Split(")")[0];
                    string orbit = inputData[i].Split(")")[1];

                    if (planet == "COM")
                    {
                        AddToMap(planet, orbit);
                        FindNext(orbit);
                    }
                }
            }

            internal void FindNext(string planetToFind)
            {
                for (int i = 0; i < inputData.Length; i++)
                {
                    string planet = inputData[i].Split(")")[0];
                    string orbit = inputData[i].Split(")")[1];

                    if(planet == planetToFind)
                    {
                        AddToMap(planet, orbit);
                        FindNext(orbit);
                    } 
                }
            }
        }
        
        static void Main(string[] args)
        {
            //string[] input = File.ReadAllLines(@"C:\Users\Korisnik\AdventOfCode2019\Day6\input.txt");
            string[] input = File.ReadAllLines(@"C:\Users\Korisnik\AdventOfCode2019\Day6\taskInput.txt");
            Map mapa = new Map(input);
            mapa.FindCOM();

            int count = 0;
            foreach (var item in mapa.PlanetNodes)
            {
                count = count + item.Orbits.Count;
            }

            Console.WriteLine("test");
        }
    }
}
