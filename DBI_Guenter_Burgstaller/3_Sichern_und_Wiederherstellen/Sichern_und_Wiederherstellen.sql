--s20170592
--Aufgabe A03
/*exec sp_addumpdevice @devtype = 'disk', 
                     @logicalname = 'BakCatalog', 
                     @physicalname = 'c:\mssql\Catalog\catalog.bak'
go*/

select name
  from sys.backup_devices
;


--Aufgabe A04



backup database Catalog
to BakCatalog
with name = 'Full_MO';
go