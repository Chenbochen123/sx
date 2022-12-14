using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Interface;
    using System.Data;
    public class QmcLedgerService : BaseService<QmcLedger>, IQmcLedgerService
    {
        #region 构造方法

        public QmcLedgerService() : base() { }

        public QmcLedgerService(string connectStringKey) : base(connectStringKey) { }

        public QmcLedgerService(NBear.Data.Gateway way) : base(way) { }

        #endregion 构造方法

        public class QueryParams
        {
            public DateTime beginDate { get; set; }
            public DateTime endDate { get; set; }
            public string billNo { get; set; }
            public string barcode { get; set; }
            public string checkResult { get; set; }
            public string materialCode { get; set; }
            public string specId { get; set; }
            public string seriesId { get; set; }

            //合格率统计使用的
            public string supplierId { get; set; }
            public string manufacturerId { get; set; }
            public string standardId { get; set; }
        }

        /// <summary>
        /// 原生属性与自定义属性的联合查询输出
        /// </summary>
        /// <returns></returns>
        public DataTable GetLedgerUnion()
        {
            string sqlstr = @"if exists (select 1 from sysobjects where id = object_id('#templedger') and type = 'U')
                              drop table #templedger 
                              select C.LedgerId,                                                     C.CheckId,      C.BillDetailId, C.OrderId, A.KeyName as 项目,        B.KeyValue as 项目值,     C.BillNo as 送检单号,
                                     C.Barcode as 条码号,                                                            C.BatchCode as 批次号,     C.SendNum as 送检数量,    C.SendUnit as 单位,       I.SpecName as 规格,
                                     case C.CheckResult when '1' then '合格' when '2' then '不合格' end as 检测结果, D.UserName as 检测人,      E.UserName as 取样人,     F.UserName as 接收人,     G.UserName as 领取人,
                                     H.UserName as 处置人,                                                           C.RecordDate as 记录时间,  C.Remark as 备注,
                                     C.ReceiveDate as 接收时间,                                                      C.SendDate as 发放时间,    C.ReturnDate as 返库时间, C.HandleDate as 处置时间, C.HandleMethod as 处置方式
                                     into #tempLedger 
                              from   QmcLedgerKey A
                              inner join QmcLedgerDetail B on A.KeyId = B.KeyId
                              left join QmcLedger C on C.LedgerId = B.LedgerId
                              left join BasUser D on C.CheckerId = D.HRCode
                              left join BasUser E on C.ExtractorId = E.HRCode
                              left join BasUser F on C.ReceiverId = F.HRCode
                              left join BasUser G on C.FetcherId = G.HRCode
                              left join BasUser H on C.HandlerId = H.HRCode
                              left join QmcSpec I on I.SpecId = C.SpecId
                              where A.DeleteFlag = '0' and C.DeleteFlag = '0'
                              DECLARE @cols AS NVARCHAR(MAX),
                                      @query  AS NVARCHAR(MAX)
                              select @cols = STUFF((SELECT ',' + QUOTENAME(项目) from #templedger group by 项目 FOR XML PATH(''), TYPE).value('.','NVARCHAR(MAX)'),1,1,'')
                              set @query = 'SELECT LedgerId, CheckId, BillDetailId, OrderId, 送检单号, 条码号, 批次号, 送检数量, 单位, 规格, 检测结果, 检测人, 取样人, 接收人, 领取人, 处置人, 处置方式, 处置时间, 发放时间, 接收时间, 返库时间, 记录时间, 备注, ' + @cols + ' from 
                                         (
                                            select *
                                            from #templedger
                                         ) x
                                         pivot 
                                         (
                                            max(项目值)
                                            for 项目 in (' + @cols + ')
                                         ) p '
                              execute(@query)
                              if exists (select 1 from sysobjects where id = object_id('#templedger') and type = 'U')
                              drop table #templedger";
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
            sqlstr.AppendLine(@"if exists (select 1 from sysobjects where id = object_id('#templedger') and type = 'U')
                              drop table #templedger 
                              select C.LedgerId,                                                     C.CheckId,      C.BillDetailId, C.OrderId, A.KeyName as 项目,        B.KeyValue as 项目值,     K.MaterialName as 原材料名称, C.BillNo as 送检单号,
                                     C.Barcode as 条码号,                                                            C.BatchCode as 批次号,     C.SendNum as 送检数量,    C.SendUnit as 单位,       I.SpecName as 规格,
                                     case C.CheckResult when '1' then '合格' when '2' then '不合格' end as 检测结果, D.UserName as 检测人,      E.UserName as 取样人,     F.UserName as 接收人,     G.UserName as 领取人,
                                     H.UserName as 处置人,                                                           C.RecordDate as 记录时间,  C.Remark as 备注,
                                     C.ReceiveDate as 接收时间,                                                      C.SendDate as 发放时间,    C.ReturnDate as 返库时间, C.HandleDate as 处置时间, C.HandleMethod as 处置方式         
                                     into #tempLedger 
                              from   QmcLedgerKey A
                              inner join QmcLedgerDetail B on A.KeyId = B.KeyId
                              left join QmcLedger C on C.LedgerId = B.LedgerId
                              left join BasUser D on C.CheckerId = D.HRCode
                              left join BasUser E on C.ExtractorId = E.HRCode
                              left join BasUser F on C.ReceiverId = F.HRCode
                              left join BasUser G on C.FetcherId = G.HRCode
                              left join BasUser H on C.HandlerId = H.HRCode
                              left join QmcSpec I on I.SpecId = C.SpecId
                              left join QmcCheckData J on J.CheckId = C.CheckId
                              left join BasMaterial K on K.MaterialCode = J.MaterCode
                              where A.DeleteFlag = '0' and C.DeleteFlag = '0'");
            if (!string.IsNullOrEmpty(param.billNo))
            {
                sqlstr.AppendFormat(" AND C.BillNo LIKE '%{0}%'", param.billNo);
                sqlstr.AppendLine();
            }
            if (!string.IsNullOrEmpty(param.barcode))
            {
                sqlstr.AppendFormat(" AND C.Barcode LIKE '%{0}%'", param.barcode);
                sqlstr.AppendLine();
            }
            if (!string.IsNullOrEmpty(param.checkResult))
            {
                if (param.checkResult != "all")
                {
                    sqlstr.AppendLine(" AND C.CheckResult = '" + param.checkResult + "'");
                }
            }
            if (!string.IsNullOrEmpty(param.specId))
            {
                sqlstr.AppendLine(" AND C.SpecId = '" + param.specId + "'");
            }
            if (!string.IsNullOrEmpty(param.materialCode))
            {
                sqlstr.AppendLine(" AND J.MaterCode = '" + param.materialCode + "'");
            }
            if (!string.IsNullOrEmpty(param.seriesId))
            {
                sqlstr.AppendLine(" AND K.MinorTypeID = '" + param.seriesId + "' AND K.MajorTypeID = '1'");
            }
            if (param.beginDate.ToString() != "0001/1/1 0:00:00")
            {
                sqlstr.AppendLine("AND C.RecordDate BETWEEN '" + param.beginDate + "' AND '" + param.endDate +"'");
            }
            sqlstr.AppendLine(@"DECLARE @cols AS NVARCHAR(MAX),
                                      @query  AS NVARCHAR(MAX)
                              select @cols = STUFF((SELECT ',' + QUOTENAME(项目) from #templedger group by 项目 FOR XML PATH(''), TYPE).value('.','NVARCHAR(MAX)'),1,1,'')
                              set @query = 'SELECT LedgerId, CheckId, BillDetailId, OrderId, 原材料名称, 送检单号, 条码号, 批次号, 送检数量, 单位, 规格, 检测结果, 检测人, 取样人, 接收人, 领取人, 处置人, 处置方式, 处置时间, 发放时间, 接收时间, 返库时间, 记录时间, 备注, ' + @cols + ' from 
                                         (
                                            select *
                                            from #templedger
                                         ) x
                                         pivot 
                                         (
                                            max(项目值)
                                            for 项目 in (' + @cols + ')
                                         ) p '
                              execute(@query)
                              if exists (select 1 from sysobjects where id = object_id('#templedger') and type = 'U')
                              drop table #templedger");
            NBear.Data.CustomSqlSection css = this.GetBySql(sqlstr.ToString());
            DataSet ds = css.ToDataSet();

            if (ds != null && ds.Tables.Count > 0)
            {
                return ds.Tables[0];
            }
            return new DataTable();
        }

        /// <summary>
        /// 获取合格率统计报表
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public DataTable GetReport(QueryParams param)
        {
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendLine(@"Select A.BillNo, A.Barcode, A.BatchCode, A.SpecId, A.SupplierId, A.ManufacturerId, A.SendNum, A.CheckResult,
                                       C.MaterialName, C.MaterialCode, F.SpecName,
                                       D.FacName as SupplierName, E.FacName as ManufacturerName
                                       From QmcLedger A
                                       Left Join QmcCheckData B On A.CheckId = B.CheckId
                                       Left Join BasMaterial C On B.MaterCode = C.MaterialCode
                                       Left Join BasFactoryInfo D On A.SupplierId = D.ObjID
                                       Left Join BasFactoryInfo E On A.ManufacturerId = E.ObjID
                                       Left Join QmcSpec F On A.SpecId = F.SpecId
                                       Where A.DeleteFlag = '0' And B.DeleteFlag = '0'");
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
            NBear.Data.CustomSqlSection css = this.GetBySql(sqlstr.ToString());
            DataSet ds = css.ToDataSet();

            //汇总报告
            if (ds != null && ds.Tables.Count > 0)
            {
                DataTable originDT = ds.Tables[0];
                DataTable resultDT = new DataTable();
                DataColumn dc0 = new DataColumn("MaterialCode");
                DataColumn dc1 = new DataColumn("SpecId");
                DataColumn dc2 = new DataColumn("SupplierId");
                DataColumn dc3 = new DataColumn("ManufacturerId");
                DataColumn dc4 = new DataColumn("原材料名称");
                DataColumn dc5 = new DataColumn("规格");
                DataColumn dc6 = new DataColumn("供应商");
                DataColumn dc7 = new DataColumn("生产商");
                DataColumn dc8 = new DataColumn("送检批次");
                DataColumn dc9 = new DataColumn("合格批次");
                DataColumn dc10 = new DataColumn("送检重量");
                DataColumn dc11 = new DataColumn("合格重量");
                DataColumn dc12 = new DataColumn("批次合格率");
                DataColumn dc13 = new DataColumn("重量合格率");
                resultDT.Columns.Add(dc0);
                resultDT.Columns.Add(dc1);
                resultDT.Columns.Add(dc2);
                resultDT.Columns.Add(dc3);
                resultDT.Columns.Add(dc4);
                resultDT.Columns.Add(dc5);
                resultDT.Columns.Add(dc6);
                resultDT.Columns.Add(dc7);
                resultDT.Columns.Add(dc8);
                resultDT.Columns.Add(dc9);
                resultDT.Columns.Add(dc10);
                resultDT.Columns.Add(dc11);
                resultDT.Columns.Add(dc12);
                resultDT.Columns.Add(dc13);
                if (originDT.Rows.Count > 0)
                {
                    foreach (DataRow originDR in originDT.Rows)
                    {
                        if (resultDT.Rows.Count > 0)
                        {
                            bool repeatFlag = false;
                            foreach (DataRow resultDR in resultDT.Rows)
                            {
                                if ((resultDR["MaterialCode"].ToString() == originDR["MaterialCode"].ToString())
                                    && (resultDR["SupplierId"].ToString() == originDR["SupplierId"].ToString())
                                    && (resultDR["ManufacturerId"].ToString() == originDR["ManufacturerId"].ToString())
                                    && (resultDR["SpecId"].ToString() == originDR["SpecId"].ToString()))
                                {
                                    repeatFlag = true;
                                    resultDR["送检重量"] = (Convert.ToDecimal(resultDR["送检重量"]) + Convert.ToDecimal(originDR["SendNum"])).ToString();
                                    resultDR["送检批次"] = (Convert.ToInt32(resultDR["送检批次"]) + 1).ToString();
                                    if (originDR["CheckResult"].ToString() == "1")
                                    {
                                        resultDR["合格批次"] = (Convert.ToInt32(resultDR["合格批次"]) + 1).ToString();
                                        resultDR["合格重量"] = (Convert.ToDecimal(resultDR["合格重量"]) + Convert.ToDecimal(originDR["SendNum"])).ToString();
                                    }
                                    else if (originDR["CheckResult"].ToString() == "2")
                                    {
                                        //skip
                                    }
                                    break;
                                }
                            }
                            if (repeatFlag)
                            {
                                continue;
                            }
                            else
                            {
                                DataRow resultDR = resultDT.NewRow();
                                resultDR["MaterialCode"] = originDR["MaterialCode"].ToString();
                                resultDR["SpecId"] = originDR["SpecId"].ToString();
                                resultDR["SupplierId"] = originDR["SupplierId"].ToString();
                                resultDR["ManufacturerId"] = originDR["ManufacturerId"].ToString();
                                resultDR["原材料名称"] = originDR["MaterialName"].ToString();
                                resultDR["规格"] = originDR["SpecName"].ToString();
                                resultDR["供应商"] = originDR["SupplierName"].ToString();
                                resultDR["生产商"] = originDR["ManufacturerName"].ToString();
                                resultDR["送检批次"] = "1";
                                resultDR["送检重量"] = originDR["SendNum"].ToString();
                                if (originDR["CheckResult"].ToString() == "1")
                                {
                                    resultDR["合格批次"] = "1";
                                    resultDR["合格重量"] = originDR["SendNum"].ToString();
                                }
                                else if (originDR["CheckResult"].ToString() == "2")
                                {
                                    resultDR["合格批次"] = "0";
                                    resultDR["合格重量"] = "0.000";
                                }
                                resultDT.Rows.Add(resultDR);
                            }
                        }
                        else
                        {
                            DataRow resultDR = resultDT.NewRow();
                            resultDR["MaterialCode"] = originDR["MaterialCode"].ToString();
                            resultDR["SpecId"] = originDR["SpecId"].ToString();
                            resultDR["SupplierId"] = originDR["SupplierId"].ToString();
                            resultDR["ManufacturerId"] = originDR["ManufacturerId"].ToString();
                            resultDR["原材料名称"] = originDR["MaterialName"].ToString();
                            resultDR["规格"] = originDR["SpecName"].ToString();
                            resultDR["供应商"] = originDR["SupplierName"].ToString();
                            resultDR["生产商"] = originDR["ManufacturerName"].ToString();
                            resultDR["送检批次"] = "1";
                            resultDR["送检重量"] = originDR["SendNum"].ToString();
                            if (originDR["CheckResult"].ToString() == "1")
                            {
                                resultDR["合格批次"] = "1";
                                resultDR["合格重量"] = originDR["SendNum"].ToString();
                            }
                            else if (originDR["CheckResult"].ToString() == "2")
                            {
                                resultDR["合格批次"] = "0";
                                resultDR["合格重量"] = "0.000";
                            }
                            resultDT.Rows.Add(resultDR);
                        }
                    }
                }
                if (resultDT.Rows.Count > 0)
                {
                    //计算合格率
                    foreach (DataRow dr in resultDT.Rows)
                    {
                        dr["批次合格率"] = Math.Round(((Convert.ToDecimal(dr["合格批次"]) / Convert.ToDecimal(dr["送检批次"])) * 100),2).ToString() + "%";
                        dr["重量合格率"] = Math.Round(((Convert.ToDecimal(dr["合格重量"]) / Convert.ToDecimal(dr["送检重量"])) * 100),2).ToString() + "%";
                    }
                }
                return resultDT;
            }
            return new DataTable();
        }

        public string GetNextLedgerId()
        {
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendLine(" Select MAX(LedgerId) + 1 as LedgerId From QmcLedger");
            string temp = this.GetBySql(sqlstr.ToString()).ToScalar().ToString();
            if (temp == "")
            {
                temp = "1";
            }
            return temp;
        }
    }
}
