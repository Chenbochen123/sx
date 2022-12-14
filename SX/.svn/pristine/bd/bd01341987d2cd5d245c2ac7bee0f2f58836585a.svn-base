using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Ext.Net;
using NBear.Common;
using Mesnac.Business.Implements;
using Mesnac.Entity;
using Mesnac.Data.Components;
using Mesnac.Business.Interface;
using Mesnac.Data.Implements;
using System.Data;
using System.Configuration;
using System.Xml;

public partial class Manager_Storage_SapInterfaceDeal_SapOrderReload : Mesnac.Web.UI.Page
{
    private IIncSapOrderReloadManager sapOrderManager = new IncSapOrderReloadManager();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!X.IsAjaxRequest && !IsPostBack)
        {
            txtBeginTime.Text = DateTime.Now.ToString("yyyy-MM-dd");
            txtEndTime.Text = DateTime.Now.ToString("yyyy-MM-dd");
            cbxOperype.Value = "10";
        }
    }

    #region 分页相关方法
    private PageResult<IncSapOrderReload> GetPageResultData(PageResult<IncSapOrderReload> pageParams)
    {
        IncSapOrderReloadManager.QueryParams queryParams = new IncSapOrderReloadManager.QueryParams();
        queryParams.pageParams = pageParams;
        queryParams.mesOrderCode = txtMesOrderCode.Text;
        queryParams.sendBeginDate = txtBeginTime.Text;
        queryParams.sendEndDate = txtEndTime.Text;
        queryParams.sapOrderCode = txtSAPOrderCode.Text;
        queryParams.isUpload = txtIsUpload.Text;
        queryParams.dealState = txtIsError.Text;
        queryParams.deleteFlag = "0";
        queryParams.operType = cbxOperype.SelectedItem.Value;

        return sapOrderManager.GetTablePageDataBySql(queryParams);
    }

    [DirectMethod]
    public object GridPanelBindData(string action, Dictionary<string, object> extraParams)
    {
        StoreRequestParameters prms = new StoreRequestParameters(extraParams);
        PageResult<IncSapOrderReload> pageParams = new PageResult<IncSapOrderReload>();
        pageParams.PageIndex = prms.Page;
        pageParams.PageSize = prms.Limit;
        pageParams.Orderfld = "BillNo desc";

        PageResult<IncSapOrderReload> lst = GetPageResultData(pageParams);
        DataTable data = lst.DataSet.Tables[0];

        int total = lst.RecordCount;

        DataTable dataNull = new DataTable();
        this.storeDetail.DataSource = dataNull;
        this.storeDetail.DataBind();
        return new { data, total };
    }

    protected void RowSelect(object sender, StoreReadDataEventArgs e)
    {
        string mesOrderCode = e.Parameters["MesOrderCode"];
        string sendDate = e.Parameters["SendDate"];

        this.storeDetail.DataSource = sapOrderManager.GetListByWhere(
            IncSapOrderReload._.MesOrderCode == mesOrderCode &&
            IncSapOrderReload._.SendDate == sendDate);
        this.storeDetail.DataBind();
    }
    #endregion

    #region 错误单据重传按钮点击方法
    [Ext.Net.DirectMethod]
    protected void ReUpload_Btn_Click(object sender, DirectEventArgs e)
    {
        Ext.Net.MessageBox msg = new MessageBox();
        //获取选中的错误单据信息
        string json = e.ExtraParams["Values"];
        string result = "";
        string url="";
        string @namespace = "";
        string classname = "";
        Dictionary<string, string>[] orderDic = JSON.Deserialize<Dictionary<string, string>[]>(json);

        //获取配置文件中接口配置
        using (XmlTextReader reader = new XmlTextReader(HttpContext.Current.Server.MapPath("./WebService.config")))
        {
            reader.WhitespaceHandling = WhitespaceHandling.All;
            while (reader.Read())
            {
                if (reader.Name == "WEBSERVICE_URL")
                {
                    reader.Read();
                    url = reader.Value;
                    break;
                }
            }
            while (reader.Read())
            {
                if (reader.Name == "WEBSERVICE_NAMESPACE")
                {
                    reader.Read();
                    @namespace = reader.Value;
                    break;
                }
            }
            while (reader.Read())
            {
                if (reader.Name == "WEBSERVICE_CLASSNAME")
                {
                    reader.Read();
                    classname = reader.Value;
                    break;
                }
            }
             reader.Close();
        }
        foreach (Dictionary<string, string> row in orderDic)
        {
            string mesOrderCode = row["MesOrderCode"];
            DateTime sendDate = Convert.ToDateTime(row["SendDate"]);
            string mesOrderType = "";
            string sapOrderCode = "";
            string sapOrderType = "";
            string cancelFlag = "";
            EntityArrayList<IncSapOrderReload> incSapOrderList = sapOrderManager.GetListByWhere(
                IncSapOrderReload._.MesOrderCode == mesOrderCode &&
                IncSapOrderReload._.SendDate == sendDate);
            if (incSapOrderList.Count > 0)
            {
                if (incSapOrderList[0].DealState.Equals("S"))
                {
                    msg.Alert("提示", "正确返回信息无需重传!").Show();
                    return;
                }
                mesOrderType = incSapOrderList[0].MesOrderType;
                sapOrderCode = incSapOrderList[0].SAPOrderCode;
                sapOrderType = incSapOrderList[0].SAPOrderType;
                cancelFlag = incSapOrderList[0].Ext_2;
                object[] storeinArgs = { mesOrderCode, cancelFlag, "1" };//入库单的参数
                object[] args = { mesOrderCode, sapOrderCode, "1" };//出库和退货的参数
                object[] adjustArgs = { mesOrderCode, sapOrderCode, cancelFlag, "1" };//调拨单的参数
                //生成报产冲产或者报废反报废参数
                string productSerid = "";
                for (int i = 0; i < incSapOrderList.Count; i++)
                {
                    if (i == 0) {
                        productSerid = "'" + incSapOrderList[i].Ext_1 + "'";
                    }
                    else
                    {
                        productSerid += " , '"+ incSapOrderList[i].Ext_1 + "'";
                    }
                }
                switch (mesOrderType)
                {
                    case "1001": ;//入库
                        result = InvokeWebservice(url, @namespace, classname, "ToSapStoreinInfo", storeinArgs).ToString();
                        break;
                    case "1002": ;//出库
                        result = InvokeWebservice(url, @namespace, classname, "ToSapStoreoutInfo", args).ToString();
                        break;
                    case "1003": ;//调拨
                        result = InvokeWebservice(url, @namespace, classname, "ToSapAdjustInfo", adjustArgs).ToString();
                        break;
                    case "1004": ;//退货
                        result = InvokeWebservice(url, @namespace, classname, "ToSapReturninInfo", args).ToString();
                        break;
                    case "1005": ;//退库
                        break;
                    case "1006": ;//调整
                        break;
                    case "1007": ;//结转
                        break;
                    case "0001": ;//报产数据
                        object[] productArgs = { "1", "1", mesOrderCode.Substring(0, 4) + "-" + mesOrderCode.Substring(4, 2) + "-" + mesOrderCode.Substring(6, 2), mesOrderCode.Substring(8, 1), mesOrderCode.Substring(9, 1), productSerid };
                        result = InvokeWebservice(url, @namespace, classname, "SetProductionsUpload", productArgs).ToString();
                        break;
                    case "0002": ;//冲产数据
                        object[] retProductArgs = { "0", "1", mesOrderCode.Substring(0, 4) + "-" + mesOrderCode.Substring(4, 2) + "-" + mesOrderCode.Substring(6, 2), mesOrderCode.Substring(8, 1), mesOrderCode.Substring(9, 1), productSerid };
                        result = InvokeWebservice(url, @namespace, classname, "SetProductionsUpload", retProductArgs).ToString();
                        break;
                    case "0003": ;//报废数据
                        object[] rejectionArgs = { "1","1",mesOrderCode , productSerid};
                        result = InvokeWebservice(url, @namespace, classname, "SetRetproductionsUpload", rejectionArgs).ToString();
                        break;
                    case "0004": ;//反向报废数据
                        object[] retRejectionArgs = { "0", "1", mesOrderCode, productSerid };
                        result = InvokeWebservice(url, @namespace, classname, "SetRetproductionsUpload", retRejectionArgs).ToString();
                        break;
                    default:
                        break;
                }
                IncSapOrderReload orderReload = incSapOrderList[0];
                orderReload.UploadDate = DateTime.Now;
                orderReload.IsUpload = "1";
                sapOrderManager.Update(orderReload);
            }
        }

        pageToolBar.DoRefresh();

        if (result.ToUpper().Equals("OK"))
        {
            msg.Alert("提示", "已成功上传，等待SAP处理返回结果。").Show();
        }
        else
        {
            msg.Alert("提示", result).Show();
        }

    }
    #endregion

    #region 修改SAP订单号
    /// <summary>
    /// 点击修改激发的事件
    /// yuany   2013年1月22日
    /// </summary>
    /// <param name="unit_num"></param>
    [Ext.Net.DirectMethod()]
    public void commandcolumn_direct_edit(string mesOrderCode)
    {
        Ext.Net.MessageBox msg = new MessageBox();
        EntityArrayList<IncSapOrderReload> sapOrderList = sapOrderManager.GetListByWhere(IncSapOrderReload._.MesOrderCode == mesOrderCode);
        if (sapOrderList.Count == 0)
        {
            msg.Alert("提示", "数据异常请联系管理员").Show();
            return;
        }
        if ("1001,1002,1003,1004".IndexOf(sapOrderList[0].MesOrderType) == -1)
        {
            msg.Alert("提示", "非入库单、出库单、退货单、调拨单不允许修改SAP订单号").Show();
            return;
        }
        modify_mes_order_code.Value = sapOrderList[0].MesOrderCode;
        modify_sap_order_code.Value = sapOrderList[0].SAPOrderCode;
        this.winModify.Show();
    }

    /// <summary>
    /// 点击取消按钮激发的事件
    /// yuany   2013年1月22日
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public void BtnCancel_Click(object sender, DirectEventArgs e)
    {
        this.winModify.Close();
    }

    /// <summary>
    /// 点击修改信息中保存按钮激发的事件
    /// yuany   2013年1月22日
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void BtnModifySave_Click(object sender, EventArgs e)
    {
        Ext.Net.MessageBox msg = new MessageBox();
        if (modify_sap_order_code.Value.ToString().Trim() == "")
        {
            msg.Alert("提示", "修改的SAP订单号不允许为空").Show();
            return;
        }
        EntityArrayList<IncSapOrderReload> sapOrderList = sapOrderManager.GetListByWhere(IncSapOrderReload._.MesOrderCode == modify_mes_order_code.Value);
        foreach (IncSapOrderReload sapOrder in sapOrderList)
        {
            IncSapOrderReload temp = sapOrder;
            temp.SAPOrderCode = modify_sap_order_code.Value.ToString();
            sapOrderManager.Update(temp);
        }
        msg.Notify("提示", "修改成功").Show();
        PagingToolbar1.DoRefresh();
        this.winModify.Close();
    }
    #endregion

    #region 动态调用webservice
    private object InvokeWebservice(string url, string @namespace, string classname, string methodname, object[] args)
    {
        try
        {
            System.Net.WebClient wc = new System.Net.WebClient();
            System.IO.Stream stream = wc.OpenRead(url + "?WSDL");
            System.Web.Services.Description.ServiceDescription sd = System.Web.Services.Description.ServiceDescription.Read(stream);
            System.Web.Services.Description.ServiceDescriptionImporter sdi = new System.Web.Services.Description.ServiceDescriptionImporter();
            sdi.AddServiceDescription(sd, "", "");
            System.CodeDom.CodeNamespace cn = new System.CodeDom.CodeNamespace(@namespace);
            System.CodeDom.CodeCompileUnit ccu = new System.CodeDom.CodeCompileUnit();
            ccu.Namespaces.Add(cn);
            sdi.Import(cn, ccu);

            Microsoft.CSharp.CSharpCodeProvider csc = new Microsoft.CSharp.CSharpCodeProvider();
            System.CodeDom.Compiler.ICodeCompiler icc = csc.CreateCompiler();

            System.CodeDom.Compiler.CompilerParameters cplist = new System.CodeDom.Compiler.CompilerParameters();
            cplist.GenerateExecutable = false;
            cplist.GenerateInMemory = true;
            cplist.ReferencedAssemblies.Add("System.dll");
            cplist.ReferencedAssemblies.Add("System.XML.dll");
            cplist.ReferencedAssemblies.Add("System.Web.Services.dll");
            cplist.ReferencedAssemblies.Add("System.Data.dll");

            System.CodeDom.Compiler.CompilerResults cr = icc.CompileAssemblyFromDom(cplist, ccu);
            if (true == cr.Errors.HasErrors)
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                foreach (System.CodeDom.Compiler.CompilerError ce in cr.Errors)
                {
                    sb.Append(ce.ToString());
                    sb.Append(System.Environment.NewLine);
                }
                throw new Exception(sb.ToString());
            }
            System.Reflection.Assembly assembly = cr.CompiledAssembly;
            Type t = assembly.GetType(@namespace + "." + classname, true, true);
            object obj = Activator.CreateInstance(t);
            System.Reflection.MethodInfo mi = t.GetMethod(methodname);
            return mi.Invoke(obj, args);
        }
        catch (Exception ex)
        {
            return "接口服务异常，请联系管理员!";
        }
    } 
    #endregion
}