using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Mesnac.Data.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Data.Implements;
    public interface IEqm_downikindService : IBaseService<Eqm_downikind>
    {
        //��ȡͣ��������Ϣ
        DataSet GetDataByParas(Eqm_downikind queryParams);
    }
    public class Eqm_downikindParams
    {
        public string Mp_ikindcode
        {
            get;
            set;
        }
        public string Mp_mkindcode
        {
            get;
            set;
        }
        public string Mp_ikindname
        {
            get;
            set;
        }
    }
}
