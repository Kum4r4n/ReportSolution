using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll.Application.Models
{
    public class ImportStatusModel
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }

        public ImportStatusModel(bool isSuccess, string message)
        {
            IsSuccess = isSuccess;
            Message = message;
        }

    }
}
