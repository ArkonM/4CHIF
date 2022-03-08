--s20170592
--Schneider Armin


drop table if exists DDLLog
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



--s20170592
--Übung 10c)
create or alter trigger LogTableEvents 
on database
after DDL_TABLE_EVENTS
as
begin
	declare @EventData xml = eventdata();
	insert into DDLLog
		(Date      , LogText														 )
	values
		(GETDATE() ,@EventData.query('data(/EVENT_INSTANCE/TSQLCommand/CommandText)'));
end;
go

--s20170592
--Übung 10d)
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

select *
  from DDLLog
;
go


--s20170592
--Übung 10e)
ALTER table DDLLog
ADD UserName varchar(50)
,	TableName varchar(50)
,	EventName varchar(15)
,	Command varchar(max)
;
go


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
		, convert(varchar(50), @EventData.query('data(//TableName)'))
		, convert(varchar(50), @EventData.query('data(//EventName)'))
		, convert(varchar(50), @EventData.query('data(//Command)')))
		;
end;
go