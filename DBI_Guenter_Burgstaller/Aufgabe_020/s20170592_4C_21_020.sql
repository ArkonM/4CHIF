--Schneider Armin
--s20170592

use kryptologie;
go

drop table if exists Numbers;
go

select top 1000 identity(int, 0, 1) as #
  into Numbers
  from sys.objects o1, sys.objects o2
;

alter table Numbers add primary key (#);
go
 

--C2
--s20170592
create or alter function dbo.Caesar(    
    @Letter char,
    @Key integer
) returns char
as
begin
	set @Letter = char(ascii(UPPER(@Letter)) + @Key);
	if(ascii(@Letter) > 90)
	begin
		declare @correction integer = (64 + ascii(@Letter)-90)
		if(@correction > 64 and @correction < 91)
		begin
			set @Letter = char(@correction) 
		end
		else
		begin
			set @Letter = char(@correction - @Key)
		end
	end
	return @Letter;
end
go

select	substring('Kleopatra', #, 1) Klartext, 
		dbo.Caesar(substring('Kleopatra', #, 1), 3) as Schlüsseltext
   from Numbers
  where # > 0 and # <= len('Kleopatra')
;
go


--C3
--s20170592
create function dbo.CaesarEncrypt(    
    @Message varchar(1024),
    @Key integer
)
  returns varchar(1024)
as
begin
	declare @i integer = 0;
	declare @res varchar(1024);

	while(@i < len(@Message))
	begin
		set @res = dbo.Caeser(@Message[@i], @Key);
		set @i += 1;
	end
end
go

