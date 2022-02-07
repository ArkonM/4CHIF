use Catalog;
go


--Calculate February
create or alter function dbo.FebDay(
	@Year int
) returns int
as
begin
	if(@Year % 400 = 0)
	begin
		return 29;
	end;
	else if(@Year % 100 = 0)
	begin
		return 28;
	end;
	else if(@Year % 4 = 0)
	begin
		return 29;
	end;
	return 28;
end;
go


--s20170592
--Übung 12
create or alter function dbo.DaysOfMonthOfYear(
  @Year int 
, @Month tinyint
) returns int
begin
return
	case
        when (@Month = 2)
			then dbo.FebDay(@Year)
		when (((@Month % 2 = 1) and (@Month <= 7)) or ((@Month % 2 = 0) and (@Month >= 8)))
            then 31
        when (((@Month % 2 = 0) and (@Month <= 7)) or ((@Month % 2 = 1) and (@Month >= 8)))
            then 30
    end
end;
go


select Value + 1900 "Year" 
     , dbo.DaysOfMonthOfYear(Value + 1900, 2) "Days in February"
     , dbo.DaysOfMonthOfYear(Value + 2000, 2) "Days in February" 
     , Value + 2000 "Year"
  from dbo.Range(0, 12, 1)
;
go


--s20170592
--Aufgabe 13a)
create or alter function dbo.dateRange1(
	@Year int
,	@Month tinyint
) returns @dayTable table
	(
	Days Date not null
	)
as
begin
	declare @day int = 1;
	declare @outpt varchar(12);
	while(@day <= dbo.DaysOfMonthOfYear(@Year, @Month))
	begin
		set @outpt = concat(cast(@Year as varchar), '-', cast(@Month as varchar), '-', cast(@Day as varchar));
		insert into @dayTable
			(Days)
		values
			(@outpt)
		set @day = @day + 1;
	end;
	return;
end;
go

select * 
  from DateRange1(2015, 2)
;
go


--s20170592
--Aufgabe 13b)
create or alter function dbo.dateRange2(
	@Year int
,	@Month tinyint
) returns table
as
return
	select concat(cast(@Year as varchar), '-', cast(@Month as varchar), '-', cast(Value as varchar)) "Days"
	  from dbo.Range(1, dbo.DaysOfMonthOfYear(@Year, @Month), 1)
	;
go


select * 
  from DateRange2(2015, 2)
;
go


--s20170592
--Aufgabe 15
create or alter function dbo.SplitString(
	@String varchar(1024)
,	@Seperator char
) returns @Split table
	(
	Slices varchar(1024)
	)
as
begin
	declare @slice varchar(1024) = '';
	declare @iterator int = 1;
	while(@iterator <= LEN(@String))
	begin
		if(substring(@String, @iterator, 1) != @Seperator)
		begin
			set @slice = @slice + substring(@String, @iterator, 1);
		end;
		else
		begin
			insert into @Split
				(Slices)
			values
				(@slice)
			set @slice = '';
		end;
		set @iterator = @iterator + 1;
	end;
	insert into @Split
		(Slices)
	values
		(@slice)
	return
end;
go

select *
  from dbo.SplitString('Hallo test test test', ' ')
;
go