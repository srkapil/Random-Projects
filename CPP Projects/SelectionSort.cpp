/**
* @Author- Kapil
*/

#include <iostream>
#include <time.h>
using namespace std;

void selectionsort(int start,int end);//function declaration
void generateList(int size); //random number generator, size the number of elements to be generated 

int size=200; //enter the size of numbers here
int *list=new int[size];//{32,45,65,100,203,56,43,20,90,1000,23,45,36,26,894}; <-manual way //add the numbers you want to sort <====


//int size=sizeof(list)/sizeof(int);//divided by size of int so that it will give the correct size of list contents;
int index=0;//used to rearrange the list (ex- swap lowest in list with index=0 of the list )

int main(){
	
	generateList(size);
	//all double things are used for calculating time
	double start=time(NULL);
	cout<<"Authour: Kapil"<<endl <<"Sorting Type: Selection Sort"<<endl<<endl;
	cout<<"Sorted List:"<<endl;
	selectionsort(0,size);//sorts the list
	
	double end=time(NULL);
	double difference=difftime(end,start);
	cout<<endl;
	printf("That sorting took %.5f seconds\n",difference);

	system("pause");//wait for a key press to terminate
	return 0;
}
/**
* @param start: the index at which the sorting should begin 
* @param size: the index at which sorting should stop
* Range: This function will work range of start to end
* So for example if 2 to 4 is given, then it will only sort contents at index 2,3,4 and not any other indexes
*/
void selectionsort(int start,int end){
	int i=start;
	int small=list[start];
	if(start==end-1){ //break recurssion
		cout<<list[start]<<endl;
		return;
	}
	else{
		//find the smallest in the given range
		while(i<end){
			if(list[i]<small){
				small=list[i];
				swap(list[i],list[index]);
			}
			i++;
		}
		index++;
		//print the value of the found smallest
		cout<<small<<",";
		selectionsort(start+1,size);//recurse until last index is reached (@see if(start=end-1) breaks the recurssion)
	}
}

void generateList(int size){
	for(int i=0;i<size;i++){
		list[i]=rand();
	}
}