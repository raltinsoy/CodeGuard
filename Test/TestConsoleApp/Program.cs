using Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var person = new Person(5, "Ramazan");
            var key = person.GetKey();
            var name = person.Name;

            Console.WriteLine("Name: " + name);
            Console.WriteLine("Key: " + key);
        }
    }
}
