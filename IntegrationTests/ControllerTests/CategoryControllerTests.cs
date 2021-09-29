using AutoMapper;
using Contracts;
using Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;
using Products;
using Products.Controllers;
using System.IO;
using System.Net.Http;

namespace IntegrationTests.ControllerTests
{
    public class CategoryControllerTests
    {
        CategoryController _controller;
        private readonly HttpClient _client;
        private readonly Mock<ILoggerManager> _loggerManager = new Mock<ILoggerManager>();
        private readonly Mock<IMapper> _mapper = new Mock<IMapper>();
        private readonly Mock<IRepositoryManager> _repositoryManager = new Mock<IRepositoryManager>();
        private RepositoryContext _context;

        public CategoryControllerTests()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            var webBuilder = new WebHostBuilder()
                .UseConfiguration(builder.Build())
                .UseStartup<Startup>();

            var server = new TestServer(webBuilder);

            _client = server.CreateClient();

            var mapper = new MapperConfiguration(cnf =>
            {
                cnf.AddProfile(new MappingProfile());
            });
            var _mapper = mapper.CreateMapper();

            IConfiguration configuration;
            var configurationBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            configuration = builder.Build();
            var optionsBuilder = new DbContextOptionsBuilder<RepositoryContext>();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("sqlConnection"));
            _context = new RepositoryContext(optionsBuilder.Options);

            _controller = new CategoryController(_repositoryManager.Object, _loggerManager.Object, _mapper.Object);
        }
    }
}
