using System;
using System.Web.Mvc;
using FluentValidation;

namespace Copter.Web.Mvc.Validation
{
    /// <summary>
    /// Mvc Validator工厂
    /// </summary>
    public class MvcValidatorFactory : ValidatorFactoryBase
    {
        public override IValidator CreateInstance(Type validatorType)
        {
            return DependencyResolver.Current.GetService(validatorType) as IValidator;
        }
    }
}
