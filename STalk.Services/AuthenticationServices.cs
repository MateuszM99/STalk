using Application.Enums;
using Application.Responses;
using Application.ViewModels;
using Domain.Models;
using IServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class AuthenticationServices : IAuthenticationServices
    {
        private readonly IConfiguration _configuration;
        private readonly IEmailSender emailSender;
        public AuthenticationServices(IConfiguration configuration,IEmailSender emailSender)
        {
            _configuration = configuration;
            this.emailSender = emailSender;
        }
        public async Task<SignUpResponse> SignUp(SignUpModel model,UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            var userExists = await userManager.FindByNameAsync(model.Username);
            if (userExists != null)
            {
                return new SignUpResponse { ResponseStatus = Status.Error, Message = "User with that username already exists!" };
            }

            var userEmailExists = await userManager.FindByEmailAsync(model.Email);
            if (userEmailExists != null)
            {
                return new SignUpResponse { ResponseStatus = Status.Error, Message = "User with that email already exists!" };
            }

            User user = new User()
            {
                UserName = model.Username,
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),                               
            };

            var result = await userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return new SignUpResponse { ResponseStatus = Status.Error, Message = "User creation failed! Please check user details and try again." };

            var token = await userManager.GenerateEmailConfirmationTokenAsync(user);
            var baseUrl = "http://localhost:44338/api/authentication/confirmEmail";
            var confirmationLink = baseUrl + String.Format("/?userId={0}&token={1}", user.Id, token);
            string message = $"Click this link to confirm your account: {confirmationLink}";

            await emailSender.SendEmailAsync(model.Email, "Confirm your account", message);

            return new SignUpResponse { ResponseStatus = Status.Success, Message = "User created succesfully." };
        }
        public async Task<SignInResponse> SignIn(SignInModel model, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            var user = await userManager.FindByNameAsync(model.Username);
            if (user != null && await userManager.CheckPasswordAsync(user, model.Password))
            {
                var userRoles = await userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

                var token = new JwtSecurityToken(
                    issuer: _configuration["JWT:ValidIssuer"],
                    audience: _configuration["JWT:ValidAudience"],
                    expires: DateTime.Now.AddHours(24),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                    );

                var tokenString = new JwtSecurityTokenHandler().WriteToken(token); 

                return new SignInResponse { ResponseStatus = Status.Success, Message= "Signed in successfully", User = user, UserRoles = userRoles, Token = tokenString};
            }
            return new SignInResponse { ResponseStatus = Status.Error, Message = "Something went wrong, check your user details"};
        }
    }
}
