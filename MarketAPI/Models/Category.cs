using System.ComponentModel.DataAnnotations;

namespace MarketAPI.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        public string Type { get; set; }
        
    }
}
