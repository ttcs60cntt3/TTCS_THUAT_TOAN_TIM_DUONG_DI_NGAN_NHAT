using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace BellmanFord
{
    class  Program
    {
        static void Main(string[] args)
        {
            /*Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormDuongDiNganNhat());*/
            string k;
            do
            {
                Console.WriteLine("==========================Thuat toan tim duong di ngan nhat==========================");
                Console.WriteLine("1. Thuat toan Dijkstra");
                Console.WriteLine("2. Thuat toan Bellman Ford");
                Console.WriteLine("3. Thuat toan Floy Warshall");

                int choice;
                do
                {
                    Console.WriteLine("Vui long lua chon giai thuat: ");
                    choice = int.Parse(Console.ReadLine());
                } while (choice < 1 && choice > 2);
                switch (choice)
                {
                    case 1:
                        {
                            Stopwatch stopWatch = new Stopwatch();
                            stopWatch.Start();
                            Console.WriteLine("=========Giai thuat Dijkstra=========");
                            Dijkstra dt = new Dijkstra();
                            dt.Print();                           
                            dt.Dijkstras();
                            // Format and display the TimeSpan value.
                            stopWatch.Stop();
                            TimeSpan ts = stopWatch.Elapsed;
                            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                                ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
                            Console.WriteLine("RunTime " + elapsedTime);
                            break;
                        }
                    case 2:
                        {
                            Stopwatch stopWatch = new Stopwatch();
                            stopWatch.Start();
                            Console.WriteLine("=========Giai thuat Bellman Ford=========");                                             
                            Bellman dt = new Bellman();
                            dt.Print();
                            dt.BellmanFord();
                            stopWatch.Stop();
                            TimeSpan ts = stopWatch.Elapsed;
                            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                            ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
                            Console.WriteLine("RunTime " + elapsedTime);
                            break;
                        }
                    case 3:
                        {
                            Stopwatch stopWatch = new Stopwatch();
                            stopWatch.Start();
                            Console.WriteLine("=========Giai thuat Bellman Ford=========");
                            Floyd dt = new Floyd();
                            dt.Print();
                            dt.floyd();
                            stopWatch.Stop();
                            TimeSpan ts = stopWatch.Elapsed;
                            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                            ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
                            Console.WriteLine("RunTime " + elapsedTime);
                            break;
                        }
                }
                Console.WriteLine("Nhap phim bat ki de quay lai menu chinh || Nhap 0 de ket thuc chuong trinh");
                k = Console.ReadLine();
            }while(k!="0");
            Console.ReadKey();            
        }
    }
    class Canh
    {
        public string u;
        public string v;
        public int cost;
        public Canh(string _u,string _v,string _cost)
        {
            u = _u;
            v = _v;
            cost = int.Parse(_cost);
        }
    }
    class Graph
    {
        List<string> dsDinh;
        private List<Canh> dsCanh;
        private string nguon;
        string path;

        public List<string> DsDinh { get => dsDinh; set => dsDinh = value; }
        public List<Canh> DsCanh { get => dsCanh; set => dsCanh = value; }
        public string Nguon { get => nguon; set => nguon = value; }

        public Graph()
        {
            dsCanh = new List<Canh>();
            dsDinh = new List<string>();
            Console.WriteLine("Vui long chon kieu nhap du lieu:");
            Console.WriteLine("1. Nhap tu ban phim");
            Console.WriteLine("2. Doc du lieu tu file");
            int choice;
            do
            {
                Console.WriteLine("Nhap lua chon cua ban");
                choice = int.Parse(Console.ReadLine());
            } while (choice != 1 && choice != 2);
            switch(choice)
            {
                case 1:
                    {
                        int n;
                        do
                        {
                            Console.WriteLine("Nhap so luong canh: ");
                            n = int.Parse(Console.ReadLine());
                            if(n<=0)
                            {
                                Console.WriteLine("So luong canh phai lon hon 0\a\a\a");
                            }
                        } while (n <= 0);

                        for(int i = 0; i < n; i++)
                        {
                            Console.WriteLine("Nhap canh thu {0} ", i + 1);
                            Console.Write("Nhap dinh s[{0}] : ", 0);
                            string s0 = Console.ReadLine();
                            Console.Write("Nhap dinh s[{0}] : ", 1);
                            string s1 = Console.ReadLine();
                            Console.Write("Nhap trong so s[{0}] : ", 2);
                            string s2 = Console.ReadLine();
                            dsCanh.Add(new Canh(s0, s1, s2));
                            if (!dsDinh.Contains(s0)) // .Contains : nội dung đã chứa
                                dsDinh.Add(s0);
                            if (!dsDinh.Contains(s1))
                                dsDinh.Add(s1);
                        }
                        Console.WriteLine("Danh sach cac dinh: ");
                        for (int i = 0; i < dsDinh.Count; i++) // dsDinh.Count : Tong dinh
                        {
                            Console.Write(dsDinh[i] + " ");
                        }
                        Console.Write("Chon dinh bat dau : ");
                        nguon = Console.ReadLine();
                        break;
                    }
                case 2:
                    {
                        try
                        {
                            Console.Write("Nhap dia chi file: ");
                            string p = Console.ReadLine();
                            path = p;
                            string[] data = System.IO.File.ReadAllLines(path);
                            for (int i = 0; i < data.Length; i++)
                            {
                                string[] s = data[i].Split(' ');
                                dsCanh.Add(new Canh(s[0], s[1], s[2]));
                                if (!dsDinh.Contains(s[0])) // .Contains : nội dung đã chứa
                                    dsDinh.Add(s[0]);
                                if (!dsDinh.Contains(s[1]))
                                    dsDinh.Add(s[1]);
                            }
                            Console.WriteLine("Danh sach cac dinh: ");
                            for (int i = 0; i < dsDinh.Count; i++) // dsDinh.Count : Tong dinh
                            {
                                Console.Write(dsDinh[i] + " ");
                            }
                            Console.Write("Chon dinh bat dau : ");
                            nguon = Console.ReadLine();
                        }
                        catch
                        {
                            Console.WriteLine("Khong mo duoc file!\a\a");
                        }
                            break;
                    }                   
            }
        }
        public String Print()
        {
            return "Dinh nguon la: " + nguon;
        }
    }
    class Dijkstra : Graph
    {
        public Dijkstra() : base()
        {
        }
        public void Dijkstras()
        {
            List<string> q = new List<string>();
            Dictionary<string, int> distance = new Dictionary<string, int>();
            Dictionary<string, string> predecessor = new Dictionary<string, string>();
            int inf = DsCanh.Max(p => p.cost) + 100;
            foreach (string v in DsDinh)
            {
                distance.Add(v, inf);
                predecessor.Add(v, null);
                q.Add(v);
            }
            distance[Nguon] = 0;
            while (q.Count != 0)
            {
                var t = distance.Where(p => q.Contains(p.Key));
                int min = t.Min(p => p.Value);
                string u = t.Where(p => p.Value == min).Select(p => p.Key).First();
                q.Remove(u);
                List<Canh> dsCanhKeU = DsCanh.Where(p => p.u == u && q.Contains(p.v)).ToList();
                foreach (Canh canhkeu in dsCanhKeU)
                {
                    int alt = distance[u] + canhkeu.cost;
                    if (alt < distance[canhkeu.v])
                    {
                        distance[canhkeu.v] = alt;
                        predecessor[canhkeu.v] = u;
                    }
                }
            }
            Console.WriteLine("ket qua tim duong di ngan nhat tu dinh " + Nguon + " : ");
            foreach (var item in distance)
            {
                Console.WriteLine(item.Key + "\t" + item.Value);
            }
        }
    }
    class Bellman : Graph
    {
        public Bellman() : base()
        {
        }
        public void BellmanFord()
        { 
            //distance[], predecessor[]
            Dictionary<string, int> distance = new Dictionary<string,int>();          //Khởi tạo biến khoảng cách, và trọng số ban đầu
            Dictionary<string, string> predecessor = new Dictionary<string,string>(); // Khởi tạo cặng đỉnh trong cùng một cạnh là predecessor : tiền tố
            /*
             Bước 1: Khởi tạo đồ thị
                    Cho mỗi đỉnh trong danh sách các đỉnh
                    distance[v] = inf           // Tại bước đầu tiên, các đỉnh có độ dài là vô cùng
                    predecessor[v] = inf        // Và không có bất kì đỉnh tiền nhiệm nào
             */
            //Xác định inf (vô cùng)
            int inf = DsCanh.Max(p => p.cost) + 100; // Tạo biến vô cùng = giá trị trọng số lớn nhất + 100
            foreach(string v in DsDinh)
            {
                distance.Add(v, inf);
                predecessor.Add(v, null);
            }
            // distance[source] = 0                   // Diem xuat luon co do lon bang 0
            distance[Nguon] = 0;
            /* bước 2: kết nạp cạnh
                for i from 1 to size(danh_sách_đỉnh):       
                for each(u, v) in danh_sách_cung:
                if khoảng_cách(v) > khoảng_cách(u) + trọng_số(u, v):
                khoảng_cách(v):= khoảng_cách(u) + trọng_số(u, v)
                đỉnh_liền_trước(v):= u*/
            for(int i = 0; i < DsDinh.Count -1; i++)
            {
                foreach(Canh c in DsCanh)
                {
                    if(distance[c.u]+c.cost < distance[c.v])
                    {
                        distance[c.v] = distance[c.u] + c.cost;
                        predecessor[c.v] = c.u;
                    }
                }
            }
            // bước 3: kiểm tra chu trình âm
            /*for each(u, v) in danh_sách_cung:
            if khoảng_cách(v) > khoảng_cách(u) + trọng_số(u, v):
            error "Đồ thị chứa chu trình âm"*/
            foreach(Canh c in DsCanh)
            {
                if(distance[c.u] + c.cost < distance[c.v])
                {
                    Console.WriteLine("Do thi co chua chu trinh am");
                }
            }
            // Trả vể độ lớn và các đỉnh nguồn
            //in kết quả
            Console.WriteLine("Duong di ngan nhat tu dinh " + Nguon + " la: ");
            /*Console.WriteLine("Duong di ngan nhat tu nguon den cac dinh la: ");
            foreach (Canh c in DsCanh)
            {
                Console.WriteLine(distance[c.u] + " -> " + distance[c.v]);
            }*/
            Console.WriteLine("--Khoang cach tu nguon den cac dinh: ");
            foreach(var item in distance)
            {
                Console.WriteLine(item.Key + " " + item.Value);
            }
            Console.WriteLine("--Duong di giua cac dinh: ");
            Console.WriteLine("Dinh | Tien nhiem");
            foreach(var item in predecessor)
            {
                Console.WriteLine(item.Key + "\t" + item.Value);       
            }
        }
    }
    class Floyd : Graph
    {
        public Floyd() : base()
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
                    if (distance[c.u] + c.cost < distance[c.v])
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
