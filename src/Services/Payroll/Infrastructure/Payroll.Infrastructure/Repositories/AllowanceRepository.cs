using Microsoft.EntityFrameworkCore;
using Payroll.Application.Interfaces.IRepositories;
using Payroll.Domain.Entity;
using Payroll.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll.Infrastructure.Repositories
{
    public class AllowanceRepository : IAllowanceRepository
    {
        private readonly PayrollDbContext _dbContext;

        public AllowanceRepository(PayrollDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Allowance>> AddAsync(List<Allowance> allowances)
        {
            await _dbContext.Allowances.AddRangeAsync(allowances);
            await _dbContext.SaveChangesAsync();
            return allowances;
        }

        public async Task<List<Allowance>> GetAsync()
        {
            var data = await _dbContext.Allowances.ToListAsync();
            return data;
        }
    }
}
