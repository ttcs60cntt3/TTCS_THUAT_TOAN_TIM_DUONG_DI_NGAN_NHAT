using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace thuattoandijkstra
{
    class Program
    {

        static void Main(string[] args)
        {

            Console.WriteLine("Dijktra");
            Graph g = new Graph("D:\\graph.txt");
            g.Dijktra();
            Console.ReadKey();
        }
    }
}
