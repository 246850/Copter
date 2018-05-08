using Copter.Result.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Copter.Result.List
{
    /// <summary>
    /// 列表集合 扩展类
    /// </summary>
    public static class PagedListExtensions
    {
        /// <summary>
        /// IPagedList列表结果 转 PagerResultOfBoolean - 泛型
        /// </summary>
        /// <typeparam name="TModel">业务实体 类型</typeparam>
        /// <param name="list">泛型IPagedList的实现集合 对象</param>
        /// <returns>PagerResultOfBoolean</returns>
        public static PagerResultOfBoolean<TModel> ToPagerResultOfBool<TModel>(this IPagedList<TModel> list) where TModel : class, new()
        {
            return new PagerResultOfBoolean<TModel>
            {
                List = list,
                Pager = list.Pager
            };
        }

        /// <summary>
        /// IPagedList列表结果 转 PagerResultOfCode - 泛型
        /// </summary>
        /// <typeparam name="TModel">业务实体 类型</typeparam>
        /// <param name="list">泛型IPagedList的实现集合 对象</param>
        /// <returns>PagerResultOfCode</returns>
        public static PagerResultOfCode<TModel> ToPagerResultOfCode<TModel>(this IPagedList<TModel> list) where TModel : class, new()
        {
            return new PagerResultOfCode<TModel>
            {
                List = list,
                Pager = list.Pager
            };
        }

        /// <summary>
        /// IPagedList列表结果 entity 转 model - 分页结果Result
        /// </summary>
        /// <typeparam name="TEntity">领域实体 类型</typeparam>
        /// <typeparam name="TModel">业务实体 类型</typeparam>
        /// <param name="source">源列表</param>
        /// <param name="list">目标列表</param>
        /// <returns>PagerResultOfBoolean</returns>
        public static PagerResultOfBoolean<TModel> ToPagerResultOfBool<TEntity, TModel>(this IPagedList<TEntity> source, IList<TModel> list) where TEntity : class, new()
        {
            return new PagerResultOfBoolean<TModel>
            {
                List = list,
                Pager = source.Pager
            };
        }

        /// <summary>
        /// IPagedList列表结果 entity 转 model - 分页结果Result
        /// </summary>
        /// <typeparam name="TEntity">领域实体 类型</typeparam>
        /// <typeparam name="TModel">业务实体 类型</typeparam>
        /// <param name="source">源列表</param>
        /// <param name="convertFunc">entity 转 model 委托</param>
        /// <returns>PagerResultOfBoolean</returns>
        public static PagerResultOfBoolean<TModel> ToPagerResultOfBool<TEntity, TModel>(this IPagedList<TEntity> source, Func<TEntity, TModel> convertFunc) where TEntity : class, new()
        {
            List<TModel> list = source.Select(convertFunc).ToList();
            return new PagerResultOfBoolean<TModel>
            {
                List = list,
                Pager = source.Pager
            };
        }

        /// <summary>
        /// IPagedList列表结果 entity 转 model - 分页结果Result
        /// </summary>
        /// <typeparam name="TEntity">领域实体 类型</typeparam>
        /// <typeparam name="TModel">业务实体 类型</typeparam>
        /// <param name="source">源列表</param>
        /// <param name="list">目标列表</param>
        /// <returns>PagerResultOfCode</returns>
        public static PagerResultOfCode<TModel> ToPagerResultOfCode<TEntity, TModel>(this IPagedList<TEntity> source, IList<TModel> list) where TEntity : class, new()
        {
            return new PagerResultOfCode<TModel>
            {
                List = list,
                Pager = source.Pager
            };
        }

        /// <summary>
        /// IPagedList列表结果 entity 转 model - 分页结果Result
        /// </summary>
        /// <typeparam name="TEntity">领域实体 类型</typeparam>
        /// <typeparam name="TModel">业务实体 类型</typeparam>
        /// <param name="source">源列表</param>
        /// <param name="convertFunc">entity 转 model 委托</param>
        /// <returns>PagerResultOfCode</returns>
        public static PagerResultOfCode<TModel> ToPagerResultOfCode<TEntity, TModel>(this IPagedList<TEntity> source, Func<TEntity, TModel> convertFunc) where TEntity : class, new()
        {
            List<TModel> list = source.Select(convertFunc).ToList();
            return new PagerResultOfCode<TModel>
            {
                List = list,
                Pager = source.Pager
            };
        }

        /// <summary>
        /// IEnumerable列表结果 转 ListResultOfBoolean - 泛型
        /// </summary>
        /// <typeparam name="TModel">业务实体 类型</typeparam>
        /// <param name="list">泛型IList的实现集合 对象</param>
        /// <returns>ListResultOfBoolean</returns>
        public static ListResultOfBoolean<TModel> ToListResultOfBool<TModel>(this IList<TModel> list) where TModel : class, new()
        {
            return new ListResultOfBoolean<TModel>
            {
                List = list
            };
        }

        /// <summary>
        /// IEnumerable列表结果 转 ListResultOfCode - 泛型
        /// </summary>
        /// <typeparam name="TModel">业务实体 类型</typeparam>
        /// <param name="list">泛型IList的实现集合 对象</param>
        /// <returns>ListResultOfCode</returns>
        public static ListResultOfCode<TModel> ToListResultOfCode<TModel>(this IList<TModel> list) where TModel : class, new()
        {
            return new ListResultOfCode<TModel>
            {
                List = list
            };
        }

        /// <summary>
        /// IPagedList列表结果 转 EasyuiPageResult列表结果 - jQuery Easyui DataGrid专用
        /// </summary>
        /// <typeparam name="TModel">业务实体 类型</typeparam>
        /// <param name="list">泛型IList的实现集合 对象</param>
        /// <returns>EasyuiPageResult列表结果</returns>
        public static EasyuiPageResult<TModel> ToEasyuiListResult<TModel>(IList<TModel> list) where TModel : class, new()
        {
            return new EasyuiPageResult<TModel>
            {
                List = list
            };
        }

        /// <summary>
        /// 分页 - Entity列表 转换 成 Model列表 - EasyuiPageResult结果
        /// </summary>
        /// <typeparam name="TEntity">领域实体 类型</typeparam>
        /// <typeparam name="TModel">业务实体 类型</typeparam>
        /// <param name="source">源列表</param>
        /// <param name="list">目标列表</param>
        /// <returns>Model EasyuiPageResult列表结果</returns>
        public static EasyuiPageResult<TModel> ToEasyuiPageResult<TEntity, TModel>(this IPagedList<TEntity> source, IList<TModel> list) where TEntity : class, new()
        {
            return new EasyuiPageResult<TModel>
            {
                Pager = source.Pager,
                List = list
            };
        }

        /// <summary>
        /// IPagedList列表结果 entity 转 model
        /// </summary>
        /// <typeparam name="TEntity">领域实体 类型</typeparam>
        /// <typeparam name="TModel">业务实体 类型</typeparam>
        /// <param name="source">entity集合 对象</param>
        /// <param name="list">model集合 对象</param>
        /// <returns>IPagedList列表Model结果集</returns>
        public static IPagedList<TModel> ToPagedList<TEntity, TModel>(this IPagedList<TEntity> source, IList<TModel> list)
            where TEntity : class, new()
            where TModel : class, new()
        {
            PagedListResult<TModel> result = new PagedListResult<TModel>(source.Pager.Page, source.Pager.PageSize, source.Pager.Total);
            result.AddRange(list);

            return result;
        }

        /// <summary>
        /// IQueryable 查询结果 转 IPagedList列表 - 通常用于数据库查询转换操作
        /// </summary>
        /// <typeparam name="TEntity">领域实体 类型</typeparam>
        /// <param name="source">IQueryable查询对象</param>
        /// <param name="page">当前页</param>
        /// <param name="pageSize">页容量大小</param>
        /// <returns>IPagedList列表结果集</returns>
        public static IPagedList<TEntity> ToPagedList<TEntity>(this IQueryable<TEntity> source, int page, int pageSize) where TEntity : class, new()
        {
            long total = source.Count();
            PagedListResult<TEntity> result = new PagedListResult<TEntity>(page, pageSize, total);
            result.AddRange(source.Skip((page - 1) * pageSize).Take(pageSize));
            return result;
        }
    }
}
