using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Intercreptors;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Aspects.AutoFac.Validation
{
    //Bu bizim Aspectimiz. Aspect=Methodun basında sonunda hata verince calısac method.
    public class ValidationAspect : MethodInterception 
    {
        private Type _validatorType;
        //Defensive Coding= Savunma odakla kodlama.Yazmasanda calısır.Ama attirubuteler type of ile calısır biz bunu kontrol ediyoruz.
        public ValidationAspect(Type validatorType)
        {
            if (!typeof(IValidator).IsAssignableFrom(validatorType))
            {
                throw new System.Exception("This isn't verify class");
            }

            _validatorType = validatorType;
        }
        //Ezmesini istedigimiz methodu yazdık. Validation dogrulama bundan dolayı onbefore u ezdik.
        protected override void OnBefore(IInvocation invocation)
        {
            //Calısma anında newler. 
            //Calısma tipini bul(Base typesini). ProductValidator kullandıgını dusun. Git oraya o ne ile calısıyor bak. Car ı buldu.
            //Sonrada ProductValidatorun kullanılıdıgı methodun parametrelerine bak. Parametrelerde cara denk gelen parametreleri bul.
            //Bu parametrelerde typeları aynı olanı al ve  herbirini gez.(Herbirinde calıs.)
            var validator = (IValidator)Activator.CreateInstance(_validatorType);
            var entityType = _validatorType.BaseType.GetGenericArguments()[0];
            var entities = invocation.Arguments.Where(t => t.GetType() == entityType);
            foreach (var entity in entities)
            {
                ValidationTool.Validate(validator, entity);
            }
        }
    }
}
