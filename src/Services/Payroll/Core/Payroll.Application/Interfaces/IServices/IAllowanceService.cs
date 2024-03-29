﻿using Payroll.Application.Models;
using Payroll.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll.Application.Interfaces.IServices
{
    public interface IAllowanceService
    {
        Task ImportDataAsync(Stream stream);
        Task<List<AllowanceResponseModel>> GetDataAsync();
    }
}
