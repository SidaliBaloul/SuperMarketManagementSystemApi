using SuperMarket.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMarket.Business.Interfaces
{
    public interface IUserService
    {
        Task<List<Userr>> GetUsersAsync();
        Task UpdateUserAsync(Userr user);
        Task<Userr> GetUserById(int id);
        Task<Userr> GetUserByUserName(string username);
    }
}
