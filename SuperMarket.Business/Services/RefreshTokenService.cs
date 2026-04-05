using SuperMarket.Business.Interfaces;
using SuperMarket.Domain.Entities;
using SuperMarket.Data.Interfaces;
using SuperMarket.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMarket.Business.Services
{
    public class RefreshTokenService : IRefreshTokenService
    {
        private readonly IRefreshTokenRepository _Repository;

        public RefreshTokenService(IRefreshTokenRepository repository)
        {
            _Repository = repository;
        }

        public async Task<RefreshToken> GetRefreshTokenAsync(int userid)
        {
            return await _Repository.GetRefreshTokenAsync(userid);
        }

        public async Task AddNewRefreshToken(RefreshToken refreshtoken)
        {
            await _Repository.AddNewRefreshToken(refreshtoken);
        }

        public async Task UpdateRefreshToken(RefreshToken refreshToken)
        {
            await _Repository.UpdateRefreshToken(refreshToken);
        }
    }
}
