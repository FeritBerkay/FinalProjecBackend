using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

//Veri erişim katmanı. Gercek degil Bellekte data varmıs gibi yapıyoruz.
namespace DataAccess.Concrete.InMemory
{
    public class InMemoryProductDal : IProductDal
    {
        //List<Product> _products; kodu ile otomatik olarak name=name falan yapmak yerine hepsini cektik.
        List<Product> _products;
        //Class calısınca direk calısır. Ctor mantıgı
        public InMemoryProductDal()
        {
            //Proje calısınca bellekte otomatik bu listeyi olusturur sanki bir veri tabanından geloymus gibi.
            _products = new List<Product> 
            { 
                //new Product{ProductID=1, CategoryID=1, ProductName="Bardak", UnitPrice=15, UnitsInStock=15},
                //new Product{ProductID=2, CategoryID=1, ProductName="Kamera", UnitPrice=500, UnitsInStock=3},
                //new Product{ProductID=3, CategoryID=2, ProductName="Telefon", UnitPrice=1500, UnitsInStock=2},
                //new Product{ProductID=4, CategoryID=2, ProductName="Klavye", UnitPrice=150, UnitsInStock=65},
                //new Product{ProductID=5, CategoryID=2, ProductName="Mouse", UnitPrice=85, UnitsInStock=1},
            };
        }
        public void Add(Product product)
        {
            _products.Add(product);
        }

        public void Delete(Product product)
        {
            //Alttaki calısmaz cunku referanslar farklı. Sen add ile yeni bir urun olusturdugun bunun referansı direk producta girmez ki. 
            //Bu nedenle biz Id lerden gidiyoruz cunku her urunun ıdsi farklı
            //_products.Remove(product);

            //LINQ= Dile gomulu sorgulama aynı sql ile flitreleme saglar.
            //P=> lamba demek
            //Gonderdigim urun ıdsine sahip olan listedeki urunu bul demek.
            Product productToDelete = _products.SingleOrDefault(p=>p.ProductId==product.ProductId);

            _products.Remove(productToDelete);


        }

        public Product Get(Expression<Func<Product, bool>> filter)
        {
            throw new NotImplementedException();
        }

        //Veri tabanını dondurmek icin.
        public List<Product> GetAll()
        {
            return _products;
        }

        public List<Product> GetAll(Expression<Func<Product, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        public List<Product> GetAllByCategory(int categoryId)
        {
            //Where kosulu icinde tum elemanların uyduguklarını yeni bir liste yapar ve biz bu listeyi yeni bir listeye atadık. 
            //Sql deki where gibi
            return _products.Where(p=> p.CategoryId==categoryId).ToList();
        }

        public List<ProductDetailDto> GetProductDetails()
        {
            throw new NotImplementedException();
        }

        public void Update(Product product)
        {
            //Gonderdigim urun ıdsine sahip olan listedeki urunu  bul demek.
            //Product productToUpdate = _products.SingleOrDefault(p => p.ProductID == product.ProductID);
            //productToUpdate.ProductName = product.ProductName;
            //productToUpdate.CategoryID = product.CategoryID;
            //productToUpdate.UnitPrice = product.UnitPrice;
            //productToUpdate.UnitsInStock = product.UnitsInStock;
        }
    }
}
