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
    public class PptCurveDataManager : BaseManager<PptCurveData>, IPptCurveDataManager
    {
		#region 属性注入与构造方法
		
        private IPptCurveDataService service;

        public PptCurveDataManager()
        {
            this.service = new PptCurveDataService();
            base.BaseService = this.service;
        }

		public PptCurveDataManager(string connectStringKey)
        {
			this.service = new PptCurveDataService(connectStringKey);
            base.BaseService = this.service;
        }

        public PptCurveDataManager(NBear.Data.Gateway way)
        {
			this.service = new PptCurveDataService(way);
            base.BaseService = this.service;
        }

        #endregion


        /// <summary>
        /// 解析曲线数据库数据
        /// 孙本强 @ 2013-04-03 12:05:51
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public List<PptCurve> IniPptCurveList(PptCurveData data)
        {
            List<PptCurve> Result = new List<PptCurve>();
            string ss = data.MixingTime;
            int count = ss.Split(':').Length;
            for (int i = 0; i < count; i++)
            {
                try
                {
                    PptCurve p = new PptCurve();
                    p.Barcode = data.Barcode;
                    p.CurveData = data.CurveData;
                    p.PlanDate = data.PlanDate;
                    p.PlanID = data.PlanID;
                    p.SerialID = data.SerialID == null ? string.Empty : ((int)data.SerialID).ToString();
                    p.IfSubed = data.IfSubed == null ? string.Empty : data.IfSubed;
                    p.SecondSpan = Convert.ToInt32(data.MixingTime.Split(':')[i]);
                    p.MixingTime = ((DateTime)data.StartDateTime).AddSeconds(p.SecondSpan);
                    if(!String.IsNullOrEmpty(data.MixingTemp))
                    {
                        p.MixingTemp = Convert.ToDecimal(data.MixingTemp.Split(':')[i]);
                    }
                    else
                    {
                        p.MixingTemp = 0;
                    }
                    if (!String.IsNullOrEmpty(data.MixingPower))
                    {
                        p.MixingPower = Convert.ToDecimal(data.MixingPower.Split(':')[i]);
                    }
                    else
                    {
                        p.MixingPower = 0;
                    }
                    if (!String.IsNullOrEmpty(data.MixingEnergy))
                    {
                        p.MixingEnergy = Convert.ToDecimal(data.MixingEnergy.Split(':')[i]);
                    }
                    else
                    {
                        p.MixingEnergy = 0;
                    }
                    if (!String.IsNullOrEmpty(data.MixingPress))
                    {
                        p.MixingPress = Convert.ToDecimal(data.MixingPress.Split(':')[i]);
                    }
                    else
                    {
                        p.MixingPress = 0;
                    }
                    if (!String.IsNullOrEmpty(data.MixingSpeed))
                    {
                        p.MixingSpeed = Convert.ToDecimal(data.MixingSpeed.Split(':')[i]);
                    }
                    else
                    {
                        p.MixingSpeed = 0;
                    }
                    if (!String.IsNullOrEmpty(data.MixingPosition))
                    {
                        p.MixingPosition = Convert.ToDecimal(data.MixingPosition.Split(':')[i]);
                    }
                    else
                    {
                        p.MixingPosition = 0;
                    }
                    Result.Add(p);
                }
                catch { }
            }
            return Result;
        }
    }


    /// <summary>
    /// PptCurve 实现类
    /// 孙本强 @ 2013-04-03 12:45:20
    /// </summary>
    /// <remarks></remarks>
    public class PptCurve
    {
        /// <summary>
        /// Gets or sets the barcode.
        /// 孙本强 @ 2013-04-03 12:45:21
        /// </summary>
        /// <value>The barcode.</value>
        /// <remarks></remarks>
        public string Barcode { get; set; }
        /// <summary>
        /// Gets or sets the curve data.
        /// 孙本强 @ 2013-04-03 12:45:21
        /// </summary>
        /// <value>The curve data.</value>
        /// <remarks></remarks>
        public string CurveData { get; set; }
        /// <summary>
        /// Gets or sets the plan date.
        /// 孙本强 @ 2013-04-03 12:45:21
        /// </summary>
        /// <value>The plan date.</value>
        /// <remarks></remarks>
        public string PlanDate { get; set; }
        /// <summary>
        /// Gets or sets the plan ID.
        /// 孙本强 @ 2013-04-03 12:45:21
        /// </summary>
        /// <value>The plan ID.</value>
        /// <remarks></remarks>
        public string PlanID { get; set; }
        /// <summary>
        /// Gets or sets the serial ID.
        /// 孙本强 @ 2013-04-03 12:45:21
        /// </summary>
        /// <value>The serial ID.</value>
        /// <remarks></remarks>
        public string SerialID { get; set; }
        /// <summary>
        /// Gets or sets the mixing time.
        /// 孙本强 @ 2013-04-03 12:45:21
        /// </summary>
        /// <value>The mixing time.</value>
        /// <remarks></remarks>
        public DateTime MixingTime { get; set; }
        /// <summary>
        /// Gets or sets the second span.
        /// 孙本强 @ 2013-04-03 12:45:21
        /// </summary>
        /// <value>The second span.</value>
        /// <remarks></remarks>
        public int SecondSpan { get; set; }
        /// <summary>
        /// Gets or sets the mixing temp.
        /// 孙本强 @ 2013-04-03 12:45:21
        /// </summary>
        /// <value>The mixing temp.</value>
        /// <remarks></remarks>
        public decimal MixingTemp { get; set; }
        /// <summary>
        /// Gets or sets the mixing power.
        /// 孙本强 @ 2013-04-03 12:45:21
        /// </summary>
        /// <value>The mixing power.</value>
        /// <remarks></remarks>
        public decimal MixingPower { get; set; }
        /// <summary>
        /// Gets or sets the mixing energy.
        /// 孙本强 @ 2013-04-03 12:45:21
        /// </summary>
        /// <value>The mixing energy.</value>
        /// <remarks></remarks>
        public decimal MixingEnergy { get; set; }
        /// <summary>
        /// Gets or sets the mixing press.
        /// 孙本强 @ 2013-04-03 12:45:21
        /// </summary>
        /// <value>The mixing press.</value>
        /// <remarks></remarks>
        public decimal MixingPress { get; set; }
        /// <summary>
        /// Gets or sets the mixing speed.
        /// 孙本强 @ 2013-04-03 12:45:22
        /// </summary>
        /// <value>The mixing speed.</value>
        /// <remarks></remarks>
        public decimal MixingSpeed { get; set; }
        /// <summary>
        /// Gets or sets if subed.
        /// 孙本强 @ 2013-04-03 12:45:22
        /// </summary>
        /// <value>If subed.</value>
        /// <remarks></remarks>
        public string IfSubed { get; set; }
        /// <summary>
        /// Gets or sets if subed.
        /// 袁洋 @ 2014年2月21日10:40:01
        /// </summary>
        /// <value>The mixing position</value>
        /// <remarks></remarks>
        public decimal MixingPosition { get; set; }
        public decimal L1 { get; set; }
        public decimal L2 { get; set; }
        public decimal L3 { get; set; }
        public decimal L4 { get; set; }
        public decimal L5 { get; set; }
        public decimal L6 { get; set; }
        public decimal L7 { get; set; }
        public decimal L8 { get; set; }
    }
}
