--create database Catalog;
--go

use Catalog;
go

/*
create login catusr with password = 'topsecret',
       default_database = Catalog,
       check_expiration = off,
       check_policy = off;
go


create user catusr for login catusr;
go


grant select
   on Parts
   to catusr
;
go


revoke select
    on Parts
  from catusr
;
go
*/

create role readers;
go

grant select
   on Parts
   to catusr
go

alter role readers
  add member catusr;
go
