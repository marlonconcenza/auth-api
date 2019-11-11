create database NetCore
go

Use NetCore
go

if exists (select 1 from sys.tables where name = 'Account')
drop table Account
go

create table Account (
	id int not null identity(1,1) primary key,
	email varchar(200) not null,
	password varchar(100) not null,
	createdAt datetime not null,
	role varchar(50) not null,
	permission varchar(100) null
)
go

if exists (select 1 from sys.tables where name = 'Permission')
drop table Permission
go

create table Permission (
	id int not null identity(1,1) primary key,
	accountId int not null,
	permission varchar(50) not null,
	constraint fk_account foreign key (accountId) references Account (id)
)
go

insert Account values ('marlon@gmail.com', 'itD+YK9qis4WoRqIf80v3g==', GETDATE(), 'Admin', 'canCreateAccount')
go

insert Permission values (1, 'canCreateAccount')
insert Permission values (1, 'canUpdateAccount')
insert Permission values (1, 'canReadAccount')
insert Permission values (1, 'canDeleteAccount')

