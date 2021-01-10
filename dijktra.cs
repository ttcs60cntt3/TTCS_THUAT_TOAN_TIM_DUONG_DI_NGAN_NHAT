using System;

namespace TTCS_THUAT_TOAN_TIM_DUONG_DI_NGAN_NHAT
{
    class dijktra
    {
        private static int KhoangCachMin(int[] khoangcach, bool[] duongdingannhat, int sodinh)
        {
            int min = int.MaxValue;
            int minIndex = 0;
            for (int v = 0; v < sodinh; ++v)
            {
                if (duongdingannhat[v] == false && khoangcach[v] <= min)
                {
                    min = khoangcach[v];
                    minIndex = v;
                }
            }
            return minIndex;
        }
        private static void Print(int[] khoangcach, int sodinh)
        {
            Console.WriteLine("khoang cach tu dinh nguon den cac dinh con lai:");

            for (int i = 0; i < sodinh; ++i)
                Console.WriteLine("{0}\t  {1}", i, khoangcach[i]);
        }
        public static void Dijkstra(int[,] graph, int dinhnguon, int sodinh)
        {
            int[] khoangcach = new int[sodinh];
            bool[] duongdingannhat = new bool[sodinh];

            for (int i = 0; i < sodinh; ++i)
            {
                khoangcach[i] = int.MaxValue;
                duongdingannhat[i] = false;
            }
            khoangcach[dinhnguon] = 0;

            for (int count = 0; count < sodinh - 1; ++count)
            {
                int u = KhoangCachMin(khoangcach, duongdingannhat, sodinh);
                duongdingannhat[u] = true;

                for (int v = 0; v < sodinh; ++v)
                    if (!duongdingannhat[v] && Convert.ToBoolean(graph[u, v])
                && khoangcach[u] != int.MaxValue && khoangcach[u] + graph[u, v] < khoangcach[v])
                        khoangcach[v] = khoangcach[u] + graph[u, v];
            }
            Print(khoangcach, sodinh);
        }
    }
}