--Schneider Armin
--s20170592

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
	declare @Mo char = '';
	declare @Tu char = '';
	declare @We char = '';
	declare @Th char = '';
	declare @Fr char = '';
	declare @Sa char = '';
	declare @SU char = '';

	declare @i int;
	declare @y int = DATEPART(WEEKDAY, 01-@Month-@Year);
	
	set @i = case
		when @y = 1 then 1
		when @y = 2 then 0
		when @y = 3 then -1
		when @y = 4 then -2
		when @y = 5 then -3
		when @y = 6 then -4
		when @y = 7 then -5
	end;

	while(@i < dbo.DaysOfMonthOfYear(@Month, @Year))
	begin
		
	end;

	insert into @Calendar
		( Mo,  Tu,  We,  Th,  Fr,  Sa,  Su)
	values
		(@Mo, @Tu, @We, @Th, @Fr, @Sa, @Su)

	return;
end;
go
	





select *
  from dbo.Calendar(2011, 2);
go