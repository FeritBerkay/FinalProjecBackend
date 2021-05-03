using System;
using System.Collections.Generic;
using System.Text;
using Business.Constans;
using Castle.DynamicProxy;
using Core.Extensions;
using Core.Utilities.Intercreptors;
using Core.Utilities.IoC;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Business.BusinessAspects.Autofac
{
    public class SecuredOperation : MethodInterception
    {
        private string[] _roles;
        //Token istegi yapıyoruz ya. Herkes icin bir http context olusturulur.
        private IHttpContextAccessor _httpContextAccessor;

        //Bana rolleri ver.(Claimleri). Split ayırıp array a atıyor. 
        public SecuredOperation(string roles)
        {
            _roles = roles.Split(',');
            //ServiceTool ile injection altyapısını aynen okuyabilmemize yarıyan bir arac.
            _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();

        }
        //Istenilen roll varsa devam. Yoksa yetkin yok hatası ver.
        protected override void OnBefore(IInvocation invocation)
        {
            var roleClaims = _httpContextAccessor.HttpContext.User.ClaimRoles();
            foreach (var role in _roles)
            {
                if (roleClaims.Contains(role))
                {
                    return;
                }
            }
            throw new Exception(Messages.AuthorizationDenied);
        }
    }
}