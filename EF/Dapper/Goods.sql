CREATE DATABASE Goods
GO

USE Goods
GO

CREATE TABLE Countries (
    Id INT PRIMARY KEY IDENTITY,
    Title NVARCHAR(100)
);

CREATE TABLE Ñities (
    Id INT PRIMARY KEY IDENTITY,
    Title NVARCHAR(100)
);

CREATE TABLE Customers (
    Id INT PRIMARY KEY IDENTITY,
    FullName NVARCHAR(100),
    DateOfBirth DATE,
    Gender NVARCHAR(10),
    Email NVARCHAR(100),
    CountryId INT FOREIGN KEY REFERENCES Countries(Id),
    CityId INT FOREIGN KEY REFERENCES Ñities(Id)
);

CREATE TABLE InterestAreas (
    Id INT PRIMARY KEY IDENTITY,
    AreaName NVARCHAR(100)
);

CREATE TABLE CustomerInterests (
    CustomerId INT FOREIGN KEY  REFERENCES Customers(Id),
    InterestAreaId INT FOREIGN KEY (InterestAreaId) REFERENCES InterestAreas(Id)
);

CREATE TABLE Promotions (
    Id INT PRIMARY KEY IDENTITY,
    PromotionName NVARCHAR(100),
    StartDate DATE,
    EndDate DATE,
    CountryId INT FOREIGN KEY REFERENCES Countries(Id)
);

CREATE TABLE PromotionGoods (
    Id INT PRIMARY KEY IDENTITY,
    PromotionId INT,
    GoodName NVARCHAR(100),
    FOREIGN KEY (PromotionId) REFERENCES Promotions(Id)
);


INSERT INTO Countries (Title) VALUES ('USA');
INSERT INTO Countries (Title) VALUES ('UK');
INSERT INTO Countries (Title) VALUES ('France');

INSERT INTO Ñities (Title) VALUES ('New York');
INSERT INTO Ñities (Title) VALUES ('London');
INSERT INTO Ñities (Title) VALUES ('Paris');

INSERT INTO Customers (FullName, DateOfBirth, Gender, Email, CountryId, CityId)
VALUES ('John Doe', '1990-05-15', 'Male', 'john.doe@example.com', 1, 1);

INSERT INTO Customers (FullName, DateOfBirth, Gender, Email, CountryId, CityId)
VALUES ('Jane Smith', '1985-10-20', 'Female', 'jane.smith@example.com', 2, 2);

INSERT INTO InterestAreas (AreaName) VALUES ('Mobile Phones');
INSERT INTO InterestAreas (AreaName) VALUES ('Laptops');
INSERT INTO InterestAreas (AreaName) VALUES ('Kitchen Appliances');

INSERT INTO CustomerInterests (CustomerId, InterestAreaId) VALUES (1, 1);
INSERT INTO CustomerInterests (CustomerId, InterestAreaId) VALUES (1, 2);
INSERT INTO CustomerInterests (CustomerId, InterestAreaId) VALUES (2, 2);

INSERT INTO Promotions (PromotionName, StartDate, EndDate, CountryId) VALUES ('Summer Sale', '2024-06-01', '2024-08-31', 1);
INSERT INTO Promotions (PromotionName, StartDate, EndDate, CountryId) VALUES ('Back to School', '2024-08-15', '2024-09-15', 2);

INSERT INTO PromotionGoods (PromotionId, GoodName) VALUES (1, 'iPhone 13');
INSERT INTO PromotionGoods (PromotionId, GoodName) VALUES (1, 'Samsung Galaxy S22');
INSERT INTO PromotionGoods (PromotionId, GoodName) VALUES (2, 'MacBook Air');
INSERT INTO PromotionGoods (PromotionId, GoodName) VALUES (2, 'HP Spectre x360');
