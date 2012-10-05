#include <stdio.h>
#include <math.h>
unsigned int X=0,Y=0; //Range
int K=0,B=0; //B is the number and K degrees
unsigned int i=4294967294;
/*
It is easier to find the roots of the given number and then counting the degree
so for example
Lets say we have B=15 and K=4 B=2
Sqrt(15)=3 so now find the remaining by 15-2^3=7
Sqrt(7)=2 then remaining 7-2^2=3
Sqrt(3)=1 then remaining is 3-2=1
Sqrt(1)=0 and thus we have 2^3+2^2+2^1+2^0=15 which is 4 degrees and thus is true.
*/
bool doesSumExist(unsigned int number){
	unsigned int numb=number;
	unsigned int power=0;
	unsigned int times=0;
	while(numb>0){
		if(times>K)return false;
		if(numb==1){ times++; numb=0; continue; }

		power=pow((double)numb,(double)1/B);
		//printf("|POWER:%d and Number: %d\n",power,numb);
		numb=numb-pow((double)B,(double)power);
		times++;
	}
	if(times==K) return true;
	return false;
};
int main(){

	/*
	scanf("%d %d",&X,&Y);
	scanf("%d",&K);
	scanf("%d",&B);*/
	K=2;
	B=2;
	X=15;
	Y=20;
	unsigned int numbersInRange=0;
	printf("%d",i);
	for(i=X;i<=Y;i++){
		if(doesSumExist(i)){
			//printf("True for %d\n",i);
			numbersInRange++;
		}
	}
	printf("%d",numbersInRange);
	return 0;
}
