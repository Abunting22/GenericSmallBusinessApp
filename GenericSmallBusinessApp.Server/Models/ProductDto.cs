namespace GenericSmallBusinessApp.Server.Models
{
    public class ProductDto
    {
        public string ProductName { get; set; } = string.Empty;
        public decimal ProductPrice { get; set; }
        public string ProductDescription { get; set; } = string.Empty;
    }
}
