--s20170592
--Übung 1
--create database raid5;
--go

use raid5;
go

drop table if exists DiskArray;
go

--Übung 2
create table DiskArray(
	BlockNo		integer primary key
,	Disk1		tinyint
,	Disk2		tinyint
,	Disk3		tinyint
,	ParityDisk	tinyint
);
go


--Übung 3
insert into DiskArray
	(BlockNo, Disk1		, Disk2		, Disk3		)
values
	(1		, ASCII('W'), ASCII('F'), ASCII('D'))
,	(2		, ASCII('i'), ASCII('i'), ASCII('a'))
,	(3		, ASCII('c'), ASCII('r'), ASCII('t'))
,	(4		, ASCII('h'), ASCII('m'), ASCII('e'))
,	(5		, ASCII('t'), ASCII('e'), ASCII('n'))
,	(6		, ASCII('i'), ASCII('n'), ASCII(' '))
,	(7		, ASCII('g'), ASCII(' '), ASCII(' '))
,	(8		, ASCII('e'), ASCII(' '), ASCII(' '))
;
go

--Übung 4
update DiskArray
   set ParityDisk = Disk1^Disk2^Disk3
;
go


--s20170592
--Übung 5
select CHAR(Disk1), CHAR(Disk2), CHAR(Disk3), ParityDisk
  from DiskArray
;
go


--s20170592
--Übung 6
update DiskArray
   set Disk1 = null
;
go

select *
  from DiskArray
;
go

--s20170592
--Übung 7
update DiskArray
   set Disk1 = Disk2^Disk3^ParityDisk
;
go

select char(Disk1) "Disk1 Wiederhergestellt"
  from DiskArray
;
go


--s20170592
--Übung 8
--Disk2
update DiskArray
   set Disk2 = null
;
go

update DiskArray
   set Disk2 = Disk1^Disk3^ParityDisk
;
go

select char(Disk2) "Disk2 Wiederhergestellt"
  from DiskArray
;
go

--Disk3
update DiskArray
   set Disk3 = null
;
go

update DiskArray
   set Disk3 = Disk1^Disk2^ParityDisk
;
go

select char(Disk3) "Disk3 Wiederhergestellt"
  from DiskArray
;
go

--ParityDisk
update DiskArray
   set ParityDisk = null
;
go

update DiskArray
   set ParityDisk = Disk1^Disk2^Disk3
;
go

select ParityDisk "ParityDisk Wiederhergestellt"
  from DiskArray
;
go