using FluentValidation;

namespace Copter.Infrastructure.Models
{
    /// <summary>
    /// FluentValidation 验证基类
    /// </summary>
    /// <typeparam name="T">验证模型 类型T</typeparam>
    public abstract class CopterBaseValidator<T>: AbstractValidator<T> where T:class, new()
    {
        protected CopterBaseValidator()
        {
        }
    }
}
