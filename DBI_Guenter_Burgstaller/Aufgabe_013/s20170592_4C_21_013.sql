--s20170592
--Schneider Armin


use Catalog;
go

/*
drop table if exists DDLLog;
go


--s20170592
--Übung 10a,b)
create table DDLLog
(
  ID int identity primary key,
  Date datetime not null default getdate(),
  LogText xml 
);
go
*/


--s20170592
--Übung 10c)
create or alter trigger LogTableEvents 
on database
after DDL_TABLE_EVENTS
as
begin
	declare @EventData xml = eventdata();
	insert into DDLLog
		(Date      
		, LogText)
	values
		(GETDATE() 
		,@EventData.query('data(/EVENT_INSTANCE/TSQLCommand/CommandText)'));
end;
go

--s20170592
--Übung 10d)
/*
create table t(
	test varchar(2)
)
go

alter table t
ADD test2 varchar(2)
;
go

drop table t
;
go
*/

/*
--s20170592
--Übung 10e)
ALTER table DDLLog
ADD UserName varchar(50)
,	TableName varchar(50)
,	EventName varchar(15)
,	Command varchar(max)
;
go
*/


--s20170592
--Übung 10f)
create or alter trigger LogTableEvents 
on database
after DDL_TABLE_EVENTS
as
begin
	declare @EventData xml = eventdata();
	insert into DDLLog
		(Date      
		, LogText
		, UserName
		, TableName
		, EventName
		, Command)
	values
		(GETDATE() 
		,@EventData.query('data(/EVENT_INSTANCE/TSQLCommand/CommandText)')
		, convert(varchar(50), @EventData.query('data(//UserName)'))
		, convert(varchar(50), @EventData.query('data(//ObjectName)'))
		, convert(varchar(50), @EventData.query('data(//EventType)'))
		, convert(varchar(50), @EventData.query('data(//TSQLCommand/CommandText)')))
		;
end;
go

/*
create table t(
	test varchar(2)
)
go

alter table t
ADD test2 varchar(2)
;
go

drop table t
;
go
*/


--s20170592
--Übung 11a
create or alter trigger AfterDelUpdDDLLog
on DDLLog
after delete, update
as
begin
	raiserror('No direct UPDATE or DELETE on DDLLog. Your attempt has been logged.', 10, 1)
	rollback;
end;
go


--s20170592
--Übung 11b
create or alter trigger AfterInsDDLLog
on DDLLog
after insert
as
begin
	raiserror('No direct INSERT into DDLLog. Your attempt has been logged.', 10, 1)
	rollback;
end;
go


--s20170592
--Übung 11c, d, e)
insert into DDLLog
	(Date, LogText)
values
	(GETDATE(), '')
;
go

select *
  from DDLLog
;
go


update DDLLog
	set EventName = ''
where exists (select *
			    from DDLLog
			 )
;

select *
  from DDLLog
;
go


delete from DDLLog
where exists (select *
			    from DDLLog
			 )
;

select *
  from DDLLog
;
go