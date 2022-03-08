--DDL-Triger auf Serverebene

/*
drop trigger if exists CatchAllOnServer on all server;
go

drop trigger if exists AfterCreateTable on database;
go

drop trigger if exists BeSafeNotSorry on database;
go


create or alter trigger CatchAllOnServer
on all server
after DDL_SERVER_LEVEL_EVENTS
as
begin
	print 'Because something is happening here but you don''t know what it is do you, Mr. Jones?.';
end
go

create database thinman
go

use thinman;
go



create or alter trigger AfterCreateTable
on database
after create_table
as
begin
	print 'CREATE TABLE detected.';
end
go


create table test(
	test int not null
);
go


create or alter trigger BeSafeNotSorry
on database
after drop_table, alter_table
as
begin
	print 'Disable trigger "BeSafeNotSorry" to drop or alter tables.';
	rollback;
end;
go


drop table test;
go

use master;
go


drop database if exists thinman;
go


create database thinman
go

use thinman;
go

drop trigger if exists BlockAlterDrop on database;
go

create or alter trigger BlockAlterDrop on database
after drop_table, alter_table
as
begin
	declare @Data xml = eventdata();

	select @Data.query('data(/EVENT_INSTANCE/PostTime)') "Event Time"
		 , @Data.query('data(/EVENT_INSTANCE/EventType)') "Event Type"
		 , @Data.query('data(/EVENT_INSTANCE/ServerName)') "Server Name"
		 , @Data.query('data(/EVENT_INSTANCE/TSQLCommand/CommandText)') "Command Text"
	;
end;
go


create table test(
	test int not null
);
go

drop table test
go

use master
go

drop database thinman

*/

use Books
go


select *
  from sys.triggers
;


begin transaction;
go


disable trigger all
on database;
go

update Titles
   set Title = 'test'
;

enable trigger all
on database;
go

update Titles
   set Title = 'test1'
;

rollback;
go


