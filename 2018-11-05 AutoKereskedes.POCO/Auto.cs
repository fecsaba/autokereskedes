using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2018_11_05_AutoKereskedes.POCO
{
    // POCO: https://en.wikipedia.org/wiki/Plain_old_CLR_object

    public enum UzemanyagEnum {
        Benzin,
        Gazolaj,
        Elektromos,
        Hibrid
    }

    public class Auto
    {
        public int Id { get; set; }
        public string Rendszam { get; set; }
        public AutoTipus Tipus { get; set; }
        public string Alvazszam { get; set; }
        public string Motorszam { get; set; }
        public DateTime? ElsoForgalombaHelyezes { get; set; }
        public bool AutomataValto { get; set; }
        public int? KmOraAllas { get; set; }
        public UzemanyagEnum Uzemanyag { get; set; }
    }
}
