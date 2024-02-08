using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Grupp_J_Väderdata
{

    public class WeatherDataProcessor
    {
        public static IEnumerable<Match> PreprocessData(string fileContent)
        {
            //Hoppar över April 2016 och Jan 2017.
            string regexPattern = @"(\d{4}-\d{2}-\d{2})\s(\d{2}:\d{2}:\d{2}),([^,]+),(\d+\.\d+),(\d+)";
            var matches = Regex.Matches(fileContent, regexPattern);
            var filteredMatches = matches.Cast<Match>()
                .Where(g => DateTime.TryParseExact(g.Groups[1].Value, "yyyy-mm-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out var date)
                            && !((date.Month == 5 && date.Year == 2016) || (date.Month == 1 && date.Year == 2017)));

            return filteredMatches;
        }
    }
}