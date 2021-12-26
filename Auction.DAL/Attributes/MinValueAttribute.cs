using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auction.DAL.Attributes
{
    public class MinValueAttribute:ValidationAttribute
    {
        int minNum;

        public MinValueAttribute(int minNum)
        {
            this.minNum = minNum;
        }
        public override bool IsValid(object value)
        {
            if (value != null)
            {
                int num=Convert.ToInt32(value);
                if (num >= minNum)
                    return true;
                else
                    this.ErrorMessage =  $"Ціна не може бути ниже за {minNum}";
            }
            return false;
        }
    }
}
/*public class UserNameAttribute : ValidationAttribute
{
    public override bool IsValid(object value)
    {
        if (value != null)
        {
            string userName = value.ToString();
            if (!userName.StartsWith("T"))
                return true;
            else
                this.ErrorMessage = "Имя не должно начинаться с буквы T";
        }
        return false;
    }
}*/