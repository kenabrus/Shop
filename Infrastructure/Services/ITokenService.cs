using System.Threading.Tasks;
using Core.Entities.Identity;

namespace Infrastructure.Services
{
    public interface ITokenService
    {
         Task<string> CreateToken(AppUser user);
    }
}