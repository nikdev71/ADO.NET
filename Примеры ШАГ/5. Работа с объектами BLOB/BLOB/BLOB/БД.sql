create database BLOBOBJECTS
go

use BLOBOBJECTS

create table FILES(
	ID int identity(1,1) NOT NULL,
	FILE_NAME nvarchar(255) NOT NULL,
	FILE_IMAGE [image],
	FILE_SIZE int)
go

create procedure ADD_FILES
@FILE_NAME nvarchar(255), 
@FILE_IMAGE image,
@FILE_SIZE int
as
insert into FILES (FILE_NAME, FILE_IMAGE, FILE_SIZE)
values(@FILE_NAME, @FILE_IMAGE, @FILE_SIZE)
go

create procedure GET_FILE @FILE_NAME nvarchar(255), 
@FILE_IMAGE image output
as
select @FILE_IMAGE=FILE_IMAGE from FILES
where @FILE_NAME=FILE_NAME
go