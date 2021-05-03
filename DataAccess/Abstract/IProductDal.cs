using Core.DataAccess;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

//DataAcces e sql kodları yazılır.
namespace DataAccess.Abstract
{
    //I interface oldugu prdocut entities i dal da yapıcagı islem.
    //Sen bir IEntityRepository nin cocgusun ve T tipin product
    public interface IProductDal:IEntityRepository<Product>
    {
        List<ProductDetailDto> GetProductDetails();
    }
}
