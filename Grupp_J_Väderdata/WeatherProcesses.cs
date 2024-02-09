using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Grupp_J_Väderdata
{
    public class WeatherProcesses
    {
        public static IEnumerable<Match> PreprocessData(string fileContent)
        {
            // Regex-mönster för att matcha varje rad i filen: Mönstret fångar datum, tid, plats, temperatur och luftfuktighet.
            string regexPattern = @"(\d{4}-\d{2}-\d{2})\s(\d{2}:\d{2}:\d{2}),([^,]+),(\d+\.\d+),(\d+)";

            // Här används regex-mönstret för att hitta alla matchningar i texten.
            var matches = Regex.Matches(fileContent, regexPattern);

            //Ignorerar de matcher som inte motsvarar de önskade kriterierna.
            return matches.Cast<Match>().Where(g =>
            {
                // TryParse den fångade datumsträngen till DateTime-objek
                DateTime.TryParse(g.Groups[1].Value, out DateTime date);

                // Kontrollerar att datumet inte är maj 2016 och januari 2017
                return !(date.Month == 5 && date.Year == 2016) && !(date.Month == 1 && date.Year == 2017);
            });
        }

        public static void TemperatureAndHumidity(IEnumerable<Match> preprocessedData, string dateInput, string location)
        {
            var selectedData = preprocessedData.Where(g => g.Groups[1].Value == dateInput && g.Groups[3].Value == location);

            if (!selectedData.Any())
            {
                Console.WriteLine($"Ingen data hittades för datumet {dateInput} {location.ToLower()}.");
                return;
            }

            var avgTemp = Math.Round(selectedData.Average(g => double.Parse(g.Groups[4].Value)), 1);
            var avgHumidity = Math.Round(selectedData.Average(g => double.Parse(g.Groups[5].Value)), 1);

            Console.WriteLine($"\nDatum: {dateInput} | Plats: {location} | Medeltemperatur: {avgTemp}°C | Medelluftfuktighet: {avgHumidity}% ");
        }

        public static void TemperatureData(IEnumerable<Match> preprocessedData, string location)
        {
            var dailyTemperatures = preprocessedData
                .Where(g => g.Groups[3].Value == location)
                .GroupBy(g => g.Groups[1].Value) // Gruppera efter datum
                .Select(g => new
                {
                    Date = g.Key,
                    AvgTemp = Math.Round(g.Average(x => double.Parse(x.Groups[4].Value)), 1) // Avrunda medeltemperaturen
                })
                .OrderByDescending(g => g.AvgTemp) // högsta till lägsta medeltemperatur
                .ToList();

            foreach (var day in dailyTemperatures)
            {
                //Här används en extention 
                $"{day.Date} | Medeltemperatur = {day.AvgTemp}°C".Print();
            }
        }


        public static void HumidityData(IEnumerable<Match> preprocessedData, string location)
        {
            var dailyHumidity = preprocessedData
                .Where(g => g.Groups[3].Value == location)
                .GroupBy(g => g.Groups[1].Value) // Grupperar efter datum
                .Select(g => new
                {
                    Date = g.Key,
                    AvgHumidity = Math.Round(g.Average(x => double.Parse(x.Groups[5].Value)), 2) // Beräkna medelluftfuktigheten
                })
                .OrderBy(g => g.AvgHumidity) // Sortera från lägsta till högsta medelluftfuktighet
                .ToList();

            foreach (var day in dailyHumidity)
            {
                Console.WriteLine($"{day.Date} | Medelluftfuktighet = {day.AvgHumidity}%");
            }
        }

        public static void MoldRiskData(IEnumerable<Match> preprocessedData, string location)
        {
            var moldRisk = preprocessedData
                .Where(g => g.Groups[3].Value == location)
                .GroupBy(g => g.Groups[1].Value) // Gruppera efter datum
                .Select(group => new
                {
                    Date = group.Key,
                    MoldRisk = group.Average(g =>
                    {
                        var temp = double.Parse(g.Groups[4].Value);
                        var humidity = double.Parse(g.Groups[5].Value);
                        // Beräkna mögelrisk baserat på temperatur och luftfuktighet
                        return humidity > 78 ? ((humidity - 78) * (temp / 15)) / 0.22 : 0;
                    })
                })
                .OrderBy(g => g.MoldRisk) // Sortera från lägsta till högsta mögelrisk
                .ToList();

            foreach (var day in moldRisk)
            {
                Console.WriteLine($"{day.Date} | Genomsnittlig mögelrisk = {day.MoldRisk:F2}");
            }
        }
    }
}