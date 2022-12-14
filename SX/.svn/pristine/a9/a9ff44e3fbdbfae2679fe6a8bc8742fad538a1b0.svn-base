using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Mesnac.Data.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    public interface IEqmStopRecordService : IBaseService<EqmStopRecord>
    {
        //获取停机记录信息
        DataSet GetDataByParas( EqmStopRecordParams queryParams );
    }
    public class EqmStopRecordParams
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
        public PageResult<EqmStopRecord> pageResult
        {
            get;
            set;
        }
    }
}
