using System;
using System.ComponentModel.DataAnnotations;

namespace Auction.DAL.Attributes
{
    public class MinValueAttribute:ValidationAttribute
    {
        ulong minNum;

        public MinValueAttribute(ulong minNum)
        {
            this.minNum = minNum;
        }
        public override bool IsValid(object value)
        {
            if (value != null)
            {
                ulong num=(ulong)value;
                if (num >= minNum)
                    return true;
                else
                    this.ErrorMessage =  $"Ціна не може бути ниже за {minNum}";
            }
            return false;
        }
    }
}
