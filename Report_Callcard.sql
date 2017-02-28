USE [Zeris]
GO
/****** Object:  StoredProcedure [dbo].[Report_Callcard]    Script Date: 2/21/2017 4:41:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Ma Truong Chu
-- Create date: 26/01/2016
-- Description:	ZPP MTD Online Call Card Check
-- =============================================
ALTER PROCEDURE [dbo].[Report_Callcard]
	-- Add the parameters for the stored procedure here
	@aID varchar(12)
AS

BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	
	SET NOCOUNT ON;
	
	declare @sqlqry varchar(8000)
	declare @column1name varchar(50)

	SET @Column1Name = '[sep=,' + CHAR(13) + CHAR(10) + 'SLSMN]'
	
	select @sqlqry='set nocount on; select SLSMN'+@column1name+',SMDESC,PDTCDE,PDTDES,CUST,CNAME
					,CMGRP2,CMGRP3,CMGRP4,CADD2,ZCMTYP,CPLUPD,CPLUPT
					from [192.168.17.170].[CallCard].dbo.[Report_Callcard] '
	

      --Email for IT team
    EXEC msdb.dbo.sp_send_dbmail
    @profile_name = 'Report_Callplan',
    @recipients = 'dple@zuelligpharma.com',
    @query = @sqlqry,
    @subject = 'ZPP MTD Online Call Card Check',
    @attach_query_result_as_file = 1 ,
    @body = 'Bao cao gui theo dang cong gop du lieu cua moi thang va chi hien thi du lieu nhung SC nao truy cap website http://callplan.zuelligpharma.com.vn/ .
     Neu gap van de truy cap website, vui long lien he voi Le Duy Phuc - IT: Tel +84 8 3910 2650 (Ext 402) hoac email: dple@zuelligpharma.com',
    @query_attachment_filename = 'ZPP MTD Online Call Card Check.csv',
    @query_result_separator=',',
    @query_result_width =32767,
    @query_result_no_padding=1;
    SELECT 'Sending email IT team is completed' 

    --Email for Sales team
    EXEC msdb.dbo.sp_send_dbmail
    @profile_name = 'Report_Callplan',
    @recipients = 'VNZP-ASMBU5Nationwide@zuelligpharma.com;vnhelpdesk@zuelligpharma.com;nhkoanh@zuelligpharma.com',
    @query = @sqlqry,
    @subject = 'ZPP MTD Online Call Card Check',
    @attach_query_result_as_file = 1 ,
    @body = 'Bao cao gui theo dang cong gop du lieu cua moi thang va chi hien thi du lieu nhung SC nao truy cap website http://callplan.zuelligpharma.com.vn/ .
     Neu gap van de truy cap website, vui long lien he voi Le Duy Phuc - IT: Tel +84 8 3910 2650 (Ext 402) hoac email: dple@zuelligpharma.com',
    @query_attachment_filename = 'ZPP MTD Online Call Card Check.csv',
    @query_result_separator=',',
    @query_result_width =32767,
    @query_result_no_padding=1;
    SELECT 'Sending email IT team is completed' 

END
