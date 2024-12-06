using System;

namespace Logic
{
    public class CoffeeSlotMachine
    {
        const int Price = 50;  // Einheitspreis für alle Produkte
        private int[] _coinValues = { 5, 10, 20, 50, 100, 200 };
        private int[] _coinsDepot;
        private int[] _currentCoins = new int[6];
        private string[] _productNames;
        private int[] _productCounter = { 0, 0, 0 };


        /// <summary>
        /// Parameterloser Constructor initialisiert das Depot mit jeweils drei Münzen
        /// und die Produkte mit den drei Standardprodukten Cappuccino, Mocca, Kakao
        /// </summary>
        public CoffeeSlotMachine()
            : this(new[] { 3, 3, 3, 3, 3, 3 }, new[] { "Cappuccino", "Mocca", "Kakao" })
        {

        }

        /// <summary>
        /// Sowohl das Münzdepot als auch die produkte werden übergeben
        /// </summary>
        /// <param name="coinDepot"></param>
        /// <param name="productNames"></param>
        public CoffeeSlotMachine(int[] coinDepot, string[] productNames)
        {
            _coinsDepot = coinDepot;
            _productNames = productNames;
        }


        /// <summary>
        /// Summe des aktuell eingeworfenen Geldes
        /// </summary>
        public int CurrentMoney
        {
            get
            {
                int currentMoney = 0;

                for (int i = 0; i < _currentCoins.Length; i++)
                {

                    currentMoney += (_currentCoins[i] * _coinValues[i]);
                }

                return currentMoney;
            }
        }

        /// <summary>
        /// Wie viele Münzen befinden sich im Münzdepot
        /// </summary>
        public int CoinsInDepot
        {
            get
            {
                int coinsInDepot = 0;

                for (int i = 0; i < _coinsDepot.Length; i++)
                {
                    coinsInDepot += _coinsDepot[i];
                }

                return coinsInDepot;
            }
        }

        /// <summary>
        /// Wie viele Produkte sind verfügbar
        /// </summary>
        public int ProductsAvailable
        {
            get
            {
                int productsAvailable = _productNames.Length;

                return productsAvailable;
            }
        }

        /// <summary>
        /// Liefert das Array der möglichen Münzwerte (_coinValues) als Kopie zurück,
        /// um Manipulationen von außen zu verhindern
        /// </summary>
        public int[] CoinValues
        {
            get
            {
                int[] coinValues = _coinValues;
                for (int i = 0; i < _coinValues.Length; i++)
                {
                    coinValues[i] = _coinValues[i];
                }

                return coinValues;
            }
        }

        /// <summary>
        /// Array mit den verfügbaren Produkten
        /// Liefert eine Kopie des Arrays _productNames zurück,
        /// um Manipulationen von außen zu verhindern
        /// </summary>
        public string[] ProductNames
        {
            get
            {
                string[] productNames = new string[_productNames.Length];
                for (int i = 0; i < _productNames.Length; i++)
                {
                    productNames[i] = _productNames[i];
                }

                return productNames;
            }
        }

        /// <summary>
        /// Liefer eine Kopie des Arrays Münzdepot (_coinsDepot) zurück,
        /// um Manipulationen von außen zu verhindern
        /// </summary>
        public int[] CoinsDepot
        {
            get
            {
                int[] CoinsDepot = new int[_coinsDepot.Length];
                for (int i = 0; i < _productNames.Length; i++)
                {
                    CoinsDepot[i] = _coinsDepot[i];
                }

                return CoinsDepot;
            }
        }

