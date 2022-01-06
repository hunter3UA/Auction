using System;
using System.ComponentModel.DataAnnotations;

namespace Auction.DAL.Attributes
{
    public class MaxValueAttribute : ValidationAttribute
    {
        int maxNum;

        public MaxValueAttribute(int minNum)
        {
            this.maxNum = minNum;
        }
        public override bool IsValid(object value)
        {
            if (value != null)
            {
                int num = Convert.ToInt32(value);
                if (num >= maxNum)
                    return true;
                else
                    this.ErrorMessage = $"Ціна не може бути вища за {maxNum}";
            }
            return false;
        }
    }
}
/*public class MinValueAttribute:ValidationAttribute
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
    }*/