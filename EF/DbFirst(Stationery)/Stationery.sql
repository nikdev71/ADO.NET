create database Stationery
go

use Stationery
go

create table TypesOfStationery(
	Id int primary key Identity(1,1),
	Title nvarchar(30) not null
)

create table Stationery(
	Id int primary key Identity(1,1),
	Title nvarchar(30) not null,
	Quantity int not null,
	Cost int not null,
	TypeId int foreign key references TypesOfStationery(Id)
)


create table Managers(
	Id int primary key Identity(1,1),
	Name nvarchar(50) not null
)
create table Firms(
	Id int primary key Identity(1,1),
	Title nvarchar(50) not null
)
create table Sales (
    Id int primary key Identity(1,1),
    StationeryId int foreign key references Stationery(Id),
    FirmId int foreign key references Firms(Id),
    ManagerId int foreign key references Managers(Id),
    Quantity int not null,
    PricePerUnitSold DECIMAL(10, 2),
    DateOfSale DATE,
)

INSERT INTO TypesOfStationery (Title)
VALUES ('Карандаши'), ('Ручки'), ('Тетради'), ('Скрепки');

INSERT INTO Managers (Name)
VALUES ('Иван Иванов'), ('Петр Петров'), ('Анна Сидорова'), ('Мария Николаева');

INSERT INTO Firms (Title)
VALUES ('Фирма1'), ('Фирма2'), ('Фирма3');

INSERT INTO Stationery (Title, Quantity, Cost, TypeId)
VALUES ('Карандаши HB', 100, 50, 1),
       ('Ручки гелевые', 80, 70, 2),
       ('Тетради в клетку', 120, 30, 3),
       ('Скрепки металлические', 200, 20, 4);

INSERT INTO Sales (StationeryId, FirmId, ManagerId, Quantity, PricePerUnitSold, DateOfSale)
VALUES (1, 1, 1, 50, 1.50, '2024-02-14'),
       (2, 2, 2, 30, 2.00, '2024-02-13'),
       (3, 3, 3, 40, 1.20, '2024-02-12'),
       (4, 1, 2, 100, 0.50, '2024-02-11');

INSERT INTO TypesOfStationery (Title)
VALUES ('Ластик'), ('Клей'), ('Блокноты');

INSERT INTO Managers (Name)
VALUES ('Ольга Козлова'), ('Алексей Смирнов'), ('Татьяна Иванова');

INSERT INTO Firms (Title)
VALUES ('Фирма4'), ('Фирма5'), ('Фирма6');

INSERT INTO Stationery (Title, Quantity, Cost, TypeId)
VALUES ('Ластик для карандашей', 150, 10, 5),
       ('Клей ПВА', 90, 40, 6),
       ('Блокнот формата А5', 80, 25, 7);

INSERT INTO Sales (StationeryId, FirmId, ManagerId, Quantity, PricePerUnitSold, DateOfSale)
VALUES (1, 4, 5, 30, 1.00, '2024-02-08'),
       (2, 5, 6, 60, 3.00, '2024-02-07'),
       (3, 6, 3, 70, 2.50, '2024-02-06');

go

create procedure all_stationeries
as
select s.Id, s.Title , s.Quantity, s.Cost , ts.Title as [Type]
from Stationery as s
join TypesOfStationery as ts on s.TypeId = ts.Id
go


create procedure all_types
as
select Id, Title
from TypesOfStationery 
go

create  procedure all_managers
as
select Id, Name
from Managers
go

create procedure all_firms
as
select Id, Title
from Firms
go

create procedure max_quantity
as
select s.Id, s.Title, ts.Title as [Type] , s.Quantity, s.Cost
from Stationery as s
join TypesOfStationery as ts on s.TypeId = ts.Id
Where Quantity = (select Max(Quantity) from Stationery)
go

CREATE PROCEDURE max_quantity_output
   @maxQuantity INT OUTPUT
AS
BEGIN
    SELECT 
        @maxQuantity = MAX(Quantity)
    FROM 
        Stationery;
END;
go

create procedure min_quantity
as
select s.Id, s.Title, ts.Title as [Type] , s.Quantity, s.Cost
from Stationery as s
join TypesOfStationery as ts on s.TypeId = ts.Id
Where Quantity = (select Min(Quantity) from Stationery)
go

CREATE PROCEDURE min_quantity_output
    @minQuantity INT OUTPUT
AS
BEGIN
    SELECT 
        @minQuantity = MIN(Quantity)
    FROM 
        Stationery;
END;
go

create procedure max_cost
as
select s.Id, s.Title, ts.Title as [Type] , s.Quantity, s.Cost
from Stationery as s
join TypesOfStationery as ts on s.TypeId = ts.Id
Where Cost = (select Max(Cost) from Stationery)
go

CREATE PROCEDURE max_cost_output
    @maxCost DECIMAL(18,2) OUTPUT
