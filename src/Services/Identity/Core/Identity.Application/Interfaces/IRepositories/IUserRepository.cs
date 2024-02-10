using Identity.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application.Interfaces.IRepositories
{
    public interface IUserRepository
    {
        Task<User> Add(User user);
        Task<User> Get(Guid id);
    }
}
