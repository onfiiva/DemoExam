create database test1;

use test1;

create table Roles
(
	name [varchar] (200) primary key not null,
	isDeleted [bit] not null default(0)
);

insert into Roles (name) values
('USER');

select * from Roles;

create table Users
(
	ID [int] primary key identity(1,1),
	username [varchar] (200) unique not null,
	password [varchar] (250) not null,
	role_name [varchar] (200) not null,
	constraint [FK_ROLE] foreign key (role_name)
	references Roles (name),
	isDeleted [bit] not null default(0)
);

insert into Users (username, password, role_name) values
('user', 'password1', 'USER');

select * from Users;

drop table if exists Test;

create table Test
(
	ID [int] primary key identity(1,1),
	name [varchar] (200) unique not null,
	isDeleted [bit] not null default(0)
);

insert into Test (name) values 
('test');

select * from Test;