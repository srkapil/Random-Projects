#include <iostream>
#include <cmath>
using namespace std;

unsigned int X=0,Y=0; //Range
int K=0,B=0; //B is the number and K degrees

/*
It is easier to find the roots of the given number and then counting the degree
so for example
Lets say we have B=15 and K=4 B=2
Sqrt(15)=3 so now find the remaining by 15-2^3=7
Sqrt(7)=2 then remaining 7-2^2=3
Sqrt(3)=1 then remaining is 3-2=1
Sqrt(1)=0 and thus we have 2^3+2^2+2^1+2^0=15 which is 4 degrees and thus is true.
*/
//The follwing method accepts same powers to appear as many time as needed
bool doesSumExist2(unsigned int number){
	unsigned int numb=number;
	unsigned int power=0;
	unsigned int times=0;
	while(numb>0){
		if(times>K)return false;
		if(numb==1){ times++; numb=0; continue; }
		else if(numb<B){ times=times+numb; numb=0;break; }
		power=pow((double)numb,(double)1/B);
		//cout<<"Power:"<<power<<" Number: "<<numb;
		numb=numb-pow((double)B,(double)power);
		//cout<<" Num: "<<numb<<endl;
		times++;
	}
	if(times==K) return true;
	return false;
};
bool doesSumExist(unsigned int number){
	unsigned int numb=number;
	unsigned int power=0;
	unsigned int times=0;
	while(numb>0){
		if(times>K)return false;
		if(numb==1){ times++; numb=0; continue; }
		else if(numb<B){ times=times+numb; numb=0;break; }
		power=pow((double)numb,(double)1/B);
		//cout<<"Power:"<<power<<" Number: "<<numb;
		numb=numb-pow((double)B,(double)power);
		//cout<<" Num: "<<numb<<endl;
		times++;
	}
	if(times==K) return true;
	return false;
};
int main(){
	unsigned int i=0;
	scanf("%d %d",&X,&Y);
	scanf("%d",&K);
	scanf("%d",&B);
	
	/*K=2;
	B=4;
	X=16;
	Y=20;*/
	unsigned int numbersInRange=0;
	for(i=X;i<=Y;i++){
		if(doesSumExist(i)){
			//cout<<"True for"<<i<<endl;
			numbersInRange++;
		}
	}
	cout<<numbersInRange;
}
