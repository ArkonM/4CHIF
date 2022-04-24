--s20170592
--Armin Schneider

use HappySushi;
go

drop table if exists Filiale;
drop table if exists Angestellter;
go



create table Filiale(
  FilialNr integer     not null primary key
, Straße   varchar(30) not null
, Ort	   varchar(20) not null
, PLZ	   varchar(4)  not null
, Telefon  varchar(20) not null
);
go

create table Angestellter(
  SVNr	   varchar(10)   not null primary key
, Vorname  varchar(44)   not null
, Nachname varchar(44)   not null
, Telefon  varchar(20)   not null
, Gehalt   decimal(6, 2) not null
, FilialNr integer		 references Filiale
);
go






insert into Filiale
	(FilialNr, Straße						, Ort		  , PLZ	  , Telefon         )
values
	(	   1 , 'Burggasse 71'               , 'Wien'      , '1070', '01 / 522 91 03')
,	(	   2 , 'Prokopigasse 4'             , 'Graz'      , '8010', '0316 / 852 285')
,	(	   3 , 'Matthias Corvinus-Straße 15', 'St. Pölten', '3100', '02742 / 51 510')
;
go

insert into Angestellter
	(SVNr		 , Vorname	   , Nachname	, Telefon		, Gehalt , FilialNr)
values
	('2072980526', 'Carina'	   , 'Aschauer'	, '0664 / 12072', 2072.00, 1	   )
,	('2075980809', 'Hans-Peter', 'Enser'	, '0664 / 12075', 2075.00, null    )
,	('2076970925', 'Michael'   , 'Ernst'	, '0664 / 12076', 2076.00, 2	   )
,	('2079980221', 'Marcel'    , 'Fortin'	, '0664 / 12079', 2079.00, 3	   )
,	('1073970812', 'Michael'   , 'Frankolin', '0664 / 11073', 1073.00, null    )
;
go



--Aufgabe 2
--s20170592
--Ja, da nicht jeder Mitarbeiter eine Filiale leitet


--Aufgabe 4
--s20170592
select Nachname, Vorname, f.Ort, a.Telefon 'TelPrivat', f.Telefon 'TelFiliale'
  from Angestellter a
  join Filiale f on f.FilialNr = a.FilialNr
  order by Nachname, Vorname
;
go

--s20170592
select Straße, Ort, PLZ, f.Telefon, a.Vorname, a.Nachname
  from Filiale f
  join Angestellter a on a.FilialNr = f.FilialNr
;
go


--Aufgabe 5
--Nein, weil ich sonst nur ein einziges NULL Attribut haben dürfte, da die NULL's in SQL Server auch unique sein müssen

--Aufgabe 7
--Bei SQLite ja, bei SQL-Server nein, weil bei SQL-Server null öfters vorkommen darf


--Aufgabe 8
--s20170592
create unique nonclustered index idx_FilialNr_uqnotnull
    on Angestellter(FilialNr)
 where FilialNr is not null;


--Aufgabe 9
--s20170592
UPDATE Angestellter
	set FilialNr = 1
  where SVNr = '1073970812'
;