AS
BEGIN
    SELECT 
        @maxCost = MAX(Cost)
    FROM 
        Stationery;
END;
go

create procedure min_cost
as
select s.Id, s.Title, ts.Title as [Type] , s.Quantity, s.Cost
from Stationery as s
join TypesOfStationery as ts on s.TypeId = ts.Id
Where Cost = (select Min(Cost) from Stationery)
go

CREATE PROCEDURE min_cost_output
    @minCost DECIMAL(18,2) OUTPUT
AS
BEGIN
    SELECT 
        @minCost = MIN(Cost)
    FROM 
        Stationery;
END;
go

create procedure cur_stationery @name nvarchar(30)
as
select s.Id, s.Title, ts.Title as [Type] , s.Cost, sa.Quantity as Sold
from Stationery as s
join TypesOfStationery as ts on s.TypeId = ts.Id
join Sales as sa on sa.StationeryId = s.Id 
Where s.Title = @name
go

CREATE PROCEDURE find_stationery_by_name
    @StationeryName NVARCHAR(100),
    @StationeryId INT OUTPUT
AS
BEGIN
    SELECT 
        @StationeryId = Stationery.Id
    FROM 
        Stationery
		Join TypesOfStationery as T on T.Id = TypeId
    WHERE 
        T.Title = @StationeryName;
END;
go

create procedure cur_manager @name nvarchar(30)
as
select s.Id, s.Title, ts.Title as [Type] , s.Cost, m.Name as Manager, sa.Quantity as Sold
from Stationery as s
join TypesOfStationery as ts on s.TypeId = ts.Id
join Sales as sa on sa.StationeryId = s.Id 
join Managers as m on sa.ManagerId = m.Id
Where m.Name = @name
go

CREATE PROCEDURE find_manager_by_name
    @ManagerName NVARCHAR(100),
    @ManagerId INT OUTPUT
AS
BEGIN
    SELECT 
        @ManagerId = Id
    FROM 
        Managers
    WHERE 
        Name = @ManagerName;
END;
go 

create procedure cur_firm @title nvarchar(30)
as
select s.Id, s.Title, ts.Title as [Type] , s.Cost, f.Title as Firm, sa.Quantity as Sold
from Stationery as s
join TypesOfStationery as ts on s.TypeId = ts.Id
join Sales as sa on sa.StationeryId = s.Id 
join Firms as f on sa.FirmId = f.Id
Where f.Title = @title
go

CREATE PROCEDURE find_firm_by_name
    @FirmName NVARCHAR(100),
    @FirmId INT OUTPUT
AS
BEGIN
    SELECT 
        @FirmId = Id
    FROM 
        Firms
    WHERE 
        Title = @FirmName;
END;
go

create procedure earliest_sale
as
select s.Id, st.Title as Stationery, f.Title as Firm, m.Name as Manager, s.Quantity, s.PricePerUnitSold, s.DateOfSale
from Sales as s
join Stationery as st on s.StationeryId = st.Id
join Firms as f on s.FirmId = f.Id
join Managers as m on m.Id = s.ManagerId
Where DateOfSale = (select MIN(DateOfSale)
					from Sales )
go

CREATE PROCEDURE earliest_sale_output
    @EarliestSaleId INT OUTPUT
AS
BEGIN
    SELECT TOP 1
        @EarliestSaleId = s.Id
    FROM 
        Sales AS s
    WHERE 
        DateOfSale = (SELECT MIN(DateOfSale) FROM Sales);
END
GO

create procedure avg_stationery
as
select  avg(Quantity) as Average_Count,  t.Title
from Stationery as s
join TypesOfStationery as t on t.Id = s.TypeId
group by t.Title
go

create procedure top_manager
as
select  Top 1 Sum(Quantity) as TotalSold,   m.Name as Manager
from Sales as s
join Managers as m on s.ManagerId = m.Id
group by m.Name
order by TotalSold desc
go

CREATE PROCEDURE top_manager_output
    @TotalSold INT OUTPUT,
    @Manager NVARCHAR(100) OUTPUT
AS
BEGIN
    SELECT TOP 1
        @TotalSold = SUM(s.Quantity),
        @Manager = m.Name
    FROM 
        Sales AS s
    JOIN 
        Managers AS m ON s.ManagerId = m.Id
    GROUP BY 
        m.Name
    ORDER BY 
        SUM(s.Quantity) DESC;
END
GO

create procedure top_manager2
as
select Top 1 Sum(Quantity*PricePerUnitSold) as TotalProfit, m.Name as Manager
from Sales as s
join Managers as m on s.ManagerId = m.Id
group by m.Name
order by TotalProfit desc
go


create procedure top_manager3 @StartDate date , @EndDate date
as
SELECT TOP 1
    m.Name AS ManagerName,
    SUM(s.Quantity * s.PricePerUnitSold) AS TotalProfit
FROM
    Sales s
