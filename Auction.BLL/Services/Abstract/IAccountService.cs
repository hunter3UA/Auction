using Auction.BLL.ViewModels;
using Auction.DAL.Models;
using System.Threading.Tasks;

namespace Auction.BLL.Services.Abstract
{
    public interface IAccountService
    {
        Task<User> CreateUser(RegisterModel registerModel);

        User Login(LoginModel loginModel);
        UserModel GetUser(int loginId);
        Task Update(UserModel userModel, int loginId);
    }
}
