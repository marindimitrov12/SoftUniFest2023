using GroseryStore.Discounts;
using GroseryStore.Enums;
using GroseryStore.Models;

namespace GroseryStore
{
    internal class Program
    {
        static void Main(string[] args)
        {
            GroceryTill till = new GroceryTill(new _1getOneHalf(),new TwoForThree());

            // Define items and their prices
            /* till.AddItem("apple", 50);
             till.AddItem("banana", 40);
             till.AddItem("tomato", 30);
             till.AddItem("potato", 26);*/
            till.AddItem("apple", 50);
            till.AddItem("banana", 40);
            till.AddItem("potato", 26);
            till.AddItem("tomatoe", 30);
            // Define special deals
            till.AddSpecialDeal(new List<string> { "apple", "banana", "tomatoe" }, DealType.TwoForThree);
            till.AddSpecialDeal(new List<string> { "potato" }, DealType.BuyOneGetOneHalfPrice);

            // Scan items
            /*till.ScanItem("apple");
            till.ScanItem("banana");
            till.ScanItem("banana");
            till.ScanItem("potato");
            till.ScanItem("tomato");
            till.ScanItem("banana");
            till.ScanItem("potato");*/
            till.ScanItem(new GroseryItem { Name="apple",PriceInClouds=50});
            till.ScanItem(new GroseryItem { Name = "banana", PriceInClouds = 40 });
           
            till.ScanItem(new GroseryItem { Name = "banana", PriceInClouds = 40 });
            till.ScanItem(new GroseryItem { Name = "potato", PriceInClouds = 26 });
            till.ScanItem(new GroseryItem { Name = "tomatoe", PriceInClouds = 30 });
            till.ScanItem(new GroseryItem { Name = "banana", PriceInClouds = 40 });
            till.ScanItem(new GroseryItem { Name = "potato", PriceInClouds = 26 });

            int totalInClouds = till.CalculateTotal();
            double totalInAws = till.ConvertToAws(totalInClouds);

            Console.WriteLine($"Total Price: {totalInClouds} clouds ({totalInAws} aws)");
        }
    }
}