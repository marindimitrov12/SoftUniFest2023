using Azure.Core;
using Core.Dtos.Requests;
using Core.Dtos.Responses;
using Core.Interfaces;
using Data;

using Data.Model;
using Microsoft.EntityFrameworkCore;

using System.Security.Cryptography;
using System.Security.Principal;

namespace Services
{
    public class CompanyService : ICompanyService
    {
        private readonly DbContext _context;
        public CompanyService(DbContext context)
        {
            _context = context;
        }

        public async Task<List<CompanyDto>> GetAllVendors()
        {
            var companies = await _context.Set<Company>().ToListAsync();
            var result = new List<CompanyDto>();
            foreach (var company in companies)
            {
                var companyDto = new CompanyDto()
                {
                    Id = company.Id,
                    Name = company.Name,
                    Role = company.Role,
                    Email = company.Email,

                };
                result.Add(companyDto);
            }
            return result;
        }

        public async Task<List<CompanyDto>> GetByStr(string str)
        {
            var companies = await _context.Set<Company>().Where(x => x.Name.Contains(str)).ToListAsync();
            var result = new List<CompanyDto>();
            foreach (var company in companies)
            {
                result.Add(new CompanyDto { Id = company.Id, Email = company.Email, Name = company.Name });
            }
            return result;
        }

       


       

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPassword(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }
    }
}