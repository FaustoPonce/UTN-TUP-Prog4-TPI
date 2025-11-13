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
        private readonly IEmployeeRepository _recepcionistaRepository;

        public AuthenticacionService(IAdminRepository adminRepository, IConfiguration config, IEmployeeRepository recepcionistaRepository)
        {
            _adminRepository = adminRepository;
            _recepcionistaRepository = recepcionistaRepository;
            _config = config;
        }

        private Admin? validateAdmin(string email, string password)
        {
            Admin? admin = _adminRepository.GetByEmail(email);
        
           
            if (admin is null) return null;
            if (admin.Password != password) return null;
            return admin;
        }
        private Employee? validateRecepcionista(string email, string password)
        {
            Employee? recepcionista = _recepcionistaRepository.GetByEmail(email);
        
           
            if (recepcionista is null) return null;
            if (recepcionista.Password != password) return null;
            return recepcionista;
        }

        public string Authenticate(LoginDto logindto)
        {
            // validar que el admin exista
            var validatedAdmin = validateAdmin(logindto.Email, logindto.Password);

            if (validatedAdmin != null)
            {
                return Generatetoken(validatedAdmin.Id.ToString(), "Admin");
            }

            // validar que el recepcionista exista
            var validatedRecepcionista = validateRecepcionista(logindto.Email, logindto.Password);
            if (validatedRecepcionista != null)
            {
                return Generatetoken(validatedRecepcionista.Id.ToString(), "Recepcionista");
            }

            throw new UnauthorizedAccessException("Credenciales invalidas");
        }


        public string Generatetoken(string userId, string role) 
        {
            // generar el token
            var securityPassword = new SymmetricSecurityKey(
                Encoding.ASCII.GetBytes(_config["Authentication:SecretForKey"]!));

            var signature = new SigningCredentials(
                securityPassword, SecurityAlgorithms.HmacSha256);

            var claimsForToken = new List<Claim>();
            claimsForToken.Add(new Claim("sub", userId));
            claimsForToken.Add(new Claim("role", role));

            //claimsForToken.Add(new Claim("role", validatedUser.Role.ToString()))



            var jwtSecurityToken = new JwtSecurityToken(
                _config["Authentication:Issuer"],
                _config["Authentication:Audience"],
                claimsForToken,
                DateTime.UtcNow,
                DateTime.UtcNow.AddHours(1),
                signature
                );

            var tokenToReturn = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

            //var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier || c.Type == "sub")?.Value;
            // esta linea chequearia el ID si quiesiera traermelo en un login por ejemplo 

            return tokenToReturn.ToString();
        }
    }
}
