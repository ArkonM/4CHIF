use Catalog;
go

--s20170592
--Aufgabe A03
/*exec sp_addumpdevice @devtype    = 'disk', 
                     @logicalname  = 'BakCatalog', 
                     @physicalname = 'c:\mssql\Catalog\catalog.bak'
go*/

select name
  from sys.backup_devices
;
go


--s20170592
--Aufgabe A04
/*backup database Catalog
    to BakCatalog
  with name = 'Full_MO'
;
go*/

restore headeronly
from BakCatalog
;
go


--s20170592
--Aufgabe A05
--1
/*begin transaction
insert into Parts
	(PartID , PartName     , PartColor  , PartPrice, PartCity   )
values
	('T7'   , 'Feder', 'lila', 2.00, 'Paris')
;
commit

--2
backup database Catalog
to disk = 'C:\mssql\Catalog\catalog2.bak'
with differential;
go*/

--3
--Das differentielle Backup ist um rund 500kb kleiner

--4
restore filelistonly
from disk = 'C:\mssql\Catalog\catalog2.bak'
;
go


--s20170592
--Aufgabe A06
--1
drop table if exists SupplierParts;
drop table if exists Suppliers;
drop table if exists Parts;

select name
  from sys.tables
;
go

--2
use master;
go
restore database Catalog
from BakCatalog
;
go

use Catalog;
go
select name
  from sys.tables
;
go