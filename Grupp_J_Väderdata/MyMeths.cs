using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using static System.Net.WebRequestMethods;
using File = System.IO.File;

namespace Grupp_J_Väderdata
{
    //Ignorera data för Maj 2016 och Jan 2017.
    internal class MyMeths
    {
        public static string path = "../../../Files/";
       

        //UTOMHUS
        public static void Method1() 
        {
            //Medeltemperatur och luftfuktighet per dag, för valt datum(sökmöjlighet med validering)
            Console.WriteLine("Skriv in datum YYYY-MM-DD: ");
            string date = Console.ReadLine();

            string filePath = path + "tempdata5-med fel.txt";
            
            string fileContent = File.ReadAllText(filePath);

            string regexPattern = @"(\d{4}-\d{2}-\d{2})\s(\d{2}:\d{2}:\d{2}),([^,]+),(\d+\.\d+),(\d+)";
            
            Regex regex = new Regex(regexPattern);

            var coll = Regex.Matches(fileContent, regexPattern);
            var sel = coll.Where(g => g.Groups[1].Value == "2016-09-11" && g.Groups[3].Value == "Inne");
            var avg = sel.Average(g => double.Parse(g.Groups[4].Value.Replace(".", ",")));


            //HELA \d{4}-\d{2}-\d{2}\s\d{2}:\d{2}:\d{2},[^,]+,\d+\.\d+,\d+
            //Hela2 (\d{4}-\d{2}-\d{2})\s(\d{2}:\d{2}:\d{2}),([^,]+),(\d+\.\d+),(\d+)
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
            
            string fileName = "tempdata5-med fel.txt";
            Regex regex = new Regex(@"2016-06-01"); //2016-06-01 13:58:30,Inne,24.8,42
            MatchCollection matches = regex.Matches(fileName);
            //string fileContents = reader.ReadToEnd();
            //using (StreamReader reader = new StreamReader(path + fileName))
            //{
            //    //string text = StreamReader.ReadAllText("tempdata5-med fel.txt");
            //    //File.ReadAllLines() → string[] – Läser alla rader i filen: string[] lines = File.ReadAllLines("tempdata5-med fel.txt")
            //    // Läser in hela filen string fileContents = reader.ReadToEnd();
            //    // Skriver ut filen I konsollen Console.WriteLine(fileContents);
            //    // Stänger filströmmen reader.Close();

            //    foreach (Regex mat in matches) 
            //    {
            //        Console.WriteLine("- " + mat.Match(fileName));

            //    }
            //}

        }

        public static void MoldFormula()
        {
            //Mögelrisk
            double moldvalue = 0;

            Console.Write("Ange temperatur 'C: ");
            double temperature = double.Parse(Console.ReadLine());

            Console.Write("Ange fuktighet %: ");
            double humidity = double.Parse(Console.ReadLine());

            // Beräkna mögelrisk baserat på temperatur och fuktighet
            moldvalue = ((humidity - 78) * (temperature / 15)) / 0.22; //((luftfuktighet -78) * (Temp/15))/0,22
            //moldvalue = (temperature - (0.55 * (1 - (humidity / 100)))) * 2.5;
            if (moldvalue >= 100) { moldvalue = 100; }
            if (moldvalue <= 0) { moldvalue = 0; }
            Console.WriteLine("Mögelrisk: " + moldvalue + "%");
        }

        public static void TestFileReadMatch() //FUNKAR!!!
        {
            string filePath = path + "tempdata5-med fel.txt";

            string fileContent = File.ReadAllText(filePath);

            string regexPattern = @"2016-06-01\s00:\d{2}:\d{2},[^,]+,\d+\.\d+,\d+";
                          //HELA \d{4}-\d{2}-\d{2}\s\d{2}:\d{2}:\d{2},[^,]+,\d+\.\d+,\d+
            Regex regex = new Regex(regexPattern);

            MatchCollection matches = regex.Matches(fileContent);

            Console.WriteLine("Matchningar: ");
            int rowCount = 1;
            foreach (Match match in matches)
            {
                Console.WriteLine(rowCount + " " + match.Value);
                rowCount++;
            }

        }





    }
}
