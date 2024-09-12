using BusinessObjects;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repositories;

namespace ProductManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private IProductRepository repository = new ProductRepository();

        [HttpGet]
        public ActionResult<IEnumerable<Category>> GetCategories() => repository.GetCategories();
    }
}
