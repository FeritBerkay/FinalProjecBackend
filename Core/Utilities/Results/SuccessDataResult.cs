using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results
{
    public class SuccessDataResult<T>:DataResult<T>
    {
        //Islem sonucu default true
        public SuccessDataResult(T data, string message):base(data,true,message)
        {

        }
        //Messagesiz hali
        public SuccessDataResult(T data):base(data,true)
        {

        }
        //Data default yok yani sadece message.
        public SuccessDataResult(string message):base(default,true,message)
        {

        }
        //Data default sade success bilgisi. 
        public SuccessDataResult(bool success):base(default,true)
        {

        }
    }
}
