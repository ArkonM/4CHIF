create database Catalog 
    on ( name = Catalog_dat
       , filename = 'c:\mssql\Catalog\catalog_dat.mdf'
       , size = 10 MB
       , maxsize = 1 GB
       , filegrowth = 50 MB
       )
log on ( name = Catalog_log
       , filename = 'c:\mssql\Catalog\catalog_log.mdf'
       , size = 2 MB
       , maxsize = 500 MB
       , filegrowth = 10 MB
       )
;
go




drop table if exists SupplierParts;
drop table if exists Suppliers;
drop table if exists Parts;
go

create table Suppliers (
  SupplierID       varchar(2) not null primary key
, SupplierName     varchar(6) not null
, SupplierCity     varchar(6) not null
, SupplierDiscount integer    not null
);
go

create table Parts (
  PartID    varchar(2)   not null primary key
, PartName  varchar(8)   not null
, PartColor varchar(6)
, PartPrice decimal(6,2)
, PartCity  char(6)      not null
);
go

create table SupplierParts (
  SupplierID varchar(2) not null references Suppliers
, PartID     varchar(2) not null references Parts
, Amount     decimal(4) not null check(Amount > 0)
, primary key (SupplierID, PartID)
);
go


begin transaction;
go

delete from SupplierParts;
delete from Suppliers;
delete from Parts;


insert into Suppliers
  (SupplierID,  SupplierName, SupplierCity, SupplierDiscount)
values 
	('L1'    , 'Schmid'     , 'London'    , 20)
,	('L2'    , 'Jonas'      , 'Paris'     , 10)
,	('L3'    , 'Berger'     , 'Paris'     , 30)
,	('L4'    , 'Klein'      , 'London'    , 20)
,	('L5'    , 'Adam'       , 'Athen'     , 30)
;
go

insert into Parts
	(PartID , PartName     , PartColor  , PartPrice, PartCity   )
values 
	('T1'   , 'Mutter'     , 'rot'      , 12       , 'London')
,	('T2'   , 'Bolzen'     , 'gelb'     , 17       , 'Paris' )
,	('T3'   , 'Schraube'   , 'blau'     , 17       , 'Rom'   )
,	('T4'   , 'Schraube'   , 'rot'      , 14       , 'London')
,	('T5'   , 'Welle'      , 'blau'     , 12       , 'Paris' )
,	('T6'   , 'Zahnrad'    , 'rot'      , 19       , 'London')
;
go

insert into SupplierParts
	(SupplierID , PartID,  Amount)
values 
	('L1'       , 'T1'  ,   300)
,	('L1'       , 'T2'  ,   200)
,	('L1'       , 'T3'  ,   400)
,	('L1'       , 'T4'  ,   200)
,	('L1'       , 'T5'  ,   100)
,	('L1'       , 'T6'  ,   100)
,	('L2'       , 'T1'  ,   300)
,	('L2'       , 'T2'  ,   400)
,	('L3'       , 'T2'  ,   200)
,	('L4'       , 'T2'  ,   200)
,	('L4'       , 'T4'  ,   300)
,	('L4'       , 'T5'  ,   400)
;
go

commit


/*
select *
 from Suppliers
;
select *
 from Parts
;
select *
 from SupplierParts
;
go*
*/


--Aufgabe 5a / Teile deren Preis unter dem Duchschnittspreis liegen
select PartName, PartPrice
  from Parts
 where PartPrice < (select avg(Partprice)
				      from Parts
				   )
;
go


--Aufgabe 5b / Gesamtumsatz der Suppliers mit Rabatten berechnen
select sp.SupplierID, sum(Amount*(p.PartPrice*((100-s.SupplierDiscount)*0.01))) as 'Total'
  from SupplierParts sp
  join Parts p on p.PartID = sp.PartID
  join Suppliers s on s.SupplierID = sp.SupplierID
  group by sp.SupplierID
;
go


--Aufgabe 5c / Welche Teile werden aus der Stadt von L3 geliefert
select PartName
  from parts
 where PartCity = (select SupplierCity
					 from Suppliers
					where SupplierID = 'L3'
				   )
;
go


--Aufgabe 6 / Datenbank in WhoSuppliesWhat umbenennen
alter database Catalog modify name = WhoSuppliesWhat
;
go


--Aufgabe 7 / Fügen Sie der Datenbank mit einem SQL Skript ein weiteres Datenfile hinzu (mit ALTER DATABASE)
alter database WhoSuppliesWhat 
  add file ( name = WhoSuppliesWhat
           , filename = 'c:\mssql\Catalog\WhoSuppliesWhat_dat.ndf'
           , size = 10 MB
           , maxsize = 1 GB
           , filegrowth = 50 MB
  )
;
go


/*Aufgabe 8
a) Go ist kein Transact-SQL statement, es ist ein Befehl, der von sqlcmd, osql utilities und SSMS erkannt wird.
b) Man kann den Speicherort im SSMS und mit
select *
from sys.database_files
;
go*/



--Aufgabe 9 / Löschen Sie die Datenbank
--Use Master, sodass WhoSuppliesWhat nichtmehr benutzt wird
use master
;
go

drop database if exists WhoSuppliesWhat
;
go