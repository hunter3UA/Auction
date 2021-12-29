using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auction.BLL.ViewModels
{
    public class Salted_Hash
    {
        public byte[] Hash { get; set; } = null;
        public byte[] Salt { get; set; } = null;
    }
}
