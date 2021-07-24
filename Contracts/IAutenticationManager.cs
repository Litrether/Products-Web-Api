using System.Threading.Tasks;
using Entities.DataTransferObjects.Incoming;

namespace Contracts
{
    public interface IAutenticationManager
    {
        public Task<bool> ValidateUser(UserValidationDto userForAuth);

        public Task<string> CreateToken();
    }
}
