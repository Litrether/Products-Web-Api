using Contracts;
using Entities;
using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.EntityFrameworkCore;
using Repository.Extensions;
using System.Threading.Tasks;

namespace Repository
{
    public class CategoryRepository : RepositoryBase<Category>, ICategoryRepository
    {
        public CategoryRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }

        public async Task<PagedList<Category>> GetAllCategoriesAsync(
            CategoryParameters categoryParameters, bool trackChanges)
        {
            var categories = await FindAll(trackChanges)
                .Search(categoryParameters.SearchTerm)
                .Sort(categoryParameters.OrderBy)
                .ToListAsync();

            return PagedList<Category>
                .ToPagedList(categories, categoryParameters.PageNumber,
                    categoryParameters.PageSize);
        }

        public async Task<Category> GetCategoryAsync(int categoryId, bool trackChanges) =>
            await FindByCondition(c => c.Id.Equals(categoryId), trackChanges)
            .SingleOrDefaultAsync();

        public void CreateCategory(Category category) =>
            Create(category);

        public void DeleteCategory(Category category) =>
            Delete(category);
    }
}
