﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dtos.Requests
{
    public class EthRequestDto
    {
        public string ClientPrivateKey { get; set; }
        public string Amount { get; set; }
        public string CompanyAccount { get; set; }
        public string ClientAccount { get; set; }
        public string ClientName { get; set; }
        public string ProductName { get; set; }
    }
}
