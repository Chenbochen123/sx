/************************************************************************************
 *      Copyright (C) 2012 mesnac.com,All Rights Reserved
 *      File:
 *				BaseService.cs
 *      Description:
 *				 基于泛型数据访问抽象基类
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
using NBear.Data;
using NBear.Common;

namespace Mesnac.Data
{
    using Mesnac.Data.Components;
    /// <summary>
    /// 基于泛型数据访问抽象基类，封装了基本数据访问操作CRUD
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class BaseService<T> : IBaseService<T> where T : NBear.Common.Entity, new()
    {
        #region 私有字段
        private readonly string procedureName = "MesnacPaging";   //分页存储过程名 -for sql2000/2005/2008
        private readonly string pagerProductName = "PagerShow";      //基于SQL语句分页的存储过程名--for sql2005/2008
        protected string tableName = String.Empty;         //对应泛型的表名
        protected T _instance = null;

        #endregion

        #region Gateway
        private Mesnac.Entity.Custom.CdbVersion GetDefaultCdbVersion()
        {
            var lst = XmlDbConfigHandler.GetInstance().GetDbVersions();
            var minKey = 1000;
            foreach (int key in lst.Keys)
            {
                if (key < minKey)
                {
                    minKey = key;
                }
            }
            var dbVersion = XmlDbConfigHandler.GetInstance().GetDbVersions()[minKey];
            return dbVersion;
        }
        private Mesnac.Entity.Custom.CdbVersion GetSessionCdbVersion()
        {
            Mesnac.Entity.Custom.CdbVersion Result = null;
            try
            {
                if (System.Web.HttpContext.Current.Session["dbVersion"] is Mesnac.Entity.Custom.CdbVersion)
                {
                    Result = System.Web.HttpContext.Current.Session["dbVersion"] as Mesnac.Entity.Custom.CdbVersion;
                }
            }
            catch { }
            return Result;
        }

        private Mesnac.Entity.Custom.CdbVersion GetCdbVersion()
        {
            Mesnac.Entity.Custom.CdbVersion Result = null;
            Result = GetSessionCdbVersion();
            if (Result==null)
            {
                Result = GetDefaultCdbVersion();
            }
            return Result;
        }
        private string connectStringKey = string.Empty;
        private Gateway _defaultGateway = null;           //当前数据库
        protected Gateway defaultGateway
        {
            get
            {
                if (_defaultGateway == null)
                {
                    string key = connectStringKey;
                    Mesnac.Entity.Custom.CdbVersion dbVersion = GetCdbVersion();
                    if (dbVersion != null)
                    {
                        if (string.IsNullOrWhiteSpace(key))
                        {
                            key = "Default";
                        }
                        Mesnac.Entity.Custom.CdbDatabase db = dbVersion.Databases[key];
                        _defaultGateway = new NBear.Data.Gateway(db.AssemblyName, db.ClassName, db.ConnStr);
                    }
                    else
                    {
                        if (string.IsNullOrWhiteSpace(key))
                        {
                            _defaultGateway = Gateway.Default;
                        }
                        else
                        {
                            _defaultGateway = new Gateway(key);
                        }
                    }
                }
                //this.tableName = this._defaultGateway.BuildDbColumnName(new T().GetEntityConfiguration().MappingName);
                this.SetTableName();
                return _defaultGateway;
            }
        }
        #endregion

        private void SetTableName()
        {
            try
            {
                if (this._instance == null)
                {
                    this._instance = new T();
                }
                this.tableName = this._instance.GetEntityConfiguration().MappingName;
            }
            catch (Exception ex)
            {
                this.tableName = typeof(T).Name;
                //this.tableName = String.Empty;
                Console.WriteLine(ex.Message);
            }
        }

        #region 默认构造方法

        public BaseService()
        {
            //if (this.defaultGateway == null) this.defaultGateway = Gateway.Default;
            //this.tableName = new T().GetEntityConfiguration().MappingName;
            this.SetTableName();
        }

        public BaseService(string connectStringKey)
        {
            this.connectStringKey = connectStringKey;
            //this.defaultGateway = new Gateway(connectStringKey);
            //this.tableName = new T().GetEntityConfiguration().MappingName;
            this.SetTableName();
        }

        public BaseService(Gateway way)
        {
            this._defaultGateway = way;
            //this.tableName = new T().GetEntityConfiguration().MappingName;
            this.SetTableName();
        }

        #endregion

        #region IBaseService<T> 成员

        /// <summary>
        /// Type类型向DBType类型转换
        /// </summary>
        /// <param name="t">Type类型</param>
        /// <returns>返回DBType</returns>
        public DbType TypeToDbType(Type t)
        {
            DbType dbt;
            try
            {
                dbt = (DbType)Enum.Parse(typeof(DbType), t.Name);
            }
            catch
            {
                dbt = DbType.Object;
            }
            return dbt;
        }

        /// <summary>
        /// 格式化字段列表
        /// </summary>
        /// <param name="columnNames">字段列表</param>
        /// <returns>返回与数据库类型无关的字段列表</returns>
        public string BuilderColumnNames(string columnNames)
        {
            if (!String.IsNullOrEmpty(columnNames))
            {
                string[] columns = columnNames.Split(new char[] { ',' });
                StringBuilder sb = new StringBuilder();
                foreach (string col in columns)
                {
                    if (String.IsNullOrEmpty(sb.ToString()))
                    {
                        sb.Append(this.defaultGateway.BuildDbColumnName(col));
                    }
                    else
                    {
                        sb.Append(",");
                        sb.Append(this.defaultGateway.BuildDbColumnName(col));
                    }
                }
                return sb.ToString();
            }
            else
            {
                return String.Empty;
            }
        }

        /// <summary>
        /// 获取数据库无关列
        /// </summary>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public string BuilderDbColumnName(string columnName)
        {
            return this.defaultGateway.BuildDbColumnName(columnName);
        }

        /// <summary>
        /// 获取数据库无关参数名
        /// </summary>
        /// <param name="paramName"></param>
        /// <returns></returns>
        public string BuildDbParamName(string paramName)
        {
            return this.defaultGateway.BuildDbParamName(paramName);
        }

        /// <summary>
        /// 按照主键查找
        /// </summary>
        /// <param name="ids">主键参数列表</param>
        /// <returns></returns>
        public T GetById(params object[] ids)
        {
            return this.defaultGateway.Find<T>(ids);
        }
        /// <summary>
        /// 指定条件的查询
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <returns>返回实体类的集合</returns>
        public EntityArrayList<T> GetListByWhere(WhereClip where)
        {
            return this.defaultGateway.From<T>().Where(where).ToArrayList<T>();
        }
        /// <summary>
        /// 指定条件和排序的查询
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <param name="order">排序字段</param>
        /// <returns>返回实体类的集合</returns>
        public EntityArrayList<T> GetListByWhereAndOrder(WhereClip where, OrderByClip order)
        {
            return this.defaultGateway.From<T>().Where(where).OrderBy(order).ToArrayList<T>();
        }
        /// <summary>
        /// 指定条件的查询
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <returns>返回数据集</returns>
        public DataSet GetDataSetByWhere(string where)
        {
            string sql = "select * from {0}{1}";
            if (String.IsNullOrEmpty(where))
            {
                sql = String.Format(sql, this.tableName, String.Empty);
            }
            else
            {
                sql = String.Format(sql, this.tableName, " " + where);
            }
            return this.defaultGateway.FromCustomSql(sql).ToDataSet();
        }
        /// <summary>
        /// 指定返回字段和阐述的查询
        /// </summary>
        /// <param name="returnFields">查询结果中应包含的字段,*代表所有字段</param>
        /// <param name="values"></param>
        /// <returns></returns>
        public DataSet GetDataSetByFieldsAndParams(string returnFields, params KeyValuePair<string, object>[] values)
        {
            string sql = "select {0} from {1} {2}";
            string where = String.Empty;
            returnFields = String.IsNullOrEmpty(returnFields) ? "*" : this.BuilderColumnNames(returnFields);
            foreach (KeyValuePair<string, object> kvp in values)
            {
                where = String.IsNullOrEmpty(where) ? "where " + this.defaultGateway.BuildDbColumnName(kvp.Key) + "=" + this.defaultGateway.BuildDbParamName(kvp.Key) : where + " and " + this.defaultGateway.BuildDbColumnName(kvp.Key) + "=" + this.defaultGateway.BuildDbParamName(kvp.Key);
            }
            sql = String.Format(sql, returnFields, this.tableName, where);
            CustomSqlSection css = this.defaultGateway.FromCustomSql(sql);
            foreach (KeyValuePair<string, object> kvp in values)
            {
                css.AddInputParameter(this.defaultGateway.BuildDbParamName(kvp.Key), this.TypeToDbType(kvp.Value.GetType()), kvp.Value);
            }
            return css.ToDataSet();
        }
        /// <summary>
        /// 指定返回字段和参数的查询
        /// </summary>
        /// <param name="returnFields">查询结果中应包含的字段,*代表所有字段</param>
        /// <param name="values">查询参数列表</param>
        /// <returns>返回数据集</returns>
        public DataSet GetDataSetByFieldAndParams(string returnFields, Dictionary<string, object> values)
        {
            string sql = "select {0} from {1} {2}";
            string where = String.Empty;
            returnFields = String.IsNullOrEmpty(returnFields) ? "*" : this.BuilderColumnNames(returnFields);
            foreach (KeyValuePair<string, object> kvp in values)
            {
                where = String.IsNullOrEmpty(where) ? "where " + this.defaultGateway.BuildDbColumnName(kvp.Key) + "=" + this.defaultGateway.BuildDbParamName(kvp.Key) : where + " and " + this.defaultGateway.BuildDbColumnName(kvp.Key) + "=" + this.defaultGateway.BuildDbParamName(kvp.Key);
            }
            sql = String.Format(sql, returnFields, this.tableName, where);
            CustomSqlSection css = this.defaultGateway.FromCustomSql(sql);
            foreach (KeyValuePair<string, object> kvp in values)
            {
                css.AddInputParameter(this.defaultGateway.BuildDbParamName(kvp.Key), this.TypeToDbType(kvp.Value.GetType()), kvp.Value);
            }
            return css.ToDataSet();
        }
        /// <summary>
        /// 指定返回字段和条件的查询
        /// </summary>
        /// <param name="returnFields">查询结果中应包含的字段,*代表所有字段</param>
        /// <param name="where">查询条件</param>
        /// <returns>返回数据集</returns>
        public DataSet GetDataSetByFieldsAndWhere(string returnFields, string where)
        {
            string sql = "select {0} from {1} {2}";
            where = String.IsNullOrEmpty(where) ? String.Empty : where;
            returnFields = String.IsNullOrEmpty(returnFields) ? "*" : this.BuilderColumnNames(returnFields);
            sql = String.Format(sql, returnFields, this.tableName, where);
            CustomSqlSection css = this.defaultGateway.FromCustomSql(sql);
            return css.ToDataSet();
        }
        /// <summary>
        /// 查询所有记录并以List形式返回
        /// </summary>
        /// <returns></returns>
        public EntityArrayList<T> GetAllList()
        {
            return this.defaultGateway.From<T>().ToArrayList<T>();
        }
        /// <summary>
        /// 查找表中的记录并排序
        /// </summary>
        /// <param name="order">排序字段</param>
        /// <returns>返回对应表的实体类的集合</returns>
        public EntityArrayList<T> GetAllListOrder(OrderByClip order)
        {
            return this.defaultGateway.From<T>().OrderBy(order).ToArrayList<T>();
        }
        /// <summary>
        /// 返回指定排序的前N条记录
        /// </summary>
        /// <param name="n">返回结果中的记录数</param>
        /// <param name="order">排序字段</param>
        /// <returns>返回实体类的集合</returns>
        public EntityArrayList<T> GetTopNListOrder(int n, OrderByClip order)
        {
            return this.defaultGateway.From<T>().OrderBy(order).ToArrayList<T>(n);
        }
        /// <summary>
        /// 返回指定条件和排序的前N条记录
        /// </summary>
        /// <param name="n">返回结果中的最大记录数</param>
        /// <param name="where">筛选条件</param>
        /// <param name="order">排序字段</param>
        /// <returns>返回实体类的集合</returns>
        public EntityArrayList<T> GetTopNListWhereOrder(int n, WhereClip wc, OrderByClip order)
        {
            return this.defaultGateway.From<T>().Where(wc).OrderBy(order).ToArrayList<T>(n);
        }
        /// <summary>
        /// 查询所有记录并以DataSet方式返回数据
        /// </summary>
        /// <returns></returns>
        public DataSet GetAllDataSet()
        {
            return this.defaultGateway.From<T>().ToDataSet();
        }
        /// <summary>
        /// 调用存储过程的分页查询方法
        /// </summary>
        /// <param name="pageResult">用于传递查询条件的分页类的对象</param>
        /// <returns>返回封装了页面数据和总记录数据的分页类对象</returns>
        public PageResult<T> GetPageData(PageResult<T> pageResult)
        {
            pageResult.Data.Clear();
            string[] paramNames ={
                this.defaultGateway.BuildDbParamName("TableName"),
                this.defaultGateway.BuildDbParamName("ReturnFields"),
                this.defaultGateway.BuildDbParamName("PageSize"),
                this.defaultGateway.BuildDbParamName("PageIndex"),
                this.defaultGateway.BuildDbParamName("Where"),
                this.defaultGateway.BuildDbParamName("Orderfld"),
                this.defaultGateway.BuildDbParamName("OrderType")
            };
            object[] paramValues = {
                pageResult.TableName,
                pageResult.ReturnFields,
                pageResult.PageSize,
                pageResult.PageIndex,
                pageResult.Where,
                pageResult.Orderfld,
                pageResult.OrderType
            };

            //pageResult.RecordCount = this.defaultGateway.Count<T>(pageResult.Where);
            StoredProcedureSection sps = this.defaultGateway.FromStoredProcedure(this.procedureName);
            for (int i = 0; i < paramNames.Length; i++)
            {
                sps.AddInputParameter(paramNames[i], this.TypeToDbType(paramValues[i].GetType()), paramValues[i]);
            }
            using (IDataReader reader = sps.ToDataReader())
            {
                while (reader.Read())
                {
                    T entity = new T();
                    entity.SetPropertyValues(reader);
                    pageResult.Data.Add(entity);
                }
                if (reader.NextResult())
                {
                    if (reader.Read())
                    {
                        pageResult.RecordCount = Convert.ToInt32(reader["RecordCount"]);
                    }
                }
                reader.Close();
            }
            return pageResult;
        }
        /// <summary>
        /// 调用基于SQL的存储过程的分页查询方法
        /// </summary>
        /// <param name="pageResult">用于传递查询条件的分页类的对象</param>
        /// <returns>返回封装了页面数据和总记录数据的分页类对象,dataSet,recordCount</returns>
        public PageResult<T> GetPageDataBySql(PageResult<T> pageResult)
        {
            string[] paramNames ={
                Gateway.Default.BuildDbParamName("QueryStr"),
                Gateway.Default.BuildDbParamName("PageSize"),
                Gateway.Default.BuildDbParamName("PageCurrent"),
                Gateway.Default.BuildDbParamName("FdShow"),
                Gateway.Default.BuildDbParamName("FdOrder")
            };
            object[] paramValues = {
                pageResult.QueryStr,
                pageResult.PageSize,
                pageResult.PageIndex,
                pageResult.ReturnFields,
                pageResult.Orderfld
            };

            StoredProcedureSection sps = this.defaultGateway.FromStoredProcedure(this.pagerProductName);
            for (int i = 0; i < paramNames.Length; i++)
            {
                sps.AddInputParameter(paramNames[i], this.TypeToDbType(paramValues[i].GetType()), paramValues[i]);
            }
            sps.AddOutputParameter(this.defaultGateway.BuildDbParamName("Rows"), DbType.Int32, 4);
            Dictionary<string, object> outValues = new Dictionary<string, object>();
            pageResult.DataSet = sps.ToDataSet(out outValues);
            pageResult.RecordCount = Convert.ToInt32(outValues["Rows"]);
            return pageResult;
        }
        /// <summary>
        /// NBear的默认分页查询方法，支持单字段主键表的分页查询
        /// </summary>
        /// <param name="pageResult">用于传递查询条件的分页类的对象</param>
        /// <returns>返回封装了页面数据和总记录数据的分页类对象</returns>
        public PageResult<T> GetPageDataByNBear(PageResult<T> pageResult)
        {
            pageResult.Data.Clear();
            pageResult.RecordCount = this.defaultGateway.Count<T>(pageResult.Wc);
            T[] results = this.defaultGateway.From<T>().Where(pageResult.Wc).OrderBy(pageResult.Obc).ToArray<T>(pageResult.PageSize, (pageResult.PageIndex - 1) * pageResult.PageSize);
            pageResult.Data.AddRange(results);
            return pageResult;
        }
        /// <summary>
        /// 调用存储过程的分页查询方法
        /// </summary>
        /// <param name="pageResult">用于传递查询条件的分页类的对象</param>
        /// <returns>返回封装了页面数据和总记录数据的分页类对象</returns>
        public PageResult<T> GetPageDataByReader(PageResult<T> pageResult)
        {
            using (IDataReader reader = this.defaultGateway.FromCustomSql(pageResult.QueryStr).ToDataReader())
            {
                int begin = pageResult.PageSize * (pageResult.PageIndex - 1) + 1;
                int count = 0;
                int pageSize = 0;
                DataTable table = new DataTable();
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    DataColumn col = new DataColumn(reader.GetName(i), reader.GetFieldType(i));
                    table.Columns.Add(col);
                }
                while (reader.Read())
                {
                    count++;
                    if (count >= begin && pageSize < pageResult.PageSize)
                    {
                        DataRow row = table.NewRow();
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            row[i] = reader[i];
                        }
                        table.Rows.Add(row);
                        pageSize++;
                    }
                }
                reader.Close();
                DataSet ds = new DataSet();
                ds.Tables.Add(table);
                pageResult.DataSet = ds;
                pageResult.RecordCount = count;
                return pageResult;
            }
        }
        /// <summary>
        /// 分页查询方法，基于分页存储过程
        /// </summary>
        /// <param name="pageResult">用于传递查询条件的分页类的对象</param>
        /// <returns>返回封装了页面数据和总页数、总记录数的结果集的数据集</returns>
        public DataSet GetPageDataSet(PageResult<T> pageResult)
        {
            string[] paramNames ={
                this.defaultGateway.BuildDbParamName("TableName"),
                this.defaultGateway.BuildDbParamName("ReturnFields"),
                this.defaultGateway.BuildDbParamName("PageSize"),
                this.defaultGateway.BuildDbParamName("PageIndex"),
                this.defaultGateway.BuildDbParamName("Where"),
                this.defaultGateway.BuildDbParamName("Orderfld"),
                this.defaultGateway.BuildDbParamName("OrderType")
            };
            object[] paramValues = {
                pageResult.TableName,
                pageResult.ReturnFields,
                pageResult.PageSize,
                pageResult.PageIndex,
                pageResult.Where,
                this.defaultGateway.BuildDbColumnName(pageResult.Orderfld),
                pageResult.OrderType
            };
            StoredProcedureSection sps = this.defaultGateway.FromStoredProcedure(this.procedureName);
            for (int i = 0; i < paramNames.Length; i++)
            {
                sps.AddInputParameter(paramNames[i], this.TypeToDbType(paramValues[i].GetType()), paramValues[i]);
            }
            return sps.ToDataSet();
        }
        /// <summary>
        /// 执行存储过程的方法
        /// </summary>
        /// <param name="storeProcedureName">存储过程的名称</param>
        /// <param name="values">存储过程的参数</param>
        /// <returns>返回存储过程执行后对应的数据集</returns>
        public DataSet GetDataSetByStoreProcedure(string storeProcedureName, params KeyValuePair<string, object>[] values)
        {
            StoredProcedureSection sps = this.defaultGateway.FromStoredProcedure(storeProcedureName);
            foreach (KeyValuePair<string, object> kvp in values)
            {
                sps.AddInputParameter(this.defaultGateway.BuildDbParamName(kvp.Key), this.TypeToDbType(kvp.Value.GetType()), kvp.Value);
            }
            return sps.ToDataSet();
        }
        /// <summary>
        /// 执行存储过程的方法
        /// </summary>
        /// <param name="storeProcedureName">存储过程的名称</param>
        /// <param name="values">存储过程的参数</param>
        /// <returns>返回存储过程执行后对应的数据集</returns>
        public DataSet GetDataSetByStoreProcedure(string storeProcedureName, Dictionary<string, object> values)
        {
            StoredProcedureSection sps = this.defaultGateway.FromStoredProcedure(storeProcedureName);
            foreach (KeyValuePair<string, object> kvp in values)
            {
                sps.AddInputParameter(this.defaultGateway.BuildDbParamName(kvp.Key), this.TypeToDbType(kvp.Value.GetType()), kvp.Value);
            }
            return sps.ToDataSet();
        }
        /// <summary>
        /// 执行自定义SQL语句
        /// </summary>
        /// <param name="sql">要执行的SQL语句</param>
        /// <returns>返回NBear自定义语句块</returns>
        public CustomSqlSection GetBySql(string sql)
        {
            return this.defaultGateway.FromCustomSql(sql);
        }
        /// <summary>
        /// 获取表中的总记录数
        /// </summary>
        /// <returns>返回总记录数</returns>
        public int GetRowCount()
        {
            return this.defaultGateway.Count<T>(WhereClip.All);
        }
        /// <summary>
        /// 获取执行条件的记录数
        /// </summary>
        /// <param name="where">指定条件</param>
        /// <returns>返回记录数</returns>
        public int GetRowCountByWhere(WhereClip where)
        {
            return this.defaultGateway.Count<T>(where);
        }
        /// <summary>
        /// 添加新记录
        /// </summary>
        /// <param name="entity">对应新记录的实体数据</param>
        /// <returns>返回追加记录的主键值</returns>
        public int Insert(T entity)
        {
            return this.defaultGateway.Save<T>(entity);
        }
        /// <summary>
        /// 批量添加新纪录
        /// </summary>
        /// <param name="lst">对应的List记录</param>
        /// <returns>返回受影响的记录行数</returns>
        public int BatchInsert(List<T> lst)
        {
            int result = 0;
            Gateway batch = defaultGateway.BeginBatchGateway(10);
            for (int i = 0; i < lst.Count; i++)
            {
                batch.Save<T>(lst[i]);
                result++;
            }
            batch.EndBatch();
            return result;
        }
        /// <summary>
        /// 更新记录
        /// </summary>
        /// <param name="entity">需要更新记录对应的实体数据</param>
        /// <returns>返回更新的记录数</returns>
        public int Update(T entity)
        {
            return this.defaultGateway.Save<T>(entity);
        }

        /// <summary>
        /// 按照条件更新字段值
        /// </summary>
        /// <param name="properties">要更新的字段集合</param>
        /// <param name="values">要更新的字段值集合</param>
        /// <param name="where">条件参数</param>
        public void Update(PropertyItem[] properties, object[] values, WhereClip where)
        {
            this.defaultGateway.Update<T>(properties, values, where);
        }

        /// <summary>
        /// 删除主键是id值得记录
        /// </summary>
        /// <param name="id">要删除记录的主键值</param>
        /// <returns>返回删除的记录条数</returns>
        public void Delete(object id)
        {
            this.defaultGateway.Delete<T>(id);
        }
        /// <summary>
        /// 删除对象对应的记录
        /// </summary>
        /// <param name="entity">与要删除记录对应的对象</param>
        public void Delete(T entity)
        {
            this.defaultGateway.Delete<T>(entity);
        }
        /// <summary>
        /// 按指定的条件删除数据
        /// </summary>
        /// <param name="where">条件</param>
        /// <returns>返回删除的记录数</returns>
        public void DeleteByWhere(WhereClip where)
        {
            this.defaultGateway.Delete<T>(where);
        }
        /// <summary>
        /// 清除表中所有记录
        /// </summary>
        public void ClearData()
        {
            string cmdText = "truncate table {0}";
            cmdText = String.Format(cmdText, this.tableName);
            this.defaultGateway.FromCustomSql(cmdText).ExecuteNonQuery();
        }

        public object GetMaxValueByProperty(PropertyItem item)
        {
            object Result = this.GetBySql("SELECT MAX(" + item.ColumnName + ") FROM " + this.tableName).ToScalar();
            if (Result == DBNull.Value || Result == null)
            {
                Result = 0;
            }
            return Result;
        }
        #endregion
    }
}
