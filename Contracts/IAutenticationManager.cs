using Entities.DataTransferObjects;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IAutenticationManager
    {
        public Task<bool> ValidateUser(UserForManipulationDto userForAuth);

        public Task<string> CreateToken();
    }
}
