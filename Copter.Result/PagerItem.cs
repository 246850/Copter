﻿namespace Copter.Result
{
    /// <summary>
    /// 分页信息类
    /// </summary>
    public sealed class PagerItem
    {
        /// <summary>
        /// 当前页
        /// </summary>
        public int Page { get; }
        /// <summary>
        /// 页容量大小
        /// </summary>
        public int PageSize { get;}
        /// <summary>
        /// 总记录数
        /// </summary>
        public long Total { get; }
        /// <summary>
        /// 总页数
        /// </summary>
        public long TotalPage
        {
            get
            {
                if (PageSize == 0) return 0;
                return (Total + PageSize - 1) / PageSize;
                //return this.Total % this.PageSize == 0 ? this.Total / this.PageSize : this.Total / this.PageSize + 1;
            }
        }
        /// <summary>
        /// 页容量大小数组
        /// </summary>
        public int[] Records { get; private set; }
        /// <summary>
        /// 是否存在上一页
        /// </summary>
        public bool HasPreviousPage { get { return Page > 1 && TotalPage > 1; } }
        /// <summary>
        /// 是否存在下一页
        /// </summary>
        public bool HasNextPage { get { return Page > 0 && Page <= TotalPage - 1; } }

        internal PagerItem() : this(1, 15, 0) { }
        internal PagerItem(int page, int pageSize, long total)
        {
            Page = page;
            PageSize = pageSize;
            Total = total;
            Records = new[] { 15, 20, 30, 50, 100 };
        }
    }
}
