using CleaningServiceApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleaningServiceApp.Services
{
    public class CleaningService
    {
        public List<ClothingItem> ClothingItems { get; set; } = new List<ClothingItem>();
        public List<Customer> Customers { get; set; } = new List<Customer>();
    }
}
