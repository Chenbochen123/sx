/************************************************************************************
 *      Copyright (C) 2012 mesnac.com,All Rights Reserved
 *      File:
 *				IBaseService.cs
 *      Description:
 *				 数据访问基础接口
 *      Author:
 *				郑立兵
 *				zhenglb@mesnac.com
 *				http://www.mesnac.com
 *      Finish DateTime:
 *				2013年01月28日
 *      History:
 ***********************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using NBear.Common;
using NBear.Data;

namespace Mesnac.Data
{
    using Mesnac.Data.Components;
    public interface IBaseService<T> where T : NBear.Common.Entity, new()
    {
        /// <summary>
        /// Type类型向DBType类型转换
        /// </summary>
        /// <param name="t">Type类型</param>
        /// <returns>返回DBType</returns>
        DbType TypeToDbType(Type t);
        /// <summary>
        /// 格式化字段列表
        /// </summary>
        /// <param name="columnNames">字段列表</param>
        /// <returns>返回与数据库类型无关的字段列表</returns>
        string BuilderColumnNames(string columnNames);
        /// <summary>
        /// 获取数据库无关列
        /// </summary>
        /// <param name="columnName">字段名称</param>
        /// <returns>返回与数据库类型无关的字段名称</returns>
        string BuilderDbColumnName(string columnName);
        /// <summary>
        /// 获取数据库无关参数名
        /// </summary>
        /// <param name="paramName">参数名称</param>
        /// <returns>返回与数据库类型无关的参数名称</returns>
        string BuildDbParamName(string paramName);
        /// <summary>
        /// 按照主键查找
        /// </summary>
        /// <param name="ids">主键参数列表</param>
        /// <returns></returns>
        T GetById(params object[] ids);
        /// <summary>
        /// 指定条件的查询
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <returns>返回实体类的集合</returns>
        EntityArrayList<T> GetListByWhere(WhereClip where);
        /// <summary>
        /// 指定条件和排序的查询
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <param name="order">排序字段</param>
        /// <returns>返回实体类的集合</returns>
        EntityArrayList<T> GetListByWhereAndOrder(WhereClip where, OrderByClip order);
        /// <summary>
        /// 指定条件的查询
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <returns>返回数据集</returns>
        DataSet GetDataSetByWhere(string where);
        /// <summary>
        /// 指定返回字段和阐述的查询
        /// </summary>
        /// <param name="returnFields">查询结果中应包含的字段,*代表所有字段</param>
        /// <param name="values"></param>
        /// <returns></returns>
        DataSet GetDataSetByFieldsAndParams(string returnFields, params KeyValuePair<string, object>[] values);
        /// <summary>
        /// 指定返回字段和参数的查询
        /// </summary>
        /// <param name="returnFields">查询结果中应包含的字段,*代表所有字段</param>
        /// <param name="values">查询参数列表</param>
        /// <returns>返回数据集</returns>
        DataSet GetDataSetByFieldAndParams(string returnFields, Dictionary<string, object> values);
        /// <summary>
        /// 指定返回字段和条件的查询
        /// </summary>
        /// <param name="returnFields">查询结果中应包含的字段,*代表所有字段</param>
        /// <param name="where">查询条件</param>
        /// <returns>返回数据集</returns>
        DataSet GetDataSetByFieldsAndWhere(string returnFields, string where);
        /// <summary>
        /// 查询所有记录并以List形式返回
        /// </summary>
        /// <returns></returns>
        EntityArrayList<T> GetAllList();
        /// <summary>
        /// 查找表中的记录并排序
        /// </summary>
        /// <param name="order">排序字段</param>
        /// <returns>返回对应表的实体类的集合</returns>
        EntityArrayList<T> GetAllListOrder(OrderByClip order);
        /// <summary>
        /// 返回指定排序的前N条记录
        /// </summary>
        /// <param name="n">返回结果中的记录数</param>
        /// <param name="order">排序字段</param>
        /// <returns>返回实体类的集合</returns>
        EntityArrayList<T> GetTopNListOrder(int n, OrderByClip order);
        /// <summary>
        /// 返回指定条件和排序的前N条记录
        /// </summary>
        /// <param name="n">返回结果中的最大记录数</param>
        /// <param name="where">筛选条件</param>
        /// <param name="order">排序字段</param>
        /// <returns>返回实体类的集合</returns>
        EntityArrayList<T> GetTopNListWhereOrder(int n, WhereClip where, OrderByClip order);
        /// <summary>
        /// 查询所有记录并以DataSet方式返回数据
        /// </summary>
        /// <returns></returns>
        DataSet GetAllDataSet();
        /// <summary>
        /// 调用存储过程的分页查询方法
        /// </summary>
        /// <param name="pageResult">用于传递查询条件的分页类的对象</param>
        /// <returns>返回封装了页面数据和总记录数据的分页类对象</returns>
        PageResult<T> GetPageData(PageResult<T> pageResult);
		/// <summary>
        /// 调用基于SQL的存储过程的分页查询方法
        /// </summary>
        /// <param name="pageResult">用于传递查询条件的分页类的对象</param>
        /// <returns>返回封装了页面数据和总记录数据的分页类对象,dataSet,recordCount</returns>
        PageResult<T> GetPageDataBySql(PageResult<T> pageResult);
        /// <summary>
        /// NBear的默认分页查询方法，支持单字段主键表的分页查询
        /// </summary>
        /// <param name="pageResult">用于传递查询条件的分页类的对象</param>
        /// <returns>返回封装了页面数据和总记录数据的分页类对象</returns>
        PageResult<T> GetPageDataByNBear(PageResult<T> pageResult);
        /// <summary>
        /// 调用存储过程的分页查询方法
        /// </summary>
        /// <param name="pageResult">用于传递查询条件的分页类的对象</param>
        /// <returns>返回封装了页面数据和总记录数据的分页类对象</returns>
        PageResult<T> GetPageDataByReader(PageResult<T> pageResult);
        /// <summary>
        /// 分页查询方法，基于分页存储过程
        /// </summary>
        /// <param name="pageResult">用于传递查询条件的分页类的对象</param>
        /// <returns>返回封装了页面数据和总页数、总记录数的结果集的数据集</returns>
        DataSet GetPageDataSet(PageResult<T> pageResult);
        /// <summary>
        /// 执行存储过程的方法
        /// </summary>
        /// <param name="storeProcedureName">存储过程的名称</param>
        /// <param name="values">存储过程的参数</param>
        /// <returns>返回存储过程执行后对应的数据集</returns>
        DataSet GetDataSetByStoreProcedure(string storeProcedureName, params KeyValuePair<string, object>[] values);
        /// <summary>
        /// 执行存储过程的方法
        /// </summary>
        /// <param name="storeProcedureName">存储过程的名称</param>
        /// <param name="values">存储过程的参数</param>
        /// <returns>返回存储过程执行后对应的数据集</returns>
        DataSet GetDataSetByStoreProcedure(string storeProcedureName, Dictionary<string, object> values);
        /// <summary>
        /// 执行自定义SQL语句
        /// </summary>
        /// <param name="sql">要执行的SQL语句</param>
        /// <returns>返回NBear自定义语句块</returns>
        CustomSqlSection GetBySql(string sql);
        /// <summary>
        /// 获取表中的总记录数
        /// </summary>
        /// <returns>返回总记录数</returns>
        int GetRowCount();
        /// <summary>
        /// 获取执行条件的记录数
        /// </summary>
        /// <param name="where">指定条件</param>
        /// <returns>返回记录数</returns>
        int GetRowCountByWhere(WhereClip where);
        /// <summary>
        /// 添加新记录
        /// </summary>
        /// <param name="entity">对应新记录的实体数据</param>
        /// <returns>返回追加记录的主键值</returns>
        int Insert(T entity);
        /// <summary>
        /// 批量添加新纪录
        /// </summary>
        /// <param name="lst">对应的List记录</param>
        /// <returns>返回受影响的记录行数</returns>
        int BatchInsert(List<T> lst);
        /// <summary>
        /// 更新记录
        /// </summary>
        /// <param name="entity">需要更新记录对应的实体数据</param>
        /// <returns>返回更新的记录数</returns>
        int Update(T entity);
        /// <summary>
        /// 按照条件更新字段值
        /// </summary>
        /// <param name="properties">要更新的字段集合</param>
        /// <param name="values">要更新的字段值集合</param>
        /// <param name="where">条件参数</param>
        void Update(PropertyItem[] properties, object[] values, WhereClip where);
        /// <summary>
        /// 删除主键是id值得记录
        /// </summary>
        /// <param name="id">要删除记录的主键值</param>
        /// <returns>返回删除的记录条数</returns>
        void Delete(object id);
        /// <summary>
        /// 删除对象对应的记录
        /// </summary>
        /// <param name="entity">与要删除记录对应的对象</param>
        void Delete(T entity);
        /// <summary>
        /// 按指定的条件删除数据
        /// </summary>
        /// <param name="where">条件</param>
        /// <returns>返回删除的记录数</returns>
        void DeleteByWhere(WhereClip where);
        /// <summary>
        /// 清除表中所有记录
        /// </summary>
        void ClearData();

        object GetMaxValueByProperty( PropertyItem item );
    }
}
