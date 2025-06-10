using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

public class CleaningService
{
    public List<ClothingItem> ClothingItems { get; private set; }
    public List<Customer> Customers { get; private set; }

    public CleaningService()
    {
        ClothingItems = new List<ClothingItem>();
        Customers = new List<Customer>();
    }

    public void AddItem(ClothingItem item)
    {
        ClothingItems.Add(item);
    }

    public void RemoveItem(string itemID)
    {
        var item = ClothingItems.FirstOrDefault(i => i.ItemID == itemID);
        if (item != null)
        {
            ClothingItems.Remove(item);
        }
    }

    public ClothingItem FindItem(string itemID)
    {
        return ClothingItems.FirstOrDefault(i => i.ItemID == itemID);
    }

    public void AddCustomer(Customer customer)
    {
        Customers.Add(customer);
    }

    public Customer FindCustomer(string name)
    {
        return Customers.FirstOrDefault(c => c.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
    }

    public bool DropOffItem(string customerName, ClothingItem item)
    {
        var customer = FindCustomer(customerName);
        if (customer != null)
        {
            item.OwnerCustomerID = customer.CustomerID; // ← új sor
            customer.DropOff(item);
            AddItem(item);
            return true;
        }
        return false;
    }

    public bool PickUpItem(string customerName, string itemID)
    {
        var customer = FindCustomer(customerName);
        var item = FindItem(itemID);

        if (customer != null && item != null && item.IsCleaned)
        {
            customer.PickUp(item);
            ClothingItems.Remove(item);
            return true;
        }

        return false;
    }

    public List<ClothingItem> ListUncleanedItems()
    {
        return ClothingItems.Where(i => !i.IsCleaned).ToList();
    }

    public List<Customer> ListCustomers()
    {
        return Customers;
    }

    // ------------------------------
    // Típus szerinti ruhakeresés
    // ------------------------------
    public List<ClothingItem> SearchItemsByType(string type)
    {
        return ClothingItems
            .Where(i => i.Type.Equals(type, StringComparison.OrdinalIgnoreCase))
            .ToList();
    }

    // ------------------------------
    // CSV fájlba mentés
    // ------------------------------
    public void SaveToFiles(string customerFile, string clothingFile)
    {
        using (StreamWriter sw = new StreamWriter(customerFile))
        {
            foreach (var c in Customers)
            {
                sw.WriteLine($"{c.CustomerID};{c.Name}");
            }
        }

        using (StreamWriter sw = new StreamWriter(clothingFile))
        {
            foreach (var item in ClothingItems)
            {
                sw.WriteLine($"{item.ItemID};{item.Type};{item.Material};{item.IsCleaned};{item.Price};{item.OwnerCustomerID}");
            }
        }
    }

    // ------------------------------
    // CSV fájlból betöltés
    // ------------------------------
    public void LoadFromFiles(string customerFile, string clothingFile)
    {
        if (File.Exists(customerFile))
        {
            foreach (var line in File.ReadAllLines(customerFile))
            {
                var parts = line.Split(';');
                if (parts.Length >= 2)
                {
                    int id = int.Parse(parts[0]);
                    string name = parts[1];
                    Customers.Add(new Customer(id, name));
                }
            }
        }

        if (File.Exists(clothingFile))
        {
            foreach (var line in File.ReadAllLines(clothingFile))
            {
                var parts = line.Split(';');
                if (parts.Length >= 6)
                {
                    var item = new ClothingItem
                    {
                        ItemID = parts[0],
                        Type = parts[1],
                        Material = parts[2],
                        IsCleaned = bool.Parse(parts[3]),
                        Price = decimal.Parse(parts[4]),
                        OwnerCustomerID = int.TryParse(parts[5], out int ownerId) ? ownerId : (int?)null
                    };
                    ClothingItems.Add(item);

                    // Ha van tulajdonos, rendeljük hozzá
                    if (item.OwnerCustomerID != null)
                    {
                        var customer = Customers.FirstOrDefault(c => c.CustomerID == item.OwnerCustomerID);
                        customer?.DropOff(item);
                    }
                }
            }
        }
    }
}
