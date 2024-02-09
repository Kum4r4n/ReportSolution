using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll.Application.Models
{
    public class AllowanceCSVModel
    {
        public int EmployeeId { get; set; }
        public int DepartmentId { get; set; }
        public DateTime Date { get; set; }
        public long Amount { get; set; }
        public string Status { get; set; } = "";
    }
}
