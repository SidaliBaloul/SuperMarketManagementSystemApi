using SuperMarket.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMarket.Business.Interfaces
{
    public interface IRefreshTokenService
    {
        Task<RefreshToken> GetRefreshTokenAsync(int userid);
        Task AddNewRefreshToken(RefreshToken refreshToken);
        Task UpdateRefreshToken(RefreshToken refreshToken);
    }
}
