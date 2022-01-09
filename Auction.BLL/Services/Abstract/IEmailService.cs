namespace Auction.BLL.Services.Abstract
{
    public interface IEmailService
    {
        bool SendPasswordConfirmed(string email, string subject, string action);
        bool SendResetPasswordKey(string email);
    }
}
