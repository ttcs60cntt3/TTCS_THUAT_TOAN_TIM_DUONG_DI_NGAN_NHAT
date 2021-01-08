#include <stdio.h>
void Ford_Bellman(int *d, int s)
{
    for (int v=0; v<V; v++)
    {
        d[v] = (A[s][v] != 0)? A[s][v] : 32000;
        Truoc[v] = s;
    }
    d[s] = 0;
    for (int k=0; k<V-2; k++)
        for (int v=0; v<V; v++)
            for (int u=0; v!=s && u<V; u++)
                if (d[v] > d[u]+A[u][v] )
                {
                    d[v] = d[u] + A[u][v]; 
                    Truoc[v] = u;
                }
}

int main()
{
    DocMTKe("D:\\GRAPH1.TXT", A, V);
    XuatMTKe(A, V);
    int s, t;
    printf("Nhap dinh dau, dinh cuoi cua duong di: ");
    scanf("%d %d", &s, &t);
    int d[MaxV];
    memset( d, 0, sizeof(d) );
    Ford_Bellman(d,s);
    return 0;
}