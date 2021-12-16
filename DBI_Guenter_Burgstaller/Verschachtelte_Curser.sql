use Catalog;
go

set nocount on;

begin
	declare @SupplierID varchar(2)
		  , @SupplierName varchar(6)
		  , @PartName varchar(8)
		  , @PartList varchar(200)
	;

	declare SuppliersCursor cursor
	for
		select SupplierID, SupplierName
		  from Suppliers
		 order by SupplierID
	;

	open SuppliersCursor;
	fetch SuppliersCursor
	 into @SupplierID, @SupplierName;

	while @@FETCH_STATUS = 0
	begin

		print 'Lieferant ' + @SupplierName + ' liefert:';

		declare SupplierPartsCursor cursor
		for
			select distinct p.PartName
			  from SupplierParts sp
			  join Parts p on sp.PartID = p.PartID
						  and sp.SupplierID = @SupplierID
			 order by p.PartName
		;

		open SupplierPartsCursor;
		fetch SupplierPartsCursor
		 into @PartName;

		set @PartList = '  ';
		while @@FETCH_STATUS = 0
		begin
			set @PartList = @PartList + @PartName + ' ';

			fetch SupplierPartsCursor
			 into @PartName;
		end;

		close SuppliersCursor;
		deallocate SupplierPartsCursor;

		print @PartList;

		fetch SuppliersCursor
		 into @SupplierID, @SupplierName;
	end;

	close SuppliersCursor;
	deallocate SuppliersCursor;

end;
go