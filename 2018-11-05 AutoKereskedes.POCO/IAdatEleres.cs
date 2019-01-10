using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2018_11_05_AutoKereskedes.POCO
{
    public interface IAdatEleres
    {
        List<AutoTipus> ListAutoTipusok();
        List<Auto> ListAutok();
        Auto GetAuto(int id);
        void DeleteAuto(Auto a);
        Auto UpdateAuto(Auto a);
        Auto InsertAuto(Auto a);
    }
}
