using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.InMemory;
using System;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            //ProductTest();
            //CategoryTest();
            ProductManager productManager = new ProductManager(new EfProductDal(), new CategoryManager(new EfCategoryDal()));
            var result = productManager.GetProductDetails();
            if (result.Succes==true)
            {
                foreach (var product in result.Data)
                {
                    Console.WriteLine("Product Name = " + product.ProductName + " Category Name= " + product.CategoryName);
                }
            }
            else
            {
                Console.WriteLine(result.Message);
            }
            
           
            //var get= productManager.GetById(2);
            //Console.WriteLine(get.ProductName + " " + get.ProductID);
        }

        private static void CategoryTest()
        {
            CategoryManager categoryManager = new CategoryManager(new EfCategoryDal());
            foreach (var category in categoryManager.GetAll().Data)
            {
                Console.WriteLine(category.CategoryName);
            }
        }

        //private static void ProductTest()
        //{
        //    ProductManager productManager = new ProductManager(new EfProductDal());
        //    foreach (var product in productManager.GetAll().Data)
        //    {
        //        Console.WriteLine(product.ProductName);
        //        Console.WriteLine(product.UnitPrice);
        //    }
    }
}
