using System;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace ncorep.AuthDir;

public class JwtSettings
{
    private readonly IConfiguration _configuration;

    public JwtSettings(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public byte[] Secret => Encoding.UTF8.GetBytes(_configuration["JWT:Key"]);
    public TimeSpan RefreshTokenLifetime => TimeSpan.Parse(_configuration["JWT:RefreshTokenLifeTime"]);
    public TimeSpan TokenLifetime => TimeSpan.Parse(_configuration["JWT:AccessTokenLifetime"] );
}