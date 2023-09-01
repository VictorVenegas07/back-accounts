using Aplication.BTOs.User;
using Aplication.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Interfaces
{
    public interface IAccountService
    {
        Task<Response<AuthenticationResponse>> AuthenticateAsync(AuthenticationRequest request, string ipAdrres);
        Task<Response<string>> RegisterAsync(RegisterRequest request, string origin);
        Task<Response<string>> DeleteUser(string id);

    }
}
