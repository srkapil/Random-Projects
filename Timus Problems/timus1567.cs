using System;
using System.Collections.Generic;
using System.Text;

namespace TimusProj
{
    class Timus1567
    {
        void calculateCost(String phrase)
        {
            int costCount = 0;
            foreach(char c in phrase){
                if (c == ' ') costCount++;
                else if (c == '.') costCount++;
                else if (c == ',') costCount += 2;
                else if (c == '!') costCount += 3;
                else
                {
                    costCount += (c - 'a') % 3 + 1;
                    //Console.Write("|" + (c - 'a')%3);
                }
            }
            Console.WriteLine(costCount);
        }
        /*
        static void Main(string[] args)
        {
            Timus1567 t = new Timus1567();
            t.calculateCost(Console.ReadLine());
        }*/
    }

}
