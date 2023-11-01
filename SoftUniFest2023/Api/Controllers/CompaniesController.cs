using Core.Dtos.Requests;
using Core.Dtos.Responses;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Api.Controllers
{
    public class CompaniesController : Controller
    {
        private readonly IAuthService _authService;
        private readonly IConfiguration _configuration;
        public CompaniesController(IAuthService authService,IConfiguration conf)
        {
            this._authService = authService;
            this._configuration = conf;
        }
        [HttpPost("registerCompany")]
        [ProducesResponseType(typeof(CompanyDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> Register(RegisterCompanyDto company)
        {
            CompanyDto result=new CompanyDto();
            try
            {
                result=await _authService.RegisterCompany(company);
            }
            catch (Exception)
            {

                throw;
            }
            result.AccessToken = CreateToken(result);
            return Ok(result);
        }
        [HttpPost("loginCompany")]
        [ProducesResponseType(typeof(CompanyDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> Login(LoginCompanyDto company)
        {
            CompanyDto result = new CompanyDto();
            try
            {
                result = await _authService.LoginCompany(company);
            }
            catch (Exception)
            {

                throw;
            }
            result.AccessToken=CreateToken(result);
            return Ok(result);
        }
        private string CreateToken(CompanyDto company)
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
