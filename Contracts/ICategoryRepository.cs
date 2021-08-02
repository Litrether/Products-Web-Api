using System.Threading.Tasks;
using Entities.Models;
using Entities.RequestFeatures;

namespace Contracts
{
    public interface ICategoryRepository
    {
        public Task<PagedList<Category>> GetAllCategoriesAsync(CategoryParameters categoryParameters,
            bool trackChanges);

        public Task<Category> GetCategoryAsync(int categoryId, bool trackChanges);

        public Task<int> GetCountAsync();

        public void CreateCategory(Category category);

        public void DeleteCategory(Category category);
    }
}
