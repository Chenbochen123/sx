using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Mesnac.Business.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Interface;
    public interface IEqmSparePartDetailTypeManager : IBaseManager<EqmSparePartDetailType>
    {
        //获取备件小类信息
        DataSet GetDataByParas( EqmSparePartDetailTypeParams queryParams );
    }
}
