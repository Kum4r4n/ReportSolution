using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Payroll.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll.Infrastructure.Context
{
    public class PayrollDbContext : DbContext
    {
        public PayrollDbContext(DbContextOptions<PayrollDbContext> options):base(options)
        {
            
        }

        public DbSet<Allowance> Allowances { get; set; }
    }
}
