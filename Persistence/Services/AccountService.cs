using Aplication.BTOs.User;
using Aplication.Enums;
using Aplication.Exceptions;
using Aplication.Feauters.Users.Command.DeleteUserCommand;
using Aplication.Interfaces;
using Aplication.Wrappers;
using Domain.Entities;
using Domain.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Persistence.Helpers;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Services
{
     public class AccountService : IAccountService
    {
        private readonly UserManager<User> userManager;
        private readonly JWTSettings jWTSettings;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly SignInManager<User> signInManager;
        private readonly IDateTimeService dateTimeService;

        public AccountService(UserManager<User> userManager, IOptions<JWTSettings> jWTSettings, RoleManager<IdentityRole> roleManager, SignInManager<User> signInManager, IDateTimeService dateTimeService)
        {
            this.userManager = userManager;
            this.jWTSettings = jWTSettings.Value;
            this.roleManager = roleManager;
            this.signInManager = signInManager;
            this.dateTimeService = dateTimeService;
        }

        public async Task<Response<AuthenticationResponse>> AuthenticateAsync(AuthenticationRequest request, string ipAdrres)
        {
           var user = await userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                throw new ApiException($"No existe una cuenta con este {request.Email}");
            }

            var result = await signInManager.PasswordSignInAsync(user, request.Password, false, lockoutOnFailure: false);
            if (!result.Succeeded)
            {
                throw new ApiException($"las credenciales del usuario {user.Email} no son validas");
            }

            JwtSecurityToken jwtSecurity = await GenerateJWToken(user);
            var rolesList = await userManager.GetRolesAsync(user).ConfigureAwait(false);
            var refreshToken =  GenerateRefreshToken(ipAdrres);
            AuthenticationResponse response = new AuthenticationResponse
            {
                Id = user.Id,
                JWToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurity),
                Email = user.Email,
                UserName = user.UserName,
                Roles = rolesList.ToList(),
                IsVerified = user.EmailConfirmed,
                RefreshToken = refreshToken.Token,
            };
            return new Response<AuthenticationResponse>(response, $"Usuario autenticado {user.UserName}");

        }


        public async Task<Response<string>> RegisterAsync(RegisterRequest request, string origin)
        {
            var userName = await userManager.FindByNameAsync(request.UserName);
            if(userName != null)
            {
                throw new ApiException($"el nombre de usuario {request.UserName} ya fue registrado anteriormente ");
            }
            var user = new User
            {
                Email = request.Email,
                Name = request.Name,
                UserName = request.UserName,
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };

            var userEmail = await userManager.FindByEmailAsync(request.Email);
            if(userEmail != null)
            {
                throw new ApiException($"el email de usuario {request.Email} ya fue registrado anteriormente ");

            }
            else
            {
                var result = await userManager.CreateAsync(user, request.Password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, Roles.Basic.ToString());
                    return new Response<string>(user.Id, message: $"Usuario registrado con exito");
                }
                else
                {
                    throw new ApiException($"{result.Errors}");
                }
            }
        }
        private async Task<JwtSecurityToken> GenerateJWToken(User usuario)
        {
            var userClaims = await userManager.GetClaimsAsync(usuario);
            var roles = await userManager.GetRolesAsync(usuario);
            var roleClaims = new List<Claim>();
            foreach (var item in roles)
            {
                roleClaims.Add(new Claim("roles", item));
            }
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub,usuario.Name),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email,usuario.Email),
                new Claim("Uid", usuario.Id),
                new Claim("ip",IpHelpers.GetIpAddress()),
            }.Union(roleClaims).Union(userClaims);
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jWTSettings.Key));
            var singningCredential = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
            var jwtSecurityToken = new JwtSecurityToken(
                issuer:jWTSettings.Issuer,
                audience:jWTSettings.Audience,
                claims:claims,
                expires: dateTimeService.Now.AddMinutes(jWTSettings.DurationMinutes),
                signingCredentials:singningCredential

                );
            return jwtSecurityToken;
        }
    
        private RefreshToken GenerateRefreshToken(string ipAdrres)
        {
            return new RefreshToken
            {
                Token = RandomTokenString(),
                Expires = DateTime.Now.AddDays(7),
                Created = DateTime.Now,
                CreateByIp = ipAdrres
            };
        }
        private string RandomTokenString()
        {
            using var rngCryptoSericeProvider = new RNGCryptoServiceProvider();
            var randomBytes = new byte[40];
            rngCryptoSericeProvider.GetBytes(randomBytes);
            return BitConverter.ToString(randomBytes).Replace("-", "");
        }


        public async Task<Response<string>> DeleteUser(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            if (user == null)
            {
                throw new ApiException($"El usuario que desea eliminar no existe");

            }
            else
            {
                await userManager.DeleteAsync(user);

                return new Response<string>("Usuario con eliminado con exito");
            }
        }
    }
}
