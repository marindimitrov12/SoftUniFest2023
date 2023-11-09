using GroseryStore.Enums;
using GroseryStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroseryStore.Inerfaces
{
    public interface IGrosaryTill
    {
        public void AddItem(string name, int priceInClouds);
        public void AddSpecialDeal(List<string> dealItems, DealType dealType);
        public void ScanItem(GroseryItem item);
        public int CalculateTotal();

        public double ConvertToAws(int totalInClouds);

    }
}
