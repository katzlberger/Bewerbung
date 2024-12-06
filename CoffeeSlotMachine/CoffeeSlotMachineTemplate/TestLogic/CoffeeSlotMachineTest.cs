
using Logic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestLogic
{
    /// <summary>
    ///This is a test class for CoffeeSlotMachineTest and is intended
    ///to contain all CoffeeSlotMachineTest Unit Tests
    ///</summary>
    [TestClass()]
    public class CoffeeSlotMachineTest
    {

        /// <summary>
        /// Vergleich zweier Arrays gleichen Typs mit vergleichbaren Elementen
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        private static bool CompareArrays<T>(T[] first, T[] second) where T : IComparable<T>
        {
            if (first.Length != second.Length)
            {
                return false;
            }
            for (int i = 0; i < first.Length; i++)
            {
                if (first[i].CompareTo(second[i]) != 0)
                {
                    return false;
                }
            }
            return true;
        }

        [TestMethod()]
        public void T01_DefaultConstructor()
        {
            CoffeeSlotMachine coffeeSlotMachine = new CoffeeSlotMachine();
            Assert.AreEqual(18, coffeeSlotMachine.CoinsInDepot, "Je Wert sollen 3 Münzen im Depot sein");
            Assert.AreEqual(3, coffeeSlotMachine.ProductsAvailable, "Defaultkonstruktor soll drei Produkte anlegen");
        }

        [TestMethod()]
        public void T02_OwnConstructor()
        {
            int[] coins = { 1, 2, 3, 4, 5, 6 };
            string[] productNames = { "Kaffee", "Tee", "Suppe", "Milch" };
            CoffeeSlotMachine coffeeSlotMachine = new CoffeeSlotMachine(coins, productNames);
            Assert.AreEqual(21, coffeeSlotMachine.CoinsInDepot, "1-6 Münzen sollen im Depot sein");
            Assert.AreEqual(4, coffeeSlotMachine.ProductsAvailable, "Vier Produkte anlegen");
        }

        [TestMethod()]
        public void T03_OwnConstructorAllEmpty()
        {
            int[] coins = new int[0];
            string[] productNames = new string[0];
            CoffeeSlotMachine coffeeSlotMachine = new CoffeeSlotMachine(coins, productNames);
            Assert.AreEqual(0, coffeeSlotMachine.CoinsInDepot, "Leeres Depot");
            Assert.AreEqual(0, coffeeSlotMachine.ProductsAvailable, "Keine Produkte");
        }

        [TestMethod()]
        public void T04_NormalOrder()
        {
            int[] returnCoins;
            int donation;
            int[] returnCoinsExpected = { 0, 0, 0, 1, 0, 0 };  // 1* 50 cent
            int[] coins = { 1, 2, 3, 4, 5, 6 };
            string[] productNames = { "Kaffee", "Tee", "Suppe", "Milch" };
            CoffeeSlotMachine coffeeSlotMachine = new CoffeeSlotMachine(coins, productNames);
            bool ok = coffeeSlotMachine.InsertCoin(100);
            Assert.IsTrue(ok, "InsertCoin sollte funktionieren");
            ok = coffeeSlotMachine.SelectProduct("Kaffee", out returnCoins, out donation);
            Assert.IsTrue(ok, "SelectProduct sollte funktionieren");
            Assert.IsTrue(CompareArrays(returnCoins, returnCoinsExpected), "Arrays mit Retourgeld sind nicht gleich");
            Assert.AreEqual(0, donation, "Es konnte das gesamte Restgeld zurückgegeben werden");
            Assert.AreEqual(21, coffeeSlotMachine.CoinsInDepot, "100 cent hinein und 50 cent hinaus");
        }

        [TestMethod()]
        public void T05_ThreeOrders200Cent()
        {
            int[] returnCoins;
            int donation;
            int[] coins = { 0, 0, 0, 3, 3, 3 };
            string[] productNames = { "Kaffee", "Tee", "Suppe", "Milch" };
            CoffeeSlotMachine coffeeSlotMachine = new CoffeeSlotMachine(coins, productNames);
            coffeeSlotMachine.InsertCoin(200);
            coffeeSlotMachine.SelectProduct("Kaffee", out returnCoins, out donation);
            coffeeSlotMachine.InsertCoin(200);
            coffeeSlotMachine.SelectProduct("Kaffee", out returnCoins, out donation);
            coffeeSlotMachine.InsertCoin(200);
            coffeeSlotMachine.SelectProduct("Kaffee", out returnCoins, out donation);
            Assert.AreEqual(0, donation, "Es konnte das gesamte Restgeld zurückgegeben werden");
            Assert.AreEqual(6, coffeeSlotMachine.CoinsInDepot, "6 * 200 cent");
        }

        [TestMethod()]
        public void T06_FourOrders200CentWithDonation()
        {
            int[] returnCoins;
            int donation;
            int[] returnCoinsExpected = { 0, 0, 0, 0, 0, 0 };  // 1 * 100 und 1* 50 cent
            int[] coins = { 0, 0, 0, 3, 3, 3 };
            string[] productNames = { "Kaffee", "Tee", "Suppe", "Milch" };
            CoffeeSlotMachine coffeeSlotMachine = new CoffeeSlotMachine(coins, productNames);
            coffeeSlotMachine.InsertCoin(200);
            coffeeSlotMachine.SelectProduct("Kaffee", out returnCoins, out donation);
            coffeeSlotMachine.InsertCoin(200);
            coffeeSlotMachine.SelectProduct("Kaffee", out returnCoins, out donation);
            coffeeSlotMachine.InsertCoin(200);
            coffeeSlotMachine.SelectProduct("Kaffee", out returnCoins, out donation);
            coffeeSlotMachine.InsertCoin(200);
            bool ok = coffeeSlotMachine.SelectProduct("Kaffee", out returnCoins, out donation);
            Assert.IsTrue(ok, "SelectProduct sollte funktionieren");
            Assert.IsTrue(CompareArrays(returnCoins, returnCoinsExpected), "Es bleiben keine passenden Münzen mehr übrig");
            Assert.AreEqual(150, donation, "Kein Retourgeld mehr");
            Assert.AreEqual(7, coffeeSlotMachine.CoinsInDepot, "7 * 200 cent");
        }

        [TestMethod()]
        public void T07_FourOrders200CentWithLessDonation()
        {
            int[] returnCoins;
            int donation;
            int[] returnCoinsExpected = { 2, 3, 4, 0, 0, 0 };
            int[] coins = { 2, 3, 4, 3, 3, 3 };
            string[] productNames = { "Kaffee", "Tee", "Suppe", "Milch" };
            CoffeeSlotMachine coffeeSlotMachine = new CoffeeSlotMachine(coins, productNames);
            coffeeSlotMachine.InsertCoin(200);
            coffeeSlotMachine.SelectProduct("Kaffee", out returnCoins, out donation);
            coffeeSlotMachine.InsertCoin(200);
            coffeeSlotMachine.SelectProduct("Kaffee", out returnCoins, out donation);
            coffeeSlotMachine.InsertCoin(200);
            coffeeSlotMachine.SelectProduct("Kaffee", out returnCoins, out donation);
            coffeeSlotMachine.InsertCoin(200);
            bool ok = coffeeSlotMachine.SelectProduct("Kaffee", out returnCoins, out donation);
            Assert.IsTrue(ok, "SelectProduct sollte funktionieren");
            Assert.IsTrue(CompareArrays(returnCoins, returnCoinsExpected), "Ganzes Kleingeld wurde zurückgegeben");
            Assert.AreEqual(30, donation, "30 cent fehlen beim retourgeld");
            Assert.AreEqual(7, coffeeSlotMachine.CoinsInDepot, "7 * 200 cent");
        }

        [TestMethod()]
        public void T08_FourOrders200CentEmptyMachine()
        {
            int[] returnCoins;
            int donation;
            int[] coins = { 2, 3, 4, 3, 3, 3 };
            string[] productNames = { "Kaffee", "Tee", "Suppe", "Milch" };
            CoffeeSlotMachine coffeeSlotMachine = new CoffeeSlotMachine(coins, productNames);
            coffeeSlotMachine.InsertCoin(200);
            coffeeSlotMachine.SelectProduct("Kaffee", out returnCoins, out donation);
            coffeeSlotMachine.InsertCoin(200);
            coffeeSlotMachine.SelectProduct("Kaffee", out returnCoins, out donation);
            coffeeSlotMachine.InsertCoin(200);
            coffeeSlotMachine.SelectProduct("Kaffee", out returnCoins, out donation);
            coffeeSlotMachine.InsertCoin(200);
            coffeeSlotMachine.SelectProduct("Kaffee", out returnCoins, out donation);
            int cents = coffeeSlotMachine.EmptyDepot();
            Assert.AreEqual(1400, cents, "7 * 200 Cent");
            Assert.AreEqual(0, coffeeSlotMachine.CoinsInDepot, "Depot wurde geleert");
            cents = coffeeSlotMachine.EmptyDepot();
            Assert.AreEqual(0, cents, "Depot wurde gerade geleert");
        }

        [TestMethod()]
        public void T09_IllegalCoins()
        {
            int[] coins = { 1, 2, 3, 4, 5, 6 };
            string[] productNames = { "Kaffee", "Tee", "Suppe", "Milch" };
            CoffeeSlotMachine coffeeSlotMachine = new CoffeeSlotMachine(coins, productNames);
            bool ok = coffeeSlotMachine.InsertCoin(7);
            Assert.IsFalse(ok, "7 ist keine gültige Münze");
            ok = coffeeSlotMachine.InsertCoin(2);
            Assert.IsFalse(ok, "2 ist keine gültige Münze");
            ok = coffeeSlotMachine.InsertCoin(20);
            Assert.IsTrue(ok, "20 ist eine gültige Münze");
            ok = coffeeSlotMachine.InsertCoin(20);
            Assert.IsTrue(ok, "20 ist eine gültige Münze");
            ok = coffeeSlotMachine.InsertCoin(10);
            Assert.IsTrue(ok, "10 ist eine gültige Münze");
            ok = coffeeSlotMachine.InsertCoin(10);
            Assert.IsFalse(ok, "Es wurden bereits 50 Cent eingeworfen");
        }


        [TestMethod()]
        public void T10_MoreCoins()
        {
            int[] returnCoins;
            int donation;
            int[] coins = { 1, 2, 3, 4, 5, 6 };
            string[] productNames = { "Kaffee", "Tee", "Suppe", "Milch" };
            CoffeeSlotMachine coffeeSlotMachine = new CoffeeSlotMachine(coins, productNames);
            bool ok = coffeeSlotMachine.InsertCoin(20);
            Assert.IsTrue(ok, "20 ist eine gültige Münze");
            ok = coffeeSlotMachine.InsertCoin(20);
            Assert.IsTrue(ok, "20 ist eine gültige Münze");
            ok = coffeeSlotMachine.InsertCoin(10);
            Assert.IsTrue(ok, "10 ist eine gültige Münze");
            Assert.AreEqual(21, coffeeSlotMachine.CoinsInDepot, "Münzen wurden noch nicht übernommen");
            ok = coffeeSlotMachine.SelectProduct("Tee", out returnCoins, out donation);
            Assert.IsTrue(ok, "Auswahl Tee sollte funktionieren");
            Assert.AreEqual(24, coffeeSlotMachine.CoinsInDepot, "3 Münzen dazu eingenommen");
        }

        [TestMethod()]
        public void T11_MoreCoinsAndCancel()
        {
            int[] returnCoinsExpected = { 0, 1, 2, 0, 0, 0 };
            int[] returnCoins;
            int[] coins = { 1, 2, 3, 4, 5, 6 };
            string[] productNames = { "Kaffee", "Tee", "Suppe", "Milch" };
            CoffeeSlotMachine coffeeSlotMachine = new CoffeeSlotMachine(coins, productNames);
            bool ok = coffeeSlotMachine.InsertCoin(20);
            Assert.IsTrue(ok, "20 ist eine gültige Münze");
            ok = coffeeSlotMachine.InsertCoin(20);
            Assert.IsTrue(ok, "20 ist eine gültige Münze");
            ok = coffeeSlotMachine.InsertCoin(10);
            Assert.IsTrue(ok, "10 ist eine gültige Münze");
            Assert.AreEqual(21, coffeeSlotMachine.CoinsInDepot, "Münzen wurden noch nicht übernommen");
            returnCoins = coffeeSlotMachine.CancelOrder();
            Assert.IsTrue(CompareArrays(returnCoins, returnCoinsExpected), "Stornierte Münzen stimmen nicht");
            Assert.AreEqual(21, coffeeSlotMachine.CoinsInDepot, "Alter Zustand bleibt erhalten");

        }

        [TestMethod()]
        public void T12_ProductsCounter()
        {
            int[] returnCoins;
            int donation;
            int[] coins = { 2, 3, 4, 3, 3, 3 };
            string[] productNames = { "Kaffee", "Tee", "Suppe", "Milch" };
            CoffeeSlotMachine coffeeSlotMachine = new CoffeeSlotMachine(coins, productNames);
            coffeeSlotMachine.InsertCoin(200);
            coffeeSlotMachine.SelectProduct("Kaffee", out returnCoins, out donation);
            coffeeSlotMachine.InsertCoin(100);
            coffeeSlotMachine.SelectProduct("Kaffee", out returnCoins, out donation);
            coffeeSlotMachine.InsertCoin(100);
            bool ok = coffeeSlotMachine.SelectProduct("tee", out returnCoins, out donation);
            Assert.IsFalse(ok, "tee gibt es nicht, nur Tee");
            coffeeSlotMachine.SelectProduct("Tee", out returnCoins, out donation);
            coffeeSlotMachine.InsertCoin(50);
            coffeeSlotMachine.SelectProduct("Suppe", out returnCoins, out donation);
            int counter;
            coffeeSlotMachine.GetCounterForProduct("Kaffee", out counter);
            Assert.AreEqual(2, counter, "Es wurde 2 * Kaffee bestellt");
            ok = coffeeSlotMachine.GetCounterForProduct("irgendwas", out counter);
            Assert.IsFalse(ok, "Produkt irgendwas ist nicht vorhanden");
            Assert.AreEqual(0, counter, "Im Fehlerfall 0 zurückgeben");
            coffeeSlotMachine.GetCounterForProduct("Suppe", out counter);
            Assert.AreEqual(1, counter, "Suppe gab es 1*");
            coffeeSlotMachine.GetCounterForProduct("Milch", out counter);
            Assert.AreEqual(0, counter, "Milch wurde nie bestellt");

            coffeeSlotMachine.InsertCoin(20);
            ok = coffeeSlotMachine.SelectProduct("Kaffee", out returnCoins, out donation);
            Assert.IsFalse(ok, "Zuwenig Geld eingeworfen");
        }

        [TestMethod()]
        public void T13_CoinsCounter()
        {
            int[] returnCoins;
            int donation;
            int[] coins = { 3, 3, 3, 3, 3, 3 };
            string[] productNames = { "Kaffee", "Tee", "Suppe", "Milch" };
            CoffeeSlotMachine coffeeSlotMachine = new CoffeeSlotMachine(coins, productNames);
            coffeeSlotMachine.InsertCoin(200);
            coffeeSlotMachine.SelectProduct("Kaffee", out returnCoins, out donation);
            coffeeSlotMachine.InsertCoin(5);
            coffeeSlotMachine.InsertCoin(5);
            coffeeSlotMachine.InsertCoin(20);
            coffeeSlotMachine.InsertCoin(50);
            coffeeSlotMachine.SelectProduct("Kaffee", out returnCoins, out donation);
            coffeeSlotMachine.InsertCoin(20);
            coffeeSlotMachine.InsertCoin(20);
            coffeeSlotMachine.InsertCoin(20);
            coffeeSlotMachine.SelectProduct("Tee", out returnCoins, out donation);
            coffeeSlotMachine.InsertCoin(50);
            coffeeSlotMachine.SelectProduct("Suppe", out returnCoins, out donation);
            int counter;
            coffeeSlotMachine.GetCounterForCoin(5, out counter);
            Assert.AreEqual(5, counter, "Am Papier durchspielen, möglichst große Münzen zurückgeben");
            coffeeSlotMachine.GetCounterForCoin(10, out counter);
            Assert.AreEqual(1, counter, "Am Papier durchspielen, möglichst große Münzen zurückgeben");
            coffeeSlotMachine.GetCounterForCoin(20, out counter);
            Assert.AreEqual(6, counter, "Am Papier durchspielen, möglichst große Münzen zurückgeben");
            coffeeSlotMachine.GetCounterForCoin(50, out counter);
            Assert.AreEqual(4, counter, "Am Papier durchspielen, möglichst große Münzen zurückgeben");
            coffeeSlotMachine.GetCounterForCoin(100, out counter);
            Assert.AreEqual(2, counter, "Am Papier durchspielen, möglichst große Münzen zurückgeben");
            coffeeSlotMachine.GetCounterForCoin(200, out counter);
            Assert.AreEqual(4, counter, "Am Papier durchspielen, möglichst große Münzen zurückgeben");
        }

    }
}
