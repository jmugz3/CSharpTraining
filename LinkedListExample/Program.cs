using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkedListExample
{
    class Program
    {
        static void Main(string[] args)
        {
            LinkedList list = new LinkedList();

            Console.WriteLine("Is it empty? - {0}", list.Empty.ToString());
            Console.WriteLine("Count: {0} ", list.Count.ToString());
            

            list.Add("Test1");
            list.Add("Test2");
            list.Add(1,"Test3");

            list.Remove(1);
            list.Add(1, "Test4");

            list.Clear();

            list.Add("Hello");
            int index = list.IndexOf("Hello");


            bool contains = list.Contains("Test7");
            list.Add("Test7");
            contains = list.Contains("Test7");

            list.Add("Test5");
            list.Add("Test6");

            object test1 = list.Get(1);
            Console.WriteLine(test1.ToString());

            object test2 = list[2];
            Console.WriteLine(test2.ToString());


            Console.WriteLine("Is it empty? - {0}", list.Empty.ToString());
            Console.ReadLine();

        }
    }
}
