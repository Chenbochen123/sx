/************************************************************************************
 *      Copyright (C) 2012 mesnac.com,All Rights Reserved
 *      File:
 *				BaseManager.cs
 *      Description:
 *				 业务逻辑抽象基类
 *      Author:
 *				郑立兵
 *				zhenglb@mesnac.com
 *				http://www.mesnac.com
 *      Finish DateTime:
 *				2013年01月28日
 *      History:
 *      
 ***********************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using NBear.Common;
using NBear.Data;

namespace Mesnac.Business
{
    using Mesnac.Data.Components;
    using Mesnac.Data;
    public abstract class BaseManager<T> : IBaseManager<T> where T : NBear.Common.Entity, new()
    {
        private IBaseService<T> baseService;

        public IBaseService<T> BaseService
        {
            set { baseService = value; }
        }

        #region 封装数据访问层的常规数据访问方法
		/// <summary>
        /// Type类型向DBType类型转换
        /// </summary>
        /// <param name="t">Type类型</param>
        /// <returns>返回DBType</returns>
        public DbType TypeToDbType(Type t)
        {
            return this.baseService.TypeToDbType(t);
        }
		/// <summary>
        /// 格式化字段列表
        /// </summary>
        /// <param name="columnNames">字段列表</param>
        /// <returns>返回与数据库类型无关的字段列表</returns>
        public string BuilderColumnNames(string columnNames)
        {
            return this.baseService.BuilderColumnNames(columnNames);
        }
        /// <summary>
        /// 获取数据库无关列
        /// </summary>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public string BuilderDbColumnName(string columnName)
        {
            return this.baseService.BuilderDbColumnName(columnName);
        }
        /// <summary>
        /// 获取数据库无关参数名
        /// </summary>
        /// <param name="paramName"></param>
        /// <returns></returns>
        public string BuildDbParamName(string paramName)
        {
            return this.baseService.BuildDbParamName(paramName);
        }
        /// <summary>
        /// 按住键或标识列查找，只有是单字段主键（非组合键）时才按主键查找
        /// </summary>
        /// <param name="ida">对应查找记录的主键值或标识值</param>
        /// <returns>返回对应记录的实体信息</returns>
        public T GetById(params object[] ids)
        {
            return this.baseService.GetById(ids);
        }
        /// <summary>
        /// 指定条件的查询
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <returns>返回实体类的集合</returns>
        public EntityArrayList<T> GetListByWhere(WhereClip where)
        {
            return this.baseService.GetListByWhere(where);
        }
        /// <summary>
        /// 指定条件和排序的查询
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <param name="order">排序字段</param>
        /// <returns>返回实体类的集合</returns>
        public EntityArrayList<T> GetListByWhereAndOrder(WhereClip where, OrderByClip order)
        {
            return this.baseService.GetListByWhereAndOrder(where, order);
        }
        /// <summary>
        /// 指定条件的查询
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <returns>返回数据集</returns>
        public DataSet GetDataSetByWhere(string where)
        {
            return this.baseService.GetDataSetByWhere(where);
        }
        /// <summary>
        /// 指定返回字段和参数的查询
        /// </summary>
        /// <param name="returnFields">查询结果中应包含的字段,*代表所有字段</param>
        /// <param name="values">查询参数列表，KeyValuePair的Key是字段名,KeyValuePair的Value是字段值</param>
        /// <returns>返回数据集</returns>
        public DataSet GetDataSetByFieldsAndParams(string returnFields, params KeyValuePair<string, object>[] values)
        {
            return this.baseService.GetDataSetByFieldsAndParams(returnFields, values);
        }
        /// <summary>
        /// 指定返回字段和参数的查询
        /// </summary>
        /// <param name="returnFields">查询结果中应包含的字段,*代表所有字段</param>
        /// <param name="values">查询参数列表</param>
        /// <returns>返回数据集</returns>
        public DataSet GetDataSetByFieldAndParams(string returnFields, Dictionary<string, object> values)
        {
            return this.baseService.GetDataSetByFieldAndParams(returnFields, values);
        }
        /// <summary>
        /// 指定返回字段和条件的查询
        /// </summary>
        /// <param name="returnFields">查询结果中应包含的字段,*代表所有字段</param>
        /// <param name="where">查询条件</param>
        /// <returns>返回数据集</returns>
        public DataSet GetDataSetByFieldsAndWhere(string returnFields, string where)
        {
            return this.baseService.GetDataSetByFieldsAndWhere(returnFields, where);
        }
        /// <summary>
        /// 查找表中所有记录
        /// </summary>
        /// <returns>返回对应表的实体类的集合</returns>
        public EntityArrayList<T> GetAllList()
        {
            return this.baseService.GetAllList();
        }
        /// <summary>
        /// 查找表中的记录并排序
        /// </summary>
        /// <param name="order">排序字段</param>
        /// <returns>返回对应表的实体类的集合</returns>
        public EntityArrayList<T> GetAllListOrder(OrderByClip order)
        {
            return this.baseService.GetAllListOrder(order);
        }
        /// <summary>
        /// 返回指定排序的前N条记录
        /// </summary>
        /// <param name="n">返回结果中的最大记录数</param>
        /// <param name="order">排序字段</param>
        /// <returns>返回实体类的集合</returns>
        public EntityArrayList<T> GetTopNListOrder(int n, OrderByClip order)
        {
            return this.baseService.GetTopNListOrder(n, order);
        }
        /// <summary>
        /// 返回指定条件和排序的前N条记录
        /// </summary>
        /// <param name="n">返回结果中的最大记录数</param>
        /// <param name="where">筛选条件</param>
        /// <param name="order">排序字段</param>
        /// <returns>返回实体类的集合</returns>
        public EntityArrayList<T> GetTopNListWhereOrder(int n, WhereClip where, OrderByClip order)
        {
            return this.baseService.GetTopNListWhereOrder(n, where, order);
        }
        /// <summary>
        /// 查找表中所有记录
        /// </summary>
        /// <returns>返回对应的数据集</returns>
        public DataSet GetAllDataSet()
        {
            return this.baseService.GetAllDataSet();
        }
        /// <summary>
        /// 调用存储过程的分页查询方法
        /// </summary>
        /// <param name="pageResult">用于传递查询条件的分页类的对象</param>
        /// <returns>返回封装了页面数据和总记录数据的分页类对象</returns>
        public PageResult<T> GetPageData(PageResult<T> pageResult)
        {
            return this.baseService.GetPageData(pageResult);
        }
		/// <summary>
        /// 调用基于SQL的存储过程的分页查询方法
        /// </summary>
        /// <param name="pageResult">用于传递查询条件的分页类的对象</param>
        /// <returns>返回封装了页面数据和总记录数据的分页类对象,dataSet,recordCount</returns>
        public PageResult<T> GetPageDataBySql(PageResult<T> pageResult)
        {
            return this.baseService.GetPageDataBySql(pageResult);
        }
        /// <summary>
        /// NBear的默认分页查询方法，支持单字段主键表的分页查询
        /// </summary>
        /// <param name="pageResult">用于传递查询条件的分页类的对象</param>
        /// <returns>返回封装了页面数据和总记录数据的分页类对象</returns>
        public PageResult<T> GetPageDataByNBear(PageResult<T> pageResult)
        {
            return this.baseService.GetPageDataByNBear(pageResult);
        }
        /// <summary>
        /// 调用存储过程的分页查询方法
        /// </summary>
        /// <param name="pageResult">用于传递查询条件的分页类的对象</param>
        /// <returns>返回封装了页面数据和总记录数据的分页类对象</returns>
        public PageResult<T> GetPageDataByReader(PageResult<T> pageResult)
        {
            return this.baseService.GetPageDataByReader(pageResult);
        }
        /// <summary>
        /// 分页查询方法，基于分页存储过程
        /// </summary>
        /// <param name="pageResult">用于传递查询条件的分页类的对象</param>
        /// <returns>返回封装了页面数据和总页数、总记录数的结果集的数据集</returns>
        public DataSet GetPageDataSet(PageResult<T> pageResult)
        {
            return this.baseService.GetPageDataSet(pageResult);
        }
        /// <summary>
        /// 执行存储过程的方法
        /// </summary>
        /// <param name="storeProcedureName">存储过程的名称</param>
        /// <param name="values">存储过程的参数</param>
        /// <returns>返回存储过程执行后对应的数据集</returns>
        public DataSet GetDataSetByStoreProcedure(string storeProcedureName, params KeyValuePair<string, object>[] values)
        {
            return this.baseService.GetDataSetByStoreProcedure(storeProcedureName, values);
        }
        /// <summary>
        /// 执行存储过程的方法
        /// </summary>
        /// <param name="storeProcedureName">存储过程的名称</param>
        /// <param name="values">存储过程的参数</param>
        /// <returns>返回存储过程执行后对应的数据集</returns>
        public DataSet GetDataSetByStoreProcedure(string storeProcedureName, Dictionary<string, object> values)
        {
            return this.baseService.GetDataSetByStoreProcedure(storeProcedureName, values);
        }
        /// <summary>
        /// 执行自定义SQL语句
        /// </summary>
        /// <param name="sql">要执行的SQL语句</param>
        /// <returns>返回NBear自定义语句块</returns>
        public CustomSqlSection GetBySql(string sql)
        {
            return this.baseService.GetBySql(sql);
        }
        /// <summary>
        /// 获取表中的总记录数
        /// </summary>
        /// <returns>返回总记录数</returns>
        public int GetRowCount()
        {
            return this.baseService.GetRowCount();
        }
        /// <summary>
        /// 获取执行条件的记录数
        /// </summary>
        /// <param name="where">指定条件</param>
        /// <returns>返回记录数</returns>
        public int GetRowCountByWhere(WhereClip where)
        {
            return this.baseService.GetRowCountByWhere(where);
        }
        /// <summary>
        /// 向表中追加一条记录
        /// </summary>
        /// <param name="entity">封装记录的实体</param>
        /// <returns>如果有自增列，则返回对应的自增列的值，否则返回受影响的行数</returns>
        public int Insert(T entity)
        {
            return this.baseService.Insert(entity);
        }
        /// <summary>
        /// 向表中批量追加记录
        /// </summary>
        /// <param name="lst">封装记录的List数组</param>
        /// <returns>返回受影响的行数</returns>
        public int BatchInsert(List<T> lst)
        {
            return this.baseService.BatchInsert(lst);
        }
        /// <summary>
        /// 更新表中的一条记录
        /// </summary>
        /// <param name="entity">封装记录的实体</param>
        /// <returns>返回受影响的行数</returns>
        public int Update(T entity)
        {
            return this.baseService.Update(entity);
        }
        /// <summary>
        /// 按照条件更新字段值
        /// </summary>
        /// <param name="properties">要更新的字段集合</param>
        /// <param name="values">要更新的字段值集合</param>
        /// <param name="where">条件参数</param>
        public void Update(PropertyItem[] properties, object[] values, WhereClip where)
        {
            this.baseService.Update(properties, values, where);
        }
        /// <summary>
        /// 删除表中的一条记录
        /// </summary>
        /// <param name="id">要删除记录的主键值和标识值</param>
        /// <returns>返回受影响的行数</returns>
        public void Delete(object id)
        {
            this.baseService.Delete(id);
        }
        /// <summary>
        /// 删除对象对应的记录
        /// </summary>
        /// <param name="entity">与要删除记录对应的对象</param>
        public void Delete(T entity)
        {
            this.baseService.Delete(entity);
        }
        /// <summary>
        /// 按指定的条件删除数据
        /// </summary>
        /// <param name="where">条件</param>
        /// <returns>返回删除的记录数</returns>
        public void DeleteByWhere(WhereClip where)
        {
            this.baseService.DeleteByWhere(where);
        }
        /// <summary>
        /// 清空表中的数据
        /// </summary>
        /// <returns>返回清除的记录数</returns>
        public void ClearData()
        {
            this.baseService.ClearData();
        }


        public object GetMaxValueByProperty( PropertyItem item )
        {
            return this.baseService.GetMaxValueByProperty( item );
        }
        #endregion
    }
}
