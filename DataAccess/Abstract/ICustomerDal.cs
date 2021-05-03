using Core.DataAccess;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Abstract
{
    //Goruldugu uzere yeni bir customer olusturduk ve open close mantıgı ile hızla olusturduk.
    public interface ICustomerDal:IEntityRepository<Customer>
    {
    }
}
