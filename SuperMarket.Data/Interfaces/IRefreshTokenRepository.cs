using SuperMarket.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMarket.Data.Interfaces
{
    public interface IRefreshTokenRepository
    {
        Task<RefreshToken> GetRefreshTokenAsync(int userid);
        Task AddNewRefreshToken(RefreshToken token);
        Task UpdateRefreshToken(RefreshToken token);
    }
}
