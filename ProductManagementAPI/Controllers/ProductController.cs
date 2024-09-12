using BusinessObjects;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repositories;

namespace ProductManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private IProductRepository repository = new ProductRepository();

        //GET: api/Products
        [HttpGet]
        public ActionResult<IEnumerable<Product>> GetProducts()
        {
            var products = repository.GetProducts()
                .Select(p => new Product
                {
                    ProductId = p.ProductId,
                    ProductName = p.ProductName,
                    CategoryId = p.CategoryId,
                    UnitsInStock = p.UnitsInStock,
                    UnitPrice = p.UnitPrice,
                    Category = p.Category // Map CategoryName
                })
                .ToList();

            return products;
        }

        [HttpGet("Details/{id}")]
        public ActionResult<Product> GetProductById(int id)
        {
            var product = repository.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        //POST: ProductsController/Products
        [HttpPost("Create")]
        public IActionResult PostProduct(ProductDto productDto)
        {
            var product = new Product
            {
                ProductId = productDto.ProductId,
                ProductName = productDto.ProductName,
                CategoryId = productDto.CategoryId,
                UnitsInStock = productDto.UnitsInStock,
                UnitPrice = productDto.UnitPrice
            };
            repository.SaveProduct(product);
            return NoContent();
        }

        //DELETE: ProductsController/Delete/5
        [HttpDelete("Delete/{id}")]
        public IActionResult DeleteProduct(int id)
        {
            var product = repository.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }
            repository.DeleteProduct(product);
            return NoContent();
        }

        [HttpPut("Edit/{id}")]
        public IActionResult UpdateProduct(int id, ProductDto productDto)
        {
            var p = repository.GetProductById(id);
            if (p == null)
            {
                return NotFound();
            }
            var product = new Product
            {
                ProductId = productDto.ProductId,
                ProductName = productDto.ProductName,
                CategoryId = productDto.CategoryId,
                UnitsInStock = productDto.UnitsInStock,
                UnitPrice = productDto.UnitPrice
            };
            repository.SaveProduct(product);
            return NoContent();
        }
    }
}
