#include <stdio.h>

int main(){
	int locker1,locker2;
	scanf("%d",&locker1);
	scanf("%d",&locker2);
	if(locker1==0 || locker2==0) printf("no"); 
	else if(locker2%2==1 || locker1%2==0) printf("yes");
	else printf("no");
	return 0;
}
