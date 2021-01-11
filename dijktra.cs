using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace thuattoandijkstra
{
    class Edge
    {
        public string u;
        public string v;
        public int w;
        public Edge(string _u, string _v, string _w)
        {
            u = _u;
            v = _v;
            w = int.Parse(_w);
        }
    }
    class Graph
    {
        List<Edge> edges;
        List<string> vertices;
        public Graph(string path)
        {
            string[] data = System.IO.File.ReadAllLines(path);
            edges = new List<Edge>();
            vertices = new List<string>();
            foreach (var line in data)
            {
                string[] s = line.Split(' ');
                edges.Add(new Edge(s[0], s[1], s[2]));
                edges.Add(new Edge(s[1], s[0], s[2]));
                if (!vertices.Contains(s[0]))
                    vertices.Add(s[0]);
                if (!vertices.Contains(s[1]))
                    vertices.Add(s[1]);
            }
        }
        public void Dijktra()
        {
            Console.Write("nhap dinh bat dau :");
            string source = Console.ReadLine();
            List<string> Q = new List<string>();
            Dictionary<string, int> dist = new Dictionary<string, int>();
            Dictionary<string, string> prev = new Dictionary<string, string>();
            int INFINITY = edges.Max(p => p.w);
            foreach (var v in vertices)
            {
                dist.Add(v, INFINITY);
                prev.Add(v, null);
                Q.Add(v);
            }
            dist[source] = 0;
            while (Q.Count != 0)
            {
                var t = dist.Where(p => Q.Contains(p.Key));
                int min = t.Min(p => p.Value);
                string u = t.Where(p => p.Value == min).Select(p => p.Key).First();
                Q.Remove(u);
                List<Edge> dsCanhKeU = edges.Where(p => p.u == u && Q.Contains(p.v)).ToList();
                foreach (Edge canhkeu in dsCanhKeU)
                {
                    int alt = dist[u] + canhkeu.w;
                    if (alt < dist[canhkeu.v])
                    {
                        dist[canhkeu.v] = alt;
                        prev[canhkeu.v] = u;
                    }
                }
            }
            Console.WriteLine("ket qua tim duong di ngan nhat tu dinh " + source + " : ");
            foreach (var item in dist)
            {
                Console.WriteLine(item.Key + "\t" + item.Value);
            }
        }
    }
}