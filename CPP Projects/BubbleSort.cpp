#include <iostream>
using namespace std;
\
int main(){
	int list[10]={32,45,65,100,203,56,43,20,90,1000};
	for(int i=0; i<sizeof(list)/4;i++){
		for(int j=0;j<(sizeof(list)/4)-1;j++){
			if(list[j]>list[j+1])
				swap(list[j],list[j+1]);
		}
	}
	for(int k=0;k<sizeof(list)/4;k++)
		cout<<list[k]<<",";
	system("pause");
	return 0;
}
