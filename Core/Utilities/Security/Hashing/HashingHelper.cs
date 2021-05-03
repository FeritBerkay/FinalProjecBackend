using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Security.Hashing
{
    //Hashleme aracı
    public class HashingHelper
    {
        //Password verip passwordHash ve passwordSalt alacagız.
        public static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac= new System.Security.Cryptography.HMACSHA512())
            {
                //Karsılıklarını yazdık ComputeHash bye istedigi icin byte yaptık. Her kullanıcı icin farklı key olusturur.
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }
        //Burada out yok cunku ben yazıcam. Yukarıda o bize veriyordu
        //Burada karsılastırma yapıyoruz. Bize verilen password ile salt ve hash karsılastırıyoruz.
        public static bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            //Keyi verdik ki keye gore calıssın.
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    //Hashler eslesmez ise. false eslenirse true.
                    if (computedHash[i]!=passwordHash[i])
                    {
                        return false;
                    }
                }
                return true;
            }
        }
    }
}
