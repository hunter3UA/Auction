using Auction.BLL.Services.Abstract;
using Auction.DAL.Models;
using Auction.DAL.UoW;
using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;



//TODO: оптимизировать сервис
namespace Auction.BLL.Services
{
    public class EmailService:IEmailService
    {
        private readonly IUnitOfWork _unitOfWork;
        public EmailService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        private MailMessage CreateMessage(string email,string subject)
        {
            MailAddress from = new MailAddress("hunter3ua@gmail.com", "Torg-company");
            MailAddress to = new MailAddress(email);
            MailMessage message = new MailMessage(from, to);
            message.Subject = subject;
            return message;
        }


        public void SendPasswordConfirmed(string email,string subject,string action)
        {
            MailMessage message= CreateMessage(email,subject);          
            Login login = _unitOfWork.LoginRepository.Get(l => l.Email==email);
            byte[] PasswordSalt = login.PasswordSalt;
            string Token = BitConverter.ToString(PasswordSalt);           
            PasswordSalt=Encoding.UTF8.GetBytes(Token);
            Token = Encoding.UTF8.GetString(PasswordSalt);

            message.Body = string.Format("Для завершення реєстрації перейдіть за посиланням:" +
                            $"<a href=\"https://localhost:44328/Account/{action}?Email={email}&Token={Token}\" title=Підтвердити реєстрацію>Підтвердити </a>"
                            );
           
            message.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 25);
            smtp.EnableSsl = true;
            smtp.Credentials = new System.Net.NetworkCredential("hunter3ua@gmail.com", "hunter3UA112233");
            smtp.Send(message);
        }

        public void SendResetPasswordKey(string email)
        {
            MailMessage message = CreateMessage(email, "Скидання пароля");
            Login login = _unitOfWork.LoginRepository.Get(l => l.Email == email);
            byte[] PasswordSalt = login.PasswordSalt;
            string Token = BitConverter.ToString(PasswordSalt);
            PasswordSalt = Encoding.UTF8.GetBytes(Token);
            Token = Encoding.UTF8.GetString(PasswordSalt);
            message.Body = string.Format($"Код для скидання пароля: {Token}" );
            message.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 25);
            smtp.EnableSsl = true;
            smtp.Credentials = new System.Net.NetworkCredential("hunter3ua@gmail.com", "hunter3UA112233");
            smtp.Send(message);
        }
       
    }
}

