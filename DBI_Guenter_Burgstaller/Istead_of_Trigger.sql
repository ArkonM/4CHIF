--Schneider Armin
--s20170592

use Catalog
go

--Übung 4
--s20170592
create or alter trigger InsteadOfInsDelAuthors
	on Parts
 instead of insert, update, delete
	as
 begin
	declare @cause varchar(8);

	if exists(select *
				from inserted
			 )
   and exists(select *
				from deleted
			 )
	begin
	set @cause = 'Update'
	end
	else if exists(select *
					 from inserted
				  )
	begin
	set @cause = 'Insert'
	end
	else if exists(select *
					 from deleted
				  )
	begin
	set @cause = 'Delete'
	end

	print('Trigger caused by ' + @cause)

	print('INSERTED')
	select *
	  from inserted

	print('DELETED');
	select *
	  from deleted

	rollback;
  end;
go


Update Parts
   set PartPrice = 18
 where PartPrice = 17
go


use Books
go

--Übung 5
--s20170592
create or alter trigger AfterUpdTitles1
	on Titles
 after update
	as
 begin
	print('AfterUpdTitles1');
  end;
go


create or alter trigger AfterUpdTitles2
	on Titles
 after update
	as
 begin
	print('AfterUpdTitles2');

	select *
	  from deleted
	;
  end;
go

--Übung 5
--s20170592
begin transaction
update Titles

   set Language = 'de'
 where Language = 'en'
;
go
rollback

--Übung 5
--Aufgabe 2
--Zuerst 1, dann 2

--Aufgabe 4
--Zuerst 1, dann 2


use Catalog
go

--Übung 7
--s20170592
select t.name, case t.is_instead_of_trigger
					when 1 then 'instead of'
					else 'after'
			   end

  from sys.triggers t
  join sys.tables tab on tab.object_id = t.parent_id
 where tab.name = 'Parts'
;