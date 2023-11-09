using Core.Dtos.Requests;
using Core.Dtos.Responses;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Api.Controllers
{
    public class CompaniesController : Controller
    {
        private readonly IAuthService _authService;
        private readonly IConfiguration _configuration;
        private readonly ICompanyService _companyService;
        public CompaniesController(IAuthService authService,IConfiguration conf, ICompanyService _companyService)
        {
            this._authService = authService;
            this._configuration = conf;
            this._companyService = _companyService;
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
        [HttpGet("getAllVendors")]
        [ProducesResponseType(typeof(CompanyDto), StatusCodes.Status200OK)]
        [Authorize(Roles = "Client")]
        public async Task<IActionResult> GetAllVendor()
        {
            var result = await _companyService.GetAllVendors();
            return Ok(result);
        }
        [HttpGet("getByStr")]
        [Authorize(Roles = "Client")]
        [ProducesResponseType(typeof(CompanyDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetByStr(string str)
        {
            var result = await _companyService.GetByStr(str);
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
