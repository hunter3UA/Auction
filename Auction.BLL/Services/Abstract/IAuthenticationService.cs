using Auction.BLL.ViewModels;
using Auction.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auction.BLL.Services.Abstract
{
    public interface IAuthenticationService
    {
        Task<User> CreateUserAsync(RegisterModel registerModel);
        Task<bool> UpdatePasswordAsync(string oldPassword, string newPassword, int loginId);
        Login Login(LoginModel loginModel);
        Task<bool> DisableUserAsync(int loginId);
        Task<bool> ConfirmAccount(string Email,string Token);
        Login GetLogin(Func<Login, bool> predicate);
        void SetLoginCookie(Login login);
    }
}
