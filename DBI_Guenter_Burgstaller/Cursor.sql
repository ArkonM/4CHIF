use Catalog;
go

drop procedure if exists CountExcessAmount;
go

create procedure CountExcessAmount(
  @Limit int
)
as
begin
	set nocount on;

	declare @SupplierID varchar(2);
	declare @Count int = 0;

	declare ExcessAmountCursor cursor
	for
	   select distinct SupplierID
	     from SupplierParts
		where Amount > @Limit
	;

	open ExcessAmountCursor;
	fetch ExcessAmountCursor into @SupplierID;
	

	while @@FETCH_STATUS = 0
	begin
		print @SupplierID;
		set @Count = @Count +1;

		fetch ExcessAmountCursor into @SupplierID;
	end;

	close ExcessAmountCursor;
	deallocate ExcessAmountCursor;

	select @Count as "Count";
end;
go




exec CountExcessAmount 200;
go