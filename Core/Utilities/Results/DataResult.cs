using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results
{
    public class DataResult<T> : Result, IDataResult<T>
    {
        //Diger resulttan farkı data da vermesi. 
        public DataResult(T data, bool succes, string message):base(succes,message)
        {
            Data = data;
        }
        public DataResult(T data, bool succes):base(succes)
        {
            Data = data;
        }
        public T Data { get; }
    }
}
