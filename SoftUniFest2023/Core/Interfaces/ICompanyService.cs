﻿using Core.Dtos.Requests;
using Core.Dtos.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface ICompanyService
    {
        
        Task<List<CompanyDto>> GetAllVendors();

        Task<List<CompanyDto>> GetByStr(string str);
    }
}
