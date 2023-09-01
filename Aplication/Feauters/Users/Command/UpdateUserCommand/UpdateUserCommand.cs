using Aplication.Exceptions;
using Aplication.Interfaces;
using Aplication.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Feauters.Users.Command.UpdateUserCommand
{
    public class UpdateUserCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
        public string EMail { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }

    }
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, Response<int>>
    {
        private readonly IRepositoryAsync<User> _repositoryAsync;
        private readonly IMapper _mapper;
        public UpdateUserCommandHandler(IRepositoryAsync<User> repositoryAsync, IMapper mapper)
        {
            _repositoryAsync= repositoryAsync;
            _mapper= mapper;
        }

        public async Task<Response<int>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _repositoryAsync.GetByIdAsync(request.Id);
            if (user == null)
            {
                throw new KeyNotFoundException($"Usuario con el ID {request.Id} no encontrado");
            }
            else
            {
                user.UserName = request.UserName;
                user.Email = request.EMail;
                user.Name= request.Name;
                user.PasswordHash = request.Password;

                await _repositoryAsync.UpdateAsync(user);

                return new Response<int>(user.Id);
            }
        }
    }
}
