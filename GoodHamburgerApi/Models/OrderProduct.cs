using System.Text.Json.Serialization;

namespace GoodHamburgerApi.Models
{
    public class OrderProduct
    {
        public int OrderId { get; set; }

        [JsonIgnore]
        public Order order { get; set; }

        public int ProductId { get; set; }

        [JsonIgnore]
        public Product product { get; set; }
    }
}
