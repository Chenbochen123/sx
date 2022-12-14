using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mesnac.Entity.Custom
{
    /// <summary>
    /// 数据库配置类
    /// </summary>
    [Serializable]
    public class CdbDatabase
    {
        private int objID;
        private int dbVersionID;
        private string dbKey;
        private string assemblyName;
        private string className;
        private string connStr;
        private string remark;

        /// <summary>
        /// 序号
        /// </summary>
        public int ObjID
        {
            get { return objID; }
            set { objID = value; }
        }
        /// <summary>
        /// 对应的数据版本ID
        /// </summary>
        public int DbVersionID
        {
            get { return dbVersionID; }
            set { dbVersionID = value; }
        }
        /// <summary>
        /// 数据库标识
        /// </summary>
        public string DbKey
        {
            get { return dbKey; }
            set { dbKey = value; }
        }
        /// <summary>
        /// 数据提供程序所在的程序集
        /// </summary>
        public string AssemblyName
        {
            get { return assemblyName; }
            set { assemblyName = value; }
        }
        /// <summary>
        /// 数据提供程序类名
        /// </summary>
        public string ClassName
        {
            get { return className; }
            set { className = value; }
        }
        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        public string ConnStr
        {
            get { return connStr; }
            set { connStr = value; }
        }
        /// <summary>
        /// 备注信息
        /// </summary>
        public string Remark
        {
            get { return remark; }
            set { remark = value; }
        }
    }
}
