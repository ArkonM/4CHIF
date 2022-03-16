--Schneider Armin
--s20170592
use Catalog
go

set nocount on;
go


--RedParts Beispiel aus xWiki
select *
  from Parts;
go

create or alter view RedParts
as select *
	 from Parts
	where PartColor = 'rot';
go

select *
  from RedParts;
go


--Übung 2
create or alter view PartsWithTax
as select PartID, PartName, PartColor, CONVERT(DECIMAL(10,2),(PartPrice*1.2)) as "PriceWithTax", CONVERT(DECIMAL(10,2),(PartPrice*0.2)) as "Tax", PartCity
	 from Parts
go


select *
  from PartsWithTax
;
go


--Select Views
select *
  from sys.views
;
go

select *
  from sys.objects
 where type = 'V'
;
go


--Übung 3
/*
insert into RedParts
	(PartID, PartName, PartColor, PartPrice, PartCity)
values
	('T8', 'Kurbel', 'blau', 49.00, 'Madrid')
;
go
*/


select *
  from RedParts
;
go

select *
  from Parts
;
go


create or alter trigger RedPartsOnly
on RedParts
instead of insert
as
begin
    if((select PartColor from inserted) = 'rot')
    begin
        insert into Parts
            (PartID, PartName, PartColor, PartPrice, PartCity)
        Values
            ((select PartID from inserted), (select PartName from inserted), (select PartColor from inserted), (select PartPrice from inserted), (select PartCity from inserted))
    end 
	else
    begin
        raiserror('Falsche Farbe', 16, 1);
    end
end
go


--Übung 4
create or alter view BlueParts
as select PartName, PartPrice
	 from Parts
	where PartColor = 'blau';
go

select *
  from BlueParts
;

insert into BlueParts
	(PartName, PartPrice)
values
	('Kurbelwelle', 49.00)
;
go


insert into PartsWithTax
	(PartID, PartName, PartColor, PriceWithTax, Tax, PartCity)
values
	('T9', 'Kurbelwelle', 'blau', 1.20, 0.20, 'Madrid')
;
go

select *
  from BlueParts
;
go

select *
  from PartsWithTax
;
go


create or alter trigger PriceCheck
on PartsWithTax
instead of insert
as
begin
    insert into Parts
        (PartID, PartName, PartColor, PartPrice, PartCity)
    Values
        ((select PartID from inserted)
		, (select PartName from inserted)
		, (select PartColor from inserted)
		, (select PriceWithTax-Tax from inserted)
		, (select PartCity from inserted))
end
go


insert into PartsWithTax
	(PartID, PartName, PartColor, PriceWithTax, Tax, PartCity)
values
	('T9', 'Kurbelwelle', 'blau', 1.20, 0.20, 'Madrid')
;
go