using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace FCCBackendTest.AuthAPI.Configuracao
{
    public static class ConfiguracaoAutenticacao
    {

        public static void AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            JwtAutenticacaoConfiguracao authSettings = configuration.GetSection("JwtAuthenticationSettings").Get<JwtAutenticacaoConfiguracao>();
            services.AddAuthentication(authOptions =>
            {
                authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(bearerOptions =>
            {
                bearerOptions.RequireHttpsMetadata = false;
                bearerOptions.SaveToken = true;
                bearerOptions.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(authSettings.SecretKey)),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = authSettings.Issuer,
                    ValidAudience = authSettings.Audience,
                    RequireExpirationTime = true
                };
            });

            services.Configure<JwtAutenticacaoConfiguracao>(opt => configuration.GetSection("JwtAuthenticationSettings").Bind(opt));
        }

    }
}
