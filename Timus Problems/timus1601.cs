using System;
using System.Collections.Generic;
using System.Text;

namespace TimusProj
{
    class timus1601
    {
        public void NormalizeMessage(String message)
        {
            StringBuilder sb=new StringBuilder();
            
            for (int i = 0; i < message.Length; i++)
            {
                if (i == 0)
                {
                    if (Char.IsLetter(message[0]))
                    {
                        sb.Append(message[i]);
                        continue;
                    }
                    else
                    {
                        //find the index at which a letter occurs
                        while (!Char.IsLetter(message[i]))
                        {
                            sb.Append(message[i]);
                            i++;
                            if (i >= message.Length) break;
                        }
                        if (i < message.Length) sb.Append(message[i]);
                    }
                }
                else if(message[i]=='.' || message[i]=='!' || message[i]=='?'){
                    while(!Char.IsLetter(message[i])){
                        sb.Append(message[i]);
                        i++;
                        if (i >= message.Length) break;
                    }
                    if (i < message.Length) sb.Append(message[i]);
                }
                else sb.Append(Char.ToLower(message[i]));
            }
            Console.WriteLine(sb.ToString());
        }
        /*
        static void Main(String[] args)
        {
            String message = Console.In.ReadToEnd();
            timus1601 t = new timus1601();
            t.NormalizeMessage(message);
            Console.Read();
        }*/
    }

}
