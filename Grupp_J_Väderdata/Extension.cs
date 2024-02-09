using System;
namespace Grupp_J_Väderdata
{
    //Enkel extentionmetod som skriver ut ett meddelande i TemperatureData
    public static class ExtentionMethod
    {
        public static void Print(this string message)
        {
            Console.WriteLine(message);
        }
    }
}