---WCS HCM Cross Check 
delete from [Zeris].[dbo].[WCS_Cross_Check_HCM]

insert into [Zeris].[dbo].[WCS_Cross_Check_HCM]
SELECT
          COUNT(*),rp_work_id
         FROM 
          (
          SELECT 
           w.rp_work_id,rpd_representation AS work_status,
           (
           CONVERT(datetime2, substring(m.rpd_data, 1, 23))
           ) AS UploadTime,
           substring(m.rpd_data, 30, 200) as rpd_data
          FROM
           [LINK_TO_WCS].[PickDirector].[dbo].rp_work w join [LINK_TO_WCS].[PickDirector].[dbo].rpd_misc m on (w.rp_misc_ftk = m.rp_misc_tk)
           INNER JOIN [LINK_TO_WCS].[PickDirector].[dbo].rpd_constant  ON w.rp_status = CAST(rpd_constant.rp_value AS SMALLINT) and rpd_constant.rp_name = 'WorkStatus'
          WHERE
           m.rp_type = 'HostUpload' AND (upper(rpd_representation) like '%COMPLETE%' or upper(rpd_representation) like '%ClOSE%')
           and
           CONVERT(datetime2, substring(m.rpd_data, 1, 23))  >= (SELECT CONVERT (date, SYSDATETIME()))
                   ) A
          GROUP BY rp_work_id
          HAVING COUNT(*) < 2


	SET NOCOUNT ON;
	
	declare @sqlqry varchar(8000)
	declare @column1name varchar(50)

	

	SET @Column1Name = '[sep=,' + CHAR(13) + CHAR(10) + 'COUNT]'
	select @sqlqry='set nocount on; select COUNT'+@column1name+',rp_work_id
								FROM [Zeris].[dbo].[WCS_Cross_Check_HCM]'
	  --Email for IT team
    EXEC msdb.dbo.sp_send_dbmail
    @profile_name = 'Report_Callplan',
    @recipients = 'vnhelpdesk@zuelligpharma.com;nthy@zuelligpharma.com;vtntran@zuelligpharma.com;vstung@zuelligpharma.com;nvson@zuelligpharma.com',
    @query = @sqlqry,
    @subject = 'WCS HCM Cross Check',
    @attach_query_result_as_file = 1 ,
    @body = 'WCS HCM Cross Check ',
    @query_attachment_filename = 'WCS_HCM_Cross_Check.csv',
    @query_result_separator=',',
    @query_result_width =32767,
    @query_result_no_padding=1;
    SELECT 'Sending Report WCS HCM Cross Check email to IT team is completed'



--- Ha Noi 
delete from [Zeris].[dbo].[WCS_Cross_Check_HN]

insert into [Zeris].[dbo].[WCS_Cross_Check_HN]
SELECT
          COUNT(*),rp_work_id
         FROM 
          (
          SELECT 
           w.rp_work_id,rpd_representation AS work_status,
           (
           CONVERT(datetime2, substring(m.rpd_data, 1, 23))
           ) AS UploadTime,
           substring(m.rpd_data, 30, 200) as rpd_data
          FROM
           [LINK_TO_WCS_HN].[PickDirector].[dbo].rp_work w join [LINK_TO_WCS_HN].[PickDirector].[dbo].rpd_misc m on (w.rp_misc_ftk = m.rp_misc_tk)
           INNER JOIN [LINK_TO_WCS_HN].[PickDirector].[dbo].rpd_constant  ON w.rp_status = CAST(rpd_constant.rp_value AS SMALLINT) and rpd_constant.rp_name = 'WorkStatus'
          WHERE
           m.rp_type = 'HostUpload' AND (upper(rpd_representation) like '%COMPLETE%' or upper(rpd_representation) like '%ClOSE%')
           and
           CONVERT(datetime2, substring(m.rpd_data, 1, 23))  >= (SELECT CONVERT (date, SYSDATETIME()))
                   ) A
          GROUP BY rp_work_id
          HAVING COUNT(*) < 2


	SET @Column1Name = '[sep=,' + CHAR(13) + CHAR(10) + 'COUNT]'
	select @sqlqry='set nocount on; select COUNT'+@column1name+',rp_work_id
								FROM [Zeris].[dbo].[WCS_Cross_Check_HN]'
	  --Email for IT team
    EXEC msdb.dbo.sp_send_dbmail
    @profile_name = 'Report_Callplan',
    @recipients = 'vnhelpdesk@zuelligpharma.com;tmhtam@zuelligpharma.com;hlanh@zuelligpharma.com;ltcuong@zuelligpharma.com',
    @query = @sqlqry,
    @subject = 'WCS HN Cross Check',
    @attach_query_result_as_file = 1 ,
    @body = 'WCS HN Cross Check ',
    @query_attachment_filename = 'WCS_HN_Cross_Check.csv',
    @query_result_separator=',',
    @query_result_width =32767,
    @query_result_no_padding=1;
    SELECT 'Sending Report WCS HN Cross Check email to IT team is completed'
