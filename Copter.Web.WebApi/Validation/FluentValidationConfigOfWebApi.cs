using System.Web.Http;
using FluentValidation;
using FluentValidation.WebApi;


namespace Copter.Web.WebApi.Validation
{
    /// <summary>
    /// FluentValidation WebApi 配置
    /// </summary>
    public sealed class FluentValidationConfigOfWebApi
    {
        public static void Configure()
        {
            HttpConfiguration configuration = GlobalConfiguration.Configuration;
            ValidatorOptions.CascadeMode = CascadeMode.StopOnFirstFailure;
            FluentValidationModelValidatorProvider.Configure(configuration, provider => provider.ValidatorFactory = new WebApiValidatorFactory());
        }
    }
}
