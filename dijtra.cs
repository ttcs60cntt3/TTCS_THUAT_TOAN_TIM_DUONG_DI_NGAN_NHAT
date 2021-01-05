using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TTCS_THUAT_TOAN_TIM_DUONG_DI_NGAN_NHAT
{
      public class TimDuongDijktra
    {
        public int[,] duLieu { get; private set; }
        List<bool> daXet { get; set; }
        List<DuongDi> listDuongDi { get; set; }
        int diemDau { get; set; }
        int diemCuoi { get; set; }

        public TimDuongDijktra(int[,] duLieu)
        {
            if (duLieu != null)
            {
                this.duLieu = duLieu;
                khoitao();
            }
        }
        public TimDuongDijktra()
        {
            FileStream f = new FileStream("D:\\dulieu1.inp", FileMode.Open);
            StreamReader rd = new StreamReader(f, Encoding.UTF8);
            string dls = rd.ReadToEnd();
            Console.WriteLine(dls);
            rd.Close();
            int[,] duLieu = new int[50,50];
            this.duLieu = duLieu;
            khoitao();
        }
        void khoitao()
        {
            listDuongDi = new List<DuongDi>();
            daXet = new List<bool>();
            for (int i = 0; i < duLieu.GetLength(0); i++) daXet.Add(false);

        }
        public List<int> TimDuong(int diemDau, int diemCuoi)
        {
            khoitao();

            diemDau--;
            diemCuoi--;
            this.diemDau = diemDau;
            this.diemCuoi = diemCuoi;
            if (diemDau < 0 || diemDau > duLieu.GetLength(0) || diemCuoi < 0 || diemCuoi > duLieu.GetLength(0)) return null;

            List<int> duong = new List<int>();


            DuongDi dd = new DuongDi();
            dd.diemDaDi.Add(diemDau);

            this.listDuongDi.Add(dd);

            int dangXet = diemDau;
            daXet[diemDau] = true;

            while (!KetThuc(diemCuoi))
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


                if (diemMoi == diemCuoi) duong = timDuong.diemDaDi;
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
                    if (diem == this.diemCuoi && min > duLieu[diem, item]) continue;
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
}