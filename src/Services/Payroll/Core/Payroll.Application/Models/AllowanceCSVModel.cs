using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Payroll.Application.Models
{
    public class AllowanceCSVModel
    {
        [Index(0)]
        public int EmployeeId { get; set; }
        [Index(1)]
        public int DepartmentId { get; set; }
        [Index(2)]
        public string Date { get; set; }
        [Index(3)]
        public long Amount { get; set; }
        [Index(4)]
        public string Status { get; set; }
    }
}
