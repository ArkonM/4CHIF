/* Aufgabe 1 */
/*
create database Catalog
	on ( name = Catalog_dat
	   , filename = 'C:\Users\Simon\Desktop\Catalog\Catalog.mdf'
	   , size = 10 MB
	   , maxsize = 100MB
	   , filegrowth = 5MB
	   )

log on ( name = Catalog_log
	   , filename = 'C:\Users\Simon\Desktop\Catalog\Catalog.ldf'
	   , size = 2 MB
	   , maxsize = 25 MB
	   , filegrowth = 4 MB
	   )
;
go
*/
/* Aufgabe 3 */
/*create database Catalog;*/

use Catalog;

drop table if exists SupplierParts;
drop table if exists Suppliers;
drop table if exists Parts;

create table Suppliers (
  SupplierID       varchar(2) not null primary key
, SupplierName     varchar(6) not null
, SupplierCity     varchar(6) not null
, SupplierDiscount integer    not null
);
go

create table Parts (
  PartID    varchar(2)    not null primary key
, PartName  varchar(8)    not null
, PartColor varchar(6)    not null
, PartPrice decimal(6, 2) not null
, PartCity   varchar(6)   not null
);
go

create table SupplierParts (
  SupplierID varchar(2) not null references Suppliers
, PartID     varchar(2) not null references Parts
, Amount	 integer    not null
, primary key (SupplierID, PartID)
);
go

/* Aufgabe 4 */

begin transaction;
go

insert into Suppliers
  (SupplierID, SupplierName, SupplierCity, SupplierDiscount)
values 
  ('L1', 'Schmid', 'London', 20)
, ('L2', 'Jonas' , 'Paris' , 10)
, ('L3', 'Berger', 'Paris' , 30)
, ('L4', 'Klein' , 'London', 20)
, ('L5', 'Adam'  , 'Athen' , 30)
;
go

insert into Parts 
  (PartID, PartName, PartColor, PartPrice, PartCity)
values
  ('T1', 'Mutter'  , 'rot' , 12, 'London')
, ('T2', 'Bolzen'  , 'gelb', 17, 'Paris' )
, ('T3', 'Schraube', 'blau', 17, 'Rom'   )
, ('T4', 'Schraube', 'rot' , 14, 'London')
, ('T5', 'Welle'   , 'blau', 12, 'Paris' )
, ('T6', 'Zahnrad' , 'rot' , 19, 'London')
;
go

insert into SupplierParts
  (SupplierID, PartID, Amount)
values
 ('L1', 'T1', 300)
,('L1', 'T2', 200)
,('L1', 'T3', 400)
,('L1', 'T4', 200)
,('L1', 'T5', 100)
,('L1', 'T6', 100)
,('L2', 'T1', 300)
,('L2', 'T2', 400)
,('L3', 'T2', 200)
,('L4', 'T2', 200)
,('L4', 'T4', 300)
,('L4', 'T5', 400)
;
go

commit;
go


/* Aufgabe 5 */


/* Aufgabe 6 
alter database Catalog modify name = WhoSuppliesWhat;
go
*/

/* Aufgabe 7 
alter database WhoSuppliesWhat
  add file ( name = test
		   , filename = 'C:\Users\Simon\Desktop\Catalog\test.ndf'
		   , size = 10 MB
		   , maxsize = 50 MB
		   , filegrowth = 5 MB
  )
;
go
*/

/* Aufgabe 8
   
*/

/* Aufgabe 9
drop database WhoSuppliesWhat;
go
*/