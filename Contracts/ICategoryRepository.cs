using Entities.Models;
using Entities.RequestFeatures;
using System.Threading.Tasks;

namespace Contracts
{
    public interface ICategoryRepository
    {
        public Task<PagedList<Category>> GetAllCategoriesAsync(
            CategoryParameters categoryParameters, bool trackChanges);

        public Task<Category> GetCategoryAsync(int categoryId, bool trackChanges);

        public void CreateCategory(Category category);

        public void DeleteCategory(Category category);
    }
}
