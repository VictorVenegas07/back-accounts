using Aplication.Interfaces;
using Aplication.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Feauters.Users.Command.DeleteUserCommand
{
    public class DeleteUserCommand: IRequest<Response<string>>
    {
        public string Id { get; set; }
    }

    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, Response<string>>
    {
        private readonly IAccountService accountService;

        public DeleteUserCommandHandler(IAccountService accountService)
        {
            this.accountService = accountService;
        }

        public Task<Response<string>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
           return  accountService.DeleteUser(request.Id);
        }
    }
}
