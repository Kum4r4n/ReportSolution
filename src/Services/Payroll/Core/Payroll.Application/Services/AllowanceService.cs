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
using System.Net.NetworkInformation;

namespace Payroll.Application.Services
{
    public class AllowanceService : IAllowanceService
    {
        private readonly IAllowanceRepository _allowanceRepository;

        public AllowanceService(IAllowanceRepository allowanceRepository)
        {
            _allowanceRepository = allowanceRepository;
        }

        public async Task ImportDataAsync(Stream stream)
        {
            
            var reader = new StreamReader(stream);
            var configuration = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                IgnoreBlankLines = true
                    
            };
            var csv = new CsvReader(reader, configuration);
            var data = csv.GetRecords<AllowanceCSVModel>();
            var entities = data.Where(w=> w.EmployeeId != null).Select(s => new Allowance()
            {
                Id = Guid.NewGuid(),
                Amount = s.Amount ?? 0,
                Date = s.Date,
                DepartmentId = s.DepartmentId ?? 0,
                EmployeeId = s.EmployeeId ?? 0,
                Status = s.Status

            }).ToList();

            var savedData = await _allowanceRepository.AddAsync(entities);
            
        }

        public async Task<List<AllowanceResponseModel>> GetDataAsync()
        {
            var data = await _allowanceRepository.GetAsync();
            var result = data.Select(s => new AllowanceResponseModel()
            {
                Id = Guid.NewGuid(),
                Amount = s.Amount,
                Date = s.Date,
                DepartmentId = s.DepartmentId,
                EmployeeId = s.EmployeeId,
                Status = s.Status
            }).ToList();

            return result;
        }
    }
}
