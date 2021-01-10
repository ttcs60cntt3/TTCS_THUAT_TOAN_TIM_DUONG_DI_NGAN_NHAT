using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TTCS_THUAT_TOAN_TIM_DUONG_DI_NGAN_NHAT
{
    public class Diem_G /// them diem G trong so cua G voi diem dau = 0
    {
        public NhapDothi
         {FileStream f = new FileStream("D:\\HocTap\\dulieu1.inp", FileMode.Open);
            StreamReader rd = new StreamReader(f, Encoding.UTF8);
            string dls = rd.ReadToEnd();
            Console.WriteLine(dls);
            rd.Close();
            int[,] duLieu = new int[50, 50];
            this.duLieu = duLieu;
            khoitao();
        }
        void khoitao()
        {
            listDuongDi = new List<DuongDi>();
            daXet = new List<bool>();
            for (int i = 0; i < duLieu.GetLength(0); i++) daXet.Add(false);

        }
    
    }
    public class Bellman /// dung bell man de tinh trong so
    {

    }
    public class WeightNew// luu lai do thi moi 
    {
        int johnson()
        {
	int s = n;				
	int u = 0, v = 0;
	for(; u < n; u++){
		add_arc(s,u,0);			
	}
	int *P = bellman_ford(G,s,n+1);		
	for(u = 0; u < n; u++){
		vlist *nbrs = G[u];		
		while(nbrs != NULL){
			v = nbrs->v;
			nbrs->Delta = nbrs->w + P[u] - P[v];	
			nbrs = nbrs->next;		
		}
	}
	Console.WriteLine(" Trong so thay doi \n");
//////////////// in list tai day
	H = (int *)malloc((n-1)*(sizeof(int)));		
	pos = (int *)malloc((n-1)*sizeof(int));
	
	int **D = (int **) malloc(n*sizeof(int*));		
	d = (int *)malloc(n*sizeof(int));			
	for( u = 0; u < n; u++){
		D[u] = (int *)malloc(n*sizeof(int));		
		dijkstra_heap(G, u, n);	 	
		for(v = 0; v < n; v++){
			D[u][v] = d[v] + P[v] - P[u];	
		}
	}
	
	return D;
}
    }
    public class TimDuongDijktra /// su dung code cua Long gia quyet Dijktra
    {
        public int[,] duLieu { get; private set; }
        List<bool> daXet { get; set; }
        List<DuongDi> listDuongDi { get; set; }
        int diemDauM { get; set; }
        int diemCuoiM { get; set; }
        public TimDuongDijktra()
        {
           // du lieu moi nhan tu WeightNew
            khoitao2();
        }
        public TimDuongDijktra(int[,] duLieu)
        {
            if (duLieu != null)
            {
                this.duLieu = duLieu;
                khoitao2);
            }
        }
    
        void khoitao2()/// nhan do  thi sau khi thay doi trong so
        {
            listDuongDi = new List<DuongDiMoi>();
            daXet = new List<bool>();
            for (int i = 0; i < duLieu.GetLength(0); i++) daXet.Add(false);

        }
        public List<int> TimDuong(int diemDauM, int diemCuoiM)
        {
            khoitao2();

            diemDauM--;
            diemCuoiM--;
            this.diemDauM = diemDauM;
            this.diemCuoiM = diemCuoiM;
            if (diemDauM < 0 || diemDauM > duLieu.GetLength(0) || diemCuoiM < 0 || diemCuoiM > duLieu.GetLength(0)) return null;

            List<int> duong = new List<int>();


            DuongDi dd = new DuongDi();
            dd.diemDaDi.Add(diemDauM);

            this.listDuongDi.Add(dd);

            int dangXet = diemDauM;
            daXet[diemDauM] = true;

            while (!KetThuc(diemCuoiM))
            {
                DuongDi timDuong = this.listDuongDi[0];
                int diemMoi = -1;

                foreach (DuongDi item in this.listDuongDi)
                {
                    int s = TimDuong(item);
                    if (s != -1)
                    {
                        if (diemMoi == -1)
                        {
                            diemMoi = s;
                            timDuong = item;
                        }
                        else
                        {
                            if (s < diemMoi)
                            {
                                diemMoi = s;
                                timDuong = item;
                            }
                        }
                    }
                }

                int soLuongThem = TimDiemKe(timDuong.diemDaDi[timDuong.diemDaDi.Count - 1]).Count;
                for (int i = 0; i < soLuongThem - 1; i++)
                {
                    DuongDi ddn = new DuongDi();
                    foreach (int item in timDuong.diemDaDi) ddn.diemDaDi.Add(item);

                    this.listDuongDi.Add(ddn);
                }

                if (diemMoi != -1)
                {
                    timDuong.diemDaDi.Add(diemMoi);
                    daXet[diemMoi] = true;
                }


                if (diemMoi == diemCuoiM) duong = timDuong.diemDaDi;
            }

            return duong;
        }
        bool KetThuc(int diemCuoi)
        {
            bool kt = true;

            foreach (DuongDi item in listDuongDi)
            {
                if (!item.isDie)
                {
                    kt = false;
                }
            }

            foreach (DuongDi item in listDuongDi)
            {
                if (!item.isDie)
                {
                    if (item.diemDaDi[item.diemDaDi.Count - 1] == diemCuoi) return true;
                }
            }

            return kt;
        }

        int TimDuong(DuongDi dd)
        {
            int le = dd.diemDaDi.Count;
            int diem = dd.diemDaDi[le - 1];
            List<int> list = TimDiemKe(diem);

            if (list.Count == 0)
            {
                dd.isDie = true;
                return -1;
            }

            int min = duLieu[diem, list[0]];
            int diemToi = list[0];
            foreach (int item in list) if (min >= duLieu[diem, item] && duLieu[diem, item] > 0)
                {
                    if (diem == this.diemCuoiM && min > duLieu[diem, item]) continue;
                    min = duLieu[diem, item];
                    diemToi = item;
                }

            return diemToi;
        }

        List<int> TimDiemKe(int diem)
        {
            List<int> listkq = new List<int>();

            for (int i = 0; i < duLieu.GetLength(0); i++)
            {
                if (diem != i && duLieu[diem, i] > 0 && !daXet[i]) listkq.Add(i);
            }

            return listkq;
        }
        public int TinhDuong(List<int> duongDi)
        {
            if (duLieu != null)
            {
                if (duongDi.Count == 0)
                {
                    return 0;
                }
                else
                {
                    int quangDuong = 0;

                    for (int i = 0; i < duongDi.Count - 1; i++)
                    {
                        quangDuong += duLieu[duongDi[i], duongDi[i + 1]];
                    }

                    return quangDuong;
                }
            }
            return 0;
        }
    }

    public class DuongDi
    {
        public List<int> diemDaDi { get; private set; }

        public bool isDie { get; set; }
        public DuongDi()
        {
            diemDaDi = new List<int>();
            this.isDie = false;

        }
        public bool ThemDiem(int diem)
        {
            foreach (int item in this.diemDaDi) if (item == diem) return false;
            this.diemDaDi.Add(diem);
            return true;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
              WriteLine.Console("Nhap diem bat dau:");// Nhap diem G vs trong so 0 vao dinh bat ban muon
        readline.Console("%d",&r);
        graph.edge[r].src = 0; 
        graph.edge[r].dest = r; 
        graph.edge[r].weight = 0; 
  
            TimDuongDijktra tt = new TimDuongDijktra();
            tt.TimDuong(1, 6);
        }
    }
}