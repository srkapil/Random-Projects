#include <iostream>
using namespace std;

/*
**NOTE: This program has a limit of 1000 integer factors which will result wrong answer for GCD if factors were more than 1000. 
*Author- Kapil
*/
	void findfactors(int n);

	//following function declarations are for finding gcd
	void findfactorsof(int x,int *nm);
	void gcd(int n1,int m1);
	int n[1000];
	int m[1000];
	int capacity1=0; //n's capacity
	int capacity2=0; //m's capacity

	int main(){
		int x=0;//used for factor input
		int no1=0,no2=0;//used for gcd input
		int userin=0;
		while(true){
			cout<<"Do you want to find factors of number OR gcd of two numbers?"<<endl;
			cout<<"Type 1 for finding factors"<<endl;;
			cout<<"Type 2 for finding greatest Common Divisor(GCD) of two numbers"<<endl;
			cout<<"Type 3 to exit"<<endl;
			cin>>userin;	

			if(userin==1){
				cout<<"Enter in a number to find its factors"<<endl;
				cin>>x;
				cout<<"Factors of the entered numbers are: ";
				findfactors(x);
			}
			else if(userin==2){
				cout<<"Enter in two number seperated with a comma to find GCD"<<endl<<"Ex: 25,5"<<endl;
				scanf("%d,%d",&no1,&no2);
				gcd(no1,no2);
				capacity1=0; //resets capacity, otherwise wrong gcd is printed.
				capacity2=0;
			}
			else if(userin==3){
				break;
			}
			else{
				cout<<"That is an invalid Input"<<endl; //when the user enters a char infinite loop is created, so program ends here to break it
				cout<<"Program will exit.";
				system("pause");
				exit(1);
			}
		}
		exit(1);
}
	void findfactors(int x){
		for(int i=1;i<=x;i++){
			if(x%i==0) {
				cout<<i<<",";
			}
		}
		cout<<endl<<endl;
	}
	void findfactorsof(int x,int nm[]){
		int k=0;
		for(int i=1;i<=x;i++){
			if(x%i==0) {
				nm[k]=i;
				k++;
			}
		}
		if(capacity1==0) capacity1=k;
		else capacity2=k;
	}
	void gcd(int n1,int m1){
		findfactorsof(n1,n);
		findfactorsof(m1,m);
		//sizeofarray=>  (sizeof(n)/sizeof(int)

		for(int i=capacity1-1;i>=0;i--){
			for(int j=capacity2-1;j>=0;j--){
				if(n[i]==m[j]) {
					cout<<endl<<"GCD is: "<<n[i]<<endl<<endl;
					return;
				}
			}
		}
	}
