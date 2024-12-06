using System;
using MobileLibrary;

namespace MobileConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Mobile[] mobiles = new Mobile[10];
            int mobileCount = 0;

            while (true)
            {

                Console.WriteLine("Menü:");
                Console.WriteLine("1. Neues Mobiltelefon erstellen");
                Console.WriteLine("2. Anruf starten");
                Console.WriteLine("3. Anruf beenden");
                Console.WriteLine("4. Telefonrechnung anzeigen");
                Console.WriteLine("5. Beenden");
                Console.Write("Wähle eine Option: ");
                string choice = Console.ReadLine();

                if (choice == "1")
                {
                    if (mobileCount < 10)
                    {
                        Console.Write("Telefonnummer eingeben: ");
                        string phoneNumber = Console.ReadLine();
                        Console.Write("Name eingeben: ");
                        string name = Console.ReadLine();
                        mobiles[mobileCount++] = new Mobile(phoneNumber, name);
                        Console.WriteLine("Mobiltelefon erfolgreich erstellt!");
                    }
                    else
                    {
                        Console.WriteLine("Es können nicht mehr als 10 Mobiltelefone erstellt werden.");
                    }
                }
                else if (choice == "2")
                {
                    Console.Write("Name des anrufenden Mobiltelefons eingeben: ");
                    string activeName = Console.ReadLine();
                    Console.Write("Name des angerufenen Mobiltelefons eingeben: ");
                    string passiveName = Console.ReadLine();

                    Mobile activeMobile = FindMobileByName(mobiles, activeName);
                    Mobile passiveMobile = FindMobileByName(mobiles, passiveName);

                    if (activeMobile != null && passiveMobile != null)
                    {
                        bool success = activeMobile.StartCallTo(passiveMobile);
                        Console.WriteLine(success ? "Anruf erfolgreich gestartet!" : "Anruf fehlgeschlagen. Eines der Mobiltelefone ist bereits im Gespräch.");
                    }
                    else
                    {
                        Console.WriteLine("Eines oder beide Mobiltelefone wurden nicht gefunden.");
                    }
                }
                else if (choice == "3")
                {
                    Console.Write("Name des Mobiltelefons eingeben, um den Anruf zu beenden: ");
                    string endCallName = Console.ReadLine();

                    Mobile endCallMobile = FindMobileByName(mobiles, endCallName);
                    if (endCallMobile != null)
                    {
                        bool success = endCallMobile.StopCall();
                        Console.WriteLine(success ? "Anruf erfolgreich beendet!" : "Anruf beenden fehlgeschlagen.");
                    }
                    else
                    {
                        Console.WriteLine("Mobiltelefon nicht gefunden.");
                    }
                }
                else if (choice == "4")
                {
                    Console.Write("Name des Mobiltelefons eingeben, um die Rechnung anzuzeigen: ");
                    string billName = Console.ReadLine();

                    Mobile billMobile = FindMobileByName(mobiles, billName);
                    if (billMobile != null)
                    {
                        Console.WriteLine($"Aktive Sekunden: {billMobile.SecondsActive}");
                        Console.WriteLine($"Passive Sekunden: {billMobile.SecondsPassive}");
                        Console.WriteLine($"Zu zahlende Cent: {billMobile.CentsToPay}");
                    }
                    else
                    {
                        Console.WriteLine("Mobiltelefon nicht gefunden.");
                    }
                }
                else if (choice == "5")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Ungültige Wahl. Bitte versuche es erneut.");
                }

                Console.ReadLine();
                Console.Clear();
            }


        }

        static Mobile FindMobileByName(Mobile[] mobiles, string name)
        {
            for (int i = 0; i < mobiles.Length; i++)
            {
                if (mobiles[i] != null && mobiles[i].Name == name)
                {
                    return mobiles[i];
                }
            }
            return null;
        }

    }
}