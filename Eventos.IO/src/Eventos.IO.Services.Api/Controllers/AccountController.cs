using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Eventos.IO.Domain.Core.Notifications;
using Eventos.IO.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Eventos.IO.Infra.CrossCutting.Identity.Models;
using Microsoft.Extensions.Logging;
using Eventos.IO.Domain.Core.Bus;
using Eventos.IO.Infra.CrossCutting.Identity.Models.AccountViewModels;
using Eventos.IO.Domain.Organizadores.Commands;
using Microsoft.AspNetCore.Authorization;
using Eventos.IO.Infra.CrossCutting.Identity.Authorization;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace Eventos.IO.Services.Api.Controllers
{
    
    public class AccountController : BaseController
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger _logger;
        private readonly IBus _bus;
        private readonly JwtTokenOptions _jwtTokenOptions;

        public AccountController(IDomainNotificationHandler<DomainNotification> notifications, 
                                 IUser user,
                                 UserManager<ApplicationUser> userManager,
                                 SignInManager<ApplicationUser> signInManager,
                                 ILoggerFactory loggerFactory,
                                 IOptions<JwtTokenOptions> jwtTokenOptions,
                                 IBus bus) 
            : base(notifications, user, bus)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _bus = bus;

            _jwtTokenOptions = jwtTokenOptions.Value;
            ThrowIfInvalidOptions(_jwtTokenOptions);

            _logger = loggerFactory.CreateLogger<AccountController>();
        }

        private static long ToUnixEpochDate(DateTime date)
            => (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);

        [HttpPost]
        [AllowAnonymous]
        [Route("nova-conta")]
        public async Task<IActionResult> Register([FromBody] RegisterViewModel model, int version)
        {
            if(version == 2)
            {
                return Response(new { Message = "API v2 não disponivel" });
            }

            if (!ModelState.IsValid) return Response(model);

            var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
            var result = await _userManager.CreateAsync(user, model.Password);

            if(result.Succeeded)
            {
                var registroCommand = new RegistrarOrganizadorCommand(Guid.Parse(user.Id), model.Nome, model.CPF, model.Email);
                _bus.SendCommand(registroCommand);

                if (!OperacaoValida())
                {
                    await _userManager.DeleteAsync(user);
                    return Response(model);
                }

                _logger.LogInformation(1, "Usuario criado com sucesso!");
                var response = GerarTokenUsuario(new LoginViewModel { Email = model.Email, Password = model.Password });
                return Response(response);
            }
            AdicionarErrosIdentity(result);
            return Response(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("conta")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            if (!ModelState.IsValid) {
                NotificarErroModelInvalida();
                return Response(model);    
            }

            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, lockoutOnFailure: true);

            if (result.Succeeded)
            {
                _logger.LogInformation(1, "Usuario logado com sucesso");
                var response = GerarTokenUsuario(model);
                return Response(response);
            }

            NotificarErro(result.ToString(), "Falha ao realizar login");
            return Response(model);
        }

        private static void ThrowIfInvalidOptions(JwtTokenOptions options)
        {
            if (options == null) throw new ArgumentNullException(nameof(options));

            if(options.ValidFor <= TimeSpan.Zero)
            {
                throw new ArgumentException("Must be a non-zero TimeSpan.", nameof(JwtTokenOptions.ValidFor));
            }

            if(options.SingingCredentials == null)
            {
                throw new ArgumentNullException(nameof(JwtTokenOptions.SingingCredentials));
            }

            if(options.JtiGenerator == null)
            {
                throw new ArgumentNullException(nameof(JwtTokenOptions.JtiGenerator));
            }
        }

        private async Task<object> GerarTokenUsuario(LoginViewModel login)
        {
            var user = await _userManager.FindByEmailAsync(login.Email);
            var userClaims = await _userManager.GetClaimsAsync(user);

            userClaims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id));
            userClaims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
            userClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, await _jwtTokenOptions.JtiGenerator()));
            userClaims.Add(new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(_jwtTokenOptions.IssuedAt).ToString(), ClaimValueTypes.Integer64));

            var jwt = new JwtSecurityToken(
                issuer: _jwtTokenOptions.Issuer,
                audience: _jwtTokenOptions.Audience,
                claims: userClaims,
                notBefore: _jwtTokenOptions.NotBefore,
                expires: _jwtTokenOptions.Expiration,
                signingCredentials: _jwtTokenOptions.SingingCredentials
                );

            var encodeJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new
            {
                access_token = encodeJwt,
                expires_in = (int)_jwtTokenOptions.ValidFor.TotalSeconds,
                user = user
            };

            return response;
        }
    }
}