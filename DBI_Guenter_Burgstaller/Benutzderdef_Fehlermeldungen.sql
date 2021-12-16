use Catalog
go

raiserror('Wassereinbruch in der Datenbank!', 16,3);

exec sp_addmessage 50001, 16, 'No record found.', 'us_english', 'False', 'replace';
go

exec sp_addmessage 50001, 16, 'KeinDatensatz gefunden.', 'German', 'False', 'replace';
go

raiserror(50001, 16, 5);



begin

	begin try
		insert into Parts
		  (PartID, PartName, PartColor, PartPrice, PartCity)
		  values
		  ('T6'  , 'Splint',    'lila',		 2.30,  'Wien' )
		  ;

		  print 'OK.';
	  end try
	  begin catch
		  select error_number()		"ErrorNumber"