using GenericSmallBusinessApp.Server.Models;

namespace GenericSmallBusinessApp.Server.Interfaces
{
    public interface IProductService
    {
        public Task<List<Product>> GetAllProductsRequest();
        public Task<Product> GetProductByProductIdRequest(int id);
        public Task<bool> AddProductRequest(ProductDto request);
        public Task<bool> UpdateProductRequest(ProductDto request, int id);
        public Task<bool> DeleteProductRequest(int id);
    }
}
