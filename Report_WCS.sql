USE [Zeris]
GO
/****** Object:  StoredProcedure [dbo].[Report_WCS]    Script Date: 2/21/2017 4:40:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Le Duy Phuc
-- Create date: 2017-01-10
-- Description:	report for WCS 
-- =============================================
ALTER PROCEDURE [dbo].[Report_WCS]
	-- Add the parameters for the stored procedure here
	@aID varchar(12)
AS

BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.

	---RoutingHistory.sql
delete from [Zeris].[dbo].[WCS_RoutingHistory]

insert into [Zeris].[dbo].[WCS_RoutingHistory]
select 
    c.cd_carrier_id, 
    --n.cd_node_id,
    n.cd_node_name,
    m.cd_divert_response_reason,
    -- DATEADD(day, DATEDIFF(day, 0, m.cd_time), 0) as cd_time
	cd_time as [Time]
    --,m.* 
from 
    [LINK_TO_WCS].[RouteDirector].[dbo].cd_carvisits_map m join [LINK_TO_WCS].[RouteDirector].[dbo].cd_carrier c on (c.cd_carrier_key = m.cd_carrier_fk)
                left join [LINK_TO_WCS].[RouteDirector].[dbo].cd_node n on (m.cd_node_fk = n.cd_node_key)
where
    m.cd_divert_response_reason is not null
and n.cd_node_name not in ('D101', 'D102')
and m.cd_time > DATEADD(day, DATEDIFF(day, 0, GETDATE()), -1)
and m.cd_time < DATEADD(day, DATEDIFF(day, 0, GETDATE()), 0)
order by 
    m.cd_time


---------------CartonCountQuery.sql
delete from [Zeris].[dbo].[WCS_CartonCountQuery]
 
insert into [Zeris].[dbo].[WCS_CartonCountQuery]
select
    SUM( case when ct.rp_con_type_id = 'FULLCASE' then  1 else  0 end ) as FULLCASE,
    SUM(case when ct.rp_con_type_id = 'CARTON'    then 1 else 0 end) as CARTON,
    --SUM(IIF(ct.rp_con_type_id = 'TOTE'      , 1, 0)) as TOTE,
    --SUM(IIF(ct.rp_con_type_id = 'CT'        , 1, 0)) as CT,
    SUM(case when ct.rp_con_type_id = 'BAG'  then 1 else 0 end) as BAG,
	count(distinct w.rp_work_tk) as WorkAssignments

from
    [LINK_TO_WCS].[PickDirector].[dbo].rp_work w join [LINK_TO_WCS].[PickDirector].[dbo].rp_allocation a on (w.rp_work_tk = a.rp_work_ftk)
            join [LINK_TO_WCS].[PickDirector].[dbo].rp_container c on (a.rp_container_ftk = c.rp_container_tk)
            join [LINK_TO_WCS].[PickDirector].[dbo].rp_con_type ct on (c.rp_con_type_ftk = ct.rp_con_type_tk)
where
    w.rp_markedForDeletion is null
and c.rp_markedForDeletion is null
and c.rp_created >= DATEADD(day, DATEDIFF(day, 0, GETDATE()), -1)
and c.rp_created < DATEADD(day, DATEDIFF(day, 0, GETDATE()), 0)
and w.rp_type <> 2

---------------ContainerQA.sql
delete from [Zeris].[dbo].[WCS_ContainerQA] 

insert into [Zeris].[dbo].[WCS_ContainerQA]
select 
    s.rp_timestamp, 
    s.rp_dest_container 
from 
    [LINK_TO_WCS].[PickDirector].[dbo].[rp_special_event] s 
where s.rp_event_type = 'LOG_QA'
and s.rp_operator_id is not null
and rp_timestamp >= DATEADD(day, DATEDIFF(day, 0, GETDATE()), -1)
and rp_timestamp < DATEADD(day, DATEDIFF(day, 0, GETDATE()), 0)


-------------------SC WA Count
delete from [Zeris].[dbo].[WCS_SC_WA_Count]

;with SCWorks as (
    select 
        distinct
        w.rp_work_id,
        z.rp_zone_id,
        w.rp_received
    from
        [LINK_TO_WCS].[PickDirector].[dbo].rp_work w join [LINK_TO_WCS].[PickDirector].[dbo].rp_passage p on (w.rp_work_tk = p.rp_work_ftk)
                join [LINK_TO_WCS].[PickDirector].[dbo].rp_zone z on (p.rp_zone_ftk = z.rp_zone_tk)
    where
        z.rp_zone_id in ('P01', 'P02', 'P03')
    -- and w.rp_full_case_flag = 0 and w.rp_full_pallet_flag = 0
	and w.rp_received >= DATEADD(day, DATEDIFF(day, 0, GETDATE()), -1)
	and w.rp_received < DATEADD(day, DATEDIFF(day, 0, GETDATE()), 0)
)
insert into [Zeris].[dbo].[WCS_SC_WA_Count]
SELECT
    P02_03Only = (
                select
                    count(distinct s.rp_work_id) as P02_03Total
                from
                    SCWorks s
                where
                    s.rp_zone_id <> 'P01'
                ),
    P01Only = (
                select
                    count(distinct s.rp_work_id) as P02_03Total
                from
                    SCWorks s
                where
                    s.rp_zone_id = 'P01'
                ),
    
    BothLevels = (
                    select
                        count(distinct s.rp_work_id) as P02_03Total
                    from
                        SCWorks s join SCWorks s2 on (s.rp_work_id = s2.rp_work_id)
                    where
                        s.rp_zone_id = 'P01'
                    and s2.rp_zone_id in ('P02', 'P03')
                )


----------------FC Zone Container Count
delete from [Zeris].[dbo].[WCS_FC_Zone_Container_Count]


;with FCWorks as (
    select 
        distinct
        --w.rp_work_id,
		c.rp_container_ck,
        z.rp_zone_id,
        w.rp_received
    from
        [LINK_TO_WCS].[PickDirector].[dbo].rp_work w join [LINK_TO_WCS].[PickDirector].[dbo].rp_allocation a on (w.rp_work_tk = a.rp_work_ftk)
				join [LINK_TO_WCS].[PickDirector].[dbo].rp_container c on (a.rp_container_ftk = c.rp_container_tk)
				join [LINK_TO_WCS].[PickDirector].[dbo].rp_task t on (t.rp_d_con_ftk = c.rp_container_tk)
				join [LINK_TO_WCS].[PickDirector].[dbo].rp_passage p on (t.rp_passage_ftk = p.rp_passage_tk and p.rp_full_case_flag = 1)
                join [LINK_TO_WCS].[PickDirector].[dbo].rp_zone z on (p.rp_zone_ftk = z.rp_zone_tk)
    where
        --z.rp_zone_id in ('P01', 'P02', 'P03')
    -- and w.rp_full_case_flag = 0 and w.rp_full_pallet_flag = 0
	--and
	 w.rp_received >= DATEADD(day, DATEDIFF(day, 0, GETDATE()), -1)
	and w.rp_received < DATEADD(day, DATEDIFF(day, 0, GETDATE()), 0)
	and 
	c.rp_markedForDeletion is null
	and w.rp_markedForDeletion is null
	and 
	p.rp_full_case_flag = 1
)
insert into [Zeris].[dbo].[WCS_FC_Zone_Container_Count]
select
	DATEADD(day, DATEDIFF(day, 0, rp_received), 0) as [Date],
	f.rp_zone_id,
	count(*) as Case_Count
from
	FCWorks f
group by
	DATEADD(day, DATEDIFF(day, 0, rp_received), 0),
	f.rp_zone_id
order by
	[Date],
	rp_zone_id
	SELECT 'Renew data for WCS is completed' 

	SET NOCOUNT ON;
	
	declare @sqlqry varchar(8000)
	declare @column1name varchar(50)

	SET @Column1Name = '[sep=,' + CHAR(13) + CHAR(10) + 'cd_carrier_id]'
	
	select @sqlqry='set nocount on; select cd_carrier_id'+@column1name+',cd_node_name,cd_divert_response_reason,Time
								FROM [Zeris].[dbo].[WCS_RoutingHistory] '

      --Email for IT team
    EXEC msdb.dbo.sp_send_dbmail
    @profile_name = 'Report_Callplan',
    @recipients = 'helpdesk@zuelligpharma.com.vn;nthy@zuelligpharma.com;rdabdullah@zuelligpharma.com',
    @query = @sqlqry,
    @subject = 'WCS Report Routing History',
    @attach_query_result_as_file = 1 ,
    @body = 'WCS Report Routing History',
    @query_attachment_filename = 'RoutingHistory.csv',
    @query_result_separator=',',
    @query_result_width =32767,
    @query_result_no_padding=1;
    SELECT 'Sending Report Routing History email to IT team is completed' 
    

	SET @Column1Name = '[sep=,' + CHAR(13) + CHAR(10) + 'FULLCASE]'
	select @sqlqry='set nocount on; select FULLCASE'+@column1name+',CARTON,BAG,WorkAssignments
								FROM [Zeris].[dbo].[WCS_CartonCountQuery]'
	  --Email for IT team
    EXEC msdb.dbo.sp_send_dbmail
    @profile_name = 'Report_Callplan',
    @recipients = 'helpdesk@zuelligpharma.com.vn;nthy@zuelligpharma.com;rdabdullah@zuelligpharma.com',
    @query = @sqlqry,
    @subject = 'WCS Report Carton Count Query',
    @attach_query_result_as_file = 1 ,
    @body = 'WCS Report Carton Count Query',
    @query_attachment_filename = 'CartonCountQuery.csv',
    @query_result_separator=',',
    @query_result_width =32767,
    @query_result_no_padding=1;
    SELECT 'Sending Report Carton Count Query email to IT team is completed' 


	SET @Column1Name = '[sep=,' + CHAR(13) + CHAR(10) + 'rp_timestamp]'
	select @sqlqry='set nocount on; select rp_timestamp'+@column1name+',rp_dest_container
								FROM [Zeris].[dbo].[WCS_ContainerQA]'
	  --Email for IT team
    EXEC msdb.dbo.sp_send_dbmail
    @profile_name = 'Report_Callplan',
    @recipients = 'helpdesk@zuelligpharma.com.vn;nthy@zuelligpharma.com;rdabdullah@zuelligpharma.com',
    @query = @sqlqry,
    @subject = 'WCS Report Container QA',
    @attach_query_result_as_file = 1 ,
    @body = 'WCS Report Container QA',
    @query_attachment_filename = 'ContainerQA.csv',
    @query_result_separator=',',
    @query_result_width =32767,
    @query_result_no_padding=1;
    SELECT 'Sending Report Container QA email to IT team is completed' 


	SET @Column1Name = '[sep=,' + CHAR(13) + CHAR(10) + 'P02_03Only]'
	select @sqlqry='set nocount on; select P02_03Only'+@column1name+',P01Only,BothLevels
								FROM [Zeris].[dbo].[WCS_SC_WA_Count]'
	  --Email for IT team
    EXEC msdb.dbo.sp_send_dbmail
    @profile_name = 'Report_Callplan',
    @recipients = 'helpdesk@zuelligpharma.com.vn;nthy@zuelligpharma.com;rdabdullah@zuelligpharma.com',
    @query = @sqlqry,
    @subject = 'WCS Report SC WA Count',
    @attach_query_result_as_file = 1 ,
    @body = 'WCS Report SC WA Count',
    @query_attachment_filename = 'SC_WA_Count.csv',
    @query_result_separator=',',
    @query_result_width =32767,
    @query_result_no_padding=1;
    SELECT 'Sending Report SC WA Count email to IT team is completed'


	SET @Column1Name = '[sep=,' + CHAR(13) + CHAR(10) + 'Date]'
	select @sqlqry='set nocount on; select Date'+@column1name+',rp_zone_id,Case_Count
								FROM [Zeris].[dbo].[WCS_FC_Zone_Container_Count]'
	  --Email for IT team
    EXEC msdb.dbo.sp_send_dbmail
    @profile_name = 'Report_Callplan',
    @recipients = 'helpdesk@zuelligpharma.com.vn;nthy@zuelligpharma.com;rdabdullah@zuelligpharma.com',
    @query = @sqlqry,
    @subject = 'WCS Report FC Zone Container Count',
    @attach_query_result_as_file = 1 ,
    @body = 'WCS Report FC Zone Container Count',
    @query_attachment_filename = 'FC_Zone_Container_Count.csv',
    @query_result_separator=',',
    @query_result_width =32767,
    @query_result_no_padding=1;
    SELECT 'Sending Report FC Zone Container Count email to IT team is completed'

END
