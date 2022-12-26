using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace Labarotory
{
    public class Bills
    {
        //  класс для хранения информации о  операциях
        public long Date { get; set; }
        public int sum = 0;
        public string transaction { get; set; }
    }
    class CompareDate : IComparer
    {
        public int Compare(object obj1, object obj2)
        {
            var first = (Bills)obj1;
            var second = (Bills)obj2;
            return first.Date.CompareTo(second.Date);
        }
    }
    public class Programm
    {
        static string NameProject = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
        public static void Sorting(Array bills, IComparer comparer)
        {
            for (int i = bills.Length - 1; i > 0; i--)
                for (int j = 1; j <= i; j++)
                {
                    object obj1 = bills.GetValue(j - 1);
                    object obj2 = bills.GetValue(j);
                    if (comparer.Compare(obj1, obj2) < 0)
                    {
                        object temporary = bills.GetValue(j);
                        bills.SetValue(bills.GetValue(j - 1), j);
                        bills.SetValue(temporary, j - 1);
                    }
                }
        }
        static string ChangeDate(string date)
        {
            date = date.Replace(":", "");
            date = date.Replace("-", "");
            date = date.Replace(" ", "");
            return date;
        }
        static void PutDataToList(string line, List<Bills> bills)
        {
            /*Метод записывает информацию в bills*/
            line = ChangeDate(line);
            string[] lines = line.Split('|');
            Bills bill = new Bills();
            if (lines.Length == 3)
            {
                bill.Date = long.Parse(lines[0]);
                bill.sum = int.Parse(lines[1]);
                bill.transaction = lines[2];
            }
            else
            {
                // ветка созданна  revert
                bill.Date = long.Parse(lines[0]);
                bill.transaction = lines[1];
            }
            bills.Add(bill);
        }
        public static string Count(string date, int sum, Bills[] bills)
        {
            bool firstTime = false;
            /*Создаём флаг firstTime который позволяет нам отслеживать было ли встреченно 
                 время введенное пользователем, если да, то флаг ползволяет на реализовать выход 
                 из цикла после встречи новго времени  */
            int lastNumber = 0;
            int length = bills.Length;
            while (sum > 0)
            {
                // При сумме отрицательной цикл завершается и программа выводит сообщение об ошибке
                if (bills[length - 1].Date == long.Parse(date)) firstTime = true;
                else if (firstTime && bills[length - 1].Date != long.Parse(date)) break;
                if (bills[length - 1].transaction == "in")
                {
                    sum += bills[length - 1].sum;
                    lastNumber = bills[length - 1].sum;
                }
                if (bills[length - 1].transaction == "out")
                {
                    sum -= bills[length - 1].sum;
                    lastNumber = bills[length - 1].sum * (-1);
                }
                if (bills[length - 1].transaction == "revert") sum -= lastNumber;
                length--;
            }
            if (sum < 0) throw new ArgumentException("sum < 0");
            else return "Итог :" + sum;
        }
        static void Main()
        {
            Console.WriteLine("Введите дату используя разделите между ними :");
            string dateOfCheck = Console.ReadLine();
            dateOfCheck = ChangeDate(dateOfCheck);

            List<Bills> bills = new List<Bills>();

            int sum = 0;

            string path = NameProject + "\\example2.txt";
            foreach (string line in File.ReadLines(path))
            {
                if (sum == 0)
                {
                    /*В первой строке всегда храниться начальная сумма, из-за этого её нельзя передавать в метод*/
                    sum = int.Parse(line);
                    continue;
                }
                PutDataToList(line, bills);
            }
            Bills[] mass = new Bills[bills.Count];
            for (int i = 0; i < bills.Count; i++)
            {
                mass[i] = new Bills();
                mass[i] = bills[i];
            }
            Sorting(mass, new CompareDate());
            Console.WriteLine(Count(dateOfCheck, sum, mass));
        }
    }
}