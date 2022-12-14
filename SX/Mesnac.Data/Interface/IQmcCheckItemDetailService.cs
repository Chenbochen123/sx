using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    public interface IQmcCheckItemDetailService : IBaseService<QmcCheckItemDetail>
    {
        //获取下一个具体项目编号
        string GetNextDetailId();
    }
}
