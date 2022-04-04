--s20170592
--Armin Schneider

use HappySushi;
go

drop table if exists Filiale;
drop table if exists Angestellter;
go


create table Angestellter(
  SVNr	   varchar(10)   not null primary key
, Vorname  varchar(44)   not null
, Nachname varchar(44)   not null
, Telefon  varchar(20)   not null
, Gehalt   decimal(6, 2) not null
);
go


create table Filiale(
  FilialNr integer     not null primary key
, Straﬂe   varchar(30) not null
, Ort	   varchar(20) not null
, PLZ	   varchar(4)  not null
, Telefon  varchar(20) not null
, SVNr	   varchar(10) not null references Angestellter unique
);
go


insert into Angestellter
	(SVNr		 , Vorname	   , Nachname	, Telefon		, Gehalt )
values
	('2072980526', 'Carina'	   , 'Aschauer'	, '0664 / 12072', 2072.00)
,	('2075980809', 'Hans-Peter', 'Enser'	, '0664 / 12075', 2075.00)
,	('2076970925', 'Michael'   , 'Ernst'	, '0664 / 12076', 2076.00)
,	('2079980221', 'Marcel'    , 'Fortin'	, '0664 / 12079', 2079.00)
,	('1073970812', 'Michael'   , 'Frankolin', '0664 / 11073', 1073.00)
;
go


insert into Filiale
	(FilialNr, Straﬂe						, Ort		  , PLZ	  , Telefon         , SVNr		  )
values
	(	   1 , 'Burggasse 71'               , 'Wien'      , '1070', '01 / 522 91 03', '2072980526')
,	(	   2 , 'Prokopigasse 4'             , 'Graz'      , '8010', '0316 / 852 285', '2076970925')
,	(	   3 , 'Matthias Corvinus-Straﬂe 15', 'St. Pˆlten', '3100', '02742 / 51 510', '2079980221')
;
go




--Aufgabe 9
--s20170592
select Nachname, Vorname, f.Ort, a.Telefon 'TelPrivat', f.Telefon 'TelFiliale'
  from Angestellter a
  join Filiale f on f.SVNr = a.SVNr
  order by Nachname, Vorname
;
go


--Aufgabe 10
--s20170592
select Straﬂe, Ort, PLZ, f.Telefon, a.Vorname, a.Nachname
  from Filiale f
  join Angestellter a on a.SVNr = f.SVNr
;
go