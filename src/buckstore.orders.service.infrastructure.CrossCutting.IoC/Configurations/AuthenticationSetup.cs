using System;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace buckstore.orders.service.infrastructure.CrossCutting.IoC.Configurations
{
    public static class AuthenticationSetup
    {
        public static void AddAuthenticationSetup(this IServiceCollection services)
        {
            var jwtSecret = Environment.GetEnvironmentVariable("JwtConfiguration__Secret");
            var jwtSettingsAudience = Environment.GetEnvironmentVariable("JwtConfiguration__Audience");
            var jwtSettingsIssuer = Environment.GetEnvironmentVariable("JwtConfiguration__TokenIssuer");

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    var key = Encoding.ASCII.GetBytes(jwtSecret);

                    var tokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateLifetime = true,
                        ValidateIssuer = true,
                        ValidateAudience = false,
                        ValidIssuer = jwtSettingsIssuer,
                        ValidAudience = jwtSettingsAudience,
                        IssuerSigningKey = new SymmetricSecurityKey(key)
                    };

                    options.TokenValidationParameters = tokenValidationParameters;
                });
        }
    }
}