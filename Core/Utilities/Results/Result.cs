using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results
{
    public class Result : IResult
    {
        //Yani yaptıgımız sey ilki calısırsa ikincisi de calısıcak. Ama ikincisi calısınca ilki calısmak zorunda degil.
        //This kendini demek. this(succes) demek. Sen calısınca succesi de calıstır. İkiside ctor oldugu icin oldu bu.
        public Result(bool succes, string message):this(succes)
        {
            //Hani Message set edilemezdi deme. Getler Read only bu nedenle ctor ici set edilebilir. 
            //Peki neden set vermedik. Kafasına gore takılmasın diye.
            Message = message;
            //Succesi assagıda verdik. Cunku her zaman mesaj istemeyiz.
            //Succes = succes;
        }
        //Hem messageli hemde mesaggesiz yazabilirsin dedik.
        public Result(bool succes)
        {
            
            Succes = succes;
        }

        public bool Succes { get; }

        public string Message { get; }

    }
}
