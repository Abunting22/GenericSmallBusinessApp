namespace GenericSmallBusinessApp.Server.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public decimal ProductPrice { get; set; }
        public string ProductDescription { get; set; } = string.Empty;
    }
}
