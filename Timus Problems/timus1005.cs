using System;
using System.Collections.Generic;
using System.Text;
/*
 * NOTES:
 * Use absolute values to check for differences
 * It will be n^2 run?
 * But it should be by checking all the possible sums and outputting the minimal
 * (Use the findMinPossible)
 * */
namespace TimusProj
{
    class timus1005
    {
        public int[] myArray;
        private int[] sumArray;
        private int arraySize;
        public void arrayInitialize(int capacity)
        {
            myArray = new int[capacity]; 
            arraySize=capacity;
        }
        /*
         * Brute force with all possible sums :S
         * */
        public void findMinPossible()
        {
            int minSum = myArray[0];
            for (int i=0;i<myArray.Length;i++)
            {
                for (int j=0;j<myArray.Length;j++)
                {

                }
             
            }
        }

        /*
         * Given a divider index returns the difference between the left and right sum 
         * of the dividing index.
         * */
        public int getDividedSum(int divIndex, int finalIndex)
        {
            int sum1 = 0;
            int sum2 = 0;
            for (int i = divIndex; i < finalIndex; i++) sum1 += myArray[i];
            for (int i = 0; i < divIndex; i++) sum2 += myArray[i];
            return Math.Abs(sum2-sum1);
        }

        /*
        static void Main(String[] args)
        {
            int[] myArray ={ 1, 2, 3, 4, 5 };
            timus1005 t = new timus1005();
            Console.WriteLine(t.getSum(myArray,0,5));

            int arraySize = Convert.ToInt32(Console.ReadLine());
            String s = Console.ReadLine();
            String[] tokens = s.Split(' ');
            t.arrayInitialize(arraySize);
            int i = 0;
            foreach (String st in tokens)
            {
                t.myArray[i] = Convert.ToInt32(st); i++;
            }
            Console.Read();
        } */
    }
}
