﻿using ContactsApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactsApplication.Services
{
    public interface IAuthService
    {
        Task<TokenInfo> GetToken();
        Task Authenticate();
        Task<bool> ValidateToken();
    }
}