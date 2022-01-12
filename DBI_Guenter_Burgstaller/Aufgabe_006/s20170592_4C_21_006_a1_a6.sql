use Catalog;
go


--Aufgabe 1
--s20170592
select @@version, GETDATE(), SYSTEM_USER, USER_NAME(), CURRENT_USER, DB_NAME()
;
go

--s20170592
drop table if exists FirstHundred;
go

create table FirstHundred(
	Num tinyint not null
);
go

DECLARE @i int = 0

WHILE @i < 100
BEGIN
    SET @i = @i + 1
    insert into FirstHundred
		(Num)
	values
		(@i)
END
go

select Num, SQRT(Num) "Wurzel"
  from FirstHundred
;
go


--Aufgabe 2
--s20170592
drop table if exists DecValues;
go

create table DecValues(
	Val bigint not null
);
go

DECLARE @i int = 0

WHILE @i <= 65535
BEGIN
    insert into DecValues
		(Val)
	values
		(@i)
	SET @i = @i + 1
END
go

--Dec2Hex (Schleife)
--s20170592
drop function if exists dbo.Dec2Hex;
go

create function dbo.Dec2Hex(
  @Dec bigint
) 
returns varchar(16)
with schemabinding

as

begin
	declare @Hex varchar(16) = '';

	  while @Dec >= 1
	begin   
        set @Hex = substring('0123456789ABCDEF', @Dec % 16 + 1, 1) + @Hex;
        set @Dec = @Dec / 16;
	end;
      
	return @Hex;
end;
go

--Dec2Hex (Rekursiv)
--s20170592
drop function if exists dbo.Dec2Hex;
go

create function dbo.Dec2Hex(@Dec bigint)
returns varchar(16)

as

begin
	if (@Dec < 1)
    begin
        return '';
    end

    return dbo.Dec2Hex(@Dec / 16) + substring('0123456789ABCDEF', @Dec % 16 + 1, 1);
end
go


select Val, dbo.Dec2Hex(Val) "Dex2Hex"
  from DecValues
;
go



--Dec2Hex (Schleife)
--s20170592
drop function if exists dbo.Hex2Dec;
go

create function dbo.Hex2Dec(
  @Hex varchar(16)
) 
returns varchar(16)
with schemabinding

as

begin
	declare @Dec bigint;

	  while @Hex >= 1
	begin   
        set @Dec = substring('0123456789ABCDEF', @Dec % 16 + 1, 1) + @Hex;
        set @Dec = @Dec / 16;
	end;
      
	return @Hex;
end;
go