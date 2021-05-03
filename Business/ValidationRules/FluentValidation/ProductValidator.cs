using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.ValidationRules.FluentValidation
{
    public class ProductValidator : AbstractValidator<Product>
    {
        //Hangi validator icin kural yazıcaksan bunu alta yz.
        public ProductValidator()
        {
            RuleFor(P => P.ProductName).NotEmpty();
            RuleFor(P => P.ProductName).MinimumLength(2);
            RuleFor(P => P.UnitPrice).NotEmpty();
            RuleFor(P => P.UnitPrice).GreaterThan(0);
            //Bole when de olur. WithMesaage ile mesesage verbilirsin.
            RuleFor(P => P.UnitPrice).GreaterThanOrEqualTo(10).When(p => p.CategoryId == 1);
            //Benim kendi kural aga. ProductMana must StartWithA
            //RuleFor(p => p.ProductName).Must(StartWithA).WithMessage("ProductsName must start 'A' letter.");
        }
        //Bool var donerse true calıs demek. Argda gonderdigimiz parametre.Yani productName
        private bool StartWithA(string arg)
        {
            return arg.StartsWith("A");
        }
    }
}
