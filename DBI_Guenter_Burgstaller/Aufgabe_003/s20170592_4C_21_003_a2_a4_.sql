--s20170592
--Aufgabe 2
create table Bytes(
  Byte integer not null primary key
);
go

declare @Count integer = 0;

while @Count <= 255
begin
  insert into Bytes
	(Byte)
  values
    (@Count);
  set @Count = @Count + 1;
end;



--s20170592
--Aufgabe 3,1
select Byte
	 , case
	     when
		   Byte & 128 = 128 then 1 else 0
		 end "128"
	 , case
	     when
		   Byte & 64 = 64 then 1 else 0
		 end "64"
	 , case
	     when
		   Byte & 32 = 32 then 1 else 0
		 end "32"
	 , case
	     when
		   Byte & 16 = 16 then 1 else 0
		 end "16"
	 , case
	     when
		   Byte & 8 = 8 then 1 else 0
		 end "8"
	 , case
	     when
		   Byte & 4 = 4 then 1 else 0
		 end "4"
	 , case
	     when
		   Byte & 2 = 2 then 1 else 0
		 end "2"
	 , case
	     when
		   Byte & 1 = 1 then 1 else 0
		 end "1"
  from Bytes
;
go


--s20170592
--Aufgabe 3,2
select Byte
	 , (case
	     when
		   Byte & 128 = 128 then '1' else '0'
		 end
	 + case
	     when
		   Byte & 64 = 64 then '1' else '0'
		 end
	 + case
	     when
		   Byte & 32 = 32 then '1' else '0'
		 end
	 + case
	     when
		   Byte & 16 = 16 then '1' else '0'
		 end
	 + case
	     when
		   Byte & 8 = 8 then '1' else '0'
		 end
	 + case
	     when
		   Byte & 4 = 4 then '1' else '0'
		 end
	 + case
	     when
		   Byte & 2 = 2 then '1' else '0'
		 end
	 + case
	     when
		   Byte & 1 = 1 then '1' else '0'
		 end) "Binary"
  from Bytes
;
go

use Catalog
go

--s20170592
--Aufagbe 4
update Parts
   set PartPrice = PartPrice * 1.05
 where cast(substring(PartID, 2, len(PartID)) as int) % 2 = 1
;
go

select *
  from Parts
;
go