using GenericSmallBusinessApp.Server.Interfaces;
using GenericSmallBusinessApp.Server.Models;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GenericSmallBusinessApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class ProductController(IProductService service) : ControllerBase
    {
        [HttpGet("GetAllProducts")]
        [AllowAnonymous]
        public async Task<ActionResult<List<Product>>> GetAllProducts()
        {
            var products = await service.GetAllProductsRequest();
            return Ok(products);
        }

        [HttpGet("GetProductById/{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<Product>> GetProductByProductId(int id)
        {
            var product = await service.GetProductByProductIdRequest(id);
            return Ok(product);
        }

        [HttpPost("AddNew")]
        public async Task<ActionResult<bool>> AddNewProduct([FromForm] ProductDto request)
        {
            var result = await service.AddProductRequest(request);
            return Ok(result);
        }

        [HttpPost("Update/{id}")]
        public async Task<ActionResult<Product>> UpdateProduct([FromForm] ProductDto request, int id)
        {
            var result = await service.UpdateProductRequest(request, id);
            return Ok(result);
        }

        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult<bool>> DeleteProduct(int id)
        {
            var result = await service.DeleteProductRequest(id);
            return Ok(result);
        }
    }
}
