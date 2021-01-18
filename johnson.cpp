

#include<stdio.h>
#include<stdlib.h>
#include<string.h>
#include<time.h>
#define INFTY 1000000000


int *H; 	
int hsize = 0; 	
int *pos;	 // mang danh dau vi tri cua moi dinh trog mang Heap
void up_heapify(int u);
int parent(int u);
void print_heap();
void decrease_key(int u, int K);
void down_heapify(int u);
int extract_min();
void build_heap(int n, int s);

typedef struct vlist{
	int v;		
	int w;		
	int Delta;	
	struct vlist *next;
}vlist;

vlist **G;			
int n;					
int **D;				
int *d;					
void read(); 				
void them_cung(int u, int v, int w);
int *bellman_ford(vlist **G, int s, int n);
void dijkstra_heap(vlist** G, int s, int n);
int  **johnson();

void inmang(int *A, int n);
void indothi();
void indscanh(int u);


int **johnson(){
	int s = n;				
	int u = 0, v = 0;
	for(; u < n; u++){
		them_cung(s,u,0);			
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
	printf("\nDo thi sau khi doi trong so\n");
	indothi();
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

int *bellman_ford(vlist **G, int s, int n){
	int* D = (int *)malloc(n*sizeof(int));
	int i = 0;
	for(; i < n; i++)D[i] = INFTY;
	D[s] = 0;
	int v = 0;
	vlist *nbrs = G[s];	
	while(nbrs != NULL){	
		v = nbrs->v;
		D[v] = nbrs->w;
		nbrs = nbrs->next;
	}
	int  u = 0;
	for(i = 0; i < n-2; i++){
			for(u = 0; u < n; u++){
				vlist *nbrs = G[u];	
				while(nbrs != NULL){
					v = nbrs->v;
					if(D[v] > D[u] + nbrs->w){
						D[v] = D[u] + nbrs->w;
					}
				nbrs = nbrs->next;	
				} 
			}
	}
	return D;
}

void dijkstra_heap(vlist** G, int s, int n){
	int i = 0;
	for(; i < n; i++)d[i] = INFTY;
	d[s] = 0;
	int v = 0;
	vlist *nbrs = G[s];
	while(nbrs != NULL){	
		v = nbrs->v;
		d[v] = nbrs->Delta;
		nbrs = nbrs->next;
	}
	build_heap(n,s);
	int u;
	while(hsize != 0){ 	
		u = extract_min();
		vlist *nbrs = G[u];	
		while(nbrs != NULL){	
			v = nbrs->v;
			if(d[v] > d[u] + nbrs->Delta){
				d[v] = d[u] + nbrs->Delta;
				decrease_key(v,d[v]);
			}
			nbrs = nbrs->next;	
		} 
	}
}

// them dinh, canh, khoang cach
void read(){
	n = 6;
	int i = 0, j = 0;
	G = (vlist **) malloc((n+1)*sizeof(vlist*));	
	for(; i < n+1; i++)G[i] = NULL;
	them_cung(0,2,-1);	
	them_cung(1,0,5);		
	them_cung(2,0,3);		
	them_cung(2,1,-3);	
	them_cung(2,4,11);	
	them_cung(3,0,-6);	
	them_cung(4,3,-3);	
	them_cung(4,5,4);		
	them_cung(5,1,-4);	
	them_cung(5,2,1);		

}
// them G
void them_cung(int u, int v, int w){
	vlist* vnode = (vlist *)malloc(sizeof(vlist));
	vnode->v = v;
	vnode->w = w;
	vnode->Delta = -1;	
	vnode->next = G[u];
	G[u] = vnode;
}


void up_heapify(int u)// u không phai là goc cua heap
{
	int v = parent(u);
	if(v != -1 && d[H[u]] < d[H[v]]){
		int tmp = H[u];
		H[u] = H[v];
		pos[H[v]] = u;		
		H[v] = tmp;
		pos[tmp] = v;		
		up_heapify(v);
	}
}


//Hai nut con cua nut u la 2u + 1 và 2u + 2, nut nho hon là nut con ben trai
int parent(int u){
	return ((u&1)==0 ? ((u-2)>> 1) : (u-1) >> 1);
}



void in_dong(){
	int i = 0;
	for(; i < hsize; i++){
		printf("%d ", H[i]);
	}
	printf("\n");
}


void decrease_key(int u, int k){
	d[u] = k;
	up_heapify(pos[u]);
}

int extract_min(){
	int tmp  = H[0];
	H[0] = H[hsize-1];
	pos[H[0]] = 0;
	hsize--;
	down_heapify(0);
	return tmp;
}

void down_heapify(int u)// u không phai là lá cua dong
{
	int m = 2*u+1;
	if(m < hsize){	
		if(2*u+2 < hsize && d[H[m]] > d[H[2*u+2]]){
			m = 2*u+2;		
		}
		if(d[H[u]] > d[H[m]]){
			int tmp = H[u];
			H[u] = H[m];
			pos[H[m]] = u;	
			H[m] = tmp;
			pos[tmp] = m;	
			down_heapify(m);		
		}
	}
}

// Xây dung dong tu mot mang trong thoi gian O (n)
void build_heap(int n, int s){
	hsize = n-1;
	memset(H,-1,hsize*sizeof(int));		
	memset(pos, -1, hsize*sizeof(int));	
	int i = 0;
	for(; i < s; i++) {
		H[i] = i;
		pos[i] = i;
	}

	for(i = s+1; i < n; i++){
		H[i-1]=i;
		pos[i] = i-1;
	}
	for(i = hsize-1; i >=0; i--){
		down_heapify(i);
	}
}


void indothi(){
	int u = 0;
	for(; u < n; u++){
	indscanh(u);
	}	
}
void indscanh(int u)
{
	vlist *it = G[u];
	while(it != NULL){
		printf("(%d->%d,%d,%d) ",u,it->v, it->w, it->Delta);
		it = it->next;
	}
	printf("\n");
}
void inmang(int *A, int n){
	int i = 0;
	for(; i < n; i++){
		printf("%d ", A[i]);
	}
	printf("\n");

}
int main(){
	read();
	indothi();
	D = johnson();
	printf("\nTat ca khoang cach cua Johnson\n");
	int i = 0;
	for(; i < n; i++) inmang(D[i], n);
}
