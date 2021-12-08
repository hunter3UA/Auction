using Auction.DAL.Models;

namespace Auction.BLL.Login
{
    public class UserSerializationModel
    {

        public int LoginId { get; set; }
        public AccountType AccountType { get; set; }
    }
}
