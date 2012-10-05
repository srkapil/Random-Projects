/* 
Author= Kapil
NOTES:My first linked list program in C++ without looking any notes :D 
Works fine xD 
Things to explore- The node reference in the parameter doesn't matter if it is not referenced to real root or not 
it works without &. How come??? */
#include <iostream>
using namespace std;

class LinkedList{
	class Node{//a sub class for node
	public:
		int value;
		Node *next;
		Node(){
			value=NULL;
			next=NULL;
		}
		Node(int val){
			value=val;
			next=NULL;
		}
		Node(int val,Node nx){
			value=val;
			next=&nx;
		}
		~Node(){
			delete next;
		}
	};
public:
	Node* root;
	int sizeOfList;
	LinkedList(){
		root=new Node;
		sizeOfList=0;
	}
	LinkedList(int value){
		root=new Node(value);
		sizeOfList=0;
	}
	Node getFirst(){
		return root->value;
	}
	void insert(Node* &n,int val){//recurse and find the last free slot and insert the value there
		if(sizeOfList==0){ //Assign the root value which will be the first item that will be added
			root->value=val;
			sizeOfList=1;
		}
		else if(n->next==NULL){
			n->next=new Node(val);
			sizeOfList++;
		}
		else{
			insert(n->next,val);
		}
	}

	void print(Node* &n){//recurse through linked list and print out all nodes
		if(n->next==NULL){
			cout<<n->value;
		}
		else{
			cout<<n->value<<"->";
			print(n->next);
		}
	}
	int getSizeOfList(){
		return this->sizeOfList;
	}
	~LinkedList(){
		delete root;
	}
};

int main(){
	LinkedList *ll=new LinkedList();
	for(int i=0;i<10;i++){
		ll->insert(ll->root,i);
	}
	ll->print(ll->root);
	cout<<endl<<"The size of the list is : "<<ll->getSizeOfList();
	delete ll;
	system("pause");
}