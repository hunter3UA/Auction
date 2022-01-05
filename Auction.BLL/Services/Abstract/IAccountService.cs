using Auction.BLL.ViewModels;
using Auction.DAL.Models;
using System;
using System.Threading.Tasks;

namespace Auction.BLL.Services.Abstract
{
    public interface IAccountService
    {
       
        Task<bool> UpdatePasswordAsync(string oldPassword, string newPassword, int loginId);
     
        UserModel GetUser(Func<User,bool> predicate);
        Task<bool> UpdateUserAsync(UserModel userModel, int loginId);
        Task<bool> DisableUserAsync(int loginId);
        Task<bool> ResetPassword(string Email, string Token, string Password);
       
    }
}
