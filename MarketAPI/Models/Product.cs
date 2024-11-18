using System.ComponentModel.DataAnnotations;

namespace MarketAPI.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Quantity { get; set; }
        public int Price { get; set; }
        public string Country { get; set; }
    }
}
