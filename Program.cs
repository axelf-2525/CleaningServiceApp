using System;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        CleaningService service = new CleaningService();
        int customerIdCounter = 1;

        // Adatok betöltése induláskor
        service.LoadFromFiles("customers.csv", "clothingitems.csv");

        // Ügyfél ID-k követése, hogy ne legyen ütközés
        if (service.ListCustomers().Any())
        {
            customerIdCounter = service.ListCustomers().Max(c => c.CustomerID) + 1;
        }

        while (true)
        {
            Console.Clear();
            Console.WriteLine("== RUHATISZTÍTÓ ÜGYVITELI RENDSZER ==");
            Console.WriteLine("1. Új ügyfél hozzáadása");
            Console.WriteLine("2. Ruhaleadás");
            Console.WriteLine("3. Ruhák listázása");
            Console.WriteLine("4. Nem tisztított ruhák listázása");
            Console.WriteLine("5. Ruhák átvétele");
            Console.WriteLine("6. Ügyfelek listázása");
            Console.WriteLine("7. Ruhák keresése típus szerint");
            Console.WriteLine("0. Kilépés");
            Console.Write("\nVálasztás: ");

            string input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    Console.Write("Ügyfél neve: ");
                    string name = Console.ReadLine();
                    var customer = new Customer(customerIdCounter++, name);
                    service.AddCustomer(customer);
                    Console.WriteLine("Ügyfél hozzáadva.");
                    break;

                case "2":
                    Console.Write("Ügyfél neve: ");
                    string custName = Console.ReadLine();
                    Console.Write("Ruhadarab azonosító (pl. R001): ");
                    string id = Console.ReadLine();
                    Console.Write("Típus (pl. ing): ");
                    string type = Console.ReadLine();
                    Console.Write("Anyag (pl. pamut): ");
                    string material = Console.ReadLine();
                    Console.Write("Ár: ");
                    if (!decimal.TryParse(Console.ReadLine(), out decimal price))
                    {
                        Console.WriteLine("Hibás árformátum!");
                        break;
                    }

                    var clothingItem = new ClothingItem
                    {
                        ItemID = id,
                        Type = type,
                        Material = material,
                        IsCleaned = false,
                        Price = price
                    };

                    if (service.DropOffItem(custName, clothingItem))
                        Console.WriteLine("Ruhadarab leadva.");
                    else
                        Console.WriteLine("Ügyfél nem található.");
                    break;

                case "3":
                    Console.WriteLine("== Összes ruhadarab ==");
                    foreach (var i in service.ClothingItems)
                        Console.WriteLine(i);
                    break;

                case "4":
                    Console.WriteLine("== Nem tisztított ruhák ==");
                    foreach (var i in service.ListUncleanedItems())
                        Console.WriteLine(i);
                    break;

                case "5":
                    Console.Write("Ügyfél neve: ");
                    string pickupName = Console.ReadLine();
                    Console.Write("Átvenni kívánt ruha azonosítója: ");
                    string pickupId = Console.ReadLine();

                    // Megtisztítjuk a ruhát demó célra
                    var itemToClean = service.FindItem(pickupId);
                    if (itemToClean != null)
                        itemToClean.IsCleaned = true;

                    if (service.PickUpItem(pickupName, pickupId))
                        Console.WriteLine("Ruhadarab átvéve.");
                    else
                        Console.WriteLine("Hiba: ruha nem tisztított vagy ügyfél/azonosító hibás.");
                    break;

                case "6":
                    Console.WriteLine("== Ügyfelek ==");
                    foreach (var c in service.ListCustomers())
                        Console.WriteLine(c);
                    break;

                case "7":
                    Console.Write("Add meg a keresett ruhatípust (pl. ing): ");
                    string searchType = Console.ReadLine();
                    var results = service.SearchItemsByType(searchType);

                    if (results.Any())
                    {
                        Console.WriteLine($"\nTalált {results.Count} db \"{searchType}\" típusú ruhadarab:");
                        foreach (var foundItem in results)
                        {
                            Console.WriteLine(foundItem);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Nincs ilyen típusú ruhadarab.");
                    }
                    break;

                case "0":
                    // Mentés kilépés előtt
                    service.SaveToFiles("customers.csv", "clothingitems.csv");
                    Console.WriteLine("Adatok elmentve. Kilépés...");
                    return;

                default:
                    Console.WriteLine("Ismeretlen opció.");
                    break;
            }

            Console.WriteLine("\nNyomj Entert a folytatáshoz...");
            Console.ReadLine();
        }
    }
}
