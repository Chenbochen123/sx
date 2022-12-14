
/****** Object:  StoredProcedure [dbo].[PagerShow]    Script Date: 02/22/2013 16:22:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*********************************************************************************
*      Copyright (C) 2013 mesnac.com,All Rights Reserved
*      Function:  PagerShow
*      Description:
*			  ����SQL����ͨ�÷�ҳ�洢����
*	   Example:
*	          exec PagerShow2 @sql,4,2,'','',@rowcount output
*********************************************************************************/
ALTER PROCEDURE [dbo].[PagerShow]
 @QueryStr varchar(max), --��ѯ���
 @PageSize int=10,   --ÿҳ�Ĵ�С(����)
 @PageCurrent int=1,   --Ҫ��ʾ��ҳ
 @FdShow VarChar(max)='', --Ҫ��ʾ���ֶ��б�(Ϊ���г������ֶΣ�
 @FdOrder NVarChar(max)='', --������ʽ
 @Rows int  OUTPUT      -- �����¼��, ���@rowsΪnull, �������¼��, ����Ҫ���
 AS
 begin
	 declare @strSQL varchar(max) -- �����
	 declare @strTmp varchar(max) -- ��ʱ����
	 declare @strOrder varchar(400) -- ��������
	 declare @head VarChar(max)
	 declare @Sql varchar(max)
	 if (@FdShow ='')
	 set @FdShow = '*'
	 if (@FdOrder='')
	 set @strOrder = ' order by getdate()'
	 else
	 set @strOrder = ' Order by ' + @FdOrder
	
	 set @QueryStr=' (' + @QueryStr + ') abc'
	 declare @lbuseidentity nvarchar(max)
	 set @lbuseidentity = 'select @rows = count(*) from '+@QueryStr
	 exec sp_executesql @lbuseidentity, N'@Rows int out', @Rows out
	 declare @StartNums varchar(20)
	 declare @EndNums varchar(20)
	 set @StartNums = (@PageSize*(@PageCurrent-1)) + 1
	 set @EndNums = (@PageSize*@PageCurrent)  
	 set @Sql = ' with t_rowtable as ( select row_number() over(' + @strOrder + ' ) as row_number,' + @FdShow + ' from ' + @QueryStr + ' ) select ' + @FdShow + ' from t_rowtable where row_number>=' + @StartNums + ' and row_number<=' + @EndNums + @strOrder
	 exec (@Sql)
 end
