using Business.Abstract;
using Business.Abstract_Referans_tutucular;
using Business.BusinessAspects.Autofac;
using Business.CCS;
using Business.Constans;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.AutoFac.Caching;
using Core.Aspects.AutoFac.Transaction;
using Core.Aspects.AutoFac.Validation;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.InMemory;
using Entities.Concrete;
using Entities.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        //Bir is sınııfı baska sınıfları newlemez
        IProductDal _productDal;
        //Bir entitymanager kendisi haric baska dalı enjekte edemez mıctın. Bu kullanıma zorundaysan Service kullan.
        //ICategoryDal _categoryDal;
        ICategoryService _categoryService;
        
        public ProductManager(IProductDal productDal, ICategoryService categoryService)
        {
            _productDal = productDal;
            _categoryService = categoryService;
        }
        //Attiributes
        [SecuredOperation("product.add,admin")]
        [ValidationAspect(typeof(ProductValidator))]
        //Yeni urun eklenince cachi bozmak icin.
        [CacheRemoveAspect("IProductService.Get")]
        public IResult Add(Product product)
        {
            //Aga sadece log bu. Validation (kural var) ama onu aspectledik yaaaa. Bole yazma gram solid degil ondan Aspectin ..... ye.
            //_logger.Log();
            //try
            //{
            //    _productDal.Add(product);
            //    return new SuccesResult(Messages.ProductAdded);
            //}
            //catch (Exception exception)
            //{

            //    _logger.Log();
            //}
            //Validation= Eklenmesini istedigimiz nesnenin ozellikleri. Bİz bu validationlar influentevalidation ile kural yerinde toplayabiliriz.
            //Validation calıstırma: Bu kadar spagetti bir kod yok yani. Ondan core ye gel bida bida yazmayı bırak. Core kopyaladım codu bakarsin.
            //Bir kategoride en fazla 10 urun olabilir. Bunu productValidator e yazamassın. Sadece yapı yazılır.
            //Bole yazınca oldu ancak Solid olmaz. Spaggeti oldu.
            //var result = _productDal.GetAll(p => p.CategoryID == product.CategoryID);
            //if (result.Count >= 10) 
            //{
            //    return new ErrorResult(); 
            //}Bunun yerine alttaki.

            //Is kurallarında calısmayanları bulmak icin.


            IResult result = BusinessRules.Run(CheckIfProductNameExist(product.ProductName),
                CheckIfProductCountOfCategoryCorrect(product.CategoryId),
                CheckIfCategoryLimitExceled());

            if (result != null)
            {
                return result;
            }


            _productDal.Add(product);
            return new SuccesResult(Messages.ProductAdded);

            //Alttaki gibi tek tek kural yazma
            //if (CheckIfProductCountOfCategoryCorrect(product.CategoryID).Succes)
            //{
            //    if (CheckIfProductNameExist(product.ProductName).Succes)
            //    {
            //        _productDal.Add(product);
            //        return new SuccesResult(Messages.ProductAdded);
            //    }
            //}
            //return new ErrorResult();
            
        }

        //Bu sekilde yazadık cunku. Bir her methodumuzda newleme yaparsak hernagi bir veritabanı degisikliginde 
        //tum methodlarımızdaki productdalları degistirmemiz gerekir.
        [CacheAspect]
        public IDataResult<List<Product>> GetAll()
        {
            //if (DateTime.Now.Hour == 23)
            //{
            //    return new ErrorDataResult<List<Product>>(Messages.MaintenanceTime);
            //}
            //Datam _productDal.GetAll(), succes true, message istege baglı istediginiz yaz.
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(),Messages.ProductsListed);

        }

        public IDataResult<List<Product>> GetAllByCategoryId(int id)
        {
            //Bizden flitre istiyordu verdik.
            return new SuccessDataResult<List<Product>> (_productDal.GetAll(p=>p.CategoryId==id));
        }   

        [CacheAspect]
        public IDataResult<Product> GetById(int produtId)
        {
            return new SuccessDataResult<Product> (_productDal.Get(p => p.ProductId == produtId));
        }

        public IDataResult<List<Product>> GetByUnitPrice(decimal min, decimal max)
        {
            return new SuccessDataResult<List<Product>> (_productDal.GetAll(p=> p.UnitPrice>=min && p.UnitPrice<=max));
        }

        public IDataResult<List<ProductDetailDto>> GetProductDetails()
        {
            return new SuccessDataResult<List<ProductDetailDto>>(_productDal.GetProductDetails(),Messages.ProductsListed);
        }
        //IProcudtServicedeki get olan butun cacheleri sildik.
        [CacheRemoveAspect("IProductService.Get")]
        public IResult Update(Product product)
        {
            throw new NotImplementedException();
        }
        //private IResult CheckIfProductCountOfCategoryCorrect(int categoryId)
        //{
        //    var result = _productDal.GetAll(p => p.CategoryId == categoryId);
        //    if (result.Count >= 10)
        //    {
        //        return new ErrorResult();
        //    }
        //    return new SuccesResult();
        //}
        private IResult CheckIfProductCountOfCategoryCorrect(int categoryId)
        {
            //Select count(*) from products where categoryId=1
            var result = _productDal.GetAll(p => p.CategoryId == categoryId).Count;
            if (result >= 15)
            {
                return new ErrorResult(Messages.ProductCountOfCategoryError);
            }
            return new SuccesResult();
        }
        private IResult CheckIfProductNameExist(string productName)
        {
            var result = _productDal.GetAll(p => p.ProductName == productName);
            if (result.Any() == true)
            {
                return new ErrorResult(Messages.ProductNameAlreadyExist);
            }
            return new SuccesResult();
        }
        private IResult CheckIfCategoryLimitExceled()
        {
            var result = _categoryService.GetAll();
            if (result.Data.Count>15)
            {
                return new ErrorResult(Messages.CategoryLimitExceled);
            }
            return new SuccesResult();
        }
        [TransactionScopeAspect]
        //Amacımız eger ikinci add calısmazsa ilk addı geri almak.
        public IResult AddTransactionalTest(Product product)
        {
            Add(product);
            if (product.ProductName.Length>10)
            {
                throw new Exception(" ");
            }
            Add(product);

            return null;
        }
    }
}
