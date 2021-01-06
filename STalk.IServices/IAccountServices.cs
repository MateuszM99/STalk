using Application.Responses;
using Application.ViewModels;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IServices
{
    public interface IAccountServices
    {
        Task<AccountResponse> RetrievePasswordAsync(string email);
        Task<AccountResponse> ResetPasswordAsync(ResetPasswordViewModel resetPasswordViewModel);
        Task<AccountResponse> ChangePasswordAsync(User user,PasswordChangeViewModel passwordChangeViewModel);
        Task<AccountResponse> ChangeUsernameAsync(User user,UsernameChangeViewModel usernameChangeViewModel);
        Task<AccountResponse> ChangeEmailAsync(User user,EmailChangeViewModel emailChangeViewModel);
        Task<AccountResponse> ChangeProfileImageAsync();        
    }
}
