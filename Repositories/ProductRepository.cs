using BusinessObjects;
using DataAccess;

namespace Repositories
{
    public class ProductRepository : IProductRepository
    {
        public void DeleteProduct(Product product) => ProductDAO.DeleteProduct(product);

        public List<Category> GetCategories() => CategoryDAO.GetCategories();

        public Product GetProductById(int productId) => ProductDAO.FindProductById(productId);

        public List<Product> GetProducts() => ProductDAO.GetProducts();

        public void SaveProduct(Product product) => ProductDAO.SaveProduct(product);
    }
}
