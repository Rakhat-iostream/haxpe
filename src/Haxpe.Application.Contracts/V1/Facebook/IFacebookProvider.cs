using System;
using System.Threading.Tasks;

namespace Haxpe.V1.Facebook
{
    public interface IFacebookProvider
    {
        Task<FacebookCustomerInfo> GetCustomerInfo(FacebookCredentials credentials);
    }
}
