using BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace ProductManagementWebClient.Controllers
{
    public class ProductController : Controller
    {
        private readonly HttpClient client;
        private string ProductApiUrl = "";
        private string CategoryApiUrl = "";

        public ProductController()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            ProductApiUrl = "https://localhost:7254/api/product";
            CategoryApiUrl = "https://localhost:7254/api/category";
        }

        public async Task<IActionResult> Index()
        {
            HttpResponseMessage response = await client.GetAsync(ProductApiUrl);
            string data = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            List<Product> products = JsonSerializer.Deserialize<List<Product>>(data, options);
            return View(products);
        }

        public async Task<IActionResult> Details(int id)
        {
            HttpResponseMessage responseMessage = await client.GetAsync($"{ProductApiUrl}/Details/{id}");
            if (!responseMessage.IsSuccessStatusCode)
            {
                return NotFound();
            }

            string data = await responseMessage.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            Product product = JsonSerializer.Deserialize<Product>(data, options);
            return View(product);
        }

        public async Task<IActionResult> Create()
        {
            HttpResponseMessage response = await client.GetAsync(CategoryApiUrl);
            string data = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            List<Category> categories = JsonSerializer.Deserialize<List<Category>>(data, options);
            ViewBag.Categories = new SelectList(categories, "CategoryId", "CategoryName");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductDto productDto)
        {
            if (!ModelState.IsValid)
            {
                // If model validation fails, repopulate the categories and return to the view
                HttpResponseMessage response = await client.GetAsync(CategoryApiUrl);
                string data = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                List<Category> categories = JsonSerializer.Deserialize<List<Category>>(data, options);
                ViewBag.Categories = new SelectList(categories, "CategoryId", "CategoryName");

                return View(productDto);
            }

            var content = new StringContent(JsonSerializer.Serialize(productDto), Encoding.UTF8, "application/json");
            HttpResponseMessage responseMessage = await client.PostAsync($"{ProductApiUrl}/Create", content);

            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(productDto);
        }

        public async Task<IActionResult> Edit(int id)
        {
            HttpResponseMessage responseMessage = await client.GetAsync($"{ProductApiUrl}/Details/{id}");
            if (!responseMessage.IsSuccessStatusCode)
            {
                return NotFound();
            }

            string data = await responseMessage.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            Product product = JsonSerializer.Deserialize<Product>(data, options);
            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, ProductDto productDto)
        {
            if (id != productDto.ProductId)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                var content = new StringContent(JsonSerializer.Serialize(productDto), Encoding.UTF8, "application/json");
                HttpResponseMessage responseMessage = await client.PutAsync($"{ProductApiUrl}/Edit/{id}", content);
                if (responseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(productDto);
        }

        public async Task<IActionResult> Delete(int id)
        {
            HttpResponseMessage responseMessage = await client.GetAsync($"{ProductApiUrl}/Details/{id}");
            if (!responseMessage.IsSuccessStatusCode)
            {
                return NotFound();
            }

            string data = await responseMessage.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            Product product = JsonSerializer.Deserialize<Product>(data, options);
            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            HttpResponseMessage responseMessage = await client.DeleteAsync($"{ProductApiUrl}/Delete/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            return NotFound();
        }
    }
}
