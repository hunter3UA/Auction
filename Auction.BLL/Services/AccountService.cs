using Auction.BLL.LoginModels;
using Auction.BLL.Mapper;
using Auction.BLL.Services.Abstract;
using Auction.BLL.ViewModels;
using Auction.DAL.Models;
using Auction.DAL.UoW;
using AutoMapper;
using System;
using System.Configuration;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Security;
using static Auction.BLL.Services.PasswordService;


namespace Auction.BLL.Services
{
    public class AccountService:IAccountService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public AccountService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _mapper = AutoMapperConfig.Configure().CreateMapper();
        }

        public async Task<User> CreateUser(RegisterModel registerModel)
        {
            try
            {
                Login newLogin = _mapper.Map<Login>(registerModel);
                newLogin.AccountType = _unitOfWork.AccountTypeRepository.Get(a => a.AccountTypeName == "User");
                newLogin.IsEnabled = true;
                Salted_Hash salt = PasswordService.Security.HashHMACSHA1.CreateSaltedHash(registerModel.Password, 64);
                newLogin.PasswordHash = salt.Hash;
                newLogin.PasswordSalt = salt.Salt;
                newLogin = _unitOfWork.LoginRepository.Add(newLogin);
                await _unitOfWork.SaveAsync();
                User newUser = _mapper.Map<User>(registerModel);
                newUser.Balance = 0;
                newUser.Login = newLogin;
                newUser.RegistredAt = DateTime.Now;
                _unitOfWork.UserRepository.Add(newUser);
                await _unitOfWork.SaveAsync();
                SetLoginCookie(newLogin);
                return newUser;
            }catch (Exception)
            {
                return new User();
            }
            

        }

        public User Login(LoginModel loginModel)
        {
            try
            {
                User userToLogin = _unitOfWork.UserRepository.Get(u => u.Login.Email == loginModel.Email);
                if (userToLogin.Login != null)
                {
                    Salted_Hash saltedHash = new Salted_Hash();
                    saltedHash.Hash = userToLogin.Login.PasswordHash;
                    saltedHash.Salt = userToLogin.Login.PasswordSalt;
                    if (Security.HashHMACSHA1.CheckSaltedHash(loginModel.Password, saltedHash))
                    {
                        Login login = _unitOfWork.LoginRepository.Get(l => l.LoginId == userToLogin.LoginId);
                        if (login.IsEnabled)
                        {
                            SetLoginCookie(login);
                            return userToLogin;
                        }
                           
                    }
                }

            }catch (Exception)
            {
                 return new User();
            }
            return new User();
        }

        /// <summary>
        /// Метод встановлює Cookie при логіні та реєстрації
        /// </summary>
        /// <param name="login"></param>
        public void SetLoginCookie(Login login)
        {
            UserSerializationModel serializationModel = new UserSerializationModel();
            serializationModel.LoginId = login.LoginId;
            serializationModel.AccountType = login.AccountType;
            serializationModel.ClientIP = HttpContext.Current.Request.UserHostAddress;
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string userJSON = serializer.Serialize(serializationModel);
            int LoginCookieInterval = Convert.ToInt32(ConfigurationManager.AppSettings["LoginCookieInterval"]);
            DateTime cookieDeathTime = DateTime.Now.AddMinutes(LoginCookieInterval);
            FormsAuthenticationTicket authenticationTicket = new FormsAuthenticationTicket(
                1,
                login.Email,
                DateTime.Now,
                cookieDeathTime,
                true,
                userJSON

                );
            WriteTicketToResponse(cookieDeathTime, authenticationTicket);
        }

        /// <summary>
        /// Метод для продвження життя Cookie. При кожному оновленні сторінки оновлюється термін дії Cookie
        /// </summary>
        /// <param name="ticket"></param>
        public static void ExtendCookieLifer(FormsAuthenticationTicket ticket)
        {
            int LoginCookieInterval = Convert.ToInt32(ConfigurationManager.AppSettings["LoginCookieInterval"]);

            DateTime cookieDeathTime = DateTime.Now.AddMinutes(LoginCookieInterval);
            FormsAuthenticationTicket authenticationTicket = new FormsAuthenticationTicket(
               1,
               ticket.Name,
               DateTime.Now,
               DateTime.Now.AddDays(1),
               true,
               ticket.UserData
               );
            WriteTicketToResponse(cookieDeathTime, authenticationTicket);
        }

        public static void WriteTicketToResponse(DateTime cookieDeathTime, FormsAuthenticationTicket authenticationTicket)
        {
            
            string encryptedTicket = FormsAuthentication.Encrypt(authenticationTicket);          
            HttpCookie loginCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
            loginCookie.Expires = cookieDeathTime;
            HttpContext.Current.Response.Cookies.Add(loginCookie);
        }

        public UserModel GetUser(int loginId)
        {
            User userToSearch= _unitOfWork.UserRepository.Get(u=>u.LoginId== loginId);
            UserModel model= _mapper.Map<UserModel>(userToSearch);
            model.Email = userToSearch.Login.Email;
            return model;
        }

        public async Task Update(UserModel userModel,int loginId)
        {
            User userToSearch= _unitOfWork.UserRepository.Get(u=>u.LoginId == loginId);
            userToSearch.FirstName = userModel.FirstName;
            userToSearch.LastName = userModel.LastName;
            _unitOfWork.UserRepository.Update(userToSearch);
            await _unitOfWork.SaveAsync();
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
