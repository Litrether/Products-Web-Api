using System.Threading.Tasks;
using Contracts;
using Entities;
using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.EntityFrameworkCore;
using Repository.Extensions;

namespace Repository
{
    public class CategoryRepository : RepositoryBase<Category>, ICategoryRepository
    {
        public CategoryRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }

        public async Task<(PagedList<Category>, int)> GetAllCategoriesAsync(
            CategoryParameters categoryParameters, bool trackChanges)
        {
            var categories = await FindAll(trackChanges)
                .Search(categoryParameters.SearchTerm)
                .Sort(categoryParameters.OrderBy)
                .ToListAsync();

            

            return (PagedList<Category>
                .ToPagedList(categories, categoryParameters.PageNumber,
                    categoryParameters.PageSize), categories.Count);
        }

        public async Task<Category> GetCategoryAsync(int categoryId, bool trackChanges) =>
            await FindByCondition(c => c.Id.Equals(categoryId), trackChanges)
            .SingleOrDefaultAsync();

        public async Task<int> GetCountAsync() =>
            await FindAll(trackChanges: false).CountAsync();


        public void CreateCategory(Category category) =>
            Create(category);

        public void DeleteCategory(Category category) =>
            Delete(category);
    }
}
