using EWI_System.Common;
using EWI_System.Model.Enties;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EWI_System.Service
{
    public class BaseService : IBaseService
    {

        private readonly ISqlSugarClient dbconn;

        public BaseService(ISqlSugarClient dbconn)
        {
            this.dbconn = dbconn.AsTenant().GetConnectionScope(0);
        }
        #region Query

        /// <summary>
        /// 主键查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public T Find<T>(int id) where T : class
        {
            return dbconn.Queryable<T>().InSingle(id);
        }

        /// <summary>
        /// 主键查询-异步版本
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<T> FindAsync<T>(int id) where T : class
        {
            return await dbconn.Queryable<T>().InSingleAsync(id);
        }

        /// <summary>
        /// 不应该暴露给上端使用者，尽量少用
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        [Obsolete("尽量避免使用，using 带表达式目录树的代替")]
        public ISugarQueryable<T> Set<T>() where T : class
        {
            return dbconn.Queryable<T>();
        }

        /// <summary>
        /// 条件查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="funcWhere"></param>
        /// <returns></returns>
        public ISugarQueryable<T> Query<T>(Expression<Func<T, bool>> funcWhere) where T : class
        {
            return dbconn.Queryable<T>().Where(funcWhere);
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="funcWhere"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="funcOrderby"></param>
        /// <param name="isAsc"></param>
        /// <returns></returns>
        public PagingData<T> QueryPage<T>(Expression<Func<T, bool>> funcWhere, int pageSize, int pageIndex, Expression<Func<T, object>> funcOrderby, bool isAsc = true) where T : class
        {
            var list = dbconn.Queryable<T>();
            if (funcWhere != null)
            {
                list = list.Where(funcWhere);
            }
            list = list.OrderByIF(true, funcOrderby, isAsc ? OrderByType.Asc : OrderByType.Desc);
            PagingData<T> result = new PagingData<T>()
            {
                DataList = list.ToPageList(pageIndex, pageSize),
                PageIndex = pageIndex,
                PageSize = pageSize,
                RecordCount = list.Count(),
            };
            return result;
        }
        #endregion

        #region Insert

        /// <summary>
        /// 新增数据-同步版本
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public T Insert<T>(T t) where T : class, new()
        {
            return dbconn.Insertable(t).ExecuteReturnEntity();
        }

        /// <summary>
        /// 新增数据-异步版本
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public async Task<T> InsertAsync<T>(T t) where T : class, new()
        {
            return await dbconn.Insertable(t).ExecuteReturnEntityAsync();
        }

        /// <summary>
        /// 批量新增-事务执行
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="tList"></param>
        /// <returns></returns>
        public async Task<bool> InsertList<T>(List<T> tList) where T : class, new()
        {
            return await dbconn.Insertable(tList.ToList()).ExecuteCommandIdentityIntoEntityAsync();
        }
        #endregion

        #region Update
        /// <summary>
        /// 是没有实现查询，直接更新的,需要Attach和State
        /// 
        /// 如果是已经在context，只能再封装一个(在具体的service)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        public async Task<bool> UpdateAsync<T>(T t) where T : class, new()
        {
            if (t == null) throw new Exception("t is null");

            return await dbconn.Updateable(t).ExecuteCommandHasChangeAsync();
        }

        public void Update<T>(List<T> tList) where T : class, new()
        {
            dbconn.Updateable(tList).ExecuteCommand();
        }

        #endregion

        #region Delete
        /// <summary>
        /// 先附加 再删除
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        public void Delete<T>(T t) where T : class, new()
        {
            dbconn.Deleteable(t).ExecuteCommand();
        }

        /// <summary>
        /// 根据主键删除
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="pId"></param>
        /// <returns></returns>
        public bool Delete<T>(object pId) where T : class, new()
        {
            T t = dbconn.Queryable<T>().InSingle(pId);
            return dbconn.Deleteable(t).ExecuteCommand() > 0;
        }

        public void Delete<T>(List<T> tList) where T : class
        {
            dbconn.Deleteable(tList).ExecuteCommand();
        }
        #endregion


        #region Other 
        ISugarQueryable<T> IBaseService.ExcuteQuery<T>(string sql) where T : class
        {
            return dbconn.SqlQueryable<T>(sql);
        }
        public void Dispose()
        {
            if (dbconn != null)
            {
                dbconn.Dispose();
            }
        }
        #endregion

    }
}
