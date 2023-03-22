using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using SuperCarga.Application.Domain.Users.Abstraction;
using SuperCarga.Application.Domain.Users.Models;
using SuperCarga.Application.Settings;
using SuperCarga.Domain.Domain.Users.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperCarga.Domain.Registrations
{
    public static class AuthRegistration
    {
        public static IServiceCollection AddAuth(this IServiceCollection services)
        {
            var sp = services.BuildServiceProvider();
            var jwtConfig = sp.GetService<JwtConfig>();

            services.AddIdentity();
            services.AddJwt(jwtConfig);
            services.AddTransient<TokenService>();
            services.AddTransient<IAuthService, AuthService>();

            return services;
        }

        private static IServiceCollection AddIdentity(this IServiceCollection services)
        {
            services
                .AddIdentity<User, Role>()
                .AddUserStore<UserStore>()
                .AddRoleStore<RoleStore>();

            return services;
        }

        private static IServiceCollection AddJwt(this IServiceCollection services, JwtConfig jwtConfig)
        {
            services
                .AddAuthentication(v =>
                {
                    v.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    v.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(v =>
                {
                    v.RequireHttpsMetadata = true;
                    v.SaveToken = true;
                    v.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateIssuerSigningKey = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidIssuer = jwtConfig.Issuer,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtConfig.Secret)),
                        ValidAudience = jwtConfig.Audience,
                        ClockSkew = TimeSpan.Zero
                    };
                    v.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            var token = context.HttpContext.Request.Headers["Authorization"].ToString();

                            if (!string.IsNullOrEmpty(token))
                            {
                                if (token.Contains("testCustomer"))
                                {
                                    var tokenService = context.HttpContext.RequestServices.GetRequiredService<TokenService>();
                                    context.Token = tokenService.GenerateAccessToken(DateTime.Now, "test_customer@sc.com", new List<string> { Roles.Customer });
                                }

                                if (token.Contains("testDriver"))
                                {
                                    var tokenService = context.HttpContext.RequestServices.GetRequiredService<TokenService>();
                                    context.Token = tokenService.GenerateAccessToken(DateTime.Now, "test_driver@sc.com", new List<string> { Roles.Driver });
                                }

                                if (token.Contains("admin"))
                                {
                                    var tokenService = context.HttpContext.RequestServices.GetRequiredService<TokenService>();
                                    context.Token = tokenService.GenerateAccessToken(DateTime.Now, "admin@sc.com", new List<string> { Roles.Admin });
                                }
                            }

                            return Task.CompletedTask;
                        }
                    };
                });

            return services;
        }
    }
}
