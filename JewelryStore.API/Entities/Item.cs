
namespace JewelryStore.API.Entities
{
    /// <summary>
    /// Item entity
    /// </summary>
    public class Item
    {
        public double GoldPricePerGram { get; set; }
        public double WeightInGrams { get; set; }
        public double Discount { get; set; }
        public double TotalPrice { get; set; }
    }
}
