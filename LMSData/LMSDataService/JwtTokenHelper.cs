using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Web;
using Antlr.Runtime;
using LIMSData;
using Microsoft.IdentityModel.Tokens;

namespace LMSDataService
{
    public class JwtTokenHelper
    {
        private const string _sec = "803b09eab3c013d4ca54922bb802bec89d5318192b0a75f201d8b3727429090bb337591abd3e44253b9a4555b7a0812e1081c39b840293f765ead731e5a05ed1";
        public static string GenerateJwtToken(string userName, string role, JwtPayload payload)
        {
            DateTime issuedAt = DateTime.UtcNow;
            DateTime expires = DateTime.UtcNow.AddMinutes(30);

            var tokenHandler = new JwtSecurityTokenHandler();

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(new []
            {
                new Claim(ClaimTypes.Name, userName), 
                new Claim(ClaimTypes.Role, role)
            }, "Custom");

            var securityKey = CreateSecurityKey();
            var signingCredentials = new SigningCredentials(securityKey,
                SecurityAlgorithms.HmacSha256Signature);
            // create JWT
            var token = (JwtSecurityToken) tokenHandler.CreateJwtSecurityToken("http://localhost:51060/",
                "http://localhost:51060/",
                claimsIdentity, issuedAt, expires, signingCredentials: signingCredentials);
            foreach (var item in payload)
            {
                token.Payload.Add(item.Key, item.Value);
            }
            var tokenString = tokenHandler.WriteToken(token);

            return tokenString;
        }

        public static Tuple<bool, string> ValidateToken(string token)
        {
            var validToken = false;
            var tokentHandler = new JwtSecurityTokenHandler();
            var securityKey = CreateSecurityKey();
            
            var tokenValidationParameters = new TokenValidationParameters()
            {
                ValidAudiences = new string[]
                {
                    "http://localhost:51060/"
                },
                ValidIssuers = new string[]
                {
                    "http://localhost:51060/"
                },
                IssuerSigningKey = securityKey

            };

            var errorMsg = string.Empty;

            try
            {
                SecurityToken validatedToken;
                tokentHandler.ValidateToken(token, tokenValidationParameters, out validatedToken);

                validToken = true;
            }
            catch (Exception e)
            {
                errorMsg = e.Message;
                validToken = false;
            }
            

            return new Tuple<bool, string>(validToken, errorMsg);
        }

        public static string GetTokenPayloadValue(string token, string key)
        {
            var jwtHandler = new JwtSecurityTokenHandler();
            var readable = jwtHandler.CanReadToken(token);

            if (readable)
            {
                var tokenVal = jwtHandler.ReadJwtToken(token);

                try
                {
                    var payloadValue = tokenVal.Payload[key].ToString();
                    return payloadValue;
                }
                catch (KeyNotFoundException e)
                {
                    throw e;
                }
                
            }

            throw new Exception("Token not readable");
        }

        private static SymmetricSecurityKey CreateSecurityKey()
        {
            var securityKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_sec));
            
            return securityKey;
        }
    }
}