using System.Threading.Tasks;
using Entities.Models;
using Entities.RequestFeatures;

namespace Contracts
{
    public interface ICategoryRepository
    {
        public Task<(PagedList<Category>, int)> GetAllCategoriesAsync(CategoryParameters categoryParameters,
            bool trackChanges);

        public Task<Category> GetCategoryAsync(int categoryId, bool trackChanges);

        public void CreateCategory(Category category);

        public void DeleteCategory(Category category);
    }
}
