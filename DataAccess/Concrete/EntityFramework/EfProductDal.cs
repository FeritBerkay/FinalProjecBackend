﻿using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Concrete.EntityFramework
{
    //Gercek dataya basladık. EntitFramework linq destekli calısan bir veri tabanındaki tabloyu class gibi iliskilendiren bir framework.
    public class EfProductDal : IfEntityRepositoryBase<Product, NorthwindContext>, IProductDal
    {
        public List<ProductDetailDto> GetProductDetails()
        {
            using (NorthwindContext context= new NorthwindContext())
            {
                //Product daki urunler ile categorileri join et product a p categirye c de.
                //Esit olanı cagırdık ve neyin nereye geldiginiz yazdık.
                var result = from p in context.Products
                             join c in context.Categories
                             on p.CategoryId equals c.CategoryId
                             select new ProductDetailDto {
                                 ProductId=p.ProductId, ProductName=p.ProductName,
                                 CategoryName=c.CategoryName, UnitsInStock=p.UnitsInStock };
                return result.ToList();
            }
        }
    }
}
