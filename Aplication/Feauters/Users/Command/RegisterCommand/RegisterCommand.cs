using Aplication.BTOs.User;
using Aplication.Interfaces;
using Aplication.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Feauters.Users.Command.RegisterCommand
{
    public class RegisterCommand : IRequest<Response<string>>
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Origin { get; set; }
    }
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, Response<string>>
    {
        private readonly IAccountService accountService;

        public RegisterCommandHandler(IAccountService accountService)
        {
            this.accountService = accountService;
        }

        public async Task<Response<string>> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            return await accountService.RegisterAsync(new RegisterRequest
            {
                Email = request.Email,
                Password = request.Password,
                ConfirmPassword = request.ConfirmPassword,
                UserName = request.UserName,
                Name = request.Name
            }, request.Origin);
        }
    }
}
