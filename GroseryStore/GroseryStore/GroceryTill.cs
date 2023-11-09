using GroseryStore.Discounts;
using GroseryStore.Enums;
using GroseryStore.Inerfaces;
using GroseryStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroseryStore
{
    public class GroceryTill: IGrosaryTill
    {
        private List<GroseryItem> items = new List<GroseryItem>();
        private List<SpecialDeals> specialDeals = new List<SpecialDeals>();
        private List<GroseryItem> scannedItems = new List<GroseryItem>();
        private readonly _1getOneHalf _discount1;
        private readonly TwoForThree _discount2;
        public GroceryTill(_1getOneHalf discount1, TwoForThree discount2)
        {
            this._discount1= discount1;
            this._discount2= discount2;
        }
        public void AddItem(string name, int priceInClouds)
        {
            items.Add(new GroseryItem { Name = name, PriceInClouds = priceInClouds });
        }

        public void AddSpecialDeal(List<string> dealItems, DealType dealType)
        {
            specialDeals.Add(new SpecialDeals { Items = dealItems, DealType = dealType });
        }

        public void ScanItem(GroseryItem item)
        {
            scannedItems.Add(item);
        }

        public int CalculateTotal()
        {
            var sum = 0;
            
            _discount1.ApplyDiscount(ref scannedItems,ref specialDeals,ref sum);
            
            Console.WriteLine(sum);
            //-------------Calculates 1 get 1 half--------------------

            //------------Calculates 2 for 3----------------------------
           
            _discount2.ApplyDiscount(ref scannedItems, ref specialDeals,ref sum);
            return sum;

            
        }
        public double ConvertToAws(int totalInClouds)
        {
            return totalInClouds / 100.0;
        }
    }

  
}
