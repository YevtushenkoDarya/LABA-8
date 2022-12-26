using System;
using System.Text;

namespace anagrams
{
    public static class StringExtensions
    {
        public static String Sort(this String input)
        {
            char[] chars = input.ToCharArray();
            Array.Sort(chars);
            return new String(chars);
        }
    }
    public class Program
    {
        static string DeleteDublicat(string str)
        {
            StringBuilder sb = new StringBuilder();
            str = str.ToUpper();
            str = str.Sort();
            for (int i = 1; i < str.Length; i++)
            {
                if (str[i] != str[i - 1]) sb.Append(str[i]);
            }
            return sb.ToString();
        }
        static void Main()
        {
            string[] strings = {"code", "ecod", "framer", "doce", "frame"};
            List<string> list = new List<string>();
            list.Add(strings[0]);
            for (int i = 1; i < strings.Length; i++)
            {
                for (int j = 0; j < list.Count; j++)
                {
                    if (DeleteDublicat(strings[i]) == DeleteDublicat(list[j]) && strings[i].Length == list[j].Length) break;
                    else if (DeleteDublicat(strings[i]) == DeleteDublicat(list[j]))
                    {
                        list.Add(strings[i]);
                    }
                    else if(j==list.Count-1) list.Add(strings[i]); 
                }
            }
            list.Sort();
            foreach (string str in list)
                Console.Write(str + " ");
        }
    }
}