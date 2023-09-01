using Aplication.BTOs.User;
using Aplication.Interfaces;
using Aplication.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Feauters.Users.Command.AuthenticateCommand
{
    public class AuthenticateCommand : IRequest<Response<AuthenticationResponse>>
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string IpAddress { get; set; }
    }
    public class AuthenticateCommandHandler : IRequestHandler<AuthenticateCommand, Response<AuthenticationResponse>>
    {
        private readonly IAccountService accountService;

        public AuthenticateCommandHandler(IAccountService accountService)
        {
            this.accountService = accountService;
        }

        public async Task<Response<AuthenticationResponse>> Handle(AuthenticateCommand request, CancellationToken cancellationToken)
        {
            return await accountService.AuthenticateAsync(new AuthenticationRequest
            { Email = request.Email, Password = request.Password }
            , request.IpAddress);
        }
    }
}
