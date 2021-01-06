using Application.Enums;
using Application.Responses;
using Application.ViewModels;
using Domain.Models;
using EntityFramework.DbContexts;
using IServices;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class AccountServices : IAccountServices
    {
        private readonly IEmailSender emailSender;
        private readonly MainDbContext appDb;
        private readonly UserManager<User> userManager;
        public AccountServices(MainDbContext appDb, UserManager<User> userManager,IEmailSender emailSender)
        {
            this.appDb = appDb;
            this.userManager = userManager;
            this.emailSender = emailSender;
    }
        public Task<AccountResponse> ChangeEmail()
        {
            throw new NotImplementedException();
        }

        public Task<AccountResponse> ChangePassword()
        {
            throw new NotImplementedException();
        }

        public Task<AccountResponse> ChangeProfileImage()
        {
            throw new NotImplementedException();
        }

        public Task<AccountResponse> ChangeUsername()
        {
            throw new NotImplementedException();
        }

        public async Task<AccountResponse> RetrievePassword(string email)
        {
            var user = await userManager.FindByEmailAsync(email);
            if(user != null)
            {
                string passwordResetToken = await userManager.GeneratePasswordResetTokenAsync(user);
                string callBackUrl = String.Format("localhost:3000/reset/{0}/{1}",user.Id,passwordResetToken);

                string message = "If you want to reset your password click on the link" + callBackUrl;

                await emailSender.SendEmailAsync(user.Email,"STalk password reset",message);
                return new AccountResponse { ResponseStatus = Status.Success,Message="The reset link was sent to your email"};
            }
            return new AccountResponse { ResponseStatus = Status.Error, Message = "No user with that email was found" };
        }

        public async Task<AccountResponse> ResetPassword(ResetPasswordViewModel resetPasswordViewModel)
        {
            var user = await userManager.FindByIdAsync(resetPasswordViewModel.UserId);
            if(user != null)
            {
                IdentityResult result = await userManager.ResetPasswordAsync(user, resetPasswordViewModel.ResetToken, resetPasswordViewModel.Password);
                if (result.Succeeded)
                {
                    return new AccountResponse { ResponseStatus = Status.Success, Message = "Password was changed succesfully" };
                }
                return new AccountResponse { ResponseStatus = Status.Error, Message = "Password could not be changed" };
            }
            return new AccountResponse { ResponseStatus = Status.Error, Message = "User not found" };
        }

    }
}
