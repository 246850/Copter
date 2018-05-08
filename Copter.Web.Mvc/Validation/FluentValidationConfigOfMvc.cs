using System.Web.Mvc;
using FluentValidation;
using FluentValidation.Mvc;

namespace Copter.Web.Mvc.Validation
{
    /// <summary>
    /// FluentValidation MVC 配置
    /// </summary>
    public sealed class FluentValidationConfigOfMvc
    {
        public static void Configure()
        {
            ModelValidatorProviders.Providers.Add(new FluentValidationModelValidatorProvider(new MvcValidatorFactory()));
            DataAnnotationsModelValidatorProvider.AddImplicitRequiredAttributeForValueTypes = false;
            ValidatorOptions.CascadeMode = CascadeMode.StopOnFirstFailure;
            FluentValidationModelValidatorProvider.Configure();
        }
    }
}
