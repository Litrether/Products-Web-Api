using Contracts;
using Entities.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Threading.Tasks;

namespace Products.ActionFilters
{
    public class ValidateProviderExistsAttribute : IAsyncActionFilter
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;

        public ValidateProviderExistsAttribute(IRepositoryManager repository,
            ILoggerManager logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context,
            ActionExecutionDelegate next)
        {
            var method = context.HttpContext.Request.Method;

            switch (method)
            {
                case "GET":
                    await GetByIdFilter(context, next);
                    break;
                case "POST":
                    await PostFilter(context, next);
                    break;
                case "PUT":
                    await PutFilter(context, next);
                    break;
                case "DELETE":
                    await DeleteFilter(context, next);
                    break;
            }
        }

        private async Task GetByIdFilter(ActionExecutingContext context,
            ActionExecutionDelegate next)
        {
            var id = (int)context.ActionArguments["id"];

            var provider = await _repository.Provider.GetProviderAsync(id, trackChanges: false);
            if (provider == null)
            {
                _logger.LogInfo($"Provider with id: {id} doesn't exist in the database");
                context.Result = new NotFoundResult();
                return;
            }

            await next();
        }

        private async Task PostFilter(ActionExecutingContext context,
            ActionExecutionDelegate next)
        {
            var providerForCreate = context.ActionArguments["provider"] as ProviderForManipulationDto;

            var isExistInDatabase = await _repository.Provider.CheckExistByName(providerForCreate.Name, trackChanges: false);
            if (isExistInDatabase)
            {
                _logger.LogInfo($"Provider with name: \"{providerForCreate.Name}\" exists in the database");
                context.Result = new BadRequestObjectResult($"Provider with name: \"{providerForCreate.Name}\" exists in the database");
                return;
            }

            await next();
        }

        private async Task PutFilter(ActionExecutingContext context,
            ActionExecutionDelegate next)
        {
            var id = (int)context.ActionArguments["id"];
            var providerForUpdate = context.ActionArguments["provider"] as ProviderForManipulationDto;

            var provider = await _repository.Category.GetCategoryAsync(id, trackChanges: true);
            if (provider == null)
            {
                _logger.LogInfo($"Provider with id: {id} doesn't exist in the database");
                context.Result = new NotFoundResult();
                return;
            }

            var isExistInDatabase = await _repository.Provider.CheckExistByName(providerForUpdate.Name, trackChanges: false);
            if (isExistInDatabase)
            {
                _logger.LogInfo($"Provider with name: \"{providerForUpdate.Name}\" exists in the database");
                context.Result = new BadRequestObjectResult($"Provider with name: \"{providerForUpdate.Name}\" exists in the database");
                return;
            }

            await next();
        }

        private async Task DeleteFilter(ActionExecutingContext context,
            ActionExecutionDelegate next)
        {
            var id = (int)context.ActionArguments["id"];

            var provider = await _repository.Provider.GetProviderAsync(id, trackChanges: false);
            if (provider == null)
            {
                _logger.LogInfo($"Provider with id: {id} doesn't exist in the database");
                context.Result = new NotFoundResult();
                return;
            }

            await next();
        }
    }
}
