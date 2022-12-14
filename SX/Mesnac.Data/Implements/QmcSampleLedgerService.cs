using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Interface;
    using System.Data;
    public class QmcSampleLedgerService : BaseService<QmcSampleLedger>, IQmcSampleLedgerService
    {
		#region 构造方法

        public QmcSampleLedgerService() : base(){ }

        public QmcSampleLedgerService(string connectStringKey) : base(connectStringKey){ }

        public QmcSampleLedgerService(NBear.Data.Gateway way) : base(way){ }

        #endregion 构造方法

        public class QueryParams
        {
            public DateTime beginDate { get; set; }
            public DateTime endDate { get; set; }
            public string billNo { get; set; }
            public string barcode { get; set; }
            public string checkResult { get; set; }
        }

        /// <summary>
        /// 联合查询输出
        /// </summary>
        /// <returns></returns>
        public DataTable GetLedgerUnion()
        {
            string sqlstr = @"select A.LedgerId, A.BillDetailId, A.OrderId, A.MaterialCode,
                                     A.SampleName as 样品名称, 
                                     C.FacName as 生产商,
                                     B.FacName as 供应商,
                                     A.FactoryCode as 厂家编号,
                                     A.Barcode as 条码号,
                                     A.BatchCode as 批次号,
                                     A.Frequency as 检测频次,
                                     CONVERT(char(20),A.SampleNum) + A.SampleUnit as 样品数量, 
                                     A.SampleCode as 样品编号, 
                                     H.SpecName as 规格,
                                     A.SampleStatus as 样品状态, 
                                     D.UserName as 取样人,
                                     E.UserName as 接收人,
                                     A.ReceiveDate as 接收时间, 
                                     A.SendDate as 发放时间,
                                     F.UserName as 领取人,
                                     A.CheckResult as 检验结果, 
                                     A.ReturnDate as 返库时间,
                                     A.HandleDate as 处置时间,
                                     A.HandleMethod as 处置方式,
                                     G.UserName as 处置人,     
                                     A.RecordDate as 记录时间,
                                     A.Remark as 备注
                              from QmcSampleLedger A
                              left join BasFactoryInfo B on A.SupplierId = B.ObjID
                              left join BasFactoryInfo C on A.ManufacturerId = C.ObjID
                              left join BasUser D on A.ExtractorId = D.HRCode
                              left join BasUser E on A.ReceiverId = E.HRCode
                              left join BasUser F on A.FetcherId = F.HRCode
                              left join BasUser G on A.HandlerId = G.HRCode
                              left join QmcSpec H on A.SpecId = H.SpecId
                              where A.DeleteFlag = '0'";
            NBear.Data.CustomSqlSection css = this.GetBySql(sqlstr.ToString());
            DataSet ds = css.ToDataSet();

            if (ds != null && ds.Tables.Count > 0)
            {
                return ds.Tables[0];
            }
            return new DataTable();
        }

        /// <summary>
        /// 原生属性与自定义属性的联合查询输出-带查询条件
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public DataTable GetLedgerUnion(QueryParams param)
        {
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendLine(@"select A.LedgerId, A.BillDetailId, A.OrderId, A.MaterialCode,
                                     A.SampleName as 样品名称, 
                                     C.FacName as 生产商,
                                     B.FacName as 供应商,
                                     A.FactoryCode as 厂家编号,
                                     A.Barcode as 条码号,
                                     A.BatchCode as 批次号,
                                     A.Frequency as 检测频次,
                                     CONVERT(char(20),A.SampleNum) + A.SampleUnit as 样品数量, 
                                     A.SampleCode as 样品编号, 
                                     A.SampleStatus as 样品状态, 
                                     H.SpecName as 规格,
                                     D.UserName as 取样人,
                                     E.UserName as 接收人,
                                     A.ReceiveDate as 接收时间, 
                                     A.SendDate as 发放时间,
                                     F.UserName as 领取人,
                                     A.CheckResult as 检验结果, 
                                     A.ReturnDate as 返库时间,
                                     A.HandleDate as 处置时间,
                                     A.HandleMethod as 处置方式,
                                     G.UserName as 处置人,     
                                     A.RecordDate as 记录时间,
                                     A.Remark as 备注
                              from QmcSampleLedger A
                              left join BasFactoryInfo B on A.SupplierId = B.ObjID
                              left join BasFactoryInfo C on A.ManufacturerId = C.ObjID
                              left join BasUser D on A.ExtractorId = D.HRCode
                              left join BasUser E on A.ReceiverId = E.HRCode
                              left join BasUser F on A.FetcherId = F.HRCode
                              left join BasUser G on A.HandlerId = G.HRCode
                              left join QmcSpec H on A.SpecId = H.SpecId
                              where A.DeleteFlag = '0'");
            if (!string.IsNullOrEmpty(param.billNo))
            {
                sqlstr.AppendFormat(" AND A.BillNo LIKE '%{0}%'", param.billNo);
                sqlstr.AppendLine();
            }
            if (!string.IsNullOrEmpty(param.barcode))
            {
                sqlstr.AppendFormat(" AND A.Barcode LIKE '%{0}%'", param.barcode);
                sqlstr.AppendLine();
            }
            if (!string.IsNullOrEmpty(param.checkResult))
            {
                if (param.checkResult != "all")
                {
                    sqlstr.AppendLine(" AND A.CheckResult = '" + param.checkResult + "'");
                }
            }
            if (param.beginDate.ToString() != "0001/1/1 0:00:00")
            {
                sqlstr.AppendLine("AND A.RecordDate BETWEEN '" + param.beginDate + "' AND '" + param.endDate + "'");
            }
            NBear.Data.CustomSqlSection css = this.GetBySql(sqlstr.ToString());
            DataSet ds = css.ToDataSet();

            if (ds != null && ds.Tables.Count > 0)
            {
                return ds.Tables[0];
            }
            return new DataTable();
        }

        public string GetNextLedgerId()
        {
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendLine(" Select MAX(LedgerId) + 1 as LedgerId From QmcSampleLedger");
            string temp = this.GetBySql(sqlstr.ToString()).ToScalar().ToString();
            if (temp == "")
            {
                temp = "1";
            }
            return temp;
        }

        /// <summary>
        /// 获取自动流水号
        /// </summary>
        /// <returns></returns>
        public string GetAutoFlowSampleCode()
        {
            StringBuilder sqlstr = new StringBuilder();
            string datePrefix = DateTime.Now.Year.ToString().Remove(0, 2) + DateTime.Now.Month.ToString().PadLeft(2, '0') + DateTime.Now.Day.ToString().PadLeft(2, '0');
            sqlstr.AppendLine("Select * from QmcSampleLedger where SampleCode Like '" + datePrefix.Trim() + "%' and deleteFlag = '0' Order By SampleCode DESC");
            NBear.Data.CustomSqlSection css = this.GetBySql(sqlstr.ToString());
            DataSet ds = css.ToDataSet();
            if (ds != null && ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        try
                        {
                            int code = Convert.ToInt32(row["SampleCode"]);
                            code = code + 1;
                            return code.ToString();
                        }
                        catch (Exception)
                        {
                            continue;
                        }
                    }
                }
                else
                {
                    return datePrefix.Trim() + "01";
                }
            }
            return String.Empty;
        }
    }
}
