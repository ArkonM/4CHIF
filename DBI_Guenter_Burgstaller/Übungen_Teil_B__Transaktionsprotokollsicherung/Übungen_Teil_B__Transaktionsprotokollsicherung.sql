--Übungen Teil B: Tansaktionsprotokollsicherung


/*use master; 
go

drop database if exists testdb;
go
 
create database testdb 
    on ( name = testdb_dat 
       , filename = 'c:\sqlsrv\testdb.mdf'
    ) 
log on ( name = testdb_log
       , filename = 'c:\sqlsrv\testdb.ldf'
    )
;
go*/


--use testdb;
--go

--s20170592
--Aufgabe B1
/*select physical_name
  from sys.database_files
;
go*/


--s20170592
--Aufgabe B2
/*alter database testdb
  set recovery full;
go

select name, recovery_model_desc
  from sys.databases
 where name = 'testdb'
;
go

dbcc sqlperf(logspace);
go*/


--s20170592
--Aufgabe B3
/*backup database testdb
to disk = 'c:\sqlbak\testdb.bak'
with init;
go

restore headeronly
   from disk = 'c:\sqlbak\testdb.bak'
;
go*/


--s20170592
--Aufgabe B4
/*drop table if exists dbo.LogTest;
go

select top 1000000 
       identity(int, 1, 1) SomeID 
     , abs(checksum(newid())) % 50000 + 1 SomeInt 
     , char(abs(checksum(newid())) % 26 + 65) + 
       char(abs(checksum(newid())) % 26 + 65) SomeLetters2 
     , cast(abs(checksum(newid())) % 10000 / 100.0 as money) SomeMoney 
     , cast(rand(checksum(newid())) * 3653.0 + 36524.0 as datetime) SomeDate 
     , right(newid(), 12) SomeHex12 
  into dbo.LogTest
  from sys.all_columns ac1 cross join sys.all_columns ac2
; 
go


select count(*)
  from dbo.LogTest
;
go

dbcc sqlperf(logspace);
go*/


--s20170592
--Aufgabe B5
/*backup log testdb
to disk = 'c:\sqlbak\testdb.trn'
with init;
go

dbcc sqlperf(logspace);
go*/


--s20170592
--Aufgabe B6

/*use master;
go

drop database if exists testdb;
go*/
