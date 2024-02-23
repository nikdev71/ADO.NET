 create database sales
        go
        
        use sales
        create table customers(
            id int not null identity(1, 1) primary key,
            name varchar (100) not null,
            age int)

            create table salesmen(
            id int not null identity(1, 1) primary key,
            name varchar (100) not null,
            age int)

            create table sales(
			id int not null identity(1, 1) primary key,
            id_cust int not null references customers(id) on delete cascade on update cascade,
            id_sal int not null references salesmen(id) on delete cascade on update cascade,			
			quantity int not null,
			price int not null,
			salesdate datetime not null,
            )
			go


        insert into salesmen (name, age) values ('Иваненко', 29)
        insert into salesmen (name, age) values ('Петренко', 27)
        insert into salesmen (name, age) values ('Сидоренко', 27)
        insert into salesmen (name, age) values ('Михайленко', 26)
        insert into salesmen (name, age) values ('Алексеенко', 27)
        insert into salesmen (name, age) values ('Пархоменко', 21)
        
        insert into customers (name, age) values ('Игнатенко', 26)
        insert into customers (name, age) values ('Семененко', 25)
        insert into customers (name, age) values ('Якименко', 25)
        insert into customers (name, age) values ('Дмитренко', 23)
        insert into customers (name, age) values ('Степаненко', 23)
        insert into customers (name, age) values ('Тарасенко', 23)
        
        insert into sales values (1, 2, 10, 50, getdate())
        insert into sales values (3, 4, 7, 42, getdate())
        insert into sales values (5, 3, 3, 40, getdate())
        insert into sales values (3, 6, 8, 70, '12-11-2014 15:00:00')
        insert into sales values (2, 3, 5, 44, '05-09-2015 15:00:00')
        insert into sales values (6, 1, 15, 100, '09-22-2015 15:00:00')
        insert into sales values (2, 4, 20, 200, '10-14-2020 15:00:00')
        insert into sales values (4, 1, 7, 300, '02-20-2023 15:00:00')
        go