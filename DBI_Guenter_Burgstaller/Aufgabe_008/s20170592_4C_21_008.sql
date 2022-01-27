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
            (1, DAY(EOMONTH(CONCAT(@Year, '-', 1, '-', 1))))
		   ,(2, DAY(EOMONTH(CONCAT(@Year, '-', 1, '-', 1))))
		   ,(3, DAY(EOMONTH(CONCAT(@Year, '-', 1, '-', 1))))
		   ,(4, DAY(EOMONTH(CONCAT(@Year, '-', 1, '-', 1))))
		   ,(5, DAY(EOMONTH(CONCAT(@Year, '-', 1, '-', 1))))
		   ,(6, DAY(EOMONTH(CONCAT(@Year, '-', 1, '-', 1))))
		   ,(7, DAY(EOMONTH(CONCAT(@Year, '-', 1, '-', 1))))
		   ,(8, DAY(EOMONTH(CONCAT(@Year, '-', 1, '-', 1))))
		   ,(9, DAY(EOMONTH(CONCAT(@Year, '-', 1, '-', 1))))
		   ,(10, DAY(EOMONTH(CONCAT(@Year, '-', 1, '-', 1))))
		   ,(11, DAY(EOMONTH(CONCAT(@Year, '-', 1, '-', 1))))
		   ,(12, DAY(EOMONTH(CONCAT(@Year, '-', 1, '-', 1))))
		   ;

    return;
end;
go

select * 
  from dbo.MonthDaysOfYear(2000)
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
        set @s = CONCAT(@Year, '-', @i, '-', 1);
            insert into @DaysInMonth
                (Month, Days)
            values
                (@i, DAY(EOMONTH(@s)));
            set @i = @i + 1;
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




create or alter function dbo.FebDay(
	@Year
) returns int

as
begin
	if(@year % 400 == 0)
		return 29;
	
end;
go

--Aufgabe 10
--s20170592
select * 
  from dbo.FebDay(2000)
;