using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TTCS_THUAT_TOAN_TIM_DUONG_DI_NGAN_NHAT
{
    class ttdijktra
    {
        //nhập ma trận trọng số có 6 đỉnh
        int  [,] matrix = new int [,] {
            {0, 1, 0, 1, 0, 0},
            {1, 0, 1, 0, 0, 0},
            {0, 1, 0, 1, 3, 0},
            {1, 0, 1, 0, 0, 0},
            {0, 0, 0, 0, 1, 0 }
        };
        int n = 6;
        void dijktra (int start , int finish)
        {
            int [] back=new int [100]; // lưu đỉnh cha
            int  [] weight= new int [100];//lưu trọng số
            int  [] mark = new int [100]; // đánh dấu đỉnh
            // khởi tạo
            for ( int i =0 ; i < n ; i++)
            {
                back[i]= -1;
                mark[i]= 0;
                weight[i]= INT_MAX;
            } 
            // xuất phát tại đỉnh đầu tiên
            back[start] = 0 ;
            weight[start] = 0;
            //kiểm tra độ liên thông của đồ thị
            int connect ;
            do
            {
               //do bắt đầu từ đỉnh 0 nên ta gán connect = -1
               connect = -1 ;
               int min = INT_MAX;
               // duyệt lần lượt qua các đỉnh
               for(int j = 0; j<n ;j++)
                    if(mark[j]==0)//nếu đỉnh chưa đc đánh dấu
                    {
                        // nếu tồn tại đường đi từ đỉnh start đến đỉnh j
                        if( matrix[start,j] !=0 && 
                        weight[j]>weight[start]+ matrix[start,j]) //weight[j] :tổng trọng số từ đỉnh bắt đầu tới đỉnh đang xét
                                                                // weight[start]+ matrix[start,j]: trọng số đang xét
                        {
                           weight[j] = weight[start]+ matrix[start,j];//dùng để so sánh lần sau
                           back[j]; // lưu lại đỉnh cha
                        }
                        // tìm đường đi ngắn nhất hiện tại dựa vào mảng weight
                        if(min > weight[j])
                        {
                            min = weight[j];
                            connect = j;// dựa vào biến connect ta có thể xác định xem đồ thị có liên thông không và đỉnh tiếp theo cần duyệt
                        }

                    } 
                    start = connect ; 
                    mark[start] =1 ;
            } while( connect = -1 && start != finish);
            Console.Write ("[0]",weight[finish]);
        }

    }
}