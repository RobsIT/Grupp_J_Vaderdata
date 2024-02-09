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

        // Läs in filinnehållet
        public static string ReadFileContent(string fileName)
        {
            string filePath = "../../../Files/" + fileName;
            return File.ReadAllText(filePath);
        }

        //Validerar datum
        private static DateTime GetValidDate()
        {
            while (true)
            {
                Console.WriteLine("Skriv in datum YYYY-MM-DD: ");
                if (DateTime.TryParse(Console.ReadLine(), out DateTime date))
                {
                    return date;
                }
                Console.WriteLine("Du har valt ett ogiltigt datum. Försök igen.");
            }
        }

        //UTOMHUS
        public static void Method1()
        {
            //Medeltemperatur och luftfuktighet per dag, för valt datum(sökmöjlighet med validering).
            Console.WriteLine("Medeltemperatur och luftfuktighet per dag, för valt datum (sökmöjlighet med validering).");

            // Läser in filinnehållet
            string fileContent = ReadFileContent("tempdata5-med fel.txt");
            var preprocessedData = WeatherProcesses.PreprocessData(fileContent);

            // Kontrollerar och formaterar det valda datumet
            DateTime date = GetValidDate();
            string dateInput = date.ToString("yyyy-MM-dd");

            // Bearbetar och visar data för "Ute"
            WeatherProcesses.TemperatureAndHumidity(preprocessedData, dateInput, "Ute");
        }



        public static void Method2()
        {
            // Sortering av varmast till kallaste dagen enligt medeltemperatur per dag
            Console.WriteLine("Varmaste till kallaste medeltemperatur per dag utomhus:");

            // Läs in och preprocessa filinnehållet
            string fileContent = ReadFileContent("tempdata5-med fel.txt");
            var preprocessedData = WeatherProcesses.PreprocessData(fileContent);

            // Bearbeta och presentera temperaturdata
            WeatherProcesses.TemperatureData(preprocessedData, "Ute");
        }



        public static void Method3()
        {
            //Sortering av torrast till fuktigaste dagen enligt medelluftfuktighet per dag
            Console.WriteLine("Torrast till fuktigaste dagen enligt medelluftfuktighet per dag utomhus:");

            // Läs in och preprocessa filinnehållet
            string fileContent = ReadFileContent("tempdata5-med fel.txt");
            var preprocessedData = WeatherProcesses.PreprocessData(fileContent);

            // Bearbeta och presentera luftfuktighetsdata
            WeatherProcesses.HumidityData(preprocessedData, "Ute");
        }



        public static void Method4()
        {
            // Sortering av minst till störst risk av mögel
            Console.WriteLine("Sortering av minst till störst risk av mögel utomhus:");

            // Läs in och preprocessa filinnehållet
            string fileContent = ReadFileContent("tempdata5-med fel.txt");
            var preprocessedData = WeatherProcesses.PreprocessData(fileContent);

            // Bearbeta och presentera mögelriskdata
            WeatherProcesses.MoldRiskData(preprocessedData, "Ute");
        }

        public static void Method5()
        {
            //◦ Datum för meteorologisk Höst
            Console.WriteLine("Datum för meteorologisk Höst:");
            string filePath = "../../../Files/" + "tempdata5-med fel.txt";
            string fileContent = File.ReadAllText(filePath);
            var preprocessedData = WeatherProcesses.PreprocessData(fileContent);//Hoppar över April 2016 och Jan 2017.
            var selectedData = preprocessedData.Where(g => g.Groups[3].Value == "Ute");
            Console.WriteLine("- " + selectedData.Count());
            //var avgTemp = selectedData.Average(g => double.Parse(g.Groups[4].Value == @"(\d+\.\d+)"));
            //var autumnTemp = selectedData.Count(g => double.Parse(g.Groups[4].Value <= "10"));
            //var autumnDays = selectedData.Select((temp, index) => new { Temperature = temp, Day = index + 1 }).Where(entry => entry.Temperature < 10.0);


            //var autumnDate = selectedData.Where(g => g.Groups[1].Value == @"(2016-\d{2}-\d{2})" && g.Groups[3].Value == "Ute" && g.Groups[4].Value == @"(\\d+\\.\\d+)");



            //Console.Write($"Meteorologisk höst börjar: {avgTemp}");


            //◦ Datum för meteologisk vinter(OBS Mild vinter!)


        }

        //INOMHUS

        public static void Method6()
        {
            //Medeltemperatur och luftfuktighet per dag, för valt datum(sökmöjlighet med validering).
            Console.WriteLine("Medeltemperatur och luftfuktighet per dag, för valt datum (sökmöjlighet med validering).");

            // Läser in filinnehållet
            string fileContent = ReadFileContent("tempdata5-med fel.txt");
            var preprocessedData = WeatherProcesses.PreprocessData(fileContent);

            // Kontrollerar och formaterar det valda datumet
            DateTime date = GetValidDate();
            string dateInput = date.ToString("yyyy-MM-dd");

            // Bearbetar och visar data för "Ute"
            WeatherProcesses.TemperatureAndHumidity(preprocessedData, dateInput, "Inne");
        }


        public static void Method7()
        {
            // Sortering av varmast till kallaste dagen enligt medeltemperatur per dag
            Console.WriteLine("Varmaste till kallaste medeltemperatur per dag inomhus:");

            // Läs in och preprocessa filinnehållet
            string fileContent = ReadFileContent("tempdata5-med fel.txt");
            var preprocessedData = WeatherProcesses.PreprocessData(fileContent);

            // Bearbeta och presentera temperaturdata
            WeatherProcesses.TemperatureData(preprocessedData, "Inne");

        }

        public static void Method8()
        {
            // Sortering av torrast till fuktigaste dagen enligt medelluftfuktighet per dag
            Console.WriteLine("Torrast till fuktigaste dagen enligt medelluftfuktighet per dag inomhus:");

            // Läs in och preprocessa filinnehållet
            string fileContent = ReadFileContent("tempdata5-med fel.txt");
            var preprocessedData = WeatherProcesses.PreprocessData(fileContent);

            // Bearbeta och presentera luftfuktighetsdata
            WeatherProcesses.HumidityData(preprocessedData, "Inne");
        }


        public static void Method9()
        {
            // Sortering av minst till störst risk av mögel
            Console.WriteLine("Sortering av minst till störst risk av mögel inomhus:");

            // Läs in och preprocessa filinnehållet
            string fileContent = ReadFileContent("tempdata5-med fel.txt");
            var preprocessedData = WeatherProcesses.PreprocessData(fileContent);

            // Bearbeta och presentera mögelriskdata
            WeatherProcesses.MoldRiskData(preprocessedData, "Inne");
        }

        //Ha kavr detta?
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

