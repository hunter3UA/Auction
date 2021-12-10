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
            Login newLogin = _mapper.Map<Login>(registerModel);
            newLogin.AccountType = _unitOfWork.AccountTypeRepository.Get(a => a.AccountTypeName == "User");
            newLogin.IsEnabled = true;
            Salted_Hash salt = PasswordService.Security.HashHMACSHA1.CreateSaltedHash(registerModel.Password, 64);
            newLogin.PasswordHash = salt.Hash;
            newLogin.PasswordSalt = salt.Salt;
            newLogin = _unitOfWork.LoginRepository.Add(newLogin);
            await _unitOfWork.SaveAsync();
            User newUser= _mapper.Map<User>(registerModel);
            newUser.Balance = 0;
            newUser.Login = newLogin;
            newUser.RegistredAt = DateTime.Now;
            _unitOfWork.UserRepository.Add(newUser);
            await _unitOfWork.SaveAsync();
            SetLoginCookie(newLogin);
            return newUser;

        }
        public User Login(LoginModel loginModel)
        {
             User userToLogin = _unitOfWork.UserRepository.Get(u=>u.Login.Email== loginModel.Email);
             if(userToLogin.Login != null)
             {
                Salted_Hash saltedHash=new Salted_Hash();
                saltedHash.Hash = userToLogin.Login.PasswordHash;
                saltedHash.Salt = userToLogin.Login.PasswordSalt;
                if (Security.HashHMACSHA1.CheckSaltedHash(loginModel.Password, saltedHash))
                {
                    Login login = _unitOfWork.LoginRepository.Get(l => l.LoginId == userToLogin.LoginId);
                    SetLoginCookie(login);
                    return userToLogin;
                }
             }
            return null;
        }
        public void SetLoginCookie(Login login)
        {
            UserSerializationModel serializationModel = new UserSerializationModel();
            serializationModel.LoginId = login.LoginId;
            serializationModel.AccountType = login.AccountType;
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

      
    }
}
