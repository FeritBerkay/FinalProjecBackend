using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results
{
    //IResult takiler var bide T tipinde datan var. Resulttakileri kullanabilmek icin yaptık burayı.
    public interface IDataResult<T>:IResult
    {
        //T tipinde bir data bu product olur list<Product> olur np yani.
        T Data { get; }
    }
}
