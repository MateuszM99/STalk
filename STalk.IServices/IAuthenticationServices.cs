using Application.Responses;
using Application.ViewModels;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IServices
{
    public interface IAuthenticationServices
    {
        Task<SignUpResponse> SignUp(SignUpModel model,UserManager<User> userManager, RoleManager<IdentityRole> roleManager);
        Task<SignInResponse> SignIn(SignInModel model, UserManager<User> userManager, RoleManager<IdentityRole> roleManager);
    }
}
