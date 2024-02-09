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
        public static void AvgTempratureAndHumidityPerDayOutside()
        {
            //Medeltemperatur och luftfuktighet per dag, för valt datum(sökmöjlighet med validering).
            Console.WriteLine("Medeltemperatur och luftfuktighet per dag, för valt datum (sökmöjlighet med validering).\n");

            // Läser in filinnehållet
            string fileContent = ReadFileContent("tempdata5-med fel.txt");
            var preprocessedData = WeatherProcesses.PreprocessData(fileContent);

            // Kontrollerar och formaterar det valda datumet
            DateTime date = GetValidDate();
            string dateInput = date.ToString("yyyy-MM-dd");

            // Bearbetar och visar data för "Ute"
            WeatherProcesses.TemperatureAndHumidity(preprocessedData, dateInput, "Ute");
        }

        public static void WarmestToColdestAvgTempOutside()
        {

            // Läs in och preprocessa filinnehållet
            string fileContent = ReadFileContent("tempdata5-med fel.txt");
            var preprocessedData = WeatherProcesses.PreprocessData(fileContent);
            // Sortering av varmast till kallaste dagen enligt medeltemperatur per dag
            Console.WriteLine("Varmaste till kallaste medeltemperatur per dag utomhus:\n");

            // Bearbeta och presentera temperaturdata
            WeatherProcesses.TemperatureData(preprocessedData, "Ute");
        }


        public static void DriestToHumidAvgHumidityOutside()
        {
            //Sortering av torrast till fuktigaste dagen enligt medelluftfuktighet per dag
            Console.WriteLine("Torrast till fuktigaste dagen enligt medelluftfuktighet per dag utomhus:\n");

            // Läs in och preprocessa filinnehållet
            string fileContent = ReadFileContent("tempdata5-med fel.txt");
            var preprocessedData = WeatherProcesses.PreprocessData(fileContent);

            // Bearbeta och presentera luftfuktighetsdata
            WeatherProcesses.HumidityData(preprocessedData, "Ute");
        }



        public static void MoldRiskSortedOutside()
        {
            // Sortering av minst till störst risk av mögel
            Console.WriteLine("Sortering av minst till störst risk av mögel utomhus:\n");

            // Läs in och preprocessa filinnehållet
            string fileContent = ReadFileContent("tempdata5-med fel.txt");
            var preprocessedData = WeatherProcesses.PreprocessData(fileContent);

            // Bearbeta och presentera mögelriskdata
            WeatherProcesses.MoldRiskData(preprocessedData, "Ute");
        }


        //INOMHUS

        public static void AvgTempratureAndHumidityPerDayInside()
        {
            //Medeltemperatur och luftfuktighet per dag, för valt datum(sökmöjlighet med validering).
            Console.WriteLine("Medeltemperatur och luftfuktighet per dag, för valt datum (sökmöjlighet med validering).\n");

            // Läser in filinnehållet
            string fileContent = ReadFileContent("tempdata5-med fel.txt");
            var preprocessedData = WeatherProcesses.PreprocessData(fileContent);

            // Kontrollerar och formaterar det valda datumet
            DateTime date = GetValidDate();
            string dateInput = date.ToString("yyyy-MM-dd");

            // Bearbetar och visar data för "Ute"
            WeatherProcesses.TemperatureAndHumidity(preprocessedData, dateInput, "Inne");
        }


        public static void WarmestToColdestAvgTempInside()
        {
            // Sortering av varmast till kallaste dagen enligt medeltemperatur per dag
            Console.WriteLine("Varmaste till kallaste medeltemperatur per dag inomhus:\n");

            // Läs in och preprocessa filinnehållet
            string fileContent = ReadFileContent("tempdata5-med fel.txt");
            var preprocessedData = WeatherProcesses.PreprocessData(fileContent);


            // Bearbeta och presentera temperaturdata
            WeatherProcesses.TemperatureData(preprocessedData, "Inne");

        }

        public static void DriestToHumidAvgHumidityInside()
        {
            // Sortering av torrast till fuktigaste dagen enligt medelluftfuktighet per dag
            Console.WriteLine("Torrast till fuktigaste dagen enligt medelluftfuktighet per dag inomhus:\n");

            // Läs in och preprocessa filinnehållet
            string fileContent = ReadFileContent("tempdata5-med fel.txt");
            var preprocessedData = WeatherProcesses.PreprocessData(fileContent);

            // Bearbeta och presentera luftfuktighetsdata
            WeatherProcesses.HumidityData(preprocessedData, "Inne");
        }


        public static void MoldRiskSortedInside()
        {
            // Sortering av minst till störst risk av mögel
            Console.WriteLine("Sortering av minst till störst risk av mögel inomhus:\n");

            // Läs in och preprocessa filinnehållet
            string fileContent = ReadFileContent("tempdata5-med fel.txt");
            var preprocessedData = WeatherProcesses.PreprocessData(fileContent);

            // Bearbeta och presentera mögelriskdata
            WeatherProcesses.MoldRiskData(preprocessedData, "Inne");
        }
    }
}

