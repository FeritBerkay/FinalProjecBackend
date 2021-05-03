using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
//Contans ı core yazmadık cunku core sirketteki tum projelerde kullanabilecek bir katman. Ancak burası sadece northwinde ozel. RentCarda olmaz.
namespace Business.Constans
{
    //Mesajları burada tutucagız yani hep added added yazmak yerine bir kere yazıp yapıyoruz. Added yerine eklendi yapmamız gerekirse ugrasmıyacagız.
    //Static newlemeye ihtiyac duymaz.
    public static class Messages
    {
        
        public static string ProductAdded = "Product Added";
        public static string ProductNameInvalid = "Product Name is invalid";
        public static string MaintenanceTime = "Invalid access time";
        public static string ProductsListed = "All Product  listed";
        public static string ProductsCountOfCategoryError= "Bir kategoride en fazla 10 urun olur.";
        public static string ProductNameAlreadyExist = "Bu isimde zaten baska bir urun var";
        public static string CategoryLimitExceled = "Categori limiti asıldıgı icin yeni urun eklenemiyor";
        public static string AuthorizationDenied= "Yetkiniz yok.";

        public static string UserNotFound = "Kullanıcı bulunamadı";
        public static string PasswordError = "Şifre hatalı";
        public static string SuccessfulLogin = "Sisteme giriş başarılı";
        public static string UserAlreadyExists = "Bu kullanıcı zaten mevcut";
        public static string UserRegistered = "Kullanıcı başarıyla kaydedildi";
        public static string AccessTokenCreated = "Access token başarıyla oluşturuldu";

        public static string ProductNameAlreadyExists = "Ürün ismi zaten mevcut";

        public static string ProductCountOfCategoryError = "Bir kategoride en fazla 10 ürün olabilir";
    }
}
