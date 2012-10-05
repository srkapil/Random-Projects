using System;
using System.Collections.Generic;
using System.Text;

namespace TimusProj
{
    public class Timus1585
    {
        String[] penguins;
        public  void Initialize(int allocationSize){
            penguins=new String[allocationSize];
        }
        public void InsertIntoArray(String val,int pos)
        {
            penguins[pos] = val;
        }
        public void processMostCount()
        {
            int c1=0,c2=0,c3=0;
            foreach (String s in penguins)
            {
                if(s.Equals("Emperor Penguin"))c1++;
                else if (s.Equals("Macaroni Penguin")) c2++;
                else if (s.Equals("Little Penguin")) c3++;
            }
            if (c1 > c2 && c1 > c3) Console.WriteLine("Emperor Penguin");
            else if (c2 > c1 && c2 > c3) Console.WriteLine("Macaroni Penguin");
            else Console.WriteLine("Little Penguin");
        }
        /*
        static void Main(String[] args)
        {
            int i = Convert.ToInt32(Console.ReadLine());
            Timus1585 t = new Timus1585();
            t.Initialize(i);
            for (int k = 0; k < i; k++)
            {
                t.InsertIntoArray(Console.ReadLine(),k);
            }
            //String[] test={"Emperor Penguin","Macaroni Penguin","Little Penguin","Emperor Penguin","Macaroni Penguin","Macaroni Penguin","Little Penguin"};
            t.processMostCount();
        }*/
    }
}
