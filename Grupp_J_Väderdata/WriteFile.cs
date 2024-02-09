using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Grupp_J_Väderdata;

class WriteFile
{
    public static void MonthSummary()
    {
        string inputFilePath = @"../../../Files/tempdata5-med fel.txt";
        string outputFilePath = @"../../../Files/MonthSummary.txt";

        string fileContent = File.ReadAllText(inputFilePath);
        var preprocessedData = WeatherProcesses.PreprocessData(fileContent);
        using (StreamWriter writer = new StreamWriter(outputFilePath))
        {
            writer.WriteLine("\n\t===============Månadssammanfattning===============\n");
            var monthlyData = preprocessedData
                .Select(match =>
                {
                    DateTime.TryParse(match.Groups[1].Value, out DateTime parsedDate);
                    string location = match.Groups[3].Value;
                    if (location == "ne") location = "Inne";

                    return new
                    {
                        Date = parsedDate,
                        Location = location,
                        Temperature = double.Parse(match.Groups[4].Value),
                        Humidity = double.Parse(match.Groups[5].Value)
                    };
                })
                .Where(x => x.Date != DateTime.MinValue)
                .GroupBy(x => new { x.Date.Year, x.Date.Month, x.Location })
                .OrderBy(x => x.Key.Year).ThenBy(x => x.Key.Month).ThenBy(x => x.Key.Location);

            foreach (var monthGroup in monthlyData)
            {
                writer.WriteLine($"År: {monthGroup.Key.Year} | Månad: {monthGroup.Key.Month} | Plats: {monthGroup.Key.Location}");
                var avgTemp = monthGroup.Average(x => x.Temperature);
                var avgHumidity = monthGroup.Average(x => x.Humidity);
                var avgMoldRisk = monthGroup.Average(x => x.Humidity > 78 ? ((x.Humidity - 78) * (x.Temperature / 15)) / 0.22 : 0);


                writer.WriteLine($"\nMedeltemperatur: {avgTemp:F1}°C, Medelluftfuktighet: {avgHumidity:F1}%, Medelmögelrisk: {avgMoldRisk:F2}\n");
            }
            writer.WriteLine("Formeln som används vid uträkning av mögelrisk: Mögelrisk = ((Luftfuktighet - 78) * (Temperatur / 15)) / 0.22");
        }
        Console.Clear();
        Console.WriteLine("Ny fil har skapats");
        return;
    }
}