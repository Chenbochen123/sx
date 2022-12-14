using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Mesnac.Business.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Interface;
    public interface IEqmStopRecordManager : IBaseManager<EqmStopRecord>
    {
        //��ȡͣ����¼��Ϣ
        DataSet GetDataByParas( EqmStopRecordParams queryParams );
    }
}
