using System;
using System.Collections.Generic;
using System.Text;

using System.Data;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Interface;
    public class QmcCheckDataDetailService : BaseService<QmcCheckDataDetail>, IQmcCheckDataDetailService
    {
        #region 构造方法

        public QmcCheckDataDetailService() : base() { }

        public QmcCheckDataDetailService(string connectStringKey) : base(connectStringKey) { }

        public QmcCheckDataDetailService(NBear.Data.Gateway way) : base(way) { }

        #endregion 构造方法

        public class QueryParams
        {
            public DateTime beginDate { get; set; }
            public DateTime endDate { get; set; }
            public string materialCode { get; set; }
            public string specId { get; set; }
            public string seriesId { get; set; }
            public string supplierId { get; set; }
            public string manufacturerId { get; set; }
            public string standardId { get; set; }
            public string itemId { get; set; }
        }

        /// <summary>
        /// 根据检测记录ID获取检测记录检测项目信息
        /// </summary>
        /// <param name="checkId"></param>
        /// <returns></returns>
        public DataSet GetDataSetByCheckId(string checkId)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("SELECT A.*");
            sb.AppendLine(", B.ItemId, B.CheckMethod, B.Remark, B.Frequency,B.OrderID,B.TeXing");
            sb.AppendLine(", B.GoodMaxValue, B.GoodMinValue, B.GoodOperator, B.GoodTextValue");
            sb.AppendLine(", B.PrimeMaxValue, B.PrimeMinValue, B.PrimeOperator, B.PrimeTextValue");
            sb.AppendLine(", B.GoodDisplayValue, B.PrimeDisplayValue, B.InputGoodMaxValue, B.InputGoodMinValue, B.InputPrimeMaxValue, B.InputPrimeMinValue");
            sb.AppendLine(", C.ItemName, C.ItemCode, C.ValueType");
            sb.AppendLine(", D.StandardName, D.ActivateDate");
            sb.AppendLine("FROM QmcCheckDataDetail A");
            sb.AppendLine("LEFT JOIN QmcCheckItemDetail B ON A.ItemDetailId = B.ItemDetailId");
            sb.AppendLine("LEFT JOIN QmcCheckItem C ON B.ItemId = C.ItemId");
            sb.AppendLine("Left Join QmcStandard D On C.StandardId = D.StandardId");
            sb.AppendFormat("WHERE A.CheckId = {0}", checkId);
            sb.AppendLine();
            sb.AppendLine("ORDER BY B.OrderID, C.ItemId");
            sb.AppendLine();

            return this.GetBySql(sb.ToString()).ToDataSet();

        }

        /// <summary>
        /// 获取SPC报表数据
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public DataTable GetSPCReport(QueryParams param)
        {
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendLine(@"Select I.ItemName, G.CheckValue, H.GoodMinValue,H.GoodOperator, H.GoodMaxValue
                                From QmcLedger A
                                Left Join QmcCheckData B On A.CheckId = B.CheckId
                                Left Join BasMaterial C On B.MaterCode = C.MaterialCode
                                Left Join BasFactoryInfo D On A.SupplierId = D.ObjID
                                Left Join BasFactoryInfo E On A.ManufacturerId = E.ObjID
                                Left Join QmcSpec F On A.SpecId = F.SpecId
                                Left Join QmcCheckDataDetail G On A.CheckId = G.CheckId
                                Left Join QmcCheckItemDetail H On G.ItemDetailId = H.ItemDetailId And H.MaterialCode = B.MaterCode
                                Left Join QmcCheckItem I On H.ItemId = I.ItemId 
                                where A.DeleteFlag = '0' And B.DeleteFlag = '0' And C.DeleteFlag = '0' 
                                And I.ValueType = '数字'");
            if (!string.IsNullOrEmpty(param.seriesId))
            {
                sqlstr.AppendFormat(" And C.MinorTypeID = {0}", param.seriesId);
                sqlstr.AppendLine();
            }
            if (!string.IsNullOrEmpty(param.materialCode))
            {
                sqlstr.AppendFormat(" AND B.MaterCode = '{0}'", param.materialCode);
                sqlstr.AppendLine();
            }
            if (param.beginDate.ToString() != "0001/1/1 0:00:00")
            {
                sqlstr.AppendLine("AND B.RecordTime BETWEEN '" + param.beginDate + "' AND '" + param.endDate + "'");
            }
            if (!string.IsNullOrEmpty(param.supplierId))
            {
                sqlstr.AppendFormat(" AND A.SupplierId = {0}", param.supplierId);
                sqlstr.AppendLine();
            }
            if (!string.IsNullOrEmpty(param.manufacturerId))
            {
                sqlstr.AppendFormat(" AND A.ManufacturerId = {0}", param.manufacturerId);
                sqlstr.AppendLine();
            }
            if (!string.IsNullOrEmpty(param.specId))
            {
                sqlstr.AppendLine(" AND A.SpecId = " + param.specId);
            }
            if (!string.IsNullOrEmpty(param.standardId))
            {
                sqlstr.AppendLine(" AND B.StandardId = " + param.standardId);
            }
            if (!string.IsNullOrEmpty(param.itemId))
            {
                sqlstr.AppendLine(" AND I.ItemId = " + param.itemId);
            }
            NBear.Data.CustomSqlSection css = this.GetBySql(sqlstr.ToString());
            DataSet ds = css.ToDataSet();
            if (ds != null && ds.Tables.Count > 0)
            {
                return ds.Tables[0];
            }
            return new DataTable();
        }
    }
}
