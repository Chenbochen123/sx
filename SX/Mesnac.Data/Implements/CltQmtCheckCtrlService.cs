using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Interface;
    public class CltQmtCheckCtrlService : BaseService<CltQmtCheckCtrl>, ICltQmtCheckCtrlService
    {
		#region 构造方法

        public CltQmtCheckCtrlService() : base(){ }

        public CltQmtCheckCtrlService(string connectStringKey) : base(connectStringKey){ }

        public CltQmtCheckCtrlService(NBear.Data.Gateway way) : base(way){ }

        #endregion 构造方法

        /// <summary>
        /// 获取项目均值趋势信息
        /// </summary>
        /// <param name="paras"></param>
        /// <returns></returns>
        public DataSet GetAvgTrendDataSetByQueryParams(ICltQmtCheckCtrlQueryParams paras)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            dict.Add("@BeginDate", paras.BeginDate);
            dict.Add("@EndDate", paras.EndDate);
            dict.Add("@ItemCd", paras.ItemCd);
            dict.Add("@MaterCode", paras.MaterCode);
            dict.Add("@StatisticType", paras.StatisticType);
            dict.Add("@WorkShopCode", paras.WorkShopCode);
            dict.Add("@ZJSID", paras.ZJSID);

            return GetDataSetByStoreProcedure("ProcQmtCheckItemAvgTrendReport", dict);
        }
        public DataSet GetCheckNotHGItemCount(ICltQmtCheckCtrlQueryParams paras)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            dict.Add("@StartDate", paras.BeginDate);
            dict.Add("@EndDate", paras.EndDate);
            dict.Add("@WorkShopCode", paras.WorkShopCode);
            dict.Add("@TJTYPE", paras.StatisticType);
            dict.Add("@ItemCd", paras.ItemCd);
            return GetDataSetByStoreProcedure("proc_GetCheckTotal", dict);
        }
        public DataSet GetCheckChart(ICltQmtCheckCtrlQueryParams paras)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            dict.Add("@StartDate", paras.BeginDate);
            dict.Add("@EndDate", paras.EndDate);
            dict.Add("@MaterCode", paras.MaterCode);
            dict.Add("@CheckItem", paras.ItemCd);
            dict.Add("@WorkShopCode", paras.WorkShopCode);
            dict.Add("@UserID", paras.UserID);
            dict.Add("@TJTYPE", paras.StatisticType);
            dict.Add("@TJDateTYPE", paras.ZJSID);
            return GetDataSetByStoreProcedure("proc_GetCheckChart", dict);
        }
        public DataSet GetFormatValue(ICltQmtCheckCtrlQueryParams paras)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            dict.Add("@MaterCode", paras.MaterCode);
            dict.Add("@Item", paras.ItemCd);
            return GetDataSetByStoreProcedure("GetFormatValue", dict);
        }
    }

    public class CltQmtCheckCtrlQueryParams : ICltQmtCheckCtrlQueryParams
    {
        public string BeginDate { get; set; }
        public string EndDate { get; set; }
        public string MaterCode { get; set; }
        public string WorkShopCode { get; set; }
        public string ZJSID { get; set; }
        public string ItemCd { get; set; }
        public string StatisticType { get; set; }
        public string UserID { get; set; }
    }

}
