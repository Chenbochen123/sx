using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Mesnac.Data.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    public interface IEqmMaintainRecordService : IBaseService<EqmMaintainRecord>
    {
        //获取维修记录信息
        DataSet GetDataByParas(EqmMaintainRecordParams queryParams);

        //新增维修记录信息
        int InsertRecord(EqmMaintainRecord record);

        //获取维修记录统计信息
        DataSet GetGroupDataByParas(List<string> list);

        //获取维修记录统计明细信息
        DataSet GetGroupDetailDataByParas(List<string> list);
    }
    
    public class EqmMaintainRecordParams
    {
        public string objID
        {
            get;
            set;
        }
        public string equipCode
        {
            get;
            set;
        }
        public string shiftID
        {
            get;
            set;
        }
        public string startTime
        {
            get;
            set;
        }
        public string endTime
        {
            get;
            set;
        }
        public string stopTypeID
        {
            get;
            set;
        }
        public string faultID
        {
            get;
            set;
        }
        public string workShopCode
        {
            get;
            set;
        }
        public string mainTypeID
        {
            get;
            set;
        }
        public string faultTypeID
        {
            get;
            set;
        }
        public string status
        {
            get;
            set;
        }
        public PageResult<EqmMaintainRecord> pageResult
        {
            get;
            set;
        }
    }
}
