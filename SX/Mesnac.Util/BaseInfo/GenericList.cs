using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;

namespace Mesnac.Util.BaseInfo
{
    public class GenericList<T> : List<T>
    {
        /// <summary>
        /// Name:   将datatable装入指定类型的集合
        /// Author: yuany
        /// Date:   2013年1月17日
        /// </summary>
        /// <param name="dt">The dt.</param>
        /// <param name="f">The f.</param>
        /// <remarks></remarks>
        public GenericList(DataTable dt, string f)
        {
            System.Type tt = System.Type.GetType("Mesnac.Entity" + f);//获取指定名称的类型
            object ff = Activator.CreateInstance(tt, null);//创建指定类型实例
            PropertyInfo[] fields = ff.GetType().GetProperties();//获取指定对象的所有公共属性
            foreach (DataRow dr in dt.Rows)
            {
                object obj = Activator.CreateInstance(tt, null);
                foreach (DataColumn dc in dt.Columns)
                {
                    foreach (PropertyInfo t in fields)
                    {
                        if (dc.ColumnName == t.Name)
                        {
                            t.SetValue(obj, dr[dc.ColumnName], null);//给对象赋值
                            continue;
                        }
                    }

                }
                this.Add((T)obj);//将对象填充到list集合
            }
        }
    }
}

