#include <iostream>
#include <string>
#include <sstream>
using namespace std;

/*
@author- Kapil
Description: Takes an input of preorder string which will need to have 0 as the external node.
Creates and then displays the height of the tree. Tree also keeps track of each node and it's parent.
Also has pre order and post order traversal methods.
*/
class Node{
public:
	char value;
	Node* left;
	Node* right;
	Node* parent;
	Node(char val){
		value=val;
		left=NULL;
		right=NULL;
		parent=NULL;
	}
	Node(char val,Node &lft,Node &rght){
		value=val;
		left=&lft;
		right=&rght;
		parent=NULL;
	}

	~Node(){
		delete left;
		delete right;
	}
};
class Tree{
private:
	int pathLength,index;
	Node* root;
	//Node* lastNode; //Reference to the last node added to the tree
	string preorderString;
	stringstream preOrder,postOrder;
public:
	Tree(string bitstring){
		preorderString=bitstring;
		pathLength=0;
		index=1;
		if(checkString(bitstring)){
			root=new Node(preorderString.at(0));
			root->parent=root;
			cout<<"Added "<<root->value<<" as the root"<<endl;
			constructTree(root,root,"");
			this->preOrderTraversal(root);
			this->postOrderTraversal(root);
			cout<<this->calculateHeight(root);
			//cout<<"Preorder Traversal:"<<preOrder.str()<<endl;
			//cout<<"Postorder Traversal:"<<postOrder.str()<<endl;
		}
		else{
			cout<<"Invalid String detected"<<endl;
		}
	}
	/*
	* This method will check if the string has right number of 0's and letters.
	* This method is not guaranteed to say if a pre order string is valid or invalid 
	* in some circumstances. Will work correctly for most of the times.
	*/
private:
	bool checkString(string bitstring){
		int letterCounter=0;
		int zeroCounter=0;
		int stringLength=bitstring.length();
		if(bitstring.at(stringLength-1)!='0' || bitstring.at(stringLength-2)!='0'){ return false; }
		for(int i=0;i<stringLength;i++){
			if(bitstring.at(i)=='0') zeroCounter++;
			else letterCounter++;
		}
		if(letterCounter+1!=zeroCounter){
			return false;
		}
		else if(zeroCounter==1) return false;
		return true;
	}
	/**
	* The following method will generate the tree using the  preorder string
	* and will store the details such as the parent of the node and so makes
	* it easier for doing other opearation on the tree.
	*/
	void constructTree(Node* node,Node* parent, string direction){
		if(node==NULL) {
			char val=preorderString.at(index);
			if(direction=="left"){
				parent->left=new Node(val);
				parent->left->parent=parent;
				node=parent->left;
				//cout<<"Added "<<preorderString.at(index)<<" to the left of "<<parent->value<<endl;
			}
			else{
				parent->right=new Node(val);
				parent->right->parent=parent;
				node=parent->right;
				//cout<<"Added "<<preorderString.at(index)<< " to the right of "<<parent->value<<endl;
			}
			index++;
		}
		if(node->left==NULL && node->value!='0') constructTree(node->left,node,"left");
		if(node->right==NULL  && node->value!='0') constructTree(node->right,node,"right");

	}
	/*
	* Traverse the tree in pre order. 
	*/
	void preOrderTraversal(Node *node){
		pathLength++;
		//cout<<node->value;
		preOrder<<node->value;
		if(node->left!=NULL) preOrderTraversal(node->left); 
		if(node->right!=NULL) preOrderTraversal(node->right);
	}
	/*
	* Traverse the tree in post order.
	*/
	void postOrderTraversal(Node *node){
		if(node->left!=NULL) postOrderTraversal(node->left); 
		if(node->right!=NULL) postOrderTraversal(node->right);
		postOrder<<node->value;
		//cout<<node->value;
	}
	int calculateHeight(Node*);
};

/*
* Calculates and returns the height of the tree as an integer.
*/
int Tree::calculateHeight(Node* node){
	int h1=0;
	int h2=0;
	if(node->left->value!='0'){
		h1= calculateHeight(node->left)+1;
	}
	if(node->right->value!='0'){
		h2= calculateHeight( node->right)+1;
	}
	return h1>h2 ? h1:h2;

}
int main(){
	//Problem with abc000d00 @ added d to the left of 0
	string input="";
	Tree* t;
	t=new Tree("dbnzhtpquumd00l00oj00x00pqf00d00fp00d00cjqm00v00n00bp00i00wdau00x00hq00m00cxl00y00qd00a00pdxcx00o00yw00h00jlw00t00tv00w00hxaf00i00ze00e00dls00z00oj00f00pexymg00w00kf00z00jfm00s00uy00v00vrfl00h00rz00h00piv00q00ks00f00lauqg00z00hd00g00xva00a00tb00y00rpxj00f00fh00d00qfs00i00tj00s00neiftbu00o00jq00y00epm00d00sv00i00rpgb00g00df00n00sbk00m00rx00i00glcku00q00xg00c00oyj00t00gd00z00crvn00o00qu00e00fru00e00ut00p00ovisfx00c00oy00h00vda00k00sp00u00xlgv00c00ur00g00sjr00e00me00o00cqxvp00q00ah00l00nrj00j00fl00k00pcix00v00gv00a00tru00n00wm00v00bergbark00l00mw00d00uyw00w00yt00u00ujom00t00gy00k00ckc00j00mn00j00rizer00f00xw00d00nlb00k00ju00l00ojgd00p00dl00e00fyn00q00ly00w00oromao00h00fe00r00teq00b00wc00s00xdpr00z00hs00s00tlp00y00ru00a00ivfrj00e00mm00x00csr00o00an00a00eovc00r00nm00y00eyk00g00ir00p00xijcpsf00n00rn00m00pzi00j00qx00y00yycw00b00qo00w00pzz00r00fb00v00xvyut00h00pp00d00dpa00y00oa00m00umki00f00xo00e00kem00z00ff00m00ruxavx00j00eh00d00uzq00q00kc00d00nnae00a00ak00y00gha00j00ac00x00atkhh00s00wq00j00fwl00o00xp00q00yddd00k00lj00i00ysh00f00yn00i00bdhgwrdbr00s00kx00a00moq00w00wa00w00myby00l00hx00f00kka00b00fd00c00xfstc00e00ll00a00prh00l00cz00n00rpgm00d00xf00y00vlf00i00yh00g00enbufd00l00xw00m00vwg00t00we00x00dael00a00mz00q00xfw00j00rc00f00hikke00l00km00o00rba00s00vm00a00ofpr00i00le00x00qbv00p00pj00f00gtzcdrv00y00jg00b00uzc00j00it00p00exxm00s00jz00q00euj00j00bn00g00vwtzt00e00dg00n00cqn00c00sy00m00ghlx00q00tm00f00dvj00t00ao00a00hshhoh00o00is00j00hfh00s00ke00y00bzyz00e00qs00w00cst00i00vg00q00jtkvh00p00lh00o00mne00a00lz00f00vucn00b00br00i00yyg00n00oq00y00wlmlodce00v00gw00d00kqy00m00vo00o00awbb00q00qz00c00mpk00f00bb00n00ekovk00x00vz00e00vlc00t00wh00e00ogbm00w00st00b00ekj00q00xj00c00yglxan00s00wt00n00krh00v00ug00h00onzv00m00rp00l00aor00l00vw00q00ttlrt00r00mf00p00omb00m00ei00r00wklf00m00df00s00krc00x00uc00n00panrhvj00f00py00q00knz00v00tb00z00pkrm00x00wy00u00mrt00u00vd00j00srawk00h00fg00g00gfc00g00fm00t00daju00j00lb00b00aef00r00nf00s00qhtzcj00e00ft00n00ykn00z00nt00o00irkl00j00on00r00iya00f00tk00s00htesd00h00be00c00kxf00m00iy00o00yfou00q00tq00h00iqh00v00om00s00indmdpkjbc00h00no00x00vut00j00kz00t00mrfw00w00tu00f00apo00t00yh00v00qqoab00y00oj00o00ytk00k00we00c00aolt00w00ug00w00bdd00m00kq00q00rwarwa00a00ks00b00emu00p00ta00o00vric00h00qw00n00aso00p00nb00r00wmatu00y00xi00x00boy00r00un00k00sauv00g00iq00a00dmn00g00ca00z00bbkhzbb00s00kg00z00yuw00d00ir00m00wjdq00s00ox00e00uef00s00ln00k00dalkg00a00jp00s00uwd00n00aq00b00jbjb00i00nk00t00fzo00a00ei00w00xriuky00n00kj00y00swy00h00gn00c00woye00h00hg00z00pgl00z00kk00f00jzfag00b00hg00z00ycp00u00bs00f00rcxx00p00ba00d00hxu00y00ki00w00gwxfsyam00n00ia00n00fsa00d00es00m00vksf00x00vf00e00xcq00p00pg00q00oqsby00k00ai00b00ahr00b00hy00s00rpgj00k00ny00p00xrb00u00js00i00hmqejw00w00um00u00ruh00t00ux00i00ejrg00g00aa00j00qmf00h00qw00f00uainc00p00xv00v00mac00x00pk00p00wwon00x00py00n00dlo00f00nw00x00fncwsoh00w00dw00u00hdx00j00wj00d00rlgg00o00kc00x00jsa00r00ah00m00orbcq00l00pm00l00hkf00p00wr00i00wcxz00m00xz00h00xaq00x00br00s00gvcuvp00l00hl00d00wdv00j00kd00o00imis00k00ve00m00vkv00t00ud00d00sdbnu00e00qb00i00hyq00s00dt00y00fmyg00y00xd00k00oni00m00tj00c00gpyctppat00a00hv00k00kbh00m00ha00g00fzcn00i00lk00c00rbj00i00yp00m00udpmf00u00qc00x00gkx00a00yc00q00ouks00u00vt00q00nsq00f00gu00k00jquscc00t00nz00k00yno00k00op00j00zyuz00g00jk00p00hxf00p00dn00h00agumf00i00hc00x00ynh00z00oe00s00yqrv00n00kv00f00klg00d00qd00t00uceqeda00p00gm00v00kua00y00cd00p00ptsv00t00ql00u00zsp00m00mc00u00dbmek00h00ql00p00qtb00q00rn00m00oyyx00a00mq00m00byl00m00cl00p00kykwok00z00jx00u00twr00z00jg00t00rcos00d00sr00w00izk00s00qz00x00vrman00x00ey00c00boi00t00fp00j00vqrm00p00uc00l00unq00y00tk00r00qczwblzp00l00kq00s00ihz00j00yr00b00chio00c00vh00g00ese00n00ua00a00ikbos00w00zr00a00inf00z00im00j00ypdp00b00tn00o00zpd00p00nc00r00knjeze00t00zh00r00nlo00v00ko00l00kcxr00b00rd00j00lyx00k00wy00t00jsalb00l00cm00w00mrp00w00fl00m00tjvm00c00pv00d00kza00g00il00w00brhdzfo00c00kf00d00aba00w00ex00o00jvoy00u00cg00v00law00i00tm00t00jdpqf00o00rk00r00jtn00j00kw00n00ixdj00m00oo00h00jzo00a00eb00z00xsbqwj00o00vp00b00vyu00x00wk00h00yqwj00h00xk00z00fek00v00yf00d00trvvy00m00yc00r00cod00i00vb00x00avcz00x00te00k00ieo00o00oh00q00");
	/* REMOVE COMMENTS FOR INPUT BASED (DONE FORGET the line above this as well)
	while(input!="exit"){
		getline(cin,input);
		t=new Tree(input);
	}*/
	system("pause");
}