using MobileLibrary;
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
using System.Windows.Threading;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Mobile leftPhone;
        private Mobile rightPhone;
        private DispatcherTimer callTimer;
        private DateTime callStartTime;

        public MainWindow()
        {
            InitializeComponent();

            CallStatus.Text = "Kein Anruf aktiv";

            // Initialisiere die Telefone
            leftPhone = new Mobile("12345", "LeftPhone");
            rightPhone = new Mobile("67890", "RightPhone");

            // Initialisiere Timer für die Anrufdauer
            callTimer = new DispatcherTimer();
            callTimer.Interval = TimeSpan.FromSeconds(1);
            callTimer.Tick += CallTimer_Tick;

            UpdateButtonStates(false); // Anfänglich keine Anrufe aktiv
        }

        private void StartCallToRight_Click(object? sender, RoutedEventArgs e)
        {
            if (leftPhone.StartCallTo(rightPhone))
            {
                CallStatus.Text = "Anruf von Links nach Rechts läuft...";
                callStartTime = DateTime.Now;
                callTimer.Start();
                UpdateButtonStates(true); // Anruf gestartet, Buttons anpassen
            }
            else
            {
                MessageBox.Show("Anruf konnte nicht gestartet werden. Beide Telefone sind beschäftigt.");
            }
        }

        private void StartCallToLeft_Click(object? sender, RoutedEventArgs e)
        {
            if (rightPhone.StartCallTo(leftPhone))
            {
                CallStatus.Text = "Anruf von Rechts nach Links läuft...";
                callStartTime = DateTime.Now;
                callTimer.Start();
                UpdateButtonStates(true); // Anruf gestartet, Buttons anpassen
            }
            else
            {
                MessageBox.Show("Anruf konnte nicht gestartet werden. Beide Telefone sind beschäftigt.");
            }
        }

        private void EndCall_Click(object? sender, RoutedEventArgs e)
        {
            if (leftPhone.IsInCall || rightPhone.IsInCall)
            {
                leftPhone.StopCall();
                rightPhone.StopCall();
                callTimer.Stop();
                CallStatus.Text = "Kein Anruf aktiv";

                // Update der Anzeige
                LeftSecondsActive.Text = leftPhone.SecondsActive.ToString();
                LeftSecondsPassive.Text = leftPhone.SecondsPassive.ToString();
                LeftCentsToPay.Text = leftPhone.CentsToPay.ToString();

                RightSecondsActive.Text = rightPhone.SecondsActive.ToString();
                RightSecondsPassive.Text = rightPhone.SecondsPassive.ToString();
                RightCentsToPay.Text = rightPhone.CentsToPay.ToString();

                UpdateButtonStates(false); // Anruf beendet, Buttons anpassen
            }
            else
            {
                MessageBox.Show("Kein Anruf aktiv, der beendet werden kann.");
            }
        }

        private void CallTimer_Tick(object? sender, EventArgs e)
        {
            // Berechne die vergangene Zeit seit dem Anrufstart
            int callDuration = (int)(DateTime.Now - callStartTime).TotalSeconds;

            // Aktualisiere den Anrufstatus
            CallStatus.Text = $"Anruf läuft... Dauer: {callDuration} Sek.";
        }

        private void UpdateButtonStates(bool isCallActive)
        {
            // Wenn ein Anruf aktiv ist, sind die Start-Buttons deaktiviert und die Stop-Buttons aktiviert
            StartCallToRightButton.IsEnabled = !isCallActive;
            StartCallToLeftButton.IsEnabled = !isCallActive;
            EndCallButton.IsEnabled = isCallActive;
            EndCallButtonRight.IsEnabled = isCallActive;
        }
    }
}