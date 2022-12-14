using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mesnac.Entity.Custom
{
    /// <summary>
    /// 数据版本类
    /// </summary>
    [Serializable]
    public class CdbVersion
    {
        private int objID;
        private string dbVersion;
        private string remark;
        private Dictionary<string, CdbDatabase> databases = new Dictionary<string,CdbDatabase>();

        /// <summary>
        /// 序号
        /// </summary>
        public int ObjID
        {
            get { return objID; }
            set { objID = value; }
        }
        /// <summary>
        /// 版本
        /// </summary>
        public string DbVersion
        {
            get { return dbVersion; }
            set { dbVersion = value; }
        }
        /// <summary>
        /// 备注信息
        /// </summary>
        public string Remark
        {
            get { return remark; }
            set { remark = value; }
        }
        /// <summary>
        /// 此版本数据下的数据库列表
        /// </summary>
        public Dictionary<string, CdbDatabase> Databases
        {
            get { return databases; }
            set { databases = value; }
        }
    }
}
