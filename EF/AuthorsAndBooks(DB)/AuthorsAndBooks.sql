create database AuthorsAndBooks
use AuthorsAndBooks

create table Authors(
	Id int primary key identity(1,1),
	[Name] nvarchar(30) not null
)

create table Books(
	Id int primary key identity(1,1),
	Title nvarchar(30) not null,
	AuthorId int foreign key references Authors(Id)
)

INSERT INTO Authors ([Name]) VALUES ('Jane Austen');
INSERT INTO Authors ([Name]) VALUES ('Leo Tolstoy');
INSERT INTO Authors ([Name]) VALUES ('Mark Twain');

INSERT INTO Books (Title, AuthorId) VALUES ('Pride and Prejudice', 1);
INSERT INTO Books (Title, AuthorId) VALUES ('Anna Karenina', 2);
INSERT INTO Books (Title, AuthorId) VALUES ('War and Peace', 2);
INSERT INTO Books (Title, AuthorId) VALUES ('The Adventures of Tom Sawyer', 3);
INSERT INTO Books (Title, AuthorId) VALUES ('Adventures of Huckleberry Finn', 3);