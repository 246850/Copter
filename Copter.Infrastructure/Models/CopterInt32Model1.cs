namespace Copter.Infrastructure.Models
{
    /// <summary>
    /// 业务模型 - 主键类型Int32抽象基类 - 带SelectListItem
    /// </summary>
    /// <typeparam name="TSelectListItem">数据载体类型</typeparam>
    public class CopterInt32Model<TSelectListItem>: CopterBaseModel<int, TSelectListItem> where TSelectListItem : class
    {
    }
}
