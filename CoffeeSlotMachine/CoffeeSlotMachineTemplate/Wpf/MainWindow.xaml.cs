using Logic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private CoffeeSlotMachine _coffeeSlotMachine;

        public MainWindow()
        {
            InitializeComponent();
            _coffeeSlotMachine = new CoffeeSlotMachine();

            // Münzwerte und Produkte initialisieren
            CoinSelection.ItemsSource = _coffeeSlotMachine.CoinValues;
            ProductSelection.ItemsSource = _coffeeSlotMachine.ProductNames;

            // Anzeige des aktuellen Einwurfs initialisieren
            UpdateCurrentMoneyDisplay();
            UpdateDepot();
        }

        private void UpdateCurrentMoneyDisplay()
        {
            CurrentMoneyDisplay.Text = $"Aktueller Einwurf: {_coffeeSlotMachine.CurrentMoney} Cent";
        }

        private void InsertCoin_Click(object sender, RoutedEventArgs e)
        {
            if (CoinSelection.SelectedItem is int coinValue)
            {
                bool success = _coffeeSlotMachine.InsertCoin(coinValue);
                Output.Text = success ? $"Münze {coinValue} Cent eingeworfen." : "Münze ungültig oder genug Geld eingeworfen.";
                UpdateCurrentMoneyDisplay();
            }
            else
            {
                Output.Text = "Bitte wählen Sie einen gültigen Münzwert.";
            }
        }

        private void SelectProduct_Click(object sender, RoutedEventArgs e)
        {
            if (ProductSelection.SelectedItem is string productName)
            {
                bool success = _coffeeSlotMachine.SelectProduct(productName, out int[] returnCoins, out int donation);
                Output.Text = success ? $"{productName} gekauft. Wechselgeld: {string.Join(", ", returnCoins)} Cent. Spende: {donation} Cent." :
                                        "Produkt nicht verfügbar oder nicht genug Geld.";
                UpdateCurrentMoneyDisplay();
                UpdateDepot();
            }
        }

        private void CancelOrder_Click(object sender, RoutedEventArgs e)
        {
            var returnCoins = _coffeeSlotMachine.CancelOrder();
            Output.Text = $"Bestellung abgebrochen. Rückgeld: {string.Join(", ", returnCoins)} Cent.";
            UpdateCurrentMoneyDisplay();
        }

        private void UpdateDepot()
        {
            CoinDepotDisplay.Items.Clear();

            // Anzeige der Münzen je Wert in der ListBox
            var depot = _coffeeSlotMachine.CoinsDepot;
            for (int i = 0; i < depot.Length; i++)
            {
                CoinDepotDisplay.Items.Add($"{_coffeeSlotMachine.CoinValues[i]} Cent: {depot[i]} Münzen");
            }
        }

       
    }
}