using Auction.BLL.Services.Abstract;
using Auction.BLL.ViewModels;
using System;
using System.Configuration;
using System.Security.Cryptography;

namespace Auction.BLL.Services
{
    public class PasswordService:IPasswordService
    {
     
        public static int hash_length = Convert.ToInt32(ConfigurationManager.AppSettings["HashLength"]);
        private static int iterations = Convert.ToInt32(ConfigurationManager.AppSettings["HashIterations"]);
        public byte[] CreateToken(int TokenLength)
        {
                if (TokenLength <= 0)
                { throw new ArgumentOutOfRangeException("TokenLength", "number must be positive"); }
                RNGCryptoServiceProvider rngCsp = new RNGCryptoServiceProvider();
                var salt = new byte[TokenLength];
                rngCsp.GetNonZeroBytes(salt);
                return salt;
        }
        public Salted_Hash CreateSaltedHash(string stringToHash, int saltLenght)
        {
                int iterations = Convert.ToInt32(ConfigurationManager.AppSettings["HashIterations"]);
                if (stringToHash == null)
                { throw new ArgumentNullException("password"); }
                else if (iterations <= 0)
                { throw new ArgumentOutOfRangeException("iterations", "number must be positive"); }
                else if (saltLenght <= 0)
                { throw new ArgumentOutOfRangeException("saltLenght", "number must be positive"); }
                Salted_Hash salted_Hash = new Salted_Hash(){ Salt = CreateToken(saltLenght) };
                using (Rfc2898DeriveBytes hashDriver = new Rfc2898DeriveBytes(
                    stringToHash,       
                    salted_Hash.Salt,   
                    iterations))        
                {
                    salted_Hash.Hash = hashDriver.GetBytes(hash_length);
                }
                return salted_Hash;
        }
        public bool CheckSaltedHash(string stringToCheck, Salted_Hash saltedHash)
        {
                if (stringToCheck == null)
                { throw new ArgumentNullException("password"); }
                else if (saltedHash.Hash == null)
                { throw new ArgumentNullException("salt"); }
                else if (saltedHash.Salt == null)
                { throw new ArgumentNullException("hashedPassword"); }
                byte[] hashGenerated = null;
                using (Rfc2898DeriveBytes hashDriver = new Rfc2898DeriveBytes(
                     stringToCheck,          
                     saltedHash.Salt,        
                     iterations)) 
                {
                     hashGenerated = hashDriver.GetBytes(hash_length);
                }
                return ByteArraysEqual(hashGenerated, saltedHash.Hash);
        }
        private bool ByteArraysEqual(byte[] buff1, byte[] buff2)
        {
                if (buff1.Length != buff2.Length)
                {
                    return false;
                }
                for (int i = 0; i < buff2.Length; i++)
                {
                    if (buff1[i] != buff2[i]) { return false; }
                }
                return true;
        }
       


    }
}