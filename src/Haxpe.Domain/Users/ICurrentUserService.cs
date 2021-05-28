using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haxpe.Users
{
    public interface ICurrentUserService
    {
        Task<Guid> GetCurrentUserIdAsync();

        Task<User> GetCurrentUserAsync();

        Task<IReadOnlyCollection<string>> GetCurrentUserRolesAsync();

        Task<IReadOnlyCollection<string>> GetCurrentUserRolesAsync(User user);
    }
}
