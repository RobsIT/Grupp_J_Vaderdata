using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;

namespace Grupp_J_Väderdata
{
    internal class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("UTOMHUS");
                Console.WriteLine("[1]Medeltemperatur och luftfuktighet per dag, för valt datum");
                Console.WriteLine("[2]Sortering av varmast till kallaste dagen enligt medeltemperatur per dag");
                Console.WriteLine("[3]Sortering av torrast till fuktigaste dagen enligt medelluftfuktighet per dag");
                Console.WriteLine("[4]Sortering av minst till störst risk av mögel");

                Console.WriteLine("\nINOMHUS");
                Console.WriteLine("[5]Medeltemperatur och luftfuktighet per dag för valt datum");
                Console.WriteLine("[6]Sortering av varmast till kallaste dagen enligt medeltemperatur per dag");
                Console.WriteLine("[7]Sortering av torrast till fuktigaste dagen enligt medelluftfuktighet per dag");
                Console.WriteLine("[8]Sortering av minst till störst risk av mögel");
                Console.WriteLine("\n[f]Skapa fil");
                Console.WriteLine("\n[q]Avsluta\n");


                ConsoleKeyInfo key = Console.ReadKey(true);

                switch (key.KeyChar)
                {
                    case '1':
                        MyMeths.AvgTempratureAndHumidityPerDayOutside();
                        break;
                    case '2':
                        //◦ Sortering av varmast till kallaste dagen enligt medeltemperatur per dag
                        MyMeths.WarmestToColdestAvgTempOutside();
                        break;
                    case '3':
                        //◦ Sortering av torrast till fuktigaste dagen enligt medelluftfuktighet per dag
                        MyMeths.DriestToHumidAvgHumidityOutside();
                        break;
                    case '4':
                        //◦ Sortering av minst till störst risk av mögel
                        MyMeths.MoldRiskSortedOutside();
                        break;
                    case '5':
                        //    Inomhus
                        //◦ Medeltemperatur för valt datum(sökmöjlighet med validering)
                        MyMeths.AvgTempratureAndHumidityPerDayInside();
                        break;
                    case '6':
                        //◦ Sortering av varmast till kallaste dagen enligt medeltemperatur per dag
                        MyMeths.WarmestToColdestAvgTempInside();
                        break;
                    case '7':
                        //◦ Sortering av torrast till fuktigaste dagen enligt medelluftfuktighet per dag
                        MyMeths.DriestToHumidAvgHumidityInside();
                        break;
                    case '8':
                        //◦ Sortering av minst till störst risk av mögel
                        MyMeths.MoldRiskSortedInside();
                        break;
                    case 'f':
                        WriteFile.MonthSummary();
                        break;
                    case 'q':
                        Console.WriteLine("Avslutar...");
                        return;

                    default:
                        Console.WriteLine("Fel! Prova igen..");
                        break;
                }

                Console.ReadKey(true);

            }
        }
    }



}