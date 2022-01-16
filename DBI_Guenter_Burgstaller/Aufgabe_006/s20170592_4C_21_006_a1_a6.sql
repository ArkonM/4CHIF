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


--Aufgabe 2a
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


select Val, dbo.Dec2Hex(Val) "Dec2Hex"
  from DecValues
;
go


--Aufgabe 2b
--s20170592
create or alter function dbo.ConvertHex(
    @Hex varchar(1)
) returns bigint
with schemabinding

as

begin
	if @Hex = 'A'
		 begin
		 return 10;
		 end
	else if @Hex = 'B'
		 begin
		 return 11;
		 end
	else if @Hex = 'C'
		 begin
		 return 12;
		 end
	else if @Hex = 'D'
		 begin
		 return 13;
		 end
	else if @Hex = 'E'
		 begin
		 return 14;
		 end
	else if @Hex = 'F'
		 begin
		 return 15;
		 end
	return cast(@Hex as bigint)
end
;
go


create or alter function dbo.Hex2Dec(
    @Hex varchar(16)
) returns bigint
with schemabinding

as

begin
    declare @ergebnis bigint = dbo.ConvertHex(substring(@Hex, 1, 1));
    declare @i int = 1;
    while @i < len(@Hex)

    begin
        set @ergebnis = cast(@ergebnis as int) * 16 + dbo.ConvertHex(substring(@Hex, @i + 1, 1));
        set @i = @i + 1;
    end

    return @ergebnis;
end
;
go

select Val, dbo.Dec2Hex(Val) "Hex", dbo.Hex2Dec(dbo.Dec2Hex(Val)) "Dec"
  from DecValues
;
go


--s20170592
create or alter function dbo.DigitSumHex(
    @Hex varchar(16)
) returns varchar(16)

as

begin
    declare @ergebnis bigint = 0;
    declare @i int = 1;
    while @i <= len(@Hex)

    begin
        set @ergebnis = @ergebnis + dbo.ConvertHex(substring(@Hex, @i, 1));
        set @i = @i + 1;
    end

    return dbo.Dec2Hex(@ergebnis);
end
;
go

select Val, dbo.Dec2Hex(Val) "Dex2Hex", dbo.DigitSum(Val) "QuerSum", dbo.DigitSumHex(dbo.Dec2Hex(Val)) "HexQuerSum", dbo.Hex2Dec(dbo.DigitSumHex(dbo.Dec2Hex(Val))) "DecQuerSum"
  from DecValues
;
go

--Die verschiedenen Quersummen sind nicht übereinstimmend
select dbo.DigitSum(255) "QuerSum", dbo.Hex2Dec(dbo.DigitSumHex(dbo.Dec2Hex(255))) "DecQuerSum"
;
go


--Aufgabe 3
--s20170592
create or alter function dbo.ROT13(
    @Txt varchar(30)
) returns varchar(30)
with schemabinding

as

begin
    declare @i int = 1;
    declare @ergebnis varchar(30) = '';
    while @i <= len(@Txt)
    begin
        declare @symbol varchar(1) = substring(@Txt, @i, 1);
        declare @lower varchar(1) = lower(@symbol);
        declare @y varchar(1);
		if @symbol = ' ' or @symbol = '!'
            begin
                set @y = @symbol;
            end
        else
            if @symbol != @lower
            begin
				set @y = char(((ascii(@lower) - ascii('A')  + 13 ) % (ascii('Z') - ascii('A') + 1)) + ascii('A'));
            end
            else
            begin
				set @y = char(((ascii(@lower) - ascii('a')  + 13 ) % (ascii('z') - ascii('a') + 1)) + ascii('a'));
            end
        set @ergebnis = @ergebnis + @y;
        set @i = @i + 1;
    end
    return @ergebnis;
end;
go


select dbo.ROT13('Re jvyy va Frr fgrpura!') "ROT13"
;
go



/*
Übung 4
s20170592

Im Object Explorer muss man das man auf das Plus der Datenbank drücken, in der die Funktion enthalten ist.
Folgend erweitert man Programmability und öffnet den Folder Funtions.
Jetzt aufgereit sind alle Funktionen sortiert in Funktionskategorien


Übung 5
s20170592

Weil man Nicht-Deterministische Funktionen in einer Benutzerdefinierten Funktion NICHT aufrufen kann
RAND() und PRINT sind Nicht-Deterministisch


Übung 6
s20170592


select OBJECTPROPERTY(OBJECT_ID('[dbo].[DigitSum]'), 'IsDeterministic') 
    as "isDeterministic"
;
go

Die Funktion muss man with schemabinding definieren

*/