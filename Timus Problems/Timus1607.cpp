#include <iostream>
using namespace std;

long petrStart, driverStart, petrIncr, driverDecr;

void Xmain(){
	bool loopstart=true;
	cin>>petrStart>>petrIncr>>driverStart>>driverDecr;
	if(petrStart>=driverStart){
		loopstart=false;
	}
	while(loopstart){
		if(driverStart<petrStart){
			//cout<<"LOOP BROKEN";
			break;
		}
		else if(petrStart+petrIncr>driverStart){
			petrStart=driverStart;
			break;
		}

		else{
			petrStart+=petrIncr;
			driverStart-=driverDecr;
			//cout<<petrStart<<"	"<<driverStart<<endl;
		}
	}
	cout<<(petrStart);
	system("pause");
	
}