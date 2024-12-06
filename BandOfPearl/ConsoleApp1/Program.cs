using BandOfPearl;
using BandOfPearl.Logic;
using System.Text;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Band band = new Band();

            //Read the data
            string[] lines = File.ReadAllLines("BandOfPearl.csv", Encoding.UTF8);
            
            for(int i = 0; i < lines.Length; i++)
            {
                //split the lines and give it to the band
                string[] data = lines[i].Split(";");
                double weight = double.Parse(data[1]);
                band.AddPearl(new Pearl(data[0], weight));
            }

            //print the result
            Console.WriteLine("Farbe      Gewicht\n");
            for(int i = 0; i < lines.Length; i++)
            {
                Console.WriteLine($"{band.GetPearlAtPosition(i).Color,-13}{band.GetPearlAtPosition(i).Weight:f2}");
            }

            Console.WriteLine("Drücken Sie eine beliebige Taste...");
            Console.ReadLine();
        }
    }
}