JOIN
    Managers m ON s.ManagerId = m.Id
WHERE
    s.DateOfSale BETWEEN @StartDate AND @EndDate
GROUP BY
    m.Name
ORDER BY
    TotalProfit DESC;
go


create procedure top_firm
as
SELECT TOP 1
    f.Title AS FirmTitle,
    SUM(s.Quantity * s.PricePerUnitSold) AS TotalPurchaseAmount
FROM
    Sales s
JOIN
    Firms f ON s.FirmId = f.Id
GROUP BY
    f.Title
ORDER BY
    TotalPurchaseAmount DESC
go

create procedure top_stationery
as
SELECT TOP 1
    ts.Title AS TypeTitle,
    SUM(s.Quantity) AS TotalQuantitySold
FROM
    Sales s
JOIN
    Stationery st ON s.StationeryId = st.Id
JOIN
    TypesOfStationery ts ON st.TypeId = ts.Id
GROUP BY
    ts.Title
ORDER BY
    TotalQuantitySold DESC
go

CREATE PROCEDURE top_stationery_output
    @TypeTitle NVARCHAR(100) OUTPUT,
    @TotalQuantitySold INT OUTPUT
AS
BEGIN
    SELECT TOP 1
        @TypeTitle = ts.Title,
        @TotalQuantitySold = SUM(s.Quantity)
    FROM
        Sales s
    JOIN
        Stationery st ON s.StationeryId = st.Id
    JOIN
        TypesOfStationery ts ON st.TypeId = ts.Id
    GROUP BY
        ts.Title
    ORDER BY
        SUM(s.Quantity) DESC;
END
GO

create procedure top_stationery2
as
SELECT 
     ts.Title AS TypeTitle,
    SUM(s.Quantity * s.PricePerUnitSold) AS TotalProfit
FROM
    Sales s
JOIN
    Stationery st ON s.StationeryId = st.Id
JOIN
    TypesOfStationery ts ON st.TypeId = ts.Id
GROUP BY
    ts.Title
ORDER BY
    TotalProfit DESC
go

create procedure stationery_pop
as
SELECT 
    s.Title AS ProductTitle,
    SUM(s.Quantity) AS TotalQuantitySold
FROM
    Sales sa
JOIN
    Stationery s ON sa.StationeryId = s.Id
GROUP BY
    s.Title
ORDER BY
    TotalQuantitySold DESC
go

CREATE PROCEDURE days_without_sales
as 
DECLARE @DaysWithoutSales INT = 5;

SELECT 
    s.Title AS StationeryTitle
FROM
    Stationery s
LEFT JOIN
    Sales sa ON s.Id = sa.StationeryId AND sa.DateOfSale >= DATEADD(DAY, -@DaysWithoutSales, GETDATE())
WHERE
    sa.StationeryId IS NULL
go

CREATE PROCEDURE InsertIntoStationery
    @Title NVARCHAR(50),
    @Quantity INT,
    @Cost DECIMAL(10,2),
    @TypeId INT
AS
INSERT INTO Stationery (Title, Quantity, Cost, TypeId)
VALUES (@Title, @Quantity, @Cost, @TypeId)
go

CREATE PROCEDURE UpdateStationery
    @Id INT,
	@Title NVARCHAR(50),
    @Quantity INT,
    @Cost DECIMAL(10,2),
    @TypeId INT
AS
Update Stationery 
set Title = @Title, Quantity = @Quantity, Cost = @Cost, TypeId = @TypeId
where Id = @Id
go

CREATE PROCEDURE InsertIntoTypes
    @Title NVARCHAR(50)
AS
INSERT INTO TypesOfStationery (Title)
VALUES (@Title)
go

create PROCEDURE UpdateTypes
    @Id INT,
	@Title NVARCHAR(50)
AS
Update TypesOfStationery 
set Title = @Title
where Id = @Id
go

CREATE PROCEDURE InsertIntoManagers
    @Name NVARCHAR(50)
AS
INSERT INTO Managers(Name)
VALUES (@Name)
go

create PROCEDURE UpdateManagers
    @Id INT,
	@Name NVARCHAR(50)
AS
Update Managers 
set [Name] = @Name
where Id = @Id
go


CREATE PROCEDURE InsertIntoFirms
    @Title NVARCHAR(50)
AS
INSERT INTO Firms (Title)
VALUES (@Title)
go

create PROCEDURE UpdateFirms
    @Id INT,
	@Title NVARCHAR(50)
AS
Update Firms 
set Title = @Title
where Id = @Id
go

create procedure delete_stationery
@Id int
as 
delete from Stationery where Id = @Id
go

create procedure delete_type
@Id int
as 
delete from TypesOfStationery where Id = @Id
go

create procedure delete_manager
@Id int
as 
delete from Managers where Id = @Id
go

create procedure delete_firm
@Id int
as 
delete from Firms where Id = @Id
go