use Catalog;
go


--s20170592
select @@version, GETDATE(), SYSTEM_USER, USER_NAME(), CURRENT_USER, DB_NAME()
;
go

--s20170592
create table FirstHundred
	(Num tinyint)