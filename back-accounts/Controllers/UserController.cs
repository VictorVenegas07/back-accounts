using Aplication.BTOs.User;
using Aplication.Feauters.Users.Command.AuthenticateCommand;
using Aplication.Feauters.Users.Command.DeleteUserCommand;
using Aplication.Feauters.Users.Command.RegisterCommand;
using Aplication.Feauters.Users.Command.UpdateUserCommand;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;


namespace back_accounts.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseApiController
    {
        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<IActionResult> AuthenticateAsync(AuthenticationRequest request)
        {
            return Ok(await Mediator.Send(new AuthenticateCommand
            {
                Email = request.Email,
                Password = request.Password,
                IpAddress = GenerateIPAddress()
            }));
        }
        [HttpPost("register")]
        public async Task<ActionResult> RegisterAsync(RegisterRequest request)
        {
            return Ok(await Mediator.Send(new RegisterCommand
            {
                Email = request.Email,
                Password = request.Password,
                ConfirmPassword = request.ConfirmPassword,
                UserName = request.UserName,
                Name = request.Name,
                Origin = Request.Headers["origin"]
            }));

        }
        [HttpDelete("Delete")]
        [Authorize]
        public async Task<ActionResult> RegisterAsync(string id)
        {
            return Ok(await Mediator.Send(new DeleteUserCommand { Id = id }));

        }
        private string GenerateIPAddress()
        {
            if (Request.Headers.ContainsKey("X-Forwarded-For"))
                return Request.Headers["X-Forwarded-For"];
            else
                return HttpContext.Connection.RemoteIpAddress.ToString();
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, UpdateUserCommand command)
        {
            if (id != command.Id)
                return BadRequest();

            return Ok(await Mediator.Send(command));
           
        }
    }
}
