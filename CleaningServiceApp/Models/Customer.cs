using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleaningServiceApp.Models
{
    public class Customer
    {
        public int CustomerID { get; set; } // Egyedi azonosító
        public string Name { get; set; } // Ügyfél neve
        public List<ClothingItem> Orders { get; set; } = new List<ClothingItem>(); // Leadott ruhák listája
    }
}
