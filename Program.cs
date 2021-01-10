using System;

namespace TTCS_THUAT_TOAN_TIM_DUONG_DI_NGAN_NHAT
{
    class Program
    {
        static void Main()
        {
            int[,] graph =
            {
             { 0, 1, 1, 0, 0, 0 },
             { 0, 0, 0, 1, 0, 3 },
             { 0, 0, 0, 0, 0, 1 },
             { 0, 2, 0, 0, 1, 0 },
             { 0, 0, 0, 0, 0, 2 },
             { 0, 0, 1, 2, 0, 0 }

            };
           dijktra.Dijkstra(graph, 0, 6);
            Console.ReadKey();
        }
    }
}
