﻿using Auction.BLL.LoginModels;
using Auction.BLL.Mapper;
using Auction.BLL.Services.Abstract;
using Auction.BLL.ViewModels;
using Auction.DAL.Models;
using Auction.DAL.UoW;
using AutoMapper;
using System;
using System.Configuration;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Security;



namespace Auction.BLL.Services
{
    public class AccountService:IAccountService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordService _passwordService;
        private readonly IMapper _mapper;
        public AccountService(IUnitOfWork unitOfWork,IPasswordService passwordService)
        {
            _unitOfWork = unitOfWork;
            _passwordService = passwordService;
            _mapper = AutoMapperConfig.Configure().CreateMapper();
        }

       
        public async Task<bool> UpdatePasswordAsync(string oldPassword,string newPassword,int loginId)
        {     
            Login loginToUpdate=_unitOfWork.LoginRepository.Get(l=>l.LoginId== loginId);
            if (loginToUpdate != null && !string.IsNullOrEmpty(newPassword) && !string.IsNullOrEmpty(oldPassword))
            {
                if (_passwordService.CheckPassword(loginToUpdate, oldPassword))
                {
                    Salted_Hash salt = _passwordService.CreateSaltedHash(newPassword, 64);
                    loginToUpdate.PasswordHash = salt.Hash;
                    loginToUpdate.PasswordSalt = salt.Salt;
                    await _unitOfWork.SaveAsync();
                    return true;
                }
            }
            return false;
            

        }

        public async Task<bool> ResetPassword(string Email, string Token, string Password)
        {
            Login login = _unitOfWork.LoginRepository.Get(l=>l.Email==Email);
            byte[] PasswordSalt = login.PasswordSalt;
            string LoginToken = BitConverter.ToString(PasswordSalt);
            PasswordSalt = Encoding.UTF8.GetBytes(Token);
            LoginToken = Encoding.UTF8.GetString(PasswordSalt);
            if (Token == LoginToken)
            {
                Salted_Hash salt = _passwordService.CreateSaltedHash(Password, 64);
                login.PasswordHash = salt.Hash;
                login.PasswordSalt = salt.Salt;
                await _unitOfWork.SaveAsync();
                return true;
            }
            return false;

        }

        public UserModel GetUser(Func<User,bool> predicate)
        {
          
           User userToSearch = _unitOfWork.UserRepository.Get(predicate);
            if (userToSearch.Login == null)
                return new UserModel();
           UserModel model= _mapper.Map<UserModel>(userToSearch);
           model.Email = userToSearch.Login.Email;
           return model;
       
           
        }

        public async Task<bool> UpdateUserAsync(UserModel userModel,int loginId)
        {
            User userToSearch= _unitOfWork.UserRepository.Get(u=>u.LoginId == loginId);
            if (userToSearch != null)
            {
                userToSearch.FirstName = userModel.FirstName;
                userToSearch.LastName = userModel.LastName;         
               
                await _unitOfWork.SaveAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> DisableUserAsync(int loginId)
        {
            Login loginToDisable=_unitOfWork.LoginRepository.Get(l=>l.LoginId== loginId);
            if (loginToDisable.IsEnabled)
            {
                loginToDisable.IsEnabled = false;
                _unitOfWork.LoginRepository.Update(loginToDisable);
                await _unitOfWork.SaveAsync();
                return true;
            }
            return false;

        }
       
    }
}
