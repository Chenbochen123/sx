using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mesnac.Util.BaseInfo
{
    /// <summary>
    /// 类转化
    /// 孙本强 @ 2013-04-03 13:11:41
    /// </summary>
    /// <remarks></remarks>
    public class ObjectConverter
    {
        /// <summary>
        /// 将类转化为字符串
        /// 孙本强 @ 2013-04-03 13:11:41
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <returns>A <see cref="System.String"/> that represents this instance.</returns>
        /// <remarks></remarks>
        public string ToString(object obj)
        {
            if (obj == null)
            {
                return null;
            }
            return obj.ToString();
        }
        /// <summary>
        /// 类转化为数字
        /// 孙本强 @ 2013-04-03 13:11:41
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public decimal? ToDecimal(object obj)
        {
            if (obj == null)
            {
                return null;
            }
            string ss = obj.ToString();
            decimal d = 0;
            if (decimal.TryParse(ss, out d))
            {
                return d;
            }
            return null;
        }
        /// <summary>
        /// 类转化为整数
        /// 孙本强 @ 2013-04-03 13:11:41
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public int? ToInt(object obj)
        {
            if (obj == null)
            {
                return null;
            }
            string ss = obj.ToString();
            int d = 0;
            if (int.TryParse(ss, out d))
            {
                return d;
            }
            return null;
        }
    }
}
