use Catalog;
go

drop table if exists Foo;
go

create table Foo
(
  Field1 int		 not null
, Field2 varchar(10) 
);
go

create unique index IX_Foo_Field2
    on Foo(Field2);
go

insert into Foo
	(Field2 , Field2)
values
	(10		, 'Hello!')
,	(10		, 'Bye!'  )
,	(50		, 'Hallo!')
;
go

exec sp_spaceused Foo;
go

create index IX_Suppliers_SupplierName_SupplierCity
    on Suppliers(SupplierName, SupplierCity);
go

select *
  from sys.key_constraints
;
select *
  from sys.foreign_keys
;

begin transaction
go



alter table Suppliers
 drop constraint PK__Supplier__4BE666949A999AB0;


alter table Suppliers
	add primary key (IX_Suppliers_SupplierName_SupplierCity)
;


rollback
go