using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Payroll.Application.Interfaces.IServices;
using Payroll.Application.Services;
using Payroll.Application.Interfaces.IRepositories;
using Payroll.Domain.Entity;

namespace Payroll.Test
{
    public class PayrollServiceTests
    {
        private readonly Mock<IAllowanceRepository> _mockAllowanceRepository = new Mock<IAllowanceRepository>();

        [Fact]
        public async Task GetAllowanceData_ShouldReturnAllData()
        {
            //arrange
            var listOfAllowance = new List<Allowance>() { new Allowance { Id = Guid.NewGuid(), Amount = 1000, Date = "2022-01-01", DepartmentId = 001, EmployeeId = 001, Status = "Approved" } };
            _mockAllowanceRepository.Setup(s => s.GetAsync()).ReturnsAsync(listOfAllowance);
            var _allowanceService = new AllowanceService(_mockAllowanceRepository.Object);

            //act
            var data = await _allowanceService.GetDataAsync();

            //asset
            _mockAllowanceRepository.Verify(repo => repo.GetAsync(), Times.Once);
            Assert.True(data.Count > 0);

        }
    }
}
