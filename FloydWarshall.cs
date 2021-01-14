# TTCS_THUAT_TOAN_TIM_DUONG_DI_NGAN_NHAT
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace BellmanFord
{
    class Floyd:Graph
    {
        public Floyd(string p = " ") : base(p)
        {
        }
        public void floyd()
        {
            Dictionary<string, int> distance = new Dictionary<string, int>();
            Dictionary<string, string> predecessor = new Dictionary<string, string>();
            int inf = DsCanh.Max(p => p.cost) + 100;
            foreach (string v in DsDinh)
            {

                distance.Add(v, inf);
                predecessor.Add(v, null);
             
            }
            distance[Nguon] = 0;
            for (int i = 0; i < DsDinh.Count - 1; i++)
            {
                foreach (Canh c in DsCanh)
                {
                    if (distance[c.u] + c.cost <distance[c.v])
                    {
                        distance[c.v] = distance[c.u] + c.cost;                      
                    }
                }
            }
            Console.WriteLine("--Khoang cach tu nguon den cac dinh: ");
            foreach (var item in distance)
            {
                Console.WriteLine(item.Key + " " + item.Value);
            }

        }

    }
}

