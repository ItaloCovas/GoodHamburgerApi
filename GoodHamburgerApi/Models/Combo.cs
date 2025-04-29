namespace GoodHamburgerApi.Models
{
    public class Combo
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal DiscountPercent { get; set; }

        public string ProductsIds { get; set; }
    }
}
