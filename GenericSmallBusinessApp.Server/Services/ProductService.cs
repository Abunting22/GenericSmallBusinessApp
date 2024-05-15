using GenericSmallBusinessApp.Server.Interfaces;
using GenericSmallBusinessApp.Server.Models;

namespace GenericSmallBusinessApp.Server.Services
{
    public class ProductService(IPrimaryRepository<Product> repository) : IProductService
    {
        public async Task<List<Product>> GetAllProductsRequest()
        {
            var products = await repository.GetAll();
            return products;
        }

        public async Task<Product> GetProductByProductIdRequest(int id)
        {
            var product = await repository.GetById(id);
            return product;
        }

        public async Task<bool> AddProductRequest(ProductDto request)
        {
            var product = ConvertDtoRequest(request);
            var result = await repository.Add(product);
            return result;
        }

        public async Task<bool> UpdateProductRequest(ProductDto request, int id)
        {
            var product = ConvertDtoRequest(request);
            product.ProductId = id;
            var result = await repository.Update(product);
            return result;
        }

        public async Task<bool> DeleteProductRequest(int id)
        {
            var result = await repository.Delete(id);
            return result;
        }

        private static Product ConvertDtoRequest(ProductDto request)
        {
            var product = new Product
            {
                ProductName = request.ProductName,
                ProductPrice = request.ProductPrice,
                ProductDescription = request.ProductDescription
            };
            return product;
        }
    }
}
