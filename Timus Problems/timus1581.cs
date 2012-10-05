using System;
using System.Collections.Generic;
using System.Text;

namespace TimusProj
{
    class timus1581
    {
        string input;
        void getInput()
        {
            input = "";
            int arraySize = Convert.ToInt32(Console.ReadLine());
            input = Console.ReadLine();
            String[] tokens=input.Split(' ');
            int i = 0;
            int[] numArray = new int[arraySize];
            foreach(String tk in tokens){
                numArray[i]=Convert.ToInt32(tk);
                i++;
            }
            processIntArray(numArray);
        }
        void processIntArray(int[] myArray)
        {
            int k=0;
            int counter=1;

            foreach (int i in myArray)
            {
                if ((k + 1) >= myArray.Length )
                {
                    Console.Write("{0} {1}", counter, i);
                    break;
                }
                else if (i == myArray[k + 1])
                {
                    counter++;
                }
                else
                {
                    Console.Write("{0} {1} ", counter, i);
                    counter = 1;
                }
                k++;
            }
            
        }

        public void startUp()
        {
            getInput();
        }
        /*
        static void Main(string[] args)
        {
            timus1581 t = new timus1581();
            t.startUp();
            Console.ReadLine();
        }*/
    }
}
