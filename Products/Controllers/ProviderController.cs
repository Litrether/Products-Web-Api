using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Products.ActionFilters;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Products.Controllers
{
    [Route("api/providers")]
    [ApiController]
    [ResponseCache(CacheProfileName = "180SecondsDuration")]
    [ApiExplorerSettings(GroupName = "v1")]
    public class ProviderController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public ProviderController(IRepositoryManager repository, ILoggerManager logger,
            IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet(Name = "GetProviders")]
        public async Task<IActionResult> GetProviders(
            [FromQuery] ProviderParameters providerParameters)
        {
            var providers = await _repository.Provider.GetAllProvidersAsync(
                providerParameters, trackChanges: false);

            var providersDto = _mapper.Map<IEnumerable<ProviderDto>>(providers);

            return Ok(providersDto);
        }

        [HttpGet("{id}", Name = "GetProvider")]
        [ServiceFilter(typeof(ValidateProviderExistsAttribute))]
        public IActionResult GetProvider(int id)
        {
            var providerEntity = HttpContext.Items["provider"] as Provider;

            var providerDto = _mapper.Map<ProviderDto>(providerEntity);
            return Ok(providerDto);
        }

        [HttpPost(Name = "CreateProvider")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateProvider(
            [FromBody] ProviderForManipilationDto provider)
        {
            var providerEntity = _mapper.Map<Provider>(provider);

            _repository.Provider.CreateProvider(providerEntity);
            await _repository.SaveAsync();

            var providerToReturn = _mapper.Map<ProviderDto>(providerEntity);

            return CreatedAtRoute("GetProvider",
                new { id = providerToReturn.Id }, providerToReturn);
        }

        [HttpPut("{id}", Name = "UpdateProvider")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ServiceFilter(typeof(ValidateProviderExistsAttribute))]
        public async Task<IActionResult> UpdateProvider(int id,
            [FromBody] ProviderForManipilationDto provider)
        {
            var providerEntity = HttpContext.Items["provider"] as Provider;

            _mapper.Map(provider, providerEntity);
            await _repository.SaveAsync();

            return NoContent();
        }

        [HttpDelete("{id}", Name = "DeleteProvider")]
        [ServiceFilter(typeof(ValidateProviderExistsAttribute))]
        public async Task<IActionResult> DeleteProvider(int id)
        {
            var providerEntity = HttpContext.Items["provider"] as Provider;

            _repository.Provider.DeleteProvider(providerEntity);
            await _repository.SaveAsync();

            return NoContent();
        }
    }
}
