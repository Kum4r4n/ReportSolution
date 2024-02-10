using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application.Interfaces.IServices
{
    public interface IPasswordService
    {
        string Hash(string password);
        bool Verify(string password, string hash);

    }
}
