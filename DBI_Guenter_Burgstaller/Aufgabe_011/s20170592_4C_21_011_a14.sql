--Schneider Armin
--s20170592

use Catalog
go


--Aufgabe 14a)

create or alter function dbo.Calendar(
  @Year int
, @Month tinyint
) returns @Calendar table
    ( 
      Mo char(2),
      Tu char(2),
      We char(2),
      Th char(2),
      Fr char(2),
      Sa char(2),
      Su char(2)
    )
as
begin
	declare @Mo char(2) = '';
	declare @Tu char(2) = '';
	declare @We char(2) = '';
	declare @Th char(2) = '';
	declare @Fr char(2) = '';
	declare @Sa char(2) = '';
	declare @Su char(2) = '';

	declare @i int;
	declare @weekday int = DATEPART(WEEKDAY, DATEFROMPARTS(@Year, @Month, 1));
	
	set @i = case
		when @weekday = 2 then  1
		when @weekday = 3 then  0
		when @weekday = 4 then -1
		when @weekday = 5 then -2
		when @weekday = 6 then -3
		when @weekday = 7 then -4
		when @weekday = 1 then -5
	end;

	while(@i <= dbo.DaysOfMonthOfYear(@Year,@Month))
	begin

		if(@i > 0 and @i <= dbo.DaysOfMonthOfYear(@Year,@Month))
		begin
		set @Mo = @i
		end;

		else
		begin
		set @Mo = ''
		end;
		set @i = @i+1


		if(@i > 0 and @i <= dbo.DaysOfMonthOfYear(@Year,@Month))
		begin
		set @Tu = @i
		end;

		else
		begin
		set @Tu = ''
		end;
		set @i = @i+1


		if(@i > 0 and @i <= dbo.DaysOfMonthOfYear(@Year,@Month))
		begin
		set @We = @i
		end;

		else
		begin
		set @We = ''
		end;
		set @i = @i+1


		if(@i > 0 and @i <= dbo.DaysOfMonthOfYear(@Year,@Month))
		begin
		set @Th = @i
		end;

		else
		begin
		set @Th = ''
		end;
		set @i = @i+1


		if(@i > 0 and @i <= dbo.DaysOfMonthOfYear(@Year,@Month))
		begin
		set @Fr = @i
		end;

		else
		begin
		set @Fr = ''
		end;
		set @i = @i+1


		if(@i > 0 and @i <= dbo.DaysOfMonthOfYear(@Year,@Month))
		begin
		set @Sa = @i
		end;

		else
		begin
		set @Sa = ''
		end;
		set @i = @i+1


		if(@i > 0 and @i <= dbo.DaysOfMonthOfYear(@Year,@Month))
		begin
		set @Su = @i
		end;

		else
		begin
		set @Su = ''
		end;
		set @i = @i+1


		insert into @Calendar
		( Mo,  Tu,  We,  Th,  Fr,  Sa,  Su)
		values
		(@Mo, @Tu, @We, @Th, @Fr, @Sa, @Su)


	end;

	return;
end;
go

--s20170592
select *
 from dbo.Calendar(2022, 2)
;
go
	

declare @lauf int = 0;

while(@lauf < 12)
begin

	set @lauf = @lauf+1;

	select DATENAME(MONTH, DATEFROMPARTS(2022, @lauf, 1))
	;

	select *
		from dbo.Calendar(2022, @lauf)
	;

end;