using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract_Referans_tutucular
{
    public interface IProductService
    {
        IDataResult<List<Product>> GetAll();
        IDataResult<List<Product>> GetAllByCategoryId(int id);
        IDataResult<List<Product>> GetByUnitPrice(decimal min, decimal max);
        IDataResult<List<ProductDetailDto>> GetProductDetails();
        IDataResult<Product> GetById(int produtId);
        //Dedikki sen void degil IResult dondur. Product ya list product degil IResult
        //void Add(Product product);
        IResult Add(Product product);
        IResult Update(Product product);
        IResult AddTransactionalTest(Product product);

        //RESTFULL --> HTTP --> Sen http.blablablabla ya girersen oraya istekte bulunursun. 
        //WebAPI deki controllers de bunu yapar istekleri tasarlar.
    }
}
