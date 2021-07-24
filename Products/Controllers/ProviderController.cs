using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Contracts;
using Entities.DataTransferObjects.Incoming;
using Entities.DataTransferObjects.Outcoming;
using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Products.ActionFilters;

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

            var providersDto = _mapper.Map<IEnumerable<ProviderOutgoingDto>>(providers);

            return Ok(providersDto);
        }

        /// <summary> Get provider by id </summary>
        /// <param name="id"></param>
        /// <returns> Provider with a given id </returns>
        [HttpGet("{id}", Name = "GetProvider")]
        [ServiceFilter(typeof(ValidateProviderAttribute))]
        public async Task<IActionResult> GetProvider(int id)
        {
            var providerEntity = await _repository.Provider.GetProviderAsync(id, trackChanges: false);

            var providerDto = _mapper.Map<ProviderOutgoingDto>(providerEntity);
            return Ok(providerDto);
        }

        /// <summary> Create newly provider </summary>
        /// <param name="provider"></param>
        /// <returns> Created provider with id </returns>
        [HttpPost(Name = "CreateProvider")]
        [Authorize(Roles = ("Administrator"))]
        [ServiceFilter(typeof(ValidateProviderAttribute))]
        public async Task<IActionResult> CreateProvider(
            [FromBody] ProviderIncomingDto provider)
        {
            var providerEntity = _mapper.Map<Provider>(provider);

            _repository.Provider.CreateProvider(providerEntity);

            try
            {
                await _repository.SaveAsync();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }

            var providerToReturn = _mapper.Map<ProviderOutgoingDto>(providerEntity);

            return RedirectToRoute("GetProvider",
                new { id = providerToReturn.Id });
        }

        /// <summary> Update an existing provider by id </summary>
        /// <param name="id"></param>
        /// <param name="provider"></param>
        /// <returns> No content </returns>
        [HttpPut("{id}", Name = "UpdateProvider")]
        [Authorize(Roles = ("Administrator"))]
        [ServiceFilter(typeof(ValidateProviderAttribute))]
        public async Task<IActionResult> UpdateProvider(int id,
            [FromBody] ProviderIncomingDto provider)
        {
            var providerEntity = await _repository.Provider.GetProviderAsync(id, trackChanges: true);

            _mapper.Map(provider, providerEntity);

            try
            {
                await _repository.SaveAsync();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }

            return NoContent();
        }

        /// <summary> Delete an existing provider by id </summary>
        /// <param name="id"></param>
        /// <returns> No content </returns>
        [HttpDelete("{id}", Name = "DeleteProvider")]
        [Authorize(Roles = ("Administrator"))]
        [ServiceFilter(typeof(ValidateProviderAttribute))]
        public async Task<IActionResult> DeleteProvider(int id)
        {
            var providerEntity = await _repository.Provider.GetProviderAsync(id, trackChanges: false);

            _repository.Provider.DeleteProvider(providerEntity);

            try
            {
                await _repository.SaveAsync();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }

            return NoContent();
        }
    }
}