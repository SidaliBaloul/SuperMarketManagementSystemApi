using Microsoft.VisualBasic;
using SuperMarket.Business.Interfaces;
using SuperMarket.Domain.Entities;
using SuperMarket.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMarket.Business.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _Repository;

        public UserService(IUserRepository repository)
        {
            _Repository = repository;
        }

        public async Task<List<Userr>> GetUsersAsync()
        {
            return await _Repository.GetUsersAsync();
        }

        public async Task UpdateUserAsync(Userr user)
        {
            await _Repository.UpdateUserAsync(user);
        }

        public async Task<Userr> GetUserById(int id)
        {
            Userr user = await _Repository.GetUserById(id);

            if(user == null)
                throw new KeyNotFoundException($"User With ID : {id} Not Found! ");

            return await _Repository.GetUserById(id);
        }

        public async Task<Userr> GetUserByUserName(string username)
        {
            return await _Repository.GetUserByUserName(username);
        }
    }
}
