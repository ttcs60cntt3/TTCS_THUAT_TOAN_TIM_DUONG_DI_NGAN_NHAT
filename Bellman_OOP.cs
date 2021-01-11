using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BellmanFord
{
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
        internal List<Canh> DsCanh { get => dsCanh; set => dsCanh = value; }
        public string Nguon { get => nguon; set => nguon = value; }

        public Graph(string p="")
        {
            path = p;
            string[] data = System.IO.File.ReadAllLines(path);
            dsCanh = new List<Canh>();
            dsDinh = new List<string>();
            for (int i = 0; i < data.Length; i++)
            {
                string[] s = data[i].Split(' ');
                dsCanh.Add(new Canh(s[0], s[1], s[2]));
                if (!dsDinh.Contains(s[0]))
                    dsDinh.Add(s[0]);
                if (!dsDinh.Contains(s[1]))
                    dsDinh.Add(s[1]);
            }
            Console.WriteLine("Danh sach cac dinh: ");
            for (int i = 0; i < dsDinh.Count; i++)
            {
                Console.Write(dsDinh[i] + " ");
            }
            Console.Write("Chon dinh bat dau : ");
            nguon = Console.ReadLine();
        }
        public String Print()
        {
            return "Dinh nguon la: " + nguon;
        }
    }
    public Bellman(string p = " ") : base(p)
        {
        }
        public void BellmanFord()
        { 
            //distance[], predecessor[]
            Dictionary<string, int> distance = new Dictionary<string,int>();
            Dictionary<string, string> predecessor = new Dictionary<string,string>();
            /*
             Bước 1: Khởi tạo đồ thị
                    Cho mỗi đỉnh trong danh sách các đỉnh
                    distance[v] = inf           // Tại bước đầu tiên, các đỉnh có độ dài là vô cùng
                    predecessor[v] = inf        // Và không có bất kì đỉnh tiền nhiệm nào
             */
            //Xác định inf (vô cùng)
            int inf = DsCanh.Max(p => p.cost) + 100;
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

    class Program
    {
        static void Main(string[] args)
        {
           Console.WriteLine("Tim duong di ngan nhat Bellman Ford");
            Bellman dt = new Bellman("a.txt");
            dt.Print();
            dt.BellmanFord();
            Console.ReadKey();
        }
    }
}
