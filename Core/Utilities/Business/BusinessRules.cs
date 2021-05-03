using Core.Utilities.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Business
{
    public class BusinessRules
    {
        //Param yazınca istedigin kadar IResult Tipinde logics verirsin.
        public static IResult Run(params IResult[] logics)
        {
            //Basarısız olani cektik. Logic verdigimiz kural.
            foreach (var logic in logics)
            {
                if (logic.Succes==false)
                {
                    return logic;
                }
            }
            return null;
        }
    }
}
