using Azure.Core;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.IdentityModel.Tokens;
using SuperMarket.Business.Interfaces;
using SuperMarket.Domain.Entities;
using SuperMarketManagementSystemApi.DTOs;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace SuperMarketManagementSystemApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _User;
        private readonly IRefreshTokenService _RefreshToken;
        private readonly ILogger<AuthController> _logger;


        public AuthController(IUserService user, IRefreshTokenService tokenservice, ILogger<AuthController> looger)
        {
            _User = user;
            _RefreshToken = tokenservice;
            _logger = looger;
        }

        private static string GenerateRefreshToken()
        {
            var bytes = new byte[64];

            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(bytes);

            return Convert.ToBase64String(bytes);
        }

        [HttpPost("Login")]
        [EnableRateLimiting("AuthLimiter")]
        public async Task<ActionResult> Login(LoginDto login)
        {
            Userr user = await _User.GetUserByUserName(login.UserName);

            if (user == null)
            {
                _logger.LogWarning("Failed Login Attempt (UserName Not Found). UserName={UserName}, IP:11111", login.UserName);
                return Unauthorized("Invalid Credantials");
            }

            bool IsPasswordValid = BCrypt.Net.BCrypt.Verify(login.Password, user.Password);

            if (!IsPasswordValid)
            {
                _logger.LogWarning("Failed Login Attempt (Password Incorrect). Password={Password}, IP:11111", login.UserName);
                return Unauthorized("Invalid Credentials. ");
            }

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Role, user.UserName)
            };

            var secretkey = Environment.GetEnvironmentVariable("JWT_SECRET_KEY");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretkey));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "SuperMarketManagementSystemApi",
                audience: "SuperMarketApiUsers",
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds
                );


            var AccessToken = new JwtSecurityTokenHandler().WriteToken(token);

            var GeneratedRefreshToken = GenerateRefreshToken();

            RefreshToken refreshToken = new RefreshToken
            {
                Token = BCrypt.Net.BCrypt.HashPassword(GeneratedRefreshToken),
                UserID = user.UserId,
                CreatedAt = DateTime.Now,
                ExpiresAt = DateTime.Now.AddDays(7),
                Revoked = false,
                Used = false
            };

            await _RefreshToken.AddNewRefreshToken(refreshToken);

            return Ok(new TokenResponse
            {
                AccessToken = AccessToken,
                RefreshToken = GeneratedRefreshToken
            });
        }

        [HttpPost("refresh")]
        [EnableRateLimiting("AuthLimiter")]
        public async Task<IActionResult> Refresh(RefreshDto request)
        {
            Userr user = await _User.GetUserByUserName(request.UserName);

            if (user == null)
            {
                _logger.LogWarning("Failed Login Attempt (UserName Not Found). UserName={UserName}, IP:11111", request.UserName);
                return Unauthorized("Invalid Credantials");
            }

            RefreshToken refreshToken = await _RefreshToken.GetRefreshTokenAsync(user.UserId);

            if (refreshToken == null)
                return Unauthorized("Invalid refresh request");

            if (refreshToken.ExpiresAt <= DateTime.UtcNow)
            {
                return Unauthorized("Refresh token expired");
            }

            bool refreshValid = BCrypt.Net.BCrypt.Verify(request.RefreshToken, refreshToken.Token);
            if (!refreshValid)
            {
                _logger.LogWarning("Failed Login Attempt (Password Incorrect). Password={Password}, IP:11111", request.UserName);
                return Unauthorized("Invalid refresh token");
            }

            var claims = new[]
            {
                     new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                     new Claim(ClaimTypes.Role, user.UserName)
            };


            var secretkey = Environment.GetEnvironmentVariable("JWT_SECRET_KEY");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretkey));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var jwt = new JwtSecurityToken(
                issuer: "SuperMarketManagementSystemApi",
                audience: "SuperMarketApiUsers",
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(30),
                signingCredentials: creds
            );

            var newAccessToken = new JwtSecurityTokenHandler().WriteToken(jwt);


            var GeneratedRefreshToken = GenerateRefreshToken();
            var NewRefreshToken = new RefreshToken
            {
                Token = BCrypt.Net.BCrypt.HashPassword(GeneratedRefreshToken),
                UserID = refreshToken.UserID,
                CreatedAt = DateTime.Now,
                ExpiresAt = DateTime.Now.AddDays(7),
                Revoked = false,
                Used = false
            };

            await _RefreshToken.AddNewRefreshToken(NewRefreshToken);

            refreshToken.Revoked = true;
            refreshToken.Used = true;

            await _RefreshToken.UpdateRefreshToken(refreshToken);

            return Ok(new TokenResponse
            {
                AccessToken = newAccessToken,
                RefreshToken = GeneratedRefreshToken
            });
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout([FromBody] LogoutDto request)
        {
            Userr user = await _User.GetUserByUserName(request.UserName);

            if (user == null)
                return Ok();

            RefreshToken refreshToken = await _RefreshToken.GetRefreshTokenAsync(user.UserId);

            if (refreshToken == null)
                return Unauthorized("Invalid refresh request");

            bool refreshValid = BCrypt.Net.BCrypt.Verify(request.RefreshToken, refreshToken.Token);
            if (!refreshValid)
                return Ok();

            refreshToken.Revoked = true;
            await _RefreshToken.UpdateRefreshToken(refreshToken);

            return Ok("Logged out successfully");
        }
    }
}
