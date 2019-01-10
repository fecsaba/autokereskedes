using _2018_11_05_AutoKereskedes.POCO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2018_11_05_AutoKereskedes
{
    public static class Extensions
    {
        public static string ToSor(this Auto a)
        {
            return string.Format("{0};{1};{2};{3};{4};{5};{6};{7};{8}",
                a.Id,
                a.Rendszam,
                a.Tipus.Id,
                a.Alvazszam,
                a.Motorszam,
                a.ElsoForgalombaHelyezes == null ?
                        "" :
                        ((DateTime)a.ElsoForgalombaHelyezes).ToString("yyyyMMdd"),
                a.AutomataValto ? "1" : "0",
                a.KmOraAllas,
                (int)a.Uzemanyag);
        }
    }
}
