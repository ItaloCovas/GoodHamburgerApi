using System.Text.Json.Serialization;

namespace GoodHamburgerApi.Models
{
    public class Order
    {
        public int Id { get; set; }

        [JsonIgnore]
        public List<OrderProduct> OrderProducts { get; set; }

        public decimal TotalPrice { get; set; } // final value with discount already
    }
}
