namespace GoodHamburgerApi.Models
{
    public class OrderProduct
    {
        public int OrderId { get; set; }

        public Order order { get; set; }

        public int ProductId { get; set; }

        public Product product { get; set; }
    }
}
