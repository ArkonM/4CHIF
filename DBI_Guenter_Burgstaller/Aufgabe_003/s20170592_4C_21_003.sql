--Transact-SQL


--Bildschirmausgabe
print 'Hallo Welt!';
go


--Kommentare
--Das ist ein einzeliger Kommentar (mittels doppelten Minuszeichen)
/*
Das ist ein Kommentar
Der auch mehrere Zeilen umfassen kann
*/


--Variablen
print @@version;
go


--Deklaration
--DECLARE Variablenname Datentyp;
declare @value int;
declare @value int, @result int, @text varchar(20);


--Wertzuweisung
declare @value int, @text varchar(20);

set @value = 42;
set @text = 'Hallo Welt!';

select @value, @text;
go


declare @Count int;

set @Count = (select count(*)
				from Parts
			   where PartCity = 'Paris'
			 );

select 'Anzahl Teile aus Paris: ', @Count;
go


declare @Count int;

select @Count = count(*)
  from Parts
 where PartCity = 'Paris'
;

select 'Anzahl Teile aus Paris: ', @Count;
go


declare @PartID char(2);
declare @PartColor char(5);

select @PartID = PartID,
	   @PartColor = PartColor
  from Parts
 where PartID = 'T5'
;

print 'Teilnummer: ' + @PartID;
print 'Teilfarbe: ' + @PartColor;
go


--Bedingte Ausführung IF..ELSE
--Einfaches Beispiel
if(select avg(PartPrice)
	 from Parts
	where PartCity = 'London') > 14
begin
	print 'Aktueller Durchschnittspreis in London zu hoch.';
end;
go

--Beispiel mit ELSE
declare @Avg decimal(10,2);

select @Avg = avg(PartPrice)
  from Parts
 where PartCity = 'London'
;

if @Avg > 18
begin
	print 'Aktueller Durchschnittspreis in London zu hoch.';
end
else
begin
	print 'Preise in London ok.';
	print 'Durchschnitt: ' + cast(@Avg as char);
end;
go

--Mit EXISTS
if exists (select PartID
			 from Parts
			where PartID = 'T5')
begin
	print 'PartID T5 exists.';
end
else
begin
	print 'PartID T5 does not exist.';
end;
go


--Fallunterschheidung CASE
select PartID
	 , case PartColor
	     when 'rot' then 'red'
		 when 'blau' then 'blue'
		 else 'unknown color'
	   end "PartColorEn"
  from Parts
;
go


select PartID
	 , PartName
	 , case
		 when PartPrice <= 12 then 'billig'
		 when PartPrice > 12 and PartPrice <  17 then 'preiswert'
		 else 'teuer'
	  end "IsABargain"
  from Parts
;
go


begin transaction

select *
  from Parts;

while (select avg(PartPrice)
		 from Parts) < 16
begin
	update Parts
	   set PartPrice = PartPrice * 1.05
	;
end;

select *
  from Parts
;

rollback;
go


--Verzögerte Ausführung mit WAITFOR
print 'Warte nun 5 Sekunden.';
go

begin
  waitfor delay '00:00:05';
  print 'Die 5 Sekunden sind vorbei.';
end;
go