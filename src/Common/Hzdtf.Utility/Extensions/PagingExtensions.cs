namespace System
{
    /// <summary>
    /// 分页扩展类
    /// @ 黄振东
    /// </summary>
    public static class PagingExtensions
    {
        /// <summary>
        /// 获取页码，从0开始
        /// </summary>
        /// <param name="page">页码，从1开始</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="total">总记录</param>
        /// <returns>页码</returns>
        public static int GetPageIndex(this int page, int pageSize, int total)
        {
            if (total == 0 || pageSize == 0)
            {
                return 0;
            }

            var pageCount = total / pageSize;
            if (total % pageSize != 0)
            {
                pageCount++;
            }

            if (page > pageCount)
            {
                return (int)(pageCount - 1);
            }

            return (int)(page - 1);
        }

        /// <summary>
        /// 获取跳过记录索引
        /// </summary>
        /// <param name="pageIndex">页码，从0开始</param>
        /// <param name="pageSize"></param>
        /// <returns>记录索引</returns>
        public static int GetSkipRecordIndex(this int pageIndex, int pageSize) => pageIndex * pageSize;
    }
}
