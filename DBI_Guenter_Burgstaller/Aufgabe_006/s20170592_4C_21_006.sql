use Catalog;
go

--s20170592
create or alter function dbo.DigitSum(
	@Number bigint
)	returns bigint
as
begin
	declare @DigitSum bigint = 0;

	while @Number >= 1
	begin
		set @DigitSum = @DigitSum + @Number % 10;
		set @Number = @Number / 10;
	end

	return @DigitSum;
end
go

select dbo.DigitSum(1000000000000000001) "Quersumme";
go


--s20170592
create or alter function dbo.Dec2Hex(
	@Dec bigint
)	returns varchar(16)
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

select dbo.Dec2Hex(65535) "Dec2Hex";
go


--s20170592
create or alter function dbo.Dec2HexRekursiv(@Dec bigint)
returns varchar(16)
as
begin
	if (@Dec < 1)
	begin
		return '';
	end

	return dbo.Dec2HexRekursiv(@Dec / 16) + substring('0123456789ABCDEF', @Dec % 16 + 1, 1);
end
go

select dbo.Dec2HexRekursiv(65535) "Dec2HexRekursiv";
go