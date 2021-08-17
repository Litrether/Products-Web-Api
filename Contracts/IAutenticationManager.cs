using Entities.DataTransferObjects.Incoming;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IAutenticationManager
    {
        public Task<bool> ValidateUser(UserValidationDto userForAuth);

        public Task<string> CreateToken();
    }
}
