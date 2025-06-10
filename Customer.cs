using System;
using System.Collections.Generic;
using System.Linq;

public class Customer
{
    public int CustomerID { get; set; }
    public string Name { get; set; }
    public List<ClothingItem> Orders { get; set; }

    public Customer(int id, string name)
    {
        CustomerID = id;
        Name = name;
        Orders = new List<ClothingItem>();
    }

    public void DropOff(ClothingItem item)
    {
        Orders.Add(item);
    }

    public void PickUp(ClothingItem item)
    {
        if (Orders.Contains(item) && item.IsCleaned)
        {
            Orders.Remove(item);
            Console.WriteLine($"[{Name}] átvette: {item.ItemID}");
        }
        else
        {
            Console.WriteLine($"[{Name}] nem veheti át ezt a ruhát (nincs megtisztítva vagy nem található).");
        }
    }

    public override string ToString()
    {
        return $"{Name} (ID: {CustomerID}) - Leadott ruhák: {Orders.Count}";
    }
}
