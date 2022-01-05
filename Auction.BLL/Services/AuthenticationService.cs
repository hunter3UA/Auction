using Auction.BLL.LoginModels;
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
    public class AuthenticationService:IAuthenticationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordService _passwordService;
        private readonly IMapper _mapper;
        public AuthenticationService(IUnitOfWork unitOfWork, IPasswordService passwordService)
        {
            _unitOfWork = unitOfWork;
            _passwordService = passwordService;
            _mapper = AutoMapperConfig.Configure().CreateMapper();
        }
        public async Task<User> CreateUserAsync(RegisterModel registerModel)
        {
            try
            {
                Login newLogin = _mapper.Map<Login>(registerModel);
                newLogin.AccountType = _unitOfWork.AccountTypeRepository.Get(a => a.AccountTypeName == "User");
                newLogin.IsEnabled = true;
                Salted_Hash salt = _passwordService.CreateSaltedHash(registerModel.Password, 64);
                newLogin.PasswordHash = salt.Hash;
                newLogin.PasswordSalt = salt.Salt;
                newLogin = _unitOfWork.LoginRepository.Add(newLogin);
                await _unitOfWork.SaveAsync();
                User newUser = _mapper.Map<User>(registerModel);
                newUser.Login = newLogin;
                newUser.RegistredAt = DateTime.Now;
                _unitOfWork.UserRepository.Add(newUser);
                await _unitOfWork.SaveAsync();  
                return newUser;
            }
            catch{ return new User();}

        }
        
        public Login Login(LoginModel loginModel)
        {
            try
            {
                Login login = _unitOfWork.LoginRepository.Get(l => l.Email == loginModel.Email);
                if (_passwordService.CheckPassword(login, loginModel.Password))
                {
                    return login;
                }
            } catch { return new Login(); }
      
            return new Login();
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
        public static void ExtendCookieLife(FormsAuthenticationTicket ticket)
        {
            try
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
            }catch { }
        }

        public static void WriteTicketToResponse(DateTime cookieDeathTime, FormsAuthenticationTicket authenticationTicket)
        {

            string encryptedTicket = FormsAuthentication.Encrypt(authenticationTicket);
            HttpCookie loginCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
            loginCookie.Expires = cookieDeathTime;
            HttpContext.Current.Response.Cookies.Add(loginCookie);
        }

        public Login GetLogin(Func<Login,bool> predicate)
        {
            try
            {
                Login login = _unitOfWork.LoginRepository.Get(predicate);
                if (login != null)
                    return login;
                else
                    return new Login();
            }
            catch { return new Login(); }
       
        }

        public async Task<bool> ConfirmAccount(string Email,string Token)
        {
            try
            {
                Login loginToConfirm = _unitOfWork.LoginRepository.Get(l => l.Email == Email);
                if (loginToConfirm != null)
                {
                    byte[] PasswordSalt = loginToConfirm.PasswordSalt;
                    string CheckToken = BitConverter.ToString(PasswordSalt);
                    PasswordSalt = Encoding.UTF8.GetBytes(CheckToken);
                    CheckToken = Encoding.UTF8.GetString(PasswordSalt);
                    if (Token == CheckToken)
                    {
                        loginToConfirm.IsConfirmed = true;
                        await _unitOfWork.SaveAsync();
                        return true;
                    }
                }
            }catch { return false; }
            return false;
        }
    }
}
