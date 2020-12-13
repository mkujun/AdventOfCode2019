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

                if (PlanetNodes.Count == 0)
                {
                    PlanetNode planetNode = new PlanetNode(planet, orbit);
                    PlanetNodes.Add(planetNode);
                }

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

                    if(!isPlanetInList)
                    {
                        PlanetNode planetNode = new PlanetNode(planet, orbit);
                        PlanetNodes.Add(planetNode);
                    }

                }

            }

        }
        
        public class Vertex
        {
            public string Name;
            public List<string> Edges;

            public Vertex(string name)
            {
                Name = name;
                Edges = new List<string>();
            }

            public void AddEdge(string edge)
            {
                Edges.Add(edge);
            }
        }

        public class Graph
        {
            internal void FindCOM(string[] inputData)
            {
                for (int i = 0; i < inputData.Length; i++)
                {
                    string planet = inputData[i].Split(")")[0];
                    string orbit = inputData[i].Split(")")[1];

                    if (planet == "COM")
                    {
                        //AddToMap(planet, orbit);
                        AddVertex(planet, orbit);
                        FindNext(orbit, inputData);
                    }
                }
            }

            internal void FindNext(string planetToFind, string[] inputData)
            {
                Console.WriteLine("Planet" + planetToFind);

                for (int i = 0; i < inputData.Length; i++)
                {
                    string planet = inputData[i].Split(")")[0];
                    string orbit = inputData[i].Split(")")[1];

                    if(planet == planetToFind)
                    {
                        AddVertex(planet, orbit);
                        FindNext(orbit, inputData);
                    } 
                }
            }
            
            public List<Vertex> Vertices;

            public List<Vertex> YouParents;
            public List<Vertex> SanParents;
            public List<Vertex> Ancestors;
            public List<string> DfsVisited;

            public int youCounter { get; private set; }
            public int sanCounter { get; private set; }

            public bool sanFound { get; private set; }
            public bool youFound { get; private set; }

            public Graph()
            {
                Vertices = new List<Vertex>();
                YouParents = new List<Vertex>();
                SanParents = new List<Vertex>();
                Ancestors = new List<Vertex>();
                DfsVisited = new List<string>();
            }

            public void FindYouParents(string child)
            {
                if (child == "COM")
                {
                    return;
                }
                else
                {
                    foreach (Vertex item in Vertices)
                    {
                        if (item.Edges.Contains(child))
                        {
                            YouParents.Add(item);
                            FindYouParents(item.Name);
                        }
                    }

                }
            }
            public void FindSanParents(string child)
            {
                if (child == "COM")
                {
                    return;
                }
                else
                {
                    foreach (Vertex item in Vertices)
                    {
                        if (item.Edges.Contains(child))
                        {
                            SanParents.Add(item);
                            FindSanParents(item.Name);
                        }
                    }

                }
            }

            public void FindAncestors()
            {
                var names = YouParents.Select(x => x.Name).Intersect(SanParents.Select(x => x.Name));
                foreach (var item in names)
                {
                    Ancestors.Add(new Vertex(item));
                }

                foreach (var item in Ancestors)
                {
                    Vertex pero = Vertices.Find(a => a.Name == item.Name);

                    BFS(pero);
                    int ivo = youCounter + sanCounter;
                    Console.WriteLine("rez:" + ivo);
                }
            }

            public void BFS(Vertex v)
            {
                Vertex YOU = new Vertex("YOU");
                Vertex SAN = new Vertex("SAN");

                List<Vertex> vertices = new List<Vertex>();
                List<Vertex> temp = new List<Vertex>();

                foreach (var item in v.Edges)
                {
                    Vertex vertex = Vertices.Find(v => v.Name == item);
                    vertices.Add(vertex);
                }

                do
                {
                    if (youFound == false)
                    {
                        if (vertices.Find(v => v.Name == "YOU") == null)
                        {
                            youCounter++;
                        }
                        else
                        {
                            youFound = true;
                        }
                    }
                    if (sanFound == false)
                    {
                        if (vertices.Find(v => v.Name == "SAN") == null)
                        {
                            sanCounter++;
                        }
                        else
                        {
                            sanFound = true;
                        }
                    }
                    

                    foreach (var item in vertices)
                    {
                        foreach (var edge in item.Edges)
                        {
                            Vertex vertex = Vertices.Find(v => v.Name == edge);

                            temp.Add(vertex);
                        }
                    }

                    vertices.Clear();
                    foreach (var item in temp)
                    {
                        vertices.Add(item);
                    }
                    temp.Clear();

                } while (!sanFound || !youFound);

            }

            public void AddVertex(string planet, string orbit)
            {
                if (Vertices.Count == 0)
                {
                    Vertex vertex = new Vertex(planet);
                    vertex.Edges.Add(orbit);
                    Vertices.Add(vertex);

                    Vertex vertex1 = new Vertex(orbit);
                    Vertices.Add(vertex1);
                }
                else
                {
                    Vertex vertex = Vertices.Find(v => v.Name == planet);

                    if (vertex != null)
                    {
                        vertex.AddEdge(orbit);
                    }

                    else
                    {
                        Vertex vertex1 = new Vertex(planet);
                        vertex1.AddEdge(orbit);
                        Vertices.Add(vertex1);
                    }

                    Vertex vertexOrbit = Vertices.Find(v => v.Name == orbit);
                    if (vertexOrbit == null)
                    {
                        Vertex vertex1 = new Vertex(orbit);
                        Vertices.Add(vertex1);
                    }

                }
            }
        }


        static void Main(string[] args)
        {
            //string[] input = File.ReadAllLines(@"C:\Users\Korisnik\AdventOfCode2019\Day6\input.txt");
            string[] input = File.ReadAllLines(@"C:\Users\Korisnik\AdventOfCode2019\Day6\taskInput.txt");
            //Map mapa = new Map(input);
            //mapa.FindCOM();

            /*
            int count = 0;
            foreach (var item in mapa.PlanetNodes)
            {
                count = count + item.Orbits.Count;
            }
            */

            Graph graph = new Graph();
            for (int i = 0; i < input.Length; i++)
            {
                string planet = input[i].Split(")")[0];
                string orbit = input[i].Split(")")[1];

                graph.AddVertex(planet, orbit);
            }
            //graph.FindCOM(input);

            graph.FindYouParents("YOU");
            graph.FindSanParents("SAN");
            graph.FindAncestors();
            Console.WriteLine("test");
        }
    }
}
