using Haxpe.Services;
using IdentityModel;
using System.Linq;
using System.Threading.Tasks;

namespace Haxpe.Infrastructure
{
    public class PasswordsGenService : IPasswordsGenService
    {

        public async Task<string> GetRandomAlphaNumeric()
        {
            CryptoRandom random = new CryptoRandom();
            var chars =
                "ABCDEFGHJKLMNOPQRSTUVWXYZabcdefghijkmnopqrstuvwxyz0123456789!@$?_-";
            return new string(chars.OrderBy(c => random.Next(chars.Length)).Take(6).ToArray());
        }
    }
}
