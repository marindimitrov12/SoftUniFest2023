using Core.Dtos.Requests;
using Core.Dtos.Responses;
using Core.Interfaces;
using Data;
using Data.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext _context;
        private readonly HttpClient _client;
    
        public AuthService(ApplicationDbContext context,HttpClient client)
        {
                _context = context;
                _client= client;  
           
        }
        public async Task<ClientDto> LoginClient(LoginClientDto client)
        {
            var clientCompany = await _context.Set<Client>().Where(c => c.Email == client.Email).FirstOrDefaultAsync();

            if (clientCompany == null)
            {
                throw new ArgumentException("Client not found");
            }

            if (!VerifyPassword(client.Password, clientCompany.PasswordHash, clientCompany.PasswordSalt))
            {
                throw new ArgumentException("Wrong password");
            }

            return new ClientDto
            {
                Id = clientCompany.Id,
                FirsName = clientCompany.FirstName,
                LastName = clientCompany.LastName,
                Email = clientCompany.Email,
                Role = clientCompany.Role,
            };
        }

        public async Task<CompanyDto> LoginCompany(LoginCompanyDto company)
        {
            var foundCompany = await _context.Set<Company>().Where(c => c.Email == company.Email).FirstOrDefaultAsync();

            if (foundCompany == null)
            {
                throw new ArgumentException("Company not found");
            }

            if (!VerifyPassword(company.Password, foundCompany.PasswordHash, foundCompany.PasswordSalt))
            {
                throw new ArgumentException("Wrong password");
            }

            return new CompanyDto
            {
                Id = foundCompany.Id,
                Name = foundCompany.Name,
                Email = foundCompany.Email,
                Role = foundCompany.Role,
            };
        }

        public async Task<ClientDto> RegisterClient(RegisterClientDto client)
        {
            Client clientEntity = new Client()
            {
                FirstName = client.FirstName,
                LastName = client.LastName,
                Email = client.Email,


            };
            CreatePasswordHash(client.Password, out byte[] passwordHash, out byte[] passwordSalt);
            clientEntity.PasswordSalt = passwordSalt;
            clientEntity.PasswordHash = passwordHash;
            await _context.AddAsync<Client>(clientEntity);
            await _context.SaveChangesAsync();
            var createdClient = await _context.Set<Client>().Where(client => client.Email == clientEntity.Email).FirstOrDefaultAsync();
            return new ClientDto
            {
                Id = createdClient.Id,
                FirsName = client.FirstName,
                LastName = client.LastName,
                Email = client.Email,
                Role = createdClient.Role,
            };
        }

        public async Task<CompanyDto> RegisterCompany(RegisterCompanyDto company)
        {
            Company companyEntity = new Company()
            {
                Name = company.Name,
                Email = company.Email
            };

            CreatePasswordHash(company.Password, out byte[] passwordHash, out byte[] passwordSalt);
            companyEntity.PasswordSalt = passwordSalt;
            companyEntity.PasswordHash = passwordHash;

            await _context.AddAsync<Company>(companyEntity);
            await _context.SaveChangesAsync();

            var createdCompany = await _context.Set<Company>().Where(company => company.Email == companyEntity.Email).FirstOrDefaultAsync();

            return new CompanyDto()
            {
                Name = createdCompany.Name,
                Email = createdCompany.Email,
                Id = createdCompany.Id,
                Role = createdCompany.Role,
            };
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
