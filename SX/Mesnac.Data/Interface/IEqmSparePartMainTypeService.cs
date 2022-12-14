using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Mesnac.Data.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    public interface IEqmSparePartMainTypeService : IBaseService<EqmSparePartMainType>
    {
        //获取备件大类信息
        DataSet GetDataByParas( EqmSparePartMainTypeParams queryParams );
    }
    public class EqmSparePartMainTypeParams
    {
        public string objID
        {
            get;
            set;
        }
        public string mainTypeName
        {
            get;
            set;
        }
        public string deleteFlag
        {
            get;
            set;
        }
        public PageResult<EqmSparePartMainType> pageResult
        {
            get;
            set;
        }
    }
}
