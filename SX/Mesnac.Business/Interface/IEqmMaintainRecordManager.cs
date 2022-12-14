using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Mesnac.Business.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Interface;
    public interface IEqmMaintainRecordManager : IBaseManager<EqmMaintainRecord>
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
}
