USE [master]
GO
/****** Object:  Database [AcademyGroup]    Script Date: 14.09.2021 0:34:04 ******/
CREATE DATABASE [AcademyGroup]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'AcademyGroup', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER01\MSSQL\DATA\AcademyGroup.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'AcademyGroup_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER01\MSSQL\DATA\AcademyGroup_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [AcademyGroup] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [AcademyGroup].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [AcademyGroup] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [AcademyGroup] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [AcademyGroup] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [AcademyGroup] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [AcademyGroup] SET ARITHABORT OFF 
GO
ALTER DATABASE [AcademyGroup] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [AcademyGroup] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [AcademyGroup] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [AcademyGroup] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [AcademyGroup] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [AcademyGroup] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [AcademyGroup] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [AcademyGroup] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [AcademyGroup] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [AcademyGroup] SET  ENABLE_BROKER 
GO
ALTER DATABASE [AcademyGroup] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [AcademyGroup] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [AcademyGroup] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [AcademyGroup] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [AcademyGroup] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [AcademyGroup] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [AcademyGroup] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [AcademyGroup] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [AcademyGroup] SET  MULTI_USER 
GO
ALTER DATABASE [AcademyGroup] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [AcademyGroup] SET DB_CHAINING OFF 
GO
ALTER DATABASE [AcademyGroup] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [AcademyGroup] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [AcademyGroup] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [AcademyGroup] SET QUERY_STORE = OFF
GO
USE [AcademyGroup]
GO
/****** Object:  Table [dbo].[AcademyGroups]    Script Date: 14.09.2021 0:34:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AcademyGroups](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.AcademyGroups] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Students]    Script Date: 14.09.2021 0:34:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Students](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](max) NULL,
	[LastName] [nvarchar](max) NULL,
	[Age] [int] NULL,
	[PointAverage] [float] NULL,
	[AcademyGroup_Id] [int] NULL,
 CONSTRAINT [PK_dbo.Students] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[AcademyGroups] ON 

INSERT [dbo].[AcademyGroups] ([Id], [Name]) VALUES (1, N'ВПД-1411')
INSERT [dbo].[AcademyGroups] ([Id], [Name]) VALUES (2, N'БПУ-1421')
INSERT [dbo].[AcademyGroups] ([Id], [Name]) VALUES (3, N'БПУ-1811')
SET IDENTITY_INSERT [dbo].[AcademyGroups] OFF
SET IDENTITY_INSERT [dbo].[Students] ON 

INSERT [dbo].[Students] ([Id], [FirstName], [LastName], [Age], [PointAverage], [AcademyGroup_Id]) VALUES (1, N'Дмитрий', N'Морозов', 20, 10.5, 1)
INSERT [dbo].[Students] ([Id], [FirstName], [LastName], [Age], [PointAverage], [AcademyGroup_Id]) VALUES (2, N'Екатерина', N'Малова', 27, 11.5, 1)
INSERT [dbo].[Students] ([Id], [FirstName], [LastName], [Age], [PointAverage], [AcademyGroup_Id]) VALUES (3, N'Максим', N'Москалик', 23, 12, 2)
INSERT [dbo].[Students] ([Id], [FirstName], [LastName], [Age], [PointAverage], [AcademyGroup_Id]) VALUES (4, N'Юлия', N'Новикова', 25, 12, 3)
SET IDENTITY_INSERT [dbo].[Students] OFF
/****** Object:  Index [IX_AcademyGroup_Id]    Script Date: 14.09.2021 0:34:05 ******/
CREATE NONCLUSTERED INDEX [IX_AcademyGroup_Id] ON [dbo].[Students]
(
	[AcademyGroup_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Students]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Students_dbo.AcademyGroups_AcademyGroup_Id] FOREIGN KEY([AcademyGroup_Id])
REFERENCES [dbo].[AcademyGroups] ([Id])
GO
ALTER TABLE [dbo].[Students] CHECK CONSTRAINT [FK_dbo.Students_dbo.AcademyGroups_AcademyGroup_Id]
GO
USE [master]
GO
ALTER DATABASE [AcademyGroup] SET  READ_WRITE 
GO
