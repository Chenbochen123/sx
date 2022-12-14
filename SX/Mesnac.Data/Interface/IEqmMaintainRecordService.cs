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
        //��ȡά�޼�¼��Ϣ
        DataSet GetDataByParas(EqmMaintainRecordParams queryParams);

        //����ά�޼�¼��Ϣ
        int InsertRecord(EqmMaintainRecord record);

        //��ȡά�޼�¼ͳ����Ϣ
        DataSet GetGroupDataByParas(List<string> list);

        //��ȡά�޼�¼ͳ����ϸ��Ϣ
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
