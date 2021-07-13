using System.Threading.Tasks;
using Entities.DataTransferObjects;

namespace Contracts
{
    public interface IAutenticationManager
    {
        public Task<bool> ValidateUser(UserForAutenticationDto userForAuth);

        public Task<string> CreateToken();
    }
}
