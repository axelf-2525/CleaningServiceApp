using System;
using System.Collections.Generic;

namespace CleaningServiceApp
{
    class Program
    {
        static void Main()
        {
            string choice = string.Empty;
            while (choice != "0")
            {
                Console.WriteLine("\n🧺 Ruhatisztító Ügyfél- és Megrendeléskezelő Rendszer\n");
                Console.WriteLine("1. Ruhák listázása");
                Console.WriteLine("2. Elérhető (nem tisztított) ruhák listázása");
                Console.WriteLine("3. Új ruhadarab felvétele");
                Console.WriteLine("4. Ruhadarab eltávolítása");
                Console.WriteLine("5. Ügyfelek listázása");
                Console.WriteLine("6. Új ügyfél hozzáadása");
                Console.WriteLine("7. Ruhadarab leadása tisztításra");
                Console.WriteLine("8. Ruhadarab átvétele");
                Console.WriteLine("9. Adatok mentése fájlba");
                Console.WriteLine("0. Kilépés");
                Console.Write("\nVálassz egy opciót: ");
                choice = Console.ReadLine();
                Console.Clear();

                switch (choice)
                {
                    case "1":
                        // Ruhák listázása
                        break;
                    case "2":
                        // Elérhető ruhák listázása
                        break;
                    case "3":
                        // Új ruhadarab felvétele
                        break;
                    case "4":
                        // Ruhadarab eltávolítása
                        break;
                    case "5":
                        // Ügyfelek listázása
                        break;
                    case "6":
                        // Új ügyfél hozzáadása
                        break;
                    case "7":
                        // Ruhadarab leadása
                        break;
                    case "8":
                        // Ruhadarab átvétele
                        break;
                    case "9":
                        // Mentés fájlba
                        break;
                    case "0":
                        // Kilépés
                        break;
                    default:
                        Console.WriteLine("Érvénytelen választás, próbáld újra!");
                        break;
                }
            }
        }
    }



 

  
}
