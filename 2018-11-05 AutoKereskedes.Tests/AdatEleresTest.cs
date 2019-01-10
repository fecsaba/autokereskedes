using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using _2018_11_05_AutoKereskedes.POCO;
using System.Linq;

namespace _2018_11_05_AutoKereskedes.Tests
{
    [TestClass]
    public class AdatEleresTest
    {
        [TestInitialize]
        public void Init()
        {
            StreamWriter sw = new StreamWriter("AutoTipusok.txt");
            sw.WriteLine("1;Lada");
            sw.WriteLine("2;Audi");
            sw.WriteLine("3;BMW");
            sw.WriteLine("4;Mercedes");
            sw.Close();

            sw = new StreamWriter("Autok.txt");
            sw.WriteLine("1;ABC123;2;AlvazNo1;MotorNo1;;1;;0");
            sw.WriteLine("2;XYZ987;3;No2Alvaz;No2Motor;20180901;1;6500;1");
            sw.Close();
        }

        [TestCleanup]
        public void Cleanup()
        {
            File.Delete("AutoTipusok.txt");
            File.Delete("Autok.txt");
        }

        [TestMethod]
        public void AutoTipusokBeolvasasa()
        {
            //arrange
            var sut = new AdatEleres();

            //act
            var lista = sut.ListAutoTipusok();

            //assert
            Assert.AreEqual(4, lista.Count);
            Assert.AreEqual(2, lista[1].Id);
            Assert.AreEqual("Audi", lista[1].Megnevezes);
        }

        [TestMethod]
        public void AutokBeolvasasa()
        {
            //arrange
            var sut = new AdatEleres();

            //act
            var lista = sut.ListAutok();

            //assert
            Assert.AreEqual(2, lista.Count);
            Assert.AreEqual(2, lista[1].Id);
            Assert.AreEqual("ABC123", lista[0].Rendszam);
            Assert.AreEqual("BMW", lista[1].Tipus.Megnevezes);
            Assert.AreEqual("AlvazNo1", lista[0].Alvazszam);
            Assert.AreEqual("No2Motor", lista[1].Motorszam);
            Assert.IsNull(lista[0].ElsoForgalombaHelyezes);
            Assert.AreEqual(2018, ((DateTime)lista[1].ElsoForgalombaHelyezes).Year);
            Assert.IsTrue(lista[0].AutomataValto);
            Assert.IsNull(lista[0].KmOraAllas);
            Assert.AreEqual(6500, lista[1].KmOraAllas);
            Assert.AreEqual(UzemanyagEnum.Benzin, lista[0].Uzemanyag);
        }

        [TestMethod]
        public void EgyAutoLekerdezes()
        {
            //arrange
            var sut = new AdatEleres();

            //act
            var auto = sut.GetAuto(2);

            //assert
            Assert.AreEqual(2, auto.Id);
            Assert.AreEqual(6500, auto.KmOraAllas);
        }

        [TestMethod]
        public void NemLetezoAuto()
        {
            var sut = new AdatEleres();
            var auto = sut.GetAuto(-1);
            Assert.IsNull(auto);
        }

        [TestMethod]
        public void UjAuto()
        {
            //arrange
            var sut = new AdatEleres();

            //act
            var auto = new Auto()
            {
                Rendszam = "JEDLIK1",
                Tipus = new AutoTipus() { Id = 4 },
                Alvazszam = "ABCDEF12345",
                Motorszam = "987654321",
                ElsoForgalombaHelyezes = new DateTime(2018, 11, 10),
                AutomataValto = true,
                KmOraAllas = 15,
                Uzemanyag = UzemanyagEnum.Hibrid
            };
            var ujAuto = sut.InsertAuto(auto);

            //assert
            Assert.IsNotNull(ujAuto);
            Assert.AreNotEqual(0, ujAuto.Id);
            Assert.AreEqual("Mercedes", ujAuto.Tipus.Megnevezes);

            string sor = File.ReadLines("autok.txt").Last();
            Assert.AreEqual("3;JEDLIK1;4;ABCDEF12345;987654321;20181110;1;15;3", sor);
            Init();
        }

        [TestMethod]
        public void AutoModositas()
        {
            //arrange
            var sut = new AdatEleres();

            //act
            var auto = new Auto()
            {
                Id = 1,
                Rendszam = "ABC123",
                Tipus = new AutoTipus() { Id = 2 },
                Alvazszam = "UjAlvazSzam",
                Motorszam = "MotorNo1",
                ElsoForgalombaHelyezes = new DateTime(2018, 11, 26),
                AutomataValto = true,
                KmOraAllas = 100000,
                Uzemanyag = UzemanyagEnum.Benzin
            };
            sut.UpdateAuto(auto);

            //assert
            string sor = File.ReadLines("autok.txt").First();
            Assert.AreEqual("1;ABC123;2;UjAlvazSzam;MotorNo1;20181126;1;100000;0", sor);
            Init();
        }

        [TestMethod]
        public void AutoModositas2()
        {
            var sut = new AdatEleres();

            var auto = sut.GetAuto(2);
            auto.Rendszam = "KING01";

            sut.UpdateAuto(auto);
            string sor = File.ReadLines("autok.txt").ToList()[1];
            Assert.AreEqual("2;KING01;3;No2Alvaz;No2Motor;20180901;1;6500;1", sor);
            Init();
        }

        [TestMethod]
        public void AutoTorles()
        {
            var sut = new AdatEleres();
            var auto = sut.GetAuto(1);
            sut.DeleteAuto(auto);

            string sor = File.ReadLines("autok.txt").First();
            Assert.IsTrue(sor.StartsWith("2;"));
            Init();
        }
    }
}