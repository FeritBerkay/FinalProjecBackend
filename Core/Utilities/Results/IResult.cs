using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results
{
    //Temel voidler icin.
    public interface IResult
    {
        //Get sadece okunabilir demek.
        bool Succes { get; }
        string Message { get; }
    }
}
