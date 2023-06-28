using FCCBackendTest.AuthAPI.Configuracao;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace FCCBackendTest.AuthAPI.Services
{
    public class AutenticacaoUsuarioService
    {

        private readonly JwtAutenticacaoConfiguracao _jwtAuthenticationSettings;

        public AutenticacaoUsuarioService(IOptions<JwtAutenticacaoConfiguracao> jwtAuthenticationSettings)
        {
            _jwtAuthenticationSettings = jwtAuthenticationSettings.Value;
        }

        public async Task<string> Login(string CPF)
        {
            string teste = await CreateToken(CPF);

            return teste;
        }

        private async Task<string> CreateToken(string CPF)
        {
            ClaimsIdentity claimsIdentity = await GetClaims(CPF);

            JwtSecurityTokenHandler jwtSecurityTokenHandler = new();

            SigningCredentials signingCredentials = GetSigningCredentials();

            SecurityTokenDescriptor securityTokenDescriptor = GenerateTokenOptions(signingCredentials, claimsIdentity);

            SecurityToken securityToken = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);

            return jwtSecurityTokenHandler.WriteToken(securityToken);
        }

        private string GenerateRefreshToken()
        {
            byte[] refreshToken = new byte[32];
            using RandomNumberGenerator randomNumberGenerator = RandomNumberGenerator.Create();

            randomNumberGenerator.GetBytes(refreshToken);

            return Convert.ToBase64String(refreshToken);
        }

        private SigningCredentials GetSigningCredentials()
        {
            byte[] jwtSecretKey = Encoding.UTF8.GetBytes(_jwtAuthenticationSettings.SecretKey);

            SymmetricSecurityKey secret = new(jwtSecretKey);

            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256Signature);
        }

        private async Task<ClaimsIdentity> GetClaims(string CPF)
        {
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim("CPF", CPF));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, ToUnixEpochDate(DateTime.UtcNow).ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(DateTime.UtcNow).ToString(), ClaimValueTypes.Integer64));

            ClaimsIdentity claimsIdentity = new();
            claimsIdentity.AddClaims(claims);

            return claimsIdentity;
        }

        private SecurityTokenDescriptor GenerateTokenOptions(SigningCredentials signingCredentials, ClaimsIdentity claimsIdentity)
        {
            SecurityTokenDescriptor tokenOptions = new()
            {
                Issuer = _jwtAuthenticationSettings.Issuer,
                Subject = claimsIdentity,
                Expires = DateTime.UtcNow.AddHours(_jwtAuthenticationSettings.ExpirationTimeInHours),
                SigningCredentials = signingCredentials,
                Audience = _jwtAuthenticationSettings.Audience,
            };

            return tokenOptions;
        }

        private static long ToUnixEpochDate(DateTime date)
        {
            return (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);
        }

    }
}
