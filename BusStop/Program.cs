using System;
using System.Collections.Generic;
using System.Linq;

namespace BusStop
{
    class Program
    {
        static void Main(string[] args)
        {
            // int[][] r = new int[][] { new int[] { 1, 2, 7 }, new int[] { 3, 6, 7 } };
            int[][] r = new int[][] { new int[] { 7, 12 }, new int[] { 4, 13, 15 }, new[] { 6 }, new[] { 15, 19 }, new[] { 9, 12, 13 } };

            var b = GetBusCountBetweenStops(r, 7, 200);

            Console.WriteLine(b);
        }

        static List<Node> GenerateNodes(int[][] stops)
        {
            var nodes = new List<Node>();

            var distinctStops = stops.SelectMany(x => x).Distinct().ToArray();

            foreach (var stop in distinctStops)
            {
                //getting all the arrau where the current stop is
                var pStops = stops
                    .Where(s => s.Contains(stop))
                    .SelectMany(s => s.Where(x => x != stop))
                    .Distinct()
                    .ToArray();

                // a new nodelist for connected nodes
                var conNodes = new List<Node>();

                var n = pStops.Select(n => new Node
                {
                    Id = n,
                    Distance = 1
                });

                conNodes.AddRange(n);

                //Generating the current node
                var newNode = new Node
                {
                    Id = stop,
                    IsChecked = false,
                    Distance = 1,
                    ConnectedNodes = conNodes
                };

                //adding the current node to the nodelist
                nodes.Add(newNode);
            }

            return nodes;
        }

        static List<Path> GetShortestPaths(int startNode, List<Node> nodes)
        {
            var paths = nodes.Select(node => new Path() { Node = node.Id, ShortestDistance = int.MaxValue }).ToList();

            var p = paths.Find(p => p.Node == startNode);

            if (p == null)
                return new List<Path>();

            p.ShortestDistance = 0;

            while (nodes.Exists(n => n.IsChecked == false))
            {
                var uncheckedNodes = nodes.FindAll(n => n.IsChecked == false);

                var nextPath = paths.OrderBy(p => p.ShortestDistance)
                    .First(p => uncheckedNodes.Contains(nodes.Find(n => n.Id == p.Node)));

                var nextNode = nodes.Find(n => n.Id == nextPath.Node);

                foreach (var node in nextNode.ConnectedNodes)
                {
                    // var currentNode = GetByName(node.Name);
                    var currentNode = nodes.Find(n => n.Id == node.Id);

                    if (currentNode.IsChecked) continue;

                    var currentPath = paths.Find(p => p.Node == currentNode.Id);
                    var newDistance = node.Distance + nextPath.ShortestDistance;

                    if (!(currentPath?.ShortestDistance > newDistance)) continue;

                    currentPath.ShortestDistance = newDistance;
                    currentPath.PreviousNode = nextPath.Node;
                }

                nextNode.IsChecked = true;
            }

            return paths;
        }

        static int GetBusCountBetweenStops(int[][] route, int source, int target)
        {
            var nodes = GenerateNodes(route);
            var generatedPaths = GetShortestPaths(source, nodes);
            if (generatedPaths.Count == 0) return -1;

            var startPath = generatedPaths.Find(p => p.Node == target);
            
            if(startPath == null) return -1;

            var paths = new List<int>();

            while (startPath.PreviousNode != 0)
            {
                startPath = generatedPaths.Find(p => p.Node.Equals(startPath.PreviousNode));
                paths.Add(startPath.Node);
            }

            if (paths[paths.Count - 1] != source) return -1;

            return paths.Count;
        }
    }


    class Node
    {
        public int Id { get; set; }
        public bool IsChecked { get; set; }
        public List<Node> ConnectedNodes { get; set; }
        public int Distance { get; set; } = 1;
    }

    class Path
    {
        public int Node { get; set; }
        public int ShortestDistance { get; set; }
        public int PreviousNode { get; set; }
    }
}