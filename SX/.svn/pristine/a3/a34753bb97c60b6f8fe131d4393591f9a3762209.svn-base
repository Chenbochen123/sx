using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Business.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Data.Interface;
    using Mesnac.Data.Implements;
    using Mesnac.Business.Interface;
    using NBear.Common;
    using System.Data;
    public class EqmSparePartStoreManager : BaseManager<EqmSparePartStore>, IEqmSparePartStoreManager
    {
		#region 属性注入与构造方法
		
        private IEqmSparePartStoreService service;

        public EqmSparePartStoreManager()
        {
            this.service = new EqmSparePartStoreService();
            base.BaseService = this.service;
        }

		public EqmSparePartStoreManager(string connectStringKey)
        {
			this.service = new EqmSparePartStoreService(connectStringKey);
            base.BaseService = this.service;
        }

        public EqmSparePartStoreManager(NBear.Data.Gateway way)
        {
			this.service = new EqmSparePartStoreService(way);
            base.BaseService = this.service;
        }

        #endregion

        #region 查询条件类定义
        public class QueryParams : EqmSparePartStoreService.QueryParams
        {
        }
        #endregion
        public PageResult<EqmSparePartStore> GetTablePageDataBySql(Mesnac.Data.Implements.EqmSparePartStoreService.QueryParams queryParams)
        {
            return this.service.GetTablePageDataBySql(queryParams);
        }

        /// <summary>
        /// @author:yuany 
        /// @datetime:2014年4月10日16:13:40
        /// @functionDesc:备件出入库操作更新实时库存表
        /// </summary>
        /// <param name="storeType">true：入库，false：出库</param>
        /// <param name="sparePartCode">备件代码</param>
        /// <param name="sparePartNum">备件数量</param>
        /// <returns></returns>
        public string UpdateSparePartStore(bool storeType, string sparePartCode,string standards, decimal sparePartNum)
        {

            EntityArrayList<EqmSparePartStore> list = this.service.GetListByWhere(EqmSparePartStore._.SparePartCode == sparePartCode);
            if (list.Count == 0)//尚无此备件的库存数据
            {
                if (storeType)
                {
                    EqmSparePartStore store = new EqmSparePartStore();
                    store.SparePartCode = sparePartCode;
                    store.MajorType = sparePartCode.Substring(0, 1);
                    store.MinorType = sparePartCode.Substring(1, 4);
                    store.Standards = standards;
                    store.CurrentStoreNum = sparePartNum;
                    store.MaxStoreNum = 9999;
                    store.MinStoreNum = 1;
                    store.DeleteFlag = "0";
                    this.service.Insert(store);
                    return "创建此备件库存数据成功[SUCCESS]";
                }
                else
                {
                    return "此备件尚无库存数据，不能进行出库操作[ERROR]";
                }

            }
            else//已有此备件的库存数据
            {
                if (storeType)
                {
                    EqmSparePartStore store = list[0];
                    store.Attach();
                    decimal resultStoreNum = Convert.ToDecimal(store.CurrentStoreNum + sparePartNum);
                    store.CurrentStoreNum = resultStoreNum;
                    if (store.MaxStoreNum < store.CurrentStoreNum)
                    {
                        return "入库后备件数量高于最大库存[ERROR]"; 
                    }
                    this.service.Update(store);
                    return "此备件库存数据更新成功[SUCCESS]";
                }
                else
                {
                    EqmSparePartStore store = list[0];
                    store.Attach();
                    decimal resultStoreNum = Convert.ToDecimal(store.CurrentStoreNum - sparePartNum);
                    if (resultStoreNum < 0)
                    {
                        return "此备件现有库存为：" + store.CurrentStoreNum + ",出库数大于当前现有库存[ERROR]";
                    }
                    store.CurrentStoreNum = resultStoreNum;
                    if (store.MinStoreNum > store.CurrentStoreNum)
                    {
                        return "出库后备件数量低于最小库存[ERROR]";
                    }
                    this.service.Update(store);
                    return "此备件库存数据更新成功[SUCCESS]";
                }
            }
        }
        public DataSet GetSparePartStoreDetail(string sparePartCode)
        {
            return this.service.GetSparePartStoreDetail(sparePartCode);
        }
    }

   
}
