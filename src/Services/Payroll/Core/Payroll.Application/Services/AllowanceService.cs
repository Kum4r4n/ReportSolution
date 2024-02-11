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

        public async Task<List<Allowance>> ImportDataAsync(StreamReader reader)
        {
            try
            {
                var configuration = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    HasHeaderRecord = true,
                    IgnoreBlankLines = true
                };
                var csv = new CsvReader(reader, configuration);

                //Employee ID, Department ID,Date,Amount,Status
                //,,,,
                //100,1,20222-10-22,10000,Approved
                //300,1,20222-10-26,500000,Pending
                //180,1,20222-10-22,18000,Approved
                //250,1,20222-10-26,650000,Approved
                //The above sample has the wrong date format; there is an extra '2' in the year section. Therefore, I save this date as a string.

                var data = csv.GetRecords<AllowanceCSVModel>();
                var entities = data.Select(s => new Allowance()
                {
                    Id = Guid.NewGuid(),
                    Amount = s.Amount,
                    Date = s.Date,
                    DepartmentId = s.DepartmentId,
                    EmployeeId = s.EmployeeId,
                    Status = s.Status

                }).ToList();

                var savedData = await _allowanceRepository.AddAsync(entities);
                return savedData;
            }
            catch (Exception ex)
            {

                throw;
            }
           
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
