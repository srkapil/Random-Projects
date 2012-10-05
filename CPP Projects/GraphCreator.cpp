/*
Author- Kapil
Info- A program that creates a graph and has functionalities such as checking for insert a node, remove a node,
check for availability of a node, get a node by its name, and print neighbours of a node.
Graph is to be used by the user and node class will be used by graph class.
NOTE: Uses array of pointers, Using vector may increase efficiency but hasn't been used here for simplicity.
*/
/*
Currently a node can have same neighbours bt they will be different objects. Need to fix it so that they point to same node.
Also efficiency can be improved by having a list that has all the information about nodes as a string. For example
it can save all the nodes with their names and its neighbours. This way we ensure it's easier to check for neighbours.
and we can get the node using getNode so that we can point to the correct node without having to duplicate the node as to 
ensure correctnes.
+Update the code by implementing Dijkstra algorithm
*/
#include <iostream>
#include <sstream>
#include <string.h>
using namespace std;
#define MAX_NEIGHBOURS 10 //<- Change the no of maximum neighbours value here (PROGRAM WILL MORE LIKELY TO CRASH IF ARRAY EXCEEDS THE LIMIT)

class Node{
private:
	int neighbourCounter;
public:
	Node* parent;
	string value;
	Node *neighbours[MAX_NEIGHBOURS];//Array of pointers (Pointer to a pointer can be used like ** - google for more details)

	Node(){
		neighbourCounter=0;
		//for(int i=0;i<MAX_NEIGHBOURS;i++){
		//	neighbours[i]=new Node();
		//}
	}
	Node(string val){
		neighbourCounter=0;
		parent=this;
		value=val;
	}
	Node(Node* prnt,string val){
		neighbourCounter=0;
		parent=prnt;
		value=val;
	}
	void addNeighbour(Node*);
	void removeNeighbour(Node&,string);
	void printNeighboursOf(string);
	Node* getNode(Node*,string);
	bool isNeighbour(string);
	~Node(){
		//delete parent;
		//delete neighbours;
	}
};
void Node::addNeighbour(Node* node){
	neighbours[neighbourCounter]=node;//add the node to the array
	neighbourCounter++;//increase the counter
};
Node* Node::getNode(Node* startingNode,string nodeVal){
	if(startingNode->value==nodeVal) return startingNode;
	else{
		if(startingNode->neighbours==NULL) cout<<"YES no neighbours";
		for(int i=0;i<startingNode->neighbourCounter;i++){
			return getNode(startingNode->neighbours[i],nodeVal);
		}
	}
};
void Node::removeNeighbour(Node &parent,string value){
	for(int i=0;i<neighbourCounter;i++){
		if(neighbours[i]->value==value){
			for(int j=i;j<neighbourCounter;j++){
				if(j==MAX_NEIGHBOURS) neighbours[j]->value="";
				else neighbours[j]=neighbours[j+1];
			}
			break;
		}
	}
};
void Node::printNeighboursOf(string nodeName){
	if(!isNeighbour(nodeName)){ 
		cout<<"The entered Node name doesn't exist";
		return;
	}
	Node* node=getNode(this,nodeName);
	for(int i=0;i<node->neighbourCounter;i++){
		cout<<node->neighbours[i]->value;
	}
};
bool Node::isNeighbour(string val){
	for(int i=0;i<neighbourCounter;i++){
		if(neighbours[i]->value==val) return true;
		else if(neighbours[i]->isNeighbour(val)) return true;//Recursive call <-Look at each neighbour's neighbours
	}
	return false;
};

class Graph{
public:
	Node* root;
	bool isNodeInGraph(string);
	void createRoot(string);
	void addNeighbourTo(string,string);
	void addNeighbourNode(Node*,Node*);
	Graph(){
		
	}
	~Graph(){}
};
void Graph::createRoot(string value){
	root=new Node(value);
};
void Graph::addNeighbourTo(string parent,string value){
	Node* node=root->getNode(root,parent);
	node->addNeighbour(new Node(value));

};
void Graph::addNeighbourNode(Node *parent,Node* nb){
	parent->addNeighbour(nb);
};
bool Graph::isNodeInGraph(string val){
	if(root->value==val) return true;
	else if(root->isNeighbour(val)) return true; //The method from Node class is a recursive method
	else return false;
};
int main(){
	Graph* graph=new Graph();
	graph->createRoot("Hello");
	//Node* n1=new Node("There");
	//Node* n2=new Node("Kapil");
	graph->addNeighbourTo("Hello","Kapil");
	graph->addNeighbourTo("Hello","Kapilx");
	graph->addNeighbourTo("Hello","Kapilxx");
	graph->addNeighbourTo("Kapil","Kapilxx");
	//graph->root->printNeighboursOf("Hello");
	graph->root->printNeighboursOf("Kapil");
	system("pause");
}
/*
Problems Encountered:
Access violation problem at adding neighbours. The problem was 
a constructor was calling another constructor at the start of its call
and thus there were some problems with that processing things correctly.
neighbourCounter was having some address values, then changing it to 
neighbourCounter=0; for all constructor calls solved it. 

*/