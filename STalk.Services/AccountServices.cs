using Application.Enums;
using Application.Responses;
using Application.ViewModels;
using Domain.Models;
using EntityFramework.DbContexts;
using IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.IO;
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
        public async Task<AccountResponse> ChangeEmailAsync(User user,EmailChangeViewModel emailChangeViewModel)
        {
            if(user != null)
            {
                if(emailChangeViewModel.Email != null && emailChangeViewModel.ConfirmEmail != null)
                {
                    if (emailChangeViewModel.Email.Equals(emailChangeViewModel.ConfirmEmail))
                    {
                        var emailChangeToken = await userManager.GenerateChangeEmailTokenAsync(user, emailChangeViewModel.Email);
                        var baseUrl = "http://localhost:44338/api/user/emailChangeConfirm";                        
                        string emailCallbackUrl = baseUrl + String.Format("/?userId={0}&email={1}&token={2}", user.Id,emailChangeViewModel.Email, emailChangeToken);
                        string message = "Click on the link to confirm email change: " + emailCallbackUrl; 
                        await emailSender.SendEmailAsync(user.Email,"Email change link",message);
                        return new AccountResponse { ResponseStatus = Status.Success, Message = "The email change link was sent to your email" };
                    }
                    return new AccountResponse { ResponseStatus = Status.Error, Message = "Emails do not match" };
                }
                return new AccountResponse { ResponseStatus = Status.Error, Message = "Request is missing some data" };
            }
            return new AccountResponse { ResponseStatus = Status.Error, Message = "User not found" };
        }

        public async Task<AccountResponse> ChangePasswordAsync(User user,PasswordChangeViewModel passwordChangeViewModel)
        {
            if(user != null)
            {
                if(passwordChangeViewModel.Password != null && passwordChangeViewModel.ConfirmPassword != null)
                {
                    if (passwordChangeViewModel.Password.Equals(passwordChangeViewModel.ConfirmPassword))
                    {
                        var passwordChangeToken = await userManager.GeneratePasswordResetTokenAsync(user);

                    }
                }
            }
            return null;
        }

        public async Task<AccountResponse> ChangeProfileImageAsync(User user,ProfileImageChangeViewModel profileImageChangeViewModel)
        {
           if(user != null)
           {
                if(profileImageChangeViewModel.profileImage != null)
                {
                    var fileExtension = Path.GetExtension(profileImageChangeViewModel.profileImage.FileName);
                    var fileContent = convertFormFileToByteArray(profileImageChangeViewModel.profileImage);
                    Domain.Models.File profileImage = new Domain.Models.File
                    {
                        FileExtension = fileExtension,
                        FileContent = fileContent,
                        User = user,
                        UserId = user.Id
                    };

                    await appDb.Files.AddAsync(profileImage);                   
                    await appDb.SaveChangesAsync();
                    return new AccountResponse { ResponseStatus = Status.Success, Message = "Succesfully changed profile image" };
                }
           }
           return new AccountResponse { ResponseStatus = Status.Error, Message = "User not found" };
        }

        public async Task<AccountResponse> ChangeUsernameAsync(User user,UsernameChangeViewModel usernameChangeViewModel)
        {
            if(user != null)
            {
                if(usernameChangeViewModel.Username != null && usernameChangeViewModel.ConfirmUsername != null)
                {
                    if (usernameChangeViewModel.Username.Equals(usernameChangeViewModel.ConfirmUsername))
                    {
                        IdentityResult result = await userManager.SetUserNameAsync(user, usernameChangeViewModel.Username);
                        if (result.Succeeded)
                        {
                            return new AccountResponse { ResponseStatus = Status.Success, Message = "The username was changed" };
                        }
                        else
                        {
                            return new AccountResponse { ResponseStatus = Status.Error, Message = "Username could not be changed" };
                        }
                    }
                    return new AccountResponse { ResponseStatus = Status.Error, Message = "Usernames do not match" };
                }
                return new AccountResponse { ResponseStatus = Status.Error, Message = "Request is missing some data" };
            }
            return new AccountResponse { ResponseStatus = Status.Error, Message = "User not found" };
        }

        public async Task<AccountResponse> RetrievePasswordAsync(string email)
        {
            var user = await userManager.FindByEmailAsync(email);
            if(user != null)
            {
                string passwordResetToken = await userManager.GeneratePasswordResetTokenAsync(user);
                string callBackUrl = String.Format("localhost:3000/reset?id={0}&token={1}",user.Id,passwordResetToken);

                string message = "If you want to reset your password click on the link" + callBackUrl;

                await emailSender.SendEmailAsync(user.Email,"STalk password reset",message);
                return new AccountResponse { ResponseStatus = Status.Success,Message="The reset link was sent to your email"};
            }
            return new AccountResponse { ResponseStatus = Status.Error, Message = "No user with that email was found" };
        }

        public async Task<AccountResponse> ResetPasswordAsync(ResetPasswordViewModel resetPasswordViewModel)
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

        private byte[] convertFormFileToByteArray(IFormFile file)
        {            
            if (file.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    file.CopyTo(ms);
                    var fileBytes = ms.ToArray();
                    string s = Convert.ToBase64String(fileBytes);
                    // act on the Base64 data
                    return fileBytes;
                }
            }
            return null;
        }


    }
}
