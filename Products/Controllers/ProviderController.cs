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

        /// <summary> Get list of all providers </summary>
        /// <param name="providerParameters"></param>
        /// <returns>The providers list</returns>
        [HttpGet(Name = "GetProviders")]
        public async Task<IActionResult> GetProviders(
            [FromQuery] ProviderParameters providerParameters)
        {
            var providers = await _repository.Provider.GetAllProvidersAsync(
                providerParameters, trackChanges: false);

            var providersDto = _mapper.Map<IEnumerable<ProviderDto>>(providers);

            return Ok(providersDto);
        }

        /// <summary> Get provider by id </summary>
        /// <param name="id"></param>
        /// <returns> Provider with a given id </returns>
        [HttpGet("{id}", Name = "GetProvider")]
        [ServiceFilter(typeof(ValidateProviderExistsAttribute))]
        public IActionResult GetProvider(int id)
        {
            //todo repair 
            var providerEntity = _repository.Provider.GetProviderAsync(id, trackChanges: false);

            var providerDto = _mapper.Map<ProviderDto>(providerEntity);
            return Ok(providerDto);
        }

        /// <summary> Create newly provider </summary>
        /// <param name="provider"></param>
        /// <returns> Created provider with id </returns>
        [HttpPost(Name = "CreateProvider")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ServiceFilter(typeof(ValidateProviderExistsAttribute))]
        public async Task<IActionResult> CreateProvider(
            [FromBody] ProviderForManipulationDto provider)
        {
            var providerEntity = _mapper.Map<Provider>(provider);

            _repository.Provider.CreateProvider(providerEntity);
            await _repository.SaveAsync();

            var providerToReturn = _mapper.Map<ProviderDto>(providerEntity);

            return RedirectToRoute("GetProvider",
                new { id = providerToReturn.Id });
        }

        /// <summary> Update an existing provider by id </summary>
        /// <param name="id"></param>
        /// <param name="provider"></param>
        /// <returns> No content </returns>
        [HttpPut("{id}", Name = "UpdateProvider")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ServiceFilter(typeof(ValidateProviderExistsAttribute))]
        public async Task<IActionResult> UpdateProvider(int id,
            [FromBody] ProviderForManipulationDto provider)
        {
            var providerEntity = await _repository.Provider.GetProviderAsync(id, trackChanges: false);

            _mapper.Map(provider, providerEntity);
            await _repository.SaveAsync();

            return NoContent();
        }

        /// <summary> Delete an existing provider by id </summary>
        /// <param name="id"></param>
        /// <returns> No content </returns>
        [HttpDelete("{id}", Name = "DeleteProvider")]
        [ServiceFilter(typeof(ValidateProviderExistsAttribute))]
        public async Task<IActionResult> DeleteProvider(int id)
        {
            var providerEntity = await _repository.Provider.GetProviderAsync(id, trackChanges: false);

            _repository.Provider.DeleteProvider(providerEntity);
            await _repository.SaveAsync();

            return NoContent();
        }
    }
}
