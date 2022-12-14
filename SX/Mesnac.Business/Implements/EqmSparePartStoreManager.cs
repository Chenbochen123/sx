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
		#region ����ע���빹�췽��
		
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

        #region ��ѯ�����ඨ��
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
        /// @datetime:2014��4��10��16:13:40
        /// @functionDesc:����������������ʵʱ����
        /// </summary>
        /// <param name="storeType">true����⣬false������</param>
        /// <param name="sparePartCode">��������</param>
        /// <param name="sparePartNum">��������</param>
        /// <returns></returns>
        public string UpdateSparePartStore(bool storeType, string sparePartCode,string standards, decimal sparePartNum)
        {

            EntityArrayList<EqmSparePartStore> list = this.service.GetListByWhere(EqmSparePartStore._.SparePartCode == sparePartCode);
            if (list.Count == 0)//���޴˱����Ŀ������
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
                    return "�����˱���������ݳɹ�[SUCCESS]";
                }
                else
                {
                    return "�˱������޿�����ݣ����ܽ��г������[ERROR]";
                }

            }
            else//���д˱����Ŀ������
            {
                if (storeType)
                {
                    EqmSparePartStore store = list[0];
                    store.Attach();
                    decimal resultStoreNum = Convert.ToDecimal(store.CurrentStoreNum + sparePartNum);
                    store.CurrentStoreNum = resultStoreNum;
                    if (store.MaxStoreNum < store.CurrentStoreNum)
                    {
                        return "���󱸼��������������[ERROR]"; 
                    }
                    this.service.Update(store);
                    return "�˱���������ݸ��³ɹ�[SUCCESS]";
                }
                else
                {
                    EqmSparePartStore store = list[0];
                    store.Attach();
                    decimal resultStoreNum = Convert.ToDecimal(store.CurrentStoreNum - sparePartNum);
                    if (resultStoreNum < 0)
                    {
                        return "�˱������п��Ϊ��" + store.CurrentStoreNum + ",���������ڵ�ǰ���п��[ERROR]";
                    }
                    store.CurrentStoreNum = resultStoreNum;
                    if (store.MinStoreNum > store.CurrentStoreNum)
                    {
                        return "����󱸼�����������С���[ERROR]";
                    }
                    this.service.Update(store);
                    return "�˱���������ݸ��³ɹ�[SUCCESS]";
                }
            }
        }
        public DataSet GetSparePartStoreDetail(string sparePartCode)
        {
            return this.service.GetSparePartStoreDetail(sparePartCode);
        }
    }

   
}
