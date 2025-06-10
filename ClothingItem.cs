public class ClothingItem
{
    public int? OwnerCustomerID { get; set; } // null, ha még nincs leadva
    public string ItemID { get; set; }
    public string Type { get; set; }
    public string Material { get; set; }
    public bool IsCleaned { get; set; }
    public decimal Price { get; set; }

    public override string ToString()
    {
        return $"{ItemID}: {Type} ({Material}), Ár: {Price} Ft, Tisztítva: {(IsCleaned ? "Igen" : "Nem")}";
    }
}
