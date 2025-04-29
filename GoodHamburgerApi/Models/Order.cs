namespace GoodHamburgerApi.Models
{
    public class Order
    {
        public int Id { get; set; }

        public List<OrderProduct> OrderProducts { get; set; }

        public decimal TotalPrice { get; set; } // final value with discount already
    }
}
