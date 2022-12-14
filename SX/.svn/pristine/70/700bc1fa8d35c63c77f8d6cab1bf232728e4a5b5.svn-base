using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Mesnac.Util.BaseInfo
{
    /// <summary>
    /// 序列化信息转化为表
    /// 孙本强 @ 2013-04-03 13:11:23
    /// </summary>
    /// <remarks></remarks>
    public class DictionaryList
    {
        /// <summary>
        /// 序列化信息转化为表
        /// 孙本强 @ 2013-04-03 13:11:23
        /// </summary>
        /// <param name="lst">The LST.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public List<Dictionary<string, object>> GenericListTo(object[] lst)
        {
            List<Dictionary<string, object>> Result = new List<Dictionary<string, object>>();
            foreach (object obj in lst)
            {
                Dictionary<string, object> dic = new Dictionary<string, object>();
                PropertyInfo[] fields = obj.GetType().GetProperties();
                foreach (PropertyInfo f in fields)
                {
                    dic.Add(f.Name.ToString(), f.GetValue(obj, null));
                }
                Result.Add(dic);
            }
            return Result;
        }
    }
}
