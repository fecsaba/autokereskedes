using _2018_11_05_AutoKereskedes.POCO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2018_11_05_AutoKereskedes
{
    public class AdatEleres : IAdatEleres
    {
        public List<AutoTipus> ListAutoTipusok()
        {
            var lista = new List<AutoTipus>();
            StreamReader sr = new StreamReader("AutoTipusok.txt");
            while (!sr.EndOfStream)
            {
                string[] adatok = sr.ReadLine().Split(';');
                lista.Add(new AutoTipus()
                {
                    Id = int.Parse(adatok[0]),
                    Megnevezes = adatok[1]
                });
            }
            sr.Close();
            return lista;
        }

        public List<Auto> ListAutok()
        {
            List<AutoTipus> autoTipusok = this.ListAutoTipusok();
            List<Auto> autok = new List<Auto>();
            StreamReader sr = new StreamReader("Autok.txt");
            while (!sr.EndOfStream)
            {
                string[] adatok = sr.ReadLine().Split(';'); //9 elem !!!
                Auto auto = AutoBetoltes(adatok, autoTipusok);
                autok.Add(auto);
            }
            sr.Close();
            return autok;
        }

        private Auto AutoBetoltes(string[] adatok, List<AutoTipus> autoTipusok)
        {
            Auto auto = new Auto()
            {
                Id = int.Parse(adatok[0]),
                Rendszam = adatok[1],
                Tipus = autoTipusok.Where(a => a.Id.ToString() == adatok[2]).FirstOrDefault(),
                Alvazszam = adatok[3],
                Motorszam = adatok[4],
                AutomataValto = adatok[6] == "1",
                KmOraAllas = string.IsNullOrEmpty(adatok[7]) ? null : (int?)int.Parse(adatok[7]),
                Uzemanyag = (UzemanyagEnum)int.Parse(adatok[8])
            };
            try
            {
                //20180901
                int ev = int.Parse(adatok[5].Substring(0, 4));
                int honap = int.Parse(adatok[5].Substring(4, 2));
                int nap = int.Parse(adatok[5].Substring(6, 2));
                auto.ElsoForgalombaHelyezes = new DateTime(ev, honap, nap);
            }
            catch
            {
                auto.ElsoForgalombaHelyezes = null;
            }
            return auto;
        }

        public Auto GetAuto(int id)
        {
            Auto auto = null;
            StreamReader sr = new StreamReader("autok.txt");
            while (!sr.EndOfStream && auto == null)
            {
                string[] adatok = sr.ReadLine().Split(';');
                if (adatok[0] == id.ToString())
                {
                    auto = AutoBetoltes(adatok, ListAutoTipusok());
                }
            }
            sr.Close();
            return auto;
        }

        public void DeleteAuto(Auto a)
        {
            StreamReader sr = new StreamReader("autok.txt");
            StreamWriter sw = new StreamWriter("autok_uj.txt");
            while (!sr.EndOfStream)
            {
                string sor = sr.ReadLine();
                string id = sor.Split(';')[0];
                if (id != a.Id.ToString())
                {
                    sw.WriteLine(sor);
                }
            }
            sw.Close();
            sr.Close();
            File.Delete("autok.txt");
            File.Move("autok_uj.txt", "autok.txt");
        }

        public Auto InsertAuto(Auto a)
        {
            a.Id = int.Parse(File.ReadLines("autok.txt").Last().Split(';')[0]) + 1;            
            StreamWriter sw = new StreamWriter("autok.txt", true);
            sw.WriteLine(a.ToSor());
            sw.Close();
#if DEBUG
            return GetAuto(a.Id);
#else
            return a;
#endif
        }

        public Auto UpdateAuto(Auto a)
        {
            StreamReader sr = new StreamReader("autok.txt");
            StreamWriter sw = new StreamWriter("autok_uj.txt");
            while (!sr.EndOfStream)
            {
                string sor = sr.ReadLine();
                string id = sor.Split(';')[0];
                if (id == a.Id.ToString())
                {
                    sw.WriteLine(a.ToSor());
                }
                else
                {
                    sw.WriteLine(sor);
                }
            }
            sw.Close();
            sr.Close();
            File.Delete("autok.txt");
            File.Move("autok_uj.txt", "autok.txt");

            return GetAuto(a.Id);
        }
    }
}
