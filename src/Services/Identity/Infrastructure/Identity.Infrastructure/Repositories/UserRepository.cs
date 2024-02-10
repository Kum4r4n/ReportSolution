using Identity.Application.Interfaces.IRepositories;
using Identity.Domain.Entity;
using Identity.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IdentityDbContext _dbContext;

        public UserRepository(IdentityDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<User> Add(User user)
        {
            var data = _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();
            return data.Entity;
        }

        public async Task<User> Get(string email)
        {
            var data = await _dbContext.Users.SingleOrDefaultAsync(s => s.Email == email);
            return data;
        }
    }
}
