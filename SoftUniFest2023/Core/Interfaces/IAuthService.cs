using Core.Dtos.Requests;
using Core.Dtos.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IAuthService
    {
        Task<ClientDto> RegisterClient(RegisterClientDto client);
        Task<ClientDto> LoginClient(LoginClientDto company);

        Task<CompanyDto> RegisterCompany(RegisterCompanyDto company);

        Task<CompanyDto> LoginCompany(LoginCompanyDto company);
    }
}
