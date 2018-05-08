namespace Copter.Data.EF
{
    /// <summary>
    /// 领域实体 映射接口
    /// </summary>
    internal interface IEntityTypeMap
    {
        /// <summary>
        /// 初始化 可选字段 Map
        /// </summary>
        void InitOptionalMap();
    }
}
