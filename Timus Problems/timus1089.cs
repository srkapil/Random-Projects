using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace TimusProj
{
    class timus1089
    {
        public String[] dictionary;
        public int sizeOfDictionary;
        public int misSpeltWordsCounter;
        public int currentIndex;
        public timus1089()
        {
            currentIndex = 0;
            misSpeltWordsCounter = 0;
            sizeOfDictionary = 0;
        }
        public void buildDictionary(string dictString)
        {
            dictionary = dictString.Split('|');
            sizeOfDictionary = dictionary.Length - 1;
        }
        /*
         * String of text as input. Checks for spelling mistakes
         * and prints the corrected version of text on the screen.
         * -NEED TO IMPROVE THE METHOD-
         *  fails at some repeated words like -
         * cat cat cat -> catss catss catss
         * */
        public void correctMistakes(String text)
        {
            String result = text;
            String splitUp = " ~`_+|\\=-!@#$%^&*()123456790{}][:\"\';?><,.";
            String[] textWords = text.Split(splitUp.ToCharArray());//text.Split(' ', '.', ',', '-');
            String tmpWordHolder="";
            foreach (String s in textWords)
            {
                tmpWordHolder=compareWithDictionary(s);
                if (!tmpWordHolder.Equals(s))
                {
                    //Console.WriteLine("Replacing {0} with {1}", s, tmpWordHolder);
                    try
                    {
                        result = result.Replace(s+" ", tmpWordHolder+" "); 
                        misSpeltWordsCounter++;
                    }
                    catch (Exception e) { }
                    
                }
            }
            Console.WriteLine(result);
            Console.WriteLine(misSpeltWordsCounter);
        }

        /*
         * Compares the given word with the dictionary of words. If the word is
         * mis-spelled then the method will correct the word and returns a string of correct word.
         * In case of word is missing from the dictionary the method will just return the original word.
         * */
        public string compareWithDictionary(string word){
            for(int i=0;i<sizeOfDictionary;i++){
                bool misSpelt=compareWords(word.Trim(),dictionary[i]);
                if (misSpelt) return dictionary[i];
            }
            return word;
        }

        /*
          * The method can detect any of the following Examples:
          * 1) moonx -> moon 2) xmoon -> moon 3) mooz -> moon (NOT the mon -> moon & viceversa case)
          * The method will return true if the mistake is by one letter, otherwise false.
         */
        public bool compareWords(string word1, string word2)
        {
            int index = 0;
            int missCount = 0;
            int lengthDifference = Math.Abs(word1.Length - word2.Length);
            if (lengthDifference > 1) return false;
            else if (lengthDifference == 0)
            {
                foreach (char c in word1)
                {
                    if (missCount > 1) return false;
                    if (c != word2[index]) missCount++;
                    index++;
                }
                if (missCount == 1) return true;
                return false;
            }
            else
            {
                if (word1.Length > word2.Length)
                {
                    if (word2.CompareTo(word1.Substring(0, word2.Length)) == 0 || word2.CompareTo(word1.Substring(1, word2.Length)) == 0) return true;
                    else return false;
                }
                else
                {
                    if (word1.CompareTo(word2.Substring(0, word1.Length)) == 0 || word1.CompareTo(word2.Substring(1, word1.Length)) == 0) return true;
                    else return false;
                }
            }
        }
        
        static void Main(String[] args)
        {
            timus1089 t = new timus1089();
            StringBuilder sb = new StringBuilder();
            while(true){
                String temp= Console.ReadLine();
                if (temp.Equals("#")) break;
                else sb.Append(temp+"|");
            }
            t.buildDictionary(sb.ToString());
            t.correctMistakes(Console.In.ReadToEnd());

            /*TESTING 
           String[] dict ={ "country", "occupies", "surface", "covers", "russia", "largest", "europe", "part", "about", "world" };
           t.dictionary = dict;
           t.sizeOfDictionary = dict.Length;
           string msg = "the rushia is the larjest cauntry in the vorld. it ockupies ebout one-seventh of the earth's surfase. it kovers the eastern park of yurope and the northern park of asia.";
           t.correctMistakes(msg); */
            //if (t.compareWords("vorld", "vorl")) Console.WriteLine("TRUE WITH VORLD");
            //if (t.compareWords("mon", "moon")) Console.WriteLine(":)");
            Console.Read();
        }
    }
}

/*
         * The method can detect any of the following Examples:
         * 1) moonx -> moon 2) xmoon -> moon 3) mooz -> moon 4) moon -> mon 
         * The method will return true if the mistake is by one letter, otherwise false.
*/
/*

        public bool compareWordsExtra(string word1,string word2){
            int index=0;
            int missCount=0;
            int lengthDifference=Math.Abs(word1.Length - word2.Length);
            if (lengthDifference > 1) return false;
            else if (lengthDifference == 0)
            {
                foreach (char c in word1)
                {
                    if (missCount > 1) return false;
                    if (c == word2[index]) ;
                    else
                    {
                        missCount++;
                    }
                    index++;
                }
                if (missCount == 1) return true;
                return false;
            }
            else
            {
                if (word1.Length > word2.Length)
                {
                    if (word2.CompareTo(word1.Substring(0, word2.Length)) == 0 || word2.CompareTo(word1.Substring(1, word1.Length - 1)) == 0) return true;
                    else
                    {
                        //NOTE POSSIBLE MISTAKES
                        foreach (char c in word1)
                        {
                            if (missCount > 1) return false;
                            if (c != word2[index])
                            {
                                missCount++;
                                if (word1[index + 1] == word2[index]) {
                                    continue;//continue for the case at which words can be moon and mon 
                                } 
                                
                            }
                            index++;
                        }
                    }
                    if (missCount == 1) return true;
                    return false;
                }
                else
                {
                    if (word1.CompareTo(word2.Substring(0, word1.Length)) == 0 || word1.CompareTo(word2.Substring(1, word2.Length - 1)) == 0) return true;
                    else {
                        //NOTE POSSIBLE MISTAKES
                        foreach (char c in word2)
                        {
                            if (missCount > 1) return false;
                            if (c != word1[index])
                            {
                                missCount++;
                                if (word2[index + 1] == word1[index]) {
                                    continue;//continue for the case at which words can be moon and mon 
                                } 
                                
                            }
                            index++;
                        }
                    } 
                    if (missCount == 1) return true;
                    return false;
                }
            }
        }
*/