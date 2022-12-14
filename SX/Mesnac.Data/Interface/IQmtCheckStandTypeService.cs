using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Mesnac.Data.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    public interface IQmtCheckStandTypeService : IBaseService<QmtCheckStandType>
    {
        //��ȡ�����׼������Ϣ
        DataSet GetDataByParas(QmtCheckStandTypeParams queryParams);

    }
    public class QmtCheckStandTypeParams
    {
        public string objID
        {
            get;
            set;
        }
        public string standTypeName
        {
            get;
            set;
        }
        public string deleteFlag
        {
            get;
            set;
        }
        public PageResult<QmtCheckStandType> pageResult
        {
            get;
            set;
        }
    }
}
