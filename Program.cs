using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TTCS_THUAT_TOAN_TIM_DUONG_DI_NGAN_NHAT
{
    class Program
    {
        static void Main(string[] args)
        {
            ttdijktra tt = new dijktra();
            tt.dijktra(1,6);
        }
    }
}
