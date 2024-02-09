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
                Console.WriteLine("UTOMHUS");
                Console.WriteLine("[1]Medeltemperatur och luftfuktighet per dag, för valt datum(sökmöjlighet med validering)");
                Console.WriteLine("[2]Sortering av varmast till kallaste dagen enligt medeltemperatur per dag");
                Console.WriteLine("[3]Sortering av torrast till fuktigaste dagen enligt medelluftfuktighet per dag");
                Console.WriteLine("[4]Sortering av minst till störst risk av mögel");
                Console.WriteLine("[5]Datum för meteorologisk Höst. Datum för meteologisk vinter(OBS Mild vinter!)");

                Console.WriteLine("\nINOMHUS");
                Console.WriteLine("[6]Medeltemperatur för valt datum(sökmöjlighet med validering)");
                Console.WriteLine("[7]Sortering av varmast till kallaste dagen enligt medeltemperatur per dag");
                Console.WriteLine("[8]Sortering av torrast till fuktigaste dagen enligt medelluftfuktighet per dag");
                Console.WriteLine("[9]Sortering av minst till störst risk av mögel");
                Console.WriteLine("[a]Mögelrisk\n");

                Console.WriteLine("\n[f]Skapa fil\n");


                //MyMeths.TestFileReadMatch();


                ConsoleKeyInfo key = Console.ReadKey(true);
                switch (key.KeyChar)
                {
                    case '1':
                        //    Utomhus:
                        //◦ Medeltemperatur och luftfuktighet per dag, för valt datum(sökmöjlighet med validering)

                        MyMeths.Method1();
                        break;
                    case '2':
                        //◦ Sortering av varmast till kallaste dagen enligt medeltemperatur per dag
                        MyMeths.Method2();
                        break;
                    case '3':
                        //◦ Sortering av torrast till fuktigaste dagen enligt medelluftfuktighet per dag
                        MyMeths.Method3();
                        break;
                    case '4':
                        //◦ Sortering av minst till störst risk av mögel
                        MyMeths.Method4();
                        break;
                    case '5':
                        //◦ Datum för meteorologisk Höst
                        //◦ Datum för meteologisk vinter(OBS Mild vinter!)
                        MyMeths.Method5();
                        break;
                    case '6':
                        //    Inomhus
                        //◦ Medeltemperatur för valt datum(sökmöjlighet med validering)
                        MyMeths.Method6();
                        break;
                    case '7':
                        //◦ Sortering av varmast till kallaste dagen enligt medeltemperatur per dag
                        MyMeths.Method7();
                        break;
                    case '8':
                        //◦ Sortering av torrast till fuktigaste dagen enligt medelluftfuktighet per dag
                        MyMeths.Method8();
                        break;
                    case '9':
                        //◦ Sortering av minst till störst risk av mögel
                        MyMeths.Method9();
                        break;
                    case 'a':
                        //Mögelrisk
                        MyMeths.MoldFormula();
                        break;

                    default:
                        Console.WriteLine("Fel! Prova igen..");
                        break;
                }

                Console.ReadKey(true);
                Console.Clear();

            }
        }
    }



}