using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ncorep.Dtos;
using ncorep.Extensions;
using ncorep.Interfaces.Business;
using ncorep.Interfaces.Data;
using ncorep.Models;

namespace ncorep.Services;

public class JwtService : IJwtService
{
    private readonly IConfiguration _configuration;
    private readonly IUnitOfWork _unitOfWork;

    public JwtService(IConfiguration configuration, IUnitOfWork unitOfWork)
    {
        _configuration = configuration;
        _unitOfWork = unitOfWork;
    }

    public async Task<ServiceResult> CreateToken(AppUser user)
    {
        var secret = Encoding.UTF8.GetBytes(_configuration["JWT:Key"]);
        var accessTokenLifeTime = TimeSpan.Parse(_configuration["JWT:AccessTokenLifeTime"]);
        var refreshTokenLifetime = TimeSpan.Parse(_configuration["JWT:RefreshTokenLifeTime"]);

        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, user.Email),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("id", user.Id)
            }),

            Expires = DateTime.UtcNow.Add(accessTokenLifeTime),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secret),
                SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        var accessToken = tokenHandler.WriteToken(token);

        var refreshToken = new RefreshToken
        {
            JwtId = token.Id,
            UserId = user.Id,
            CreatedAt = DateTime.UtcNow,
            ExpriryDate = DateTime.UtcNow.Add(refreshTokenLifetime)
        };

        await _unitOfWork.RefreshTokens.InsertAsync(refreshToken);

        await _unitOfWork.RefreshTokens.SaveChanges();

        var userBaseResponse = new AuthenticationResultDto
            {AccessToken = accessToken, RefreshToken = refreshToken.Token};
        return new ServiceResult {Data = userBaseResponse, StatusCode = 200};
    }

    public async Task<ServiceResult> RefreshToken(RefreshRequest refreshRequest)
    {
        var accessToken = refreshRequest.AccessToken;
        var refreshToken = refreshRequest.RefreshToken;

        var validatedToken = GetPrincipalFromToken(accessToken);

        if (validatedToken == null) return new ServiceResult {ErrorMessage = "invalid token", StatusCode = 401};

        var expiryDateTimeUnix = long.Parse(validatedToken.Claims
            .Single(x => x.Type == JwtRegisteredClaimNames.Exp).Value);

        var expiryDateTimeUtc = DateTimeOffset.FromUnixTimeSeconds(expiryDateTimeUnix);

        if (expiryDateTimeUtc < DateTime.UtcNow) return new ServiceResult {ErrorMessage = "token expired"};

        var jti = validatedToken.Claims.Single(x => x.Type == JwtRegisteredClaimNames.Jti).Value;

        var storedRefreshToken = await _unitOfWork.RefreshTokens.GetOneByQueryAsync(x => x.Token == refreshToken);

        if (storedRefreshToken == null) return new ServiceResult {ErrorMessage = "this refresh token does not exist"};

        if (DateTime.UtcNow > storedRefreshToken.ExpriryDate)
            return new ServiceResult {ErrorMessage = "refresh token has been expired"};

        if (storedRefreshToken.Invalidated)
            return new ServiceResult {ErrorMessage = "this refresh token has been invalidated"};

        if (storedRefreshToken.Used)
            return new ServiceResult {ErrorMessage = "this refresh token has been used"};

        if (storedRefreshToken.JwtId != jti)
            return new ServiceResult {ErrorMessage = "this refresh token does not match the jwt"};

        storedRefreshToken.Used = true;
        _unitOfWork.RefreshTokens.Update(storedRefreshToken);
        await _unitOfWork.RefreshTokens.SaveChanges();

        var userId = validatedToken.Claims.Single(z => z.Type.ToString() == "id").Value.ToString();
        var user = await _unitOfWork.Users.GetOneByQueryAsync(x => userId.Equals(x.Id));
        return await CreateToken(user);
    }

    private ClaimsPrincipal GetPrincipalFromToken(string token)
    {
        var expiredTokenValidationParameters = ServiceExtensions.ValidationParameters(_configuration, true);
        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, expiredTokenValidationParameters, out var validatedToken);

            if (!IsJwtWithValidSecurityAlgorithm(validatedToken)) return null;

            return principal;
        }
        catch (Exception)
        {
            return null;
        }
    }

    private static bool IsJwtWithValidSecurityAlgorithm(SecurityToken validatedToken)
    {
        return validatedToken is JwtSecurityToken jwtSecurityToken &&
               jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                   StringComparison.InvariantCultureIgnoreCase);
    }
}