using Core.Dtos.Requests;
using Core.Dtos.Responses;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Api.Controllers
{
    public class ClientsController : Controller
        
    {
        private readonly IAuthService _authService;
        private readonly IConfiguration _configuration;
        private readonly IClientService _clientService;
        public ClientsController(IAuthService authService,IConfiguration configuration, IClientService _clientService)
        {
                this._authService = authService;
            this._configuration = configuration;
            this._clientService = _clientService;
        }
        [HttpPost("registerClient")]
        [ProducesResponseType(typeof(ClientDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> Register(RegisterClientDto client)
        {
            ClientDto result = new ClientDto();
            try
            {
                result = await _authService.RegisterClient(client);
            }
            catch (Exception)
            {
                return BadRequest();
            }

            result.AccessToken = CreateToken(result);

            return Ok(result);

        }
        [HttpPost("loginClient")]
        [ProducesResponseType(typeof(ClientDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> Login(LoginClientDto client)
        {
            ClientDto result=new ClientDto();
            try
            {
                result = await _authService.LoginClient(client);
            }
            catch (Exception)
            {

                throw;
            }
            result.AccessToken = CreateToken(result);
            return Ok(result);
        }
        [HttpPost("payWithCripto")]
        [ProducesResponseType(typeof(CriptoPaymentResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> PayWithCripto(EthRequestDto dto)
        {

            var result = await _clientService.Pay(dto.ClientPrivateKey, dto.CompanyAccount, dto.ClientAccount, Convert.ToDecimal(dto.Amount));
            if (result.Error != null)
            {
                return BadRequest(result.Error);
            }

            var res = await _clientService.AddProductToClient(dto.ClientName, dto.ProductName);
            if (res)
            {
                return Ok(result);
            }
            return BadRequest("you already own it");
        }
        private string CreateToken(ClientDto company)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Role, company.Role)
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(2),
                signingCredentials: credentials);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
    }
}
