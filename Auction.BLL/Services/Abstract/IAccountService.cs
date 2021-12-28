﻿using Auction.BLL.ViewModels;
using Auction.DAL.Models;
using System;
using System.Threading.Tasks;

namespace Auction.BLL.Services.Abstract
{
    public interface IAccountService
    {
        Task<User> CreateUserAsync(RegisterModel registerModel);

        User Login(LoginModel loginModel);
        UserModel GetUser(Func<User,bool> predicate);
        Task UpdateAsync(UserModel userModel, int loginId);
        Task<bool> DisableUserAsync(int loginId);
    }
}
