
/****** Object:  StoredProcedure [dbo].[MesnacPaging]    Script Date: 02/22/2013 16:22:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*********************************************************************************
*      Copyright (C) 2013 mesnac.com,All Rights Reserved
*      Function:  MesnacPaging
*      Description:
*			  基于表通用分页存储过程
*	   Example:
*	          MesnacPaging @TableName='表名',@Orderfld='排序列名'
*********************************************************************************/
ALTER PROCEDURE [dbo].[MesnacPaging]
(
	@TableName		nvarchar(50),			-- 表名
	@ReturnFields	nvarchar(2000) = '*',	-- 需要返回的列 
	@PageSize		int = 10,				-- 每页记录数
	@PageIndex		int = 1,				-- 当前页码
	@Where			nvarchar(2000) = '',	-- 查询条件
	@Orderfld		nvarchar(2000),			-- 排序字段名 最好为唯一主键
	@OrderType		int = 1					-- 排序类型 1:降序 其它为升序
	
)
AS
    DECLARE @TotalRecord int
	DECLARE @TotalPage int
	DECLARE @CurrentPageSize int
    DECLARE @TotalRecordForPageIndex int
    DECLARE @OrderBy nvarchar(255)
    DECLARE @CutOrderBy nvarchar(255)
	
	if @OrderType = 1
		BEGIN
			set @OrderBy = ' Order by ' + REPLACE(@Orderfld,',',' desc,') + ' desc '
			set @CutOrderBy = ' Order by '+ REPLACE(@Orderfld,',',' asc,') + ' asc '
		END
	else
		BEGIN
			set @OrderBy = ' Order by ' +  REPLACE(@Orderfld,',',' asc,') + ' asc '
			set @CutOrderBy = ' Order by '+ REPLACE(@Orderfld,',',' desc,') + ' desc '			
		END
	
	
        -- 记录总数
	declare @countSql nvarchar(4000)  
	set @countSql='SELECT @TotalRecord=Count(*) From '+@TableName+' '+@Where
	execute sp_executesql @countSql,N'@TotalRecord int out',@TotalRecord out
	
	SET @TotalPage=(@TotalRecord-1)/@PageSize+1
	SET @CurrentPageSize=@PageSize
        IF(@TotalPage=@PageIndex)
	BEGIN
		SET @CurrentPageSize=@TotalRecord%@PageSize
		IF(@CurrentPageSize=0)
			SET @CurrentPageSize=@PageSize
	END
	-- 返回记录
	set @TotalRecordForPageIndex=@PageIndex*@PageSize
	exec('SELECT * FROM
		(SELECT TOP '+@CurrentPageSize+' * FROM
			(SELECT TOP '+@TotalRecordForPageIndex+' '+@ReturnFields+'
			FROM '+@TableName+' '+@Where+' '+@OrderBy+') TB2
		'+@CutOrderBy+') TB3
              '+@OrderBy)
	-- 返回总页数和总记录数
	SELECT @TotalPage as PageCount,@TotalRecord as RecordCount
