#include <stdio.h>
int main(){
int n1=0;
int n2=0;
scanf("%d %d",&n1, &n2);
printf("%d %d", ((n1+n2-1)-n1), ((n1+n2-1)-n2));
return 0;
}
