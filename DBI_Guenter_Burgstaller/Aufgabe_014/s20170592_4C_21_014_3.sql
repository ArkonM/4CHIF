IF EXISTS (SELECT 1 FROM sys.tables WHERE name = 's20170592')
   DROP TABLE s20170592
go

CREATE TABLE s20170592
(
	Id          int IDENTITY(1,1) NOT NULL
,	Name        varchar(100)	  NULL
,	Value       varchar(100)	  NULL
,	DateChanged datetime		  NULL
,	CONSTRAINT PK_SampleTable PRIMARY KEY CLUSTERED (Id ASC)
)
go


INSERT INTO s20170592(Name, Value) 
	SELECT 'Name1', 'Value1'
	 UNION ALL 
	SELECT 'Name2', 'Value2'
	 UNION ALL 
	SELECT 'Name3', 'Value3'
go


SELECT * 
  from s20170592
 begin transaction  
    INSERT INTO s20170592
		(Name, Value) 
	VALUES
		('Name4', 'Value4')
   --UPDATE s20170592 SET Name = Name + Name
   --UPDATE s20170592 SET Name = Name + Name WHERE Name = 'Name1'
   UPDATE s20170592 
	  SET Name = Name + Name
	WHERE ID = 2
   DELETE 
     FROM s20170592 
    WHERE ID = 4
  WAITFOR DELAY '00:0:10'  
ROLLBACK;
go


--=====================================
-- Windows/Session #2
--=====================================

---------------------------------------------------
-- This window/session is default READ COMMITTED --
---------------------------------------------------
SELECT @@SPID

begin transaction

   SELECT * 
     from s20170592
  WAITFOR DELAY '00:00:10'  
go

	SELECT * 
	  from s20170592
   WAITFOR DELAY '00:00:10'
go

   SELECT * 
     from s20170592
go
ROLLBACK

SELECT b.name, c.name, a.* 
  from sys.dm_tran_locks a
 INNER JOIN sys.databases b ON a.resource_database_id = database_id
 INNER JOIN sys.objects c ON a.resource_associated_entity_id = object_id
go

 

--=====================================
-- Windows/Session #3
--=====================================



-----------------------------------------------------
-- This window/session is default REPEATABLE READ --
-----------------------------------------------------

SET TRANSACTION ISOLATION LEVEL REPEATABLE READ  
begin transaction

   SELECT * 
     from s20170592
  WAITFOR DELAY '00:00:10'  
go

	SELECT * 
	  from s20170592
   WAITFOR DELAY '00:00:10'
go

   SELECT * 
     from s20170592
go
ROLLBACK




--=====================================
-- Windows/Session #4
--=====================================

-----------------------------------------------------
-- This window/session is SERIALIZABLE             --
-----------------------------------------------------
SELECT @@SPID

SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
begin transaction

   SELECT * 
     from s20170592
  WAITFOR DELAY '00:00:10'  
go

	SELECT * 
	  from s20170592
   WAITFOR DELAY '00:00:10'
go

   SELECT * 
     from s20170592
go
ROLLBACK