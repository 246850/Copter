using System;
using FluentValidation;
using System.Web.Http;

namespace Copter.Web.WebApi.Validation
{
    public class WebApiValidatorFactory : ValidatorFactoryBase
    {
        public override IValidator CreateInstance(Type validatorType)
        {
            //if(_container.IsRegistered(validatorType)){
            //    return _container.BeginLifetimeScope(MatchingScopeLifetimeTags.RequestLifetimeScopeTag).Resolve(validatorType) as IValidator;
            //}
            //return null;


            return GlobalConfiguration.Configuration.DependencyResolver.GetService(validatorType) as IValidator;
        }
    }
}
