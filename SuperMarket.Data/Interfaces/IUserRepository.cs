using SuperMarket.Domain.Entities;

namespace SuperMarket.Data.Interfaces
{
    public interface IUserRepository
    {
        Task<List<Userr>> GetUsersAsync();
        Task UpdateUserAsync(Userr user);
        Task<Userr> GetUserById(int id);
        Task<Userr> GetUserByUserName(string username);
    }
}
