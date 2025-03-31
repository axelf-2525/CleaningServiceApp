using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleaningServiceApp.Models
{
    public class ClothingItem
    {
        public string ItemID { get; set; } // Egyedi azonosító (pl. R001)
        public string Type { get; set; }   // Ruhadarab típusa (pl. ing, nadrág)
        public string Material { get; set; } // Anyag (pl. pamut, gyapjú)
        public bool IsCleaned { get; set; } // Tisztítás állapota
        public decimal Price { get; set; } // Tisztítás díja
    }
}
