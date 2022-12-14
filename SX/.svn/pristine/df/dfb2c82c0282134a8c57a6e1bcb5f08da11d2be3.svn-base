using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using NBear.Common;
using Mesnac.Business.Implements;
using Mesnac.Entity;
using Mesnac.Data.Components;
using Mesnac.Business.Interface;
using Mesnac.Data.Implements;

public partial class Manager_Storage_StorageAutoScroll : System.Web.UI.Page
{
    protected PstStorageManager storageManager = new PstStorageManager();

    public void LoadData()
    {
        string storageID = string.Empty;
        if (Request.QueryString["StorageID"] != null)
            storageID = Request.QueryString["StorageID"].ToString();
        string storagePlaceID = string.Empty;
        if (Request.QueryString["StoragePlaceID"] != null)
            storagePlaceID = Request.QueryString["StoragePlaceID"].ToString();
        string materCode = string.Empty;
        if (Request.QueryString["MaterCode"] != null)
            materCode = Request.QueryString["MaterCode"].ToString();
        DataSet ds = GetStorageInfo(storageID, storagePlaceID, materCode);
        gvStorage.DataSource = ds;
        gvStorage.DataBind();

        for (int i = 0; i < gvStorage.Rows.Count; i++)
        {
            DataRowView mydrv = ds.Tables[0].DefaultView[i];
            string pname = Convert.ToString(mydrv["Status"]);
            if (pname.Equals("发放中"))
            {
                gvStorage.Rows[i].BackColor = System.Drawing.Color.LightGreen;//gvStorage.Rows[i].Cells[4].BackColor更改第i+1行，第4+1列的单元格背景颜色
           
                mydrv["Status"] = "发放中";
              
            }
        }
      
        lblMessage.Text = "所有库存数据量：" + ds.Tables[0].Rows.Count.ToString() + "条   ";
    }
    public DataSet GetStorageInfo(string StorageID, string StoragePlaceID, string MaterCode)
    {
        string sql = @"select A.Barcode, A.ProductNo, A.StorageID, B.StorageName, A.StoragePlaceID, C.StoragePlaceName, 
                                A.FactoryID, E.FacSimpleName, A.MaterCode, D.MaterialName, A.ProcDate, CONVERT(varchar(10), 
                                DATEADD(DAY, D.ValidDate, A.ProcDate), 120) ValidDate, A.Num, A.PieceWeight, A.RealWeight, 
                                A.RecordDate, SUBSTRING(A.Barcode, 15, 4) FacCode, SUBSTRING(A.Barcode, 10, 5) SendDate, 
                                dbo.FuncGetStorageStatus(A.Barcode, A.StorageID,  A.StoragePlaceID) Status,A.Batch
                            from PstStorage A
                            left join BasStorage B on A.StorageID = B.StorageID
                            left join BasStoragePlace C on A.StoragePlaceID = C.StoragePlaceID
                            left join BasMaterial D on A.MaterCode = D.MaterialCode
                            left join BasFactoryInfo E on A.FactoryID = E.ObjID
                            where 1 = 1 and A.RealWeight > 0";
        if (!string.IsNullOrEmpty(StorageID))
            sql += " AND A.StorageID = '" + StorageID + "'";
        if (!string.IsNullOrEmpty(StoragePlaceID))
            sql += " AND A.StoragePlaceID = '" + StoragePlaceID + "'";
        if (!string.IsNullOrEmpty(MaterCode))
            sql += " AND A.MaterCode = '" + MaterCode + "'";
        return storageManager.GetBySql(sql).ToDataSet();
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        LoadData();
    }

    //protected void timer1_Tick(object sender, EventArgs e)
    //{
    //    LoadData();
    //}

    protected void gvStorage_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "if(this!=prev){c=this.style.backgroundColor;this.style.backgroundColor='#A7A7A7'}");//当鼠标停留时更改背景色
            e.Row.Attributes.Add("onmouseout", "if(this!=prev){this.style.backgroundColor=c}");//当鼠标移开时还原背景色
            e.Row.Attributes["style"] = "Cursor:hand";//设置悬浮鼠标指针形状为"小手"
            e.Row.Attributes.Add("onclick", "selectx(this)");//调用页面javascript
        }

        //e.Row.Cells[0].Width = 200;
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        LoadData();
    }
}