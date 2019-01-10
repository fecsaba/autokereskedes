using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2018_11_05_AutoKereskedes.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var lista = new AdatEleres().ListAutoTipusok();
            lista.ForEach(t => System.Console.WriteLine(t.Megnevezes));
            System.Console.ReadKey();
        }
    }
}
