using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DTOs
{
    //Tek basına bir tablo degil biz join yapıcaz burada ondan dolayı IEntity olmaz.
    //Join yapmamızın sebebi biz product name ile category nameyi aynı yerde cagıramayız.
    public class ProductDetailDto:IDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string  CategoryName { get; set; }
        public short UnitsInStock { get; set; }
    }
}
