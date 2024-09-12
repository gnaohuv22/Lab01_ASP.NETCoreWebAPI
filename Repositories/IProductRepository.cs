using BusinessObjects;

namespace Repositories
{
    public interface IProductRepository
    {
        void SaveProduct(Product product);
        Product GetProductById(int productId);
        void DeleteProduct(Product product);
        List<Category> GetCategories();
        List<Product> GetProducts();
    }
}
