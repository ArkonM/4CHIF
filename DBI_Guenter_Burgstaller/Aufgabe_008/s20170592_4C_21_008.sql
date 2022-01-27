use Catalog;
go

--Übung 8
--s20170592
create or alter function dbo.Range(
  @From int
, @To int
, @Step int
) returns @Range table
	(
	Value int not null primary key
	)
as
begin
	if @Step > 0 and @From < @To
	begin
		while @From <= @To
		begin
			insert into @Range
			(Value)
			values
			(@From)

			set @From += @Step
		end
	end
	else if @Step = 0
	begin
		insert into @Range
			(Value)
			values
			(@From)
		   ,(@To)
	end
	return;
end;
go

select *
  from dbo.Range(10,150,10)
;
go

select *
  from dbo.Range(10,150,0)
;
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

--Aufgabe 9a
--s20170592
create or alter function dbo.MonthDaysOfYear(
   @Year int
) returns @DaysInMonth table
    ( 
      Month tinyint,
      Days tinyint
    )
as
begin
    declare @s varchar(20);  

        insert into @DaysInMonth
            (Month, Days)
        values
            (1,					31)
		   ,(2,  dbo.FebDay(@Year))
		   ,(3,					31)
		   ,(4,					30)
		   ,(5,					31)
		   ,(6,					30)
		   ,(7,					31)
		   ,(8,					31)
		   ,(9,					30)
		   ,(10,				31)
		   ,(11,				30)
		   ,(12,				31)
		   ;
    return;
end;
go

select * 
  from dbo.MonthDaysOfYear(2001)
;
go

--Übung 9b
--s20170592
create or alter function dbo.MonthDaysOfYear(
   @Year int
) returns @DaysInMonth table
    ( 
      Month tinyint,
      Days tinyint
    )
as
begin
    declare @i int ;
    set @i = 1;
    declare @s varchar(20);

    while (@i <= 12)
        begin
			if(@i <= 7)
			begin
				if(@i = 2)
				begin
					insert into @DaysInMonth
						(Month, Days)
					values
						(@i, dbo.FebDay(@Year));
				end;
				else if(@i % 2 = 0)
				begin
				insert into @DaysInMonth
					(Month, Days)
				values
					(@i, 30);
				end;
				else
				begin
				insert into @DaysInMonth
					(Month, Days)
				values
					(@i, 31);
				end;
				set @i = @i + 1;
			end;
			else
			begin
				if(@i % 2 = 0)
				begin
				insert into @DaysInMonth
					(Month, Days)
				values
					(@i, 31);
				end;
				else
				begin
				insert into @DaysInMonth
					(Month, Days)
				values
					(@i, 30);
				end;
				set @i = @i + 1;
			end;
        end;
    return;
end;
go


select *
  from dbo.MonthDaysOfYear(2000)
;
go


--Aufgabe 9c
--s20170592
create or alter function dbo.MonthDaysOfYear(
   @Year int
) returns @DaysInMonth table
    ( 
      Month tinyint,
      Days tinyint
    )
as
begin
    insert into @DaysInMonth
        select *, case
                    when Value = 1  then 31
                    when Value = 2  then dbo.FebDay(@Year)
                    when Value = 3  then 31
                    when Value = 4  then 30
                    when Value = 5  then 31
                    when Value = 6  then 30
                    when Value = 7  then 31
                    when Value = 8  then 31
                    when Value = 9  then 30
                    when Value = 10 then 31
                    when Value = 11 then 30
                    when Value = 12 then 31
                 end "Days"
          from dbo.Range(1, 12, 1)
    ;
    return
end;
go


select *
  from MonthDaysOfYear(2000)
;
go


--Aufgabe 9d
--s20170592
create or alter function dbo.MonthDaysOfYearInline(
   @Year int
) returns table
as
return
    select *, 
	case
        when Value = 1  then 31
        when Value = 2  then dbo.FebDay(@Year)
        when Value = 3  then 31
        when Value = 4  then 30
        when Value = 5  then 31
        when Value = 6  then 30
        when Value = 7  then 31
        when Value = 8  then 31
        when Value = 9  then 30
        when Value = 10 then 31
        when Value = 11 then 30
        when Value = 12 then 31
        end "Days"
    from dbo.Range(1, 13, 1)
;

go

select *
  from MonthDaysOfYearInline(2000)
;
go



--Aufgabe 10
--s20170592
select value "Year", (select Days 
						from dbo.MonthDaysOfYear(value) 
					   where Month=2
					 ) "Days in February"
  from dbo.Range(2000, 2018, 1) 
;
go


--Aufgabe 11a
--s20170592
select value-100 "Year", (select Days 
						    from dbo.MonthDaysOfYear(value-100) 
					       where Month=2
					     ) "Days in February"
					   , (select Days 
						    from dbo.MonthDaysOfYear(value) 
					       where Month=2
					     ) "Days in February"
					   , value "Year"
  
  from dbo.Range(2000, 2018, 1) 
;
go

--Aufgabe 11b
--s20170592
--Weil das Jahr 2000 durch 400 dividierbar ist und somit ein Schaltjahr ist
--1900 ist nicht durch 400 Teilbar, aber durch 100 und deswegen kein Schaltjahr