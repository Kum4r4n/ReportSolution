using Identity.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application.Interfaces.IServices
{
    public interface IUserService
    {
        Task<TokenModel> Login(LoginRequestModel loginRequestModel);
        Task<TokenModel> RegisterUser(RegisterRequestModel registerRequestModel);
    }
}
