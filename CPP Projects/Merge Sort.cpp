/**
Author: Kapil
The following is tested for some cases and it worked just fine. The following are some of the test cases used:

1) Random number check : PASSED 
2) Odd number sized Array Check(3,2,1): PASSED
3) Even number sized Array Check(10,2,3,4): PASSED
4) Already Sorted Array Check:(1,2,3) PASSED
5) Same numbers repeated Check(1,2,3,3,5): PASSED
6) Same number in different places(1,2,1,5): PASSED  
7) Test with 10000 numbers :PASSED

My notes: I implemented this without looking at any pseudo code anywhere, But I used the wiki's animation that 
simulates how merge sort is done. I used recursion for splitting and merging is just simple loop that sorts 2 lists.
*/
#include <iostream>
#include <vector>
#include <Windows.h>
using namespace std;


void printOut(vector<int> *myVector){
	for(int i=0;i<myVector->size();i++){
		cout<<myVector->at(i)<<",";
	}
	cout<<endl;
}

/*
Given that left and right are already sorted, merge them in a way 
such that the new resulting list will be sorted.
*/
vector<int>* merge(vector<int> *left, vector<int> *right){
	vector<int> *result=new vector<int>();
	int min=left->at(0);
	int vectorSize=left->size();
	for(int i=0;i<vectorSize;i++){
		//cout<<"I:"<<i<<endl;
		if(right->empty()){
			//cout<<"Feedback: Right list is empty, Adding all elements in left "<<left->at(i)<<endl;
			min=left->at(i);
			result->push_back(min);
			continue;
		}
		else if(left->at(i)<right->at(0)){
			//cout<<"Feedback: Left has the minium, Added to the result "<<left->at(i)<<endl;
			min=left->at(i);
			result->push_back(min);
		}else{
			//cout<<"Feedback: Right has the minium, Added to the result "<<right->at(0)<<endl;
			min=right->at(0);
			result->push_back(min);
			//cout<<"Feedback: Checking other elements in right list"<<endl;
			int j=1;
			if(right->size()<=1){
				if(right->size()==1){
					right->erase(right->begin());
					min=left->at(i);
					result->push_back(min);
				}
				continue;
			}
			while(right->at(j)<left->at(i)){
				min=right->at(j);
				result->push_back(min);
				//cout<<"MIN:"<<min<<" j: "<<j<<endl;
				j++;
				if(j>=right->size()){
					//cout<<right->size();
					break;
				}
			}
			right->erase(right->begin(),right->begin()+j);
			min=left->at(i);
			result->push_back(min);
		}
	}
	if(!right->empty()){
		//cout<<"Feedback: Adding the remainings in right list"<<endl;
		for(int j=0;j<right->size();j++){
			result->push_back(right->at(j));
		}
	}
	return result;
}

/*
Split the given list into two seperate lists : left and right
Split till the list has no more than 2 elements, Merge them and 
traverse the merged lists back and merge it with other lists.
*/
vector<int>* sort(vector<int>* numarray){
	int sizeOfVector=numarray->size();
	int middle=0;
	if(sizeOfVector%2==1)middle=numarray->size()/2;
	else middle=numarray->size()/2-1;
	/*
	cout<<"Size of array is:"<<numarray->size()<<"Middle is at:"<<middle<<endl;
	cout<<"VECTOR:"<<endl;
	printOut(numarray);
	cout<<endl;
	*/
	if(numarray->size()<=1){
		return numarray;
	}
	vector<int>* left=new vector<int>();
	vector<int>* right=new vector<int>();
	for(int i=0;i<numarray->size();i++){
		if(i<=middle){
			left->push_back(numarray->at(i));
		}
		else{
			right->push_back(numarray->at(i));
		}
	}
	/*
	cout<<"LEFT:";
	printOut(left);
	cout<<"RIGHT:";
	printOut(right);*/
	return merge(sort(left),sort(right));

}

int main(){
	int testArray[]={5,4,3,2,1};
	vector<int> *nonsorted=new vector<int>();

	
	nonsorted->push_back(10);
	nonsorted->push_back(5);
	nonsorted->push_back(0);
	nonsorted->push_back(1);
	nonsorted->push_back(30);
	nonsorted->push_back(8);
	nonsorted->push_back(0);

	/*STRESS TEST with 10000 Numbers
	for(int i=10000;i>0;i--){
		nonsorted->push_back(i);
	}
	*/
	cout<<"The array to be sorted is:"<<endl;
	printOut(nonsorted);
	cout<<"Resulting sorted array:"<<endl;
	printOut(sort(nonsorted));

	system("pause");
}