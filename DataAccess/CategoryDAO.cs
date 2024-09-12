using BusinessObjects;

namespace DataAccess
{
    public class CategoryDAO
    {
        public static List<Category> GetCategories()
        {
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    return context.Categories.ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting categories", ex);
            }
        }
    }
}
