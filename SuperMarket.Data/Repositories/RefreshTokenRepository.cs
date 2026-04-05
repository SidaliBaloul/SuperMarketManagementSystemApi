using SuperMarket.Data.DBContexte;
using SuperMarket.Domain.Entities;
using SuperMarket.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMarket.Data.Repositories
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly SuperMarketDbContext _Context;

        public RefreshTokenRepository(SuperMarketDbContext context)
        {
            _Context = context;
        }

        public async Task<RefreshToken> GetRefreshTokenAsync(int userid)
        {
            return _Context.RefreshTokens.Where(s => s.Used == false && s.Revoked == false).FirstOrDefault(x => x.UserID == userid);
        }

        public async Task AddNewRefreshToken(RefreshToken token)
        {
            _Context.RefreshTokens.Add(token);
            await _Context.SaveChangesAsync();
        }

        public async Task UpdateRefreshToken(RefreshToken token)
        {
            _Context.RefreshTokens.Update(token);
            await _Context.SaveChangesAsync();
        }
    }
}
