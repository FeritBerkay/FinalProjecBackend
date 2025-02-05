﻿using Castle.DynamicProxy;
using System;

namespace Core.Utilities.Intercreptors
{
    public abstract class MethodInterception : MethodInterceptionBaseAttribute
    {
        //Virtual method ezmeni bekliyen method.
        protected virtual void OnBefore(IInvocation invocation) { }
        //invocation da method isimleri.
        protected virtual void OnAfter(IInvocation invocation) { }
        protected virtual void OnException(IInvocation invocation, System.Exception e) { }
        protected virtual void OnSuccess(IInvocation invocation) { }
        public override void Intercept(IInvocation invocation)
        {
            var isSuccess = true;
            OnBefore(invocation);
            try
            {
                invocation.Proceed();
            }
            catch (Exception e)
            {
                isSuccess = false;
                OnException(invocation, e);
                throw;
            }
            finally
            {
                if (isSuccess)
                {
                    OnSuccess(invocation);
                }
            }
            OnAfter(invocation);
        }
    }
}
