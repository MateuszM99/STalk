using Application.Responses;
using Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IServices
{
    public interface IAccountServices
    {
        Task<AccountResponse> RetrievePassword(string email);
        Task<AccountResponse> ResetPassword(ResetPasswordViewModel resetPasswordViewModel);
        Task<AccountResponse> ChangePassword();
        Task<AccountResponse> ChangeUsername();
        Task<AccountResponse> ChangeEmail();
        Task<AccountResponse> ChangeProfileImage();        
    }
}
