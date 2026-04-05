using Microsoft.EntityFrameworkCore;
using SuperMarket.Data.DBContexte;
using SuperMarket.Domain.Entities;
using SuperMarket.Data.Interfaces;

namespace SuperMarket.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly SuperMarketDbContext _Context;

        public UserRepository(SuperMarketDbContext context)
        {
            _Context = context;
        }

        public async Task<List<Userr>> GetUsersAsync()
        {
            return await _Context.Users.ToListAsync();
        }

        public async Task UpdateUserAsync(Userr user)
        {
            _Context.Users.Update(user);
            await _Context.SaveChangesAsync();  
        }

        public async Task<Userr> GetUserById(int id)
        {
            return await _Context.Users.FirstOrDefaultAsync(u => u.UserId == id);
        }

        public async Task<Userr> GetUserByUserName(string username)
        {
            return await _Context.Users.FirstOrDefaultAsync(u => u.UserName == username);
        }
    }
}