        /// <summary>
        /// Eine Münze wird eingeworfen. Der Wert wird in Cent angegeben
        /// Ungültige Werte (z.B. 17) fallen genau so durch, wie unzulässige
        /// Münzen (1 Cent, 2 Cent). Wurden schon zumindest 50 Cent eingeworfen,
        /// fällt die Münze ebenfalls durch.
        /// </summary>
        /// <param name="coinValue"></param>
        /// <returns>Wurde die Münze übernommen</returns>
        public bool InsertCoin(int coinValue)
        {
            if (coinValue <= 0 || coinValue % 5 != 0)
            {
                return false;
            }

            if (CurrentMoney >= Price)
            {
                return false;
            }

            for(int i = 0; i < _coinValues.Length; i++)
            {
                if (_coinValues[i] == coinValue)
                {
                    _currentCoins[i]++;
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Der Kunde wählt das Produkt aus. Falls es existiert, wird der
        /// jeweilige Produktzähler erhöht und das geld eingenommen.
        /// </summary>
        /// <param name="productName"></param>
        /// <param name="returnCoins"></param>
        /// <param name="donation">Spende, die nicht mehr zurükgegeben werden konnte</param>
        /// <returns>Existiert das Produkt</returns>
        public bool SelectProduct(string productName, out int[] returnCoins, out int donation)
        {
            returnCoins = new int [6];
            donation = 0;

            if (CurrentMoney < Price)
            {
                return false;
            }

            int money = CurrentMoney - Price;

            for (int i = 0; i < _productNames.Length; i++)
            {
                if(productName == _productNames[i])
                {
                    _productCounter[i]++;

                    for (int j = _coinValues.Length - 1; j >= 0 && money > 0; j--)
                    {
                        while (_coinValues[j] <= money && _coinsDepot[j] > 0)
                        {
                            returnCoins[j]++;
                            money = money - _coinValues[j];
                            _coinsDepot[j]--;
                        }
                    }

                    donation = money;

                    for (int j = 0; j < _currentCoins.Length; j++)
                    {
                        _coinsDepot[j] += _currentCoins[j];
                        _currentCoins[j] = 0;
                    }

                    return true;
                }
            }
            
            return false;
        }


        /// <summary>
        /// Die aktuelle Bestellung wird abgebrochen. 
        /// Die Anzahl der eingeworfenen Münzen wird je Wert 5c-200c in einem
        /// Array zurückgegeben (5 cent == Index:0, 200 cent == Index 5
        /// </summary>
        /// <returns>Array mit der Anzahl der Münzen je Wert</returns>
        public int[] CancelOrder()
        {
            int[] returnCoins = new int[6];

            for (int i = 0; i < returnCoins.Length; i++)
            {

                returnCoins[i] = _currentCoins[i];
                _currentCoins[i] = 0;

            }

            return returnCoins;
        }


        /// <summary>
        /// Depot leeren und Summe des Inhalts zurückgeben
        /// </summary>
        /// <returns></returns>
        public int EmptyDepot()
        {
            int coins = 0;
            for (int i = 0; i < _coinsDepot.Length; i++)
            {
                coins = coins + _coinsDepot[i] * _coinValues[i];
                _coinsDepot[i] = 0;
            }

            return coins;
        }

        /// <summary>
        /// Liest den aktuellen Produktzählerstand für 
        /// das Produkt aus.
        /// </summary>
        /// <param name="productName"></param>
        /// <param name="counter">aktueller Produktzählerstand</param>
        /// <returns>true, wenn das Produkt existiert</returns>
        public bool GetCounterForProduct(string productName, out int counter)
        {
            counter = 0;
            for (int i = 0; i < _productCounter.Length; i++)
            {
                if (productName == _productNames[i])
                {
                    counter = _productCounter[i];
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Liest den aktuellen Münzzählerstand (Depot) für 
        /// den übergebenen Münzwert zurücks.
        /// </summary>
        /// <param name="coinValue"></param>
        /// <param name="counter"></param>
        /// <returns>true, wenn die Münze existiert</returns>
        public bool GetCounterForCoin(int coinValue, out int counter)
        {
            counter = 0;
            for (int i = 0; i < _coinsDepot.Length; i++)
            {
                if (coinValue == _coinValues[i])
                {
                    counter = _coinsDepot[i];
                    return true;
                }
            }
            return false;
        }
    }
}