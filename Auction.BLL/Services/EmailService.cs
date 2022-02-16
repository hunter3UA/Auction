using Auction.BLL.Services.Abstract;
using Auction.DAL.Models;
using Auction.DAL.UoW;
using System;
using System.Configuration;
using System.Net.Mail;
using System.Text;



namespace Auction.BLL.Services
{
    public class EmailService:IEmailService
    {
        private readonly IUnitOfWork _unitOfWork;
        public EmailService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        private MailMessage SendMessage(string email,string subject,string body)
        {
            try
            {
                MailAddress from = new MailAddress(
                    ConfigurationManager.AppSettings["CompanyEmail"], 
                    ConfigurationManager.AppSettings["CompanyName"]);
                MailAddress to = new MailAddress(email);
                MailMessage message = new MailMessage(from, to);
                message.Subject = subject;
                message.Body = body;
                message.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient(
                    ConfigurationManager.AppSettings["SmptGmailServer"], 
                    Convert.ToInt32(ConfigurationManager.AppSettings["SmtpPort"]));
                smtp.EnableSsl = true;
                smtp.Credentials = new System.Net.NetworkCredential(
                    ConfigurationManager.AppSettings["CompanyEmail"], 
                    ConfigurationManager.AppSettings["EmailPassword"]);
                smtp.Send(message);
                return message;
            }
            catch { return null; }
        }

        public bool SendPasswordConfirmed(string email,string subject,string action)
        {
            try
            {
                Login login = _unitOfWork.LoginRepository.Get(l => l.Email == email);
                if (login != null && login.LoginId != 0)
                {
                    byte[] PasswordSalt = login.PasswordSalt;
                    string Token = BitConverter.ToString(PasswordSalt);
                    PasswordSalt = Encoding.UTF8.GetBytes(Token);
                    Token = Encoding.UTF8.GetString(PasswordSalt);
                    string body = string.Format("Для завершення реєстрації перейдіть за посиланням:" +
                                    $"<a href=\"https://localhost:44328/Account/{action}?Email={email}&Token={Token}\" title=Підтвердити реєстрацію>Підтвердити </a>"
                                    ); 
                    MailMessage message = SendMessage(email, subject,body);
                    return true;
                }
                return false;
            }catch { return false; }
       
        }

        public bool SendResetPasswordKey(string email)
        {
            try
            {               
                Login login = _unitOfWork.LoginRepository.Get(l => l.Email == email);
                if (login != null)
                {
                    byte[] PasswordSalt = login.PasswordSalt;
                    string Token = BitConverter.ToString(PasswordSalt);
                    PasswordSalt = Encoding.UTF8.GetBytes(Token);
                    Token = Encoding.UTF8.GetString(PasswordSalt);
                    string body = string.Format($"Код для скидання пароля: {Token}");
                    MailMessage message = SendMessage(email, "Скидання пароля", body);
                    return true;
                }
                return false;
            }catch { return false; }
        }
       
    }
}


