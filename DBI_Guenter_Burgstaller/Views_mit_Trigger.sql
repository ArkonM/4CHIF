use Books

drop table if exists _Books;

create table _Books
(
  ISBN10    char(10)		not null primary key
, Author    varchar(32)		not null
, Title     varchar(48)		not null
, Language  char(2)     
, isDeleted bit				not null default 1
, DeletedOn smalldatetime	
, DeletedBy varchar(32)
);

begin transaction;

insert into _Books
  (ISBN10,       Author               , Title                                 , Language)
values
  ('014241493X', 'John Green'         , 'Paper Towns'                         ,     'en')
, ('3446234770', 'John Green'         , 'Margos Spuren'                       ,     'de')
, ('3746656367', 'Lion Feuchtwanger'  , 'Goya'                                ,     'de')
, ('374665632X', 'Lion Feuchtwanger'  , 'Der falsche Nero'                    ,     'de')
, ('3518395041', 'Ernst Weiﬂ'         , 'Der arme Verschwender'               ,     'de')
, ('3551789762', 'Ryuichiro Utsumi'   , 'Von der Natur des Menschen'          ,     'de')
, ('3630620124', 'Ryunosuke Akutagawa', 'Rashomon'                            ,     'de')
, ('3423136200', 'Antal Szerb'        , 'Reise im Mondlicht'                  ,     'de')
, ('3423114118', 'Heimito von Doderer', 'Die Wasserf‰lle von Slunj'           ,     'de')
, ('3442453305', 'Sven Regener'       , 'Herr Lehmann'                        ,     'de')
, ('3442730503', 'Haruki Murakami'    , 'Naokos L‰cheln'                      ,     'de')
, ('0416199259', 'Benjamin Hoff'      , 'The Tao of Pooh and the Te of Piglet',     'en')
, ('0931137071', 'Geoffrey James'     , 'The Tao of Programming'              ,     'de')
, ('140522310X', 'Andy Stanton'       , 'You''re a Bad Man, Mr Gum!'          ,     'en')
, ('3462034901', 'Joseph Roth'        , 'Die Geschichte von der 1002. Nacht'  ,     'de')
, ('3423107423', 'Italo Calvino'      , 'Der Ritter, den es nicht gab'        ,     'de')
, ('3257238606', 'Georges Simenon'    , 'Maigret und der Clochard'            ,     'de')
, ('3423138721', 'Walter Kappacher'   , 'Selina oder Das andere Leben'        ,     'de')
, ('3423135794', 'Leo Perutz'         , 'Die dritte Kugel'                    ,     'de')
, ('3423131608', 'Leo Perutz'         , 'Der schwedische Reiter'              ,     'de')
;

commit;
go



create or alter view Books
as
select ISBN10, Author, Title, Language
  from _Books
 where isDeleted = 1;
;
go


create or alter trigger InsteadOfDelBooks
on _Books
instead of delete
as
begin
	update _Books
	   set isDeleted = 0
	     , DeletedOn = GETDATE()
		 , DeletedBy = USER_NAME()
	 where ISBN10 in (select ISBN10
						from deleted
					 )
  end;
go



select *
  from _Books
;
go

delete Books
 where Author = 'John Green'
;