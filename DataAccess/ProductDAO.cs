using BusinessObjects;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class ProductDAO
    {
        public static List<Product> GetProducts()
        {
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    return context.Products
                        .Include(p => p.Category)
                        .Select(p => new Product
                        {
                            ProductId = p.ProductId,
                            ProductName = p.ProductName,
                            CategoryId = p.CategoryId,
                            UnitsInStock = p.UnitsInStock,
                            Category = p.Category,
                            UnitPrice = p.UnitPrice
                        })
                        .ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting products", ex);
            }
        }

        public static Product FindProductById(int productId)
        {
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    var product = context.Products
                        .Include(p => p.Category)
                        .Where(p => p.ProductId == productId)
                        .Select(p => new Product
                        {
                            ProductId = p.ProductId,
                            ProductName = p.ProductName,
                            CategoryId = p.CategoryId,
                            UnitsInStock = p.UnitsInStock,
                            UnitPrice = p.UnitPrice,
                            Category = p.Category // Map CategoryName
                        })
                        .FirstOrDefault();

                    return product;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error finding product", ex);
            }
        }

        public static void SaveProduct(Product product)
        {
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    var productToUpdate = context.Products.FirstOrDefault(p => p.ProductId == product.ProductId);
                    if (productToUpdate == null)
                    {
                        context.Products.Add(product);
                    }
                    else
                    {
                        context.Entry(productToUpdate).CurrentValues.SetValues(product);

                        //context.Products.Update(product);
                    }
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error saving product", ex);
            }
        }

        public static void DeleteProduct(Product product)
        {
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    var p = context.Products.FirstOrDefault(c => c.ProductId == product.ProductId);
                    context.Products.Remove(p);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting product", ex);
            }
        }
    }
}
