using Application.Interfaces;
using Application.Models.Request;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class AuthenticacionService : IAuthenticacionService
    {
        private readonly IAdminRepository _adminRepository;
        private readonly IConfiguration _config;

        public AuthenticacionService(IAdminRepository adminRepository, IConfiguration config)
        {
            _adminRepository = adminRepository;
            _config = config;
        }

        private Admin? validateAdmin(string email, string password) 
        { 
            Admin? admin = _adminRepository.GetByEmail(email);

            if (admin is null) return null;
            if (admin.Password != password) return null;
            return admin;
        }

        public string Authenticate(LoginDto logindto)
        {
            // validar que el admin exista
            var validatedAdmin = validateAdmin(logindto.Email, logindto.Password);

            if (validatedAdmin is null) throw new Exception("credenciales invalidas");

            // generar el token
            var securityPassword = new SymmetricSecurityKey(
                Encoding.ASCII.GetBytes(_config["Authentication:SecretForKey"]!));

            var signature = new SigningCredentials(
                securityPassword, SecurityAlgorithms.HmacSha256);

            var claimsForToken = new List<Claim>();
            claimsForToken.Add(new Claim("sub", validatedAdmin.Id.ToString()));

            var jwtSecurityToken = new JwtSecurityToken(
                _config["Authentication:Issuer"],
                _config["Authentication:Audience"],
                claimsForToken,
                DateTime.UtcNow,
                DateTime.UtcNow.AddHours(1),
                signature
                );

            var tokenToReturn = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

            return tokenToReturn.ToString();
        }
    }
}
