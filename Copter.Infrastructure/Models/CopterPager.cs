namespace Copter.Infrastructure.Models
{
    /// <summary>
    /// 分页数据接收载体
    /// </summary>
    public class CopterPager
    {
        private int _page;

        /// <summary>
        /// 当前页
        /// </summary>
        public int Page
        {
            get
            {
                if (_page < 1) return 1;
                return _page;
            }
            set
            {
                if (value > 0)
                    _page = value;
            }
        }
        private int _pageSize;
        /// <summary>
        /// 页容量大小
        /// </summary>
        public int PageSize
        {
            get
            {
                if (_pageSize < 1) return 10;
                return _pageSize;
            }
            set
            {
                if (value > 0)
                    _pageSize = value;
            }
        }
    }
}
