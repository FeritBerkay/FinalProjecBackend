using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results
{
    public class ErrorDataResult<T> : DataResult<T>
    {
        //Islem sonucu default false
        public ErrorDataResult(T data, string message) : base(data, false, message)
        {

        }
        //Messagesiz hali
        public ErrorDataResult(T data) : base(data, false)
        {

        }
        //Data default yok yani sadece message.
        public ErrorDataResult(string message) : base(default, false, message)
        {

        }
        //Data default sade success bilgisi. 
        public ErrorDataResult(bool success) : base(default, false)
        {

        }
    }
}
