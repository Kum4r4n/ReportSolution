using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application.Interfaces.IServices
{
    public interface ITokenService
    {
        string GenerateAccessToken(IEnumerable<Claim> claims);
    }
}
