using CsvHelper.Configuration;
using CsvHelper;
using Payroll.Application.Interfaces.IServices;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Payroll.Domain.Entity;
using Payroll.Application.Models;
using Payroll.Application.Interfaces.IRepositories;

namespace Payroll.Application.Services
{
    public class AllowanceService : IAllowanceService
    {
        private readonly IAllowanceRepository _allowanceRepository;

        public AllowanceService(IAllowanceRepository allowanceRepository)
        {
            _allowanceRepository = allowanceRepository;
        }

        public async Task ImportData(StreamReader reader)
        {
            var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture));
            var data = csv.GetRecords<AllowanceCSVModel>();
            var entities = data.Select(s=> new Allowance()
            {
                Id = Guid.NewGuid(),
                Amount = s.Amount,
                Date = s.Date,
                DepartmentId = s.DepartmentId,
                EmployeeId = s.EmployeeId,
                Status = s.Status

            }).ToList();

            var savedData = await _allowanceRepository.AddAsync(entities);
        }
    }
}
