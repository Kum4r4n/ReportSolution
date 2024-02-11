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

        [Fact]
        public async Task ImportAllowances_ShouldReturnSuccessResponse()
        {
            // Arrange
            var stream = new MemoryStream();
            var streamWriter = new StreamWriter(stream);

            // Write CSV content to the stream
            streamWriter.Write("EmployeeId,DepartmentId,Date,Amount,Status\n" +
                               "100,1,2022-10-22,10000,Approved\n" +
                               "300,1,2022-10-26,500000,Pending\n" +
                               "180,1,2022-10-22,18000,Approved\n" +
                               "250,1,2022-10-26,650000,Approved");

            streamWriter.Flush();
            stream.Position = 0;

            _mockAllowanceRepository.Setup(repo => repo.AddAsync(It.IsAny<List<Allowance>>())).ReturnsAsync(new List<Allowance>());

            var _allowanceService = new AllowanceService(_mockAllowanceRepository.Object);

            // Act
            await _allowanceService.ImportDataAsync(stream);

            // Assert
            _mockAllowanceRepository.Verify(repo => repo.AddAsync(It.IsAny<List<Allowance>>()), Times.Once);
        }
    }
}
