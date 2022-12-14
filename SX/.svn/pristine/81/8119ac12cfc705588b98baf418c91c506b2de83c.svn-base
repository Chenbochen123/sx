
/****** Object:  StoredProcedure [dbo].[PagerShow]    Script Date: 02/22/2013 16:22:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*********************************************************************************
*      Copyright (C) 2013 mesnac.com,All Rights Reserved
*      Function:  PagerShow
*      Description:
*			  基于SQL语句的通用分页存储过程
*	   Example:
*	          exec PagerShow2 @sql,4,2,'','',@rowcount output
*********************************************************************************/
ALTER PROCEDURE [dbo].[PagerShow]
 @QueryStr varchar(max), --查询语句
 @PageSize int=10,   --每页的大小(行数)
 @PageCurrent int=1,   --要显示的页
 @FdShow VarChar(max)='', --要显示的字段列表(为空列出所有字段）
 @FdOrder NVarChar(max)='', --排序表达式
 @Rows int  OUTPUT      -- 输出记录数, 如果@rows为null, 则输出记录数, 否则不要输出
 AS
 begin
	 declare @strSQL varchar(max) -- 主语句
	 declare @strTmp varchar(max) -- 临时变量
	 declare @strOrder varchar(400) -- 排序类型
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
