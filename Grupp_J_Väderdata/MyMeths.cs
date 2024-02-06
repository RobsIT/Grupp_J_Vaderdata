using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace Grupp_J_Väderdata
{
    
    internal class MyMeths
    {
        public static string path = "../../../Files/";
        public static void TestFileRead()
        {
            //Testa hämta listan
            string fileName = "tempdata5-med fel.txt";
            using (StreamReader reader = new StreamReader(path + fileName))
            {
                string line = reader.ReadLine();
                int rowCount = 1;
                while (line != null) 
                {
                    Console.WriteLine(rowCount + " " + line);
                    rowCount++;
                    line = reader.ReadLine();
                }
            }

        }

        //UTOMHUS
        public static void Method1() 
        {
            //Medeltemperatur och luftfuktighet per dag, för valt datum(sökmöjlighet med validering)
            //Ignorera data för Maj 2016 och Jan 2017.
            string fileName = "tempdata5-med fel.txt";
            using (StreamReader reader = new StreamReader(path + fileName))
            {
                //string text = StreamReader.ReadAllText("tempdata5-med fel.txt");
                //File.ReadAllLines() → string[] – Läser alla rader i filen: string[] lines = File.ReadAllLines("tempdata5-med fel.txt")
                // Läser in hela filen string fileContents = reader.ReadToEnd();
                // Skriver ut filen I konsollen Console.WriteLine(fileContents);
                // Stänger filströmmen reader.Close();


                Regex regex = new Regex(@"2016-06-01"); //2016-06-01 13:58:30,Inne,24.8,42
                MatchCollection matches = regex.Matches(fileName);
                //string fileContents = reader.ReadToEnd();
                
                foreach (Regex mat in matches) 
                {
                    Console.WriteLine("- " + mat.Match(fileName));
                    
                }
            }
            
        }
        public static void Method2() 
        {
            //◦ Sortering av varmast till kallaste dagen enligt medeltemperatur per dag


        }
        public static void Method3() 
        {
            //◦ Sortering av torrast till fuktigaste dagen enligt medelluftfuktighet per dag


        }
        public static void Method4() 
        {
            //◦ Sortering av minst till störst risk av mögel


        }
        public static void Method5() 
        {
            //◦ Datum för meteorologisk Höst
            //◦ Datum för meteologisk vinter(OBS Mild vinter!)


        }

        //INOMHUS
        public static void Method6() 
        {
            //◦ Medeltemperatur för valt datum(sökmöjlighet med validering)


        }
        public static void Method7() 
        {
            //◦ Sortering av varmast till kallaste dagen enligt medeltemperatur per dag


        }
        public static void Method8() 
        {
            //◦ Sortering av torrast till fuktigaste dagen enligt medelluftfuktighet per dag


        }
        public static void Method9() 
        {
            //◦ Sortering av minst till störst risk av mögel


        }

        public static void MoldFormula2()
        {
            //Mögelrisk
            double moldvalue = 0;

            Console.Write("Ange temperatur 'C: ");
            double temperature = double.Parse(Console.ReadLine());

            Console.Write("Ange fuktighet %: ");
            double humidity = double.Parse(Console.ReadLine());

            // Beräkna mögelrisk baserat på temperatur och fuktighet
            moldvalue = ((humidity - 78) * (temperature / 15)) / 0.22;
            //moldvalue = (temperature - (0.55 * (1 - (humidity / 100)))) * 2.5;
            if (moldvalue >= 100) { moldvalue = 100; }
            if (moldvalue <= 0) { moldvalue = 0; }
            Console.WriteLine("Mögelrisk: " + moldvalue + "%");
        }







    }
}
