#include <stdio.h>
int main(){
	int n=0;
	int k=0;
	scanf("%d %d",&n, &k);
	if(n<k) printf("2");
	else{
		if(k==2) printf("%d",n);
		else{
			int x=k;
			if(2*n%k>0) printf("%d",(2*n/k)+1);
			else printf("%d",2*n/k);
		}
	}
	return 0;
}
