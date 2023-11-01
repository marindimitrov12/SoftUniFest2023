using Core.Dtos.Requests;
using Core.Dtos.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IClientService
    {
        Task<ClientDto> Register(RegisterClientDto client);
        Task<ClientDto> Login(LoginClientDto company);
        Task<CriptoPaymentResponse> Pay(string clientPrivateKey, string companyAccount, string clientAccount, decimal amount);
        Task<bool> AddProductToClient(string clientname, string productname);
    }
}
