using System;
using System.Collections.Generic;
using System.Globalization;
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

    internal class MyMeths
    {
        public static string path = "../../../Files/";

        //UTOMHUS
        public static void Method1()
        {
            //Medeltemperatur och luftfuktighet per dag, för valt datum(sökmöjlighet med validering): FUNGERAR! Lägg till kontroll av datum
            Console.WriteLine("Skriv in datum YYYY-MM-DD: ");
            string dateInput = Console.ReadLine();

            if (!DateTime.TryParseExact(dateInput, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime date))
            {
                Console.WriteLine("Felaktigt datumformat. Ange datumet i formatet YYYY-MM-DD.");
                return;
            }

            string filePath = "../../../Files/" + "tempdata5-med fel.txt";
            string fileContent = File.ReadAllText(filePath);
            var preprocessedData = WeatherDataProcessor.PreprocessData(fileContent);

            var selectedData = preprocessedData.Where(g => g.Groups[1].Value == dateInput && g.Groups[3].Value == "Ute");

            if (!selectedData.Any())
            {
                Console.WriteLine($"Ingen data hittades för datumet {dateInput} Ute.");
                return;
            }
            var avgTemp = selectedData.Average(g => double.Parse(g.Groups[4].Value, CultureInfo.InvariantCulture));
            var avgHumidity = selectedData.Average(g => double.Parse(g.Groups[5].Value, CultureInfo.InvariantCulture));

            Console.WriteLine($"Medeltemperatur: {avgTemp:F2}°C, Medelluftfuktighet: {avgHumidity:F2}% för datumet {dateInput} Ute.");
        }


        //◦ Sortering av varmast till kallaste dagen enligt medeltemperatur per dag
        public static void Method2()
        {
            Console.WriteLine("Varmaste till kallaste medeltemperatur per dag utomhus:");
            string filePath = "../../../Files/" + "tempdata5-med fel.txt";
            string fileContent = File.ReadAllText(filePath);
            var preprocessedData = WeatherDataProcessor.PreprocessData(fileContent);

            var dailyTemperatures = preprocessedData
                .Where(g => g.Groups[3].Value == "Ute")
                .GroupBy(g => g.Groups[1].Value) // Gruppera efter datum
                .Select(g => new
                {
                    Date = g.Key,
                    // Avrunda medeltemperaturen till två decimaler
                    AvgTemp = Math.Round(g.Average(x => double.Parse(x.Groups[4].Value, CultureInfo.InvariantCulture)), 1)
                })
                .OrderByDescending(g => g.AvgTemp) // Sortera från högsta till lägsta medeltemperatur
                .ToList();

            foreach (var day in dailyTemperatures)
            {
                // Uppdaterad utskrift för att visa den avrundade medeltemperaturen
                Console.WriteLine($"{day.Date}: Medeltemperatur = {day.AvgTemp}°C");
            }
        }


        //◦ Sortering av torrast till fuktigaste dagen enligt medelluftfuktighet per dag
        public static void Method3()
        {
            Console.WriteLine("Torrast till fuktigaste dagen enligt medelluftfuktighet per dag utomhus:");
            string filePath = "../../../Files/" + "tempdata5-med fel.txt";
            string fileContent = File.ReadAllText(filePath);
            var preprocessedData = WeatherDataProcessor.PreprocessData(fileContent);

            var dailyHumidity = preprocessedData
                .Where(g => g.Groups[3].Value == "Ute")
                .GroupBy(g => g.Groups[1].Value) // Gruppera efter datum
                .Select(g => new
                {
                    Date = g.Key,
                    AvgHumidity = Math.Round(g.Average(x => double.Parse(x.Groups[5].Value, CultureInfo.InvariantCulture)), 2) // Beräkna medelluftfuktigheten för dagen
                })
                .OrderBy(g => g.AvgHumidity) // Sortera från lägsta till högsta medelluftfuktighet
                .ToList();

            foreach (var day in dailyHumidity)
            {
                Console.WriteLine($"{day.Date}: Medelluftfuktighet = {day.AvgHumidity}%");
            }
        }


        //◦ Sortering av minst till störst risk av mögel
        public static void Method4()
        {
            Console.WriteLine("Sortering av minst till störst risk av mögel utomhus:");
            string filePath = "../../../Files/" + "tempdata5-med fel.txt";
            string fileContent = File.ReadAllText(filePath);

            var preprocessedData = WeatherDataProcessor.PreprocessData(fileContent);

            var moldRisk = preprocessedData
                .Where(g => g.Groups[3].Value == "Ute")
                .GroupBy(g => g.Groups[1].Value) // Gruppera efter datum
                .Select(group => new
                {
                    Date = group.Key,
                    MoldRisk = group.Average(g =>
                    {
                        var temp = double.Parse(g.Groups[4].Value, CultureInfo.InvariantCulture);
                        var humidity = double.Parse(g.Groups[5].Value, CultureInfo.InvariantCulture);
                        // Exempel på ett villkor: Utför beräkningen endast om luftfuktigheten är över 78%
                        return humidity > 78 ?
                               ((humidity - 78) * (temp / 15)) / 0.22 :
                               0; // Sätt mögelrisken till 0 om villkoret inte är uppfyllt
                    })
                })
                .OrderBy(g => g.MoldRisk)
                .ToList();

            foreach (var day in moldRisk)
            {
                Console.WriteLine($"{day.Date}: Genomsnittlig mögelrisk = {day.MoldRisk:F2}");
            }
        }




        //◦ Datum för meteorologisk Höst
        //◦ Datum för meteologisk vinter(OBS Mild vinter!)
        public static void Method5()
        {

        }

        //INOMHUS
        //Medeltemperatur och luftfuktighet per dag, för valt datum(sökmöjlighet med validering): FUNGERAR! Lägg till kontroll av datum
        public static void Method6()
        {
            Console.WriteLine("Skriv in datum YYYY-MM-DD: ");
            string dateInput = Console.ReadLine();

            if (!DateTime.TryParseExact(dateInput, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime date))
            {
                Console.WriteLine("Felaktigt datumformat. Ange datumet i formatet YYYY-MM-DD.");
                return;
            }

            string filePath = "../../../Files/" + "tempdata5-med fel.txt";
            string fileContent = File.ReadAllText(filePath);
            var preprocessedData = WeatherDataProcessor.PreprocessData(fileContent);

            var selectedData = preprocessedData.Where(g => g.Groups[1].Value == dateInput && g.Groups[3].Value == "Inne");

            if (!selectedData.Any())
            {
                Console.WriteLine($"Ingen data hittades för datumet {dateInput} Inne.");
                return;
            }

            var avgTemp = selectedData.Average(g => double.Parse(g.Groups[4].Value, CultureInfo.InvariantCulture));
            var avgHumidity = selectedData.Average(g => double.Parse(g.Groups[5].Value, CultureInfo.InvariantCulture));

            Console.WriteLine($"Medeltemperatur: {avgTemp:F2}°C, Medelluftfuktighet: {avgHumidity:F2}% för datumet {dateInput} Inne.");
        }

        //◦ Sortering av varmast till kallaste dagen enligt medeltemperatur per dag
        public static void Method7()
        {
            Console.WriteLine("Varmaste till kallaste medeltemperatur per dag inomhus:");
            string filePath = "../../../Files/" + "tempdata5-med fel.txt";
            string fileContent = File.ReadAllText(filePath);
            var preprocessedData = WeatherDataProcessor.PreprocessData(fileContent);

            var dailyTemperatures = preprocessedData
                .Where(g => g.Groups[3].Value == "Inne")
                .GroupBy(g => g.Groups[1].Value) // Gruppera efter datum
                .Select(g => new
                {
                    Date = g.Key,
                    // Avrunda medeltemperaturen till två decimaler
                    AvgTemp = Math.Round(g.Average(x => double.Parse(x.Groups[4].Value, CultureInfo.InvariantCulture)), 2)
                })
                .OrderByDescending(g => g.AvgTemp) // Sortera från högsta till lägsta medeltemperatur
                .ToList();

            foreach (var day in dailyTemperatures)
            {
                // Uppdaterad utskrift för att visa den avrundade medeltemperaturen
                Console.WriteLine($"{day.Date}: Medeltemperatur = {day.AvgTemp}°C");
            }
        }


        //◦ Sortering av torrast till fuktigaste dagen enligt medelluftfuktighet per dag
        public static void Method8()
        {
            Console.WriteLine("Torrast till fuktigaste dagen enligt medelluftfuktighet per dag inomhus:");
            string filePath = "../../../Files/" + "tempdata5-med fel.txt";
            string fileContent = File.ReadAllText(filePath);
            var preprocessedData = WeatherDataProcessor.PreprocessData(fileContent);

            var dailyHumidity = preprocessedData
                 .Where(g => g.Groups[3].Value == "Inne")
                .GroupBy(g => g.Groups[1].Value) // Gruppera efter datum
                .Select(g => new
                {
                    Date = g.Key,
                    AvgHumidity = g.Average(x => double.Parse(x.Groups[5].Value, CultureInfo.InvariantCulture)) // Beräkna medelluftfuktigheten för dagen
                })
                .OrderBy(g => g.AvgHumidity) // Sortera från lägsta till högsta medelluftfuktighet
                .ToList();

            foreach (var day in dailyHumidity)
            {
                Console.WriteLine($"{day.Date}: Medelluftfuktighet = {day.AvgHumidity}%");
            }
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


        public static void WriteFile()
        {

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
            string filePath = "../../../Files/" + "tempdata5-med fel.txt";

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

