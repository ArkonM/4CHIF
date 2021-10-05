create database lt0;
go

use lt0;
go

drop table if exists lt, l, t;
go

create table l (
  lnr    varchar(2) not null primary key
, lname  varchar(6) not null
, rabatt decimal(2) not null
, stadt  varchar(6) not null
);
go

create table t (
  tnr    varchar(2)   not null primary key
, tname  varchar(8)   not null
, farbe  varchar(5)
, preis  decimal(4,2)
, stadt  char(6)      not null
);
go

create table lt (
  lnr    varchar(2) not null references l
, tnr    varchar(2) not null references t
, menge  decimal(4) not null check(menge > 0)
, primary key (lnr, tnr)
);
go

insert into l 
  (lnr,  lname,    rabatt, stadt   )
values 
  ('L1', 'Schmid',     20, 'London')
, ('L2',  'Jonas',     10, 'Paris' )
, ('L3', 'Berger',     30, 'Paris' )
, ('L4',  'Klein',     20, 'London')
, ('L5',   'Adam',     30, 'Athen' )
;
go

insert into t 
  (tnr , tname     , farbe  , preis, stadt   )
values 
  ('T1',   'Mutter',   'rot',    12, 'London')
, ('T2',   'Bolzen',  'gelb',    17, 'Paris' )
, ('T3', 'Schraube',  'blau',    17, 'Rom'   )
, ('T4', 'Schraube',   'rot',    14, 'London')
, ('T5',    'Welle',  'blau',  null, 'Paris' )
, ('T6',  'Zahnrad',    null,    19, 'London')
;
go

insert into lt
  (lnr , tnr,  menge)
values 
  ('L1', 'T1',   300)
, ('L1', 'T2',   200)
, ('L1', 'T3',   400)
, ('L1', 'T4',   200)
, ('L1', 'T5',   100)
, ('L1', 'T6',   100)
, ('L2', 'T1',   300)
, ('L2', 'T2',   400)
, ('L3', 'T2',   200)
, ('L4', 'T2',   200)
, ('L4', 'T4',   300)
, ('L4', 'T5',   400)
;
go


select *
  from t
 where preis > 5
;
go


select *
  from t
 order by farbe
;
go


select sum(preis) as 'Summe preis'
  from t
;
go


select count(preis) as 'preis', count(*) as '*'
  from t
;
go


select avg(preis) as 'AVG', min(preis) as 'MIN', max(preis) as 'MAX'
  from t
;
go












use master;
go

drop database lt0;
go