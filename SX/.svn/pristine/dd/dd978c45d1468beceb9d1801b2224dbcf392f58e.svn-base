using System;
using System.Collections.Generic;
using System.Text;

using System.Data;

namespace Mesnac.Data.Interface
{
    using Mesnac.Entity;
    public interface IQmtCheckStandDetailService : IBaseService<QmtCheckStandDetail>
    {
        DataSet GetDataByParas(IQmtCheckStandDetailParams queryParams);
    }

    public interface IQmtCheckStandDetailParams
    {
        string StandId
        {
            get;
            set;
        }
        string ItemCd
        {
            get;
            set;
        }
        string WeightId
        {
            get;
            set;
        }
        string PermMin
        {
            get;
            set;
        }
        string IfMin
        {
            get;
            set;
        }
        string PermMax
        {
            get;
            set;
        }
        string IfMax
        {
            get;
            set;
        }
        string DeleteFlag
        {
            get;
            set;
        }

    }

}
