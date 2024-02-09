using Payroll.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll.Application.Interfaces.IRepositories
{
    public interface IAllowanceRepository
    {
        Task<List<Allowance>> AddAsync(List<Allowance> allowances);
        Task<List<Allowance>> GetAsync();
    }
}
