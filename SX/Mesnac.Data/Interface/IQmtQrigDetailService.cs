using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Mesnac.Data.Interface
{
    using Mesnac.Data.Components;
    using Mesnac.Entity;

    public interface IQmtQrigDetailService : IBaseService<QmtQrigDetail>
    {
        DataSet GetDataByParas(IQmtQrigDetailParams queryParams);
    }

    public interface IQmtQrigDetailParams
    {
        string SeqNo { get; set; }
        string DeleteFlag { get; set; }

        PageResult<QmtQrigDetail> PageResult { get; set; }

    }
}
