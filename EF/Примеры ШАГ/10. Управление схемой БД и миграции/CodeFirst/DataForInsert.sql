SET IDENTITY_INSERT [dbo].[AcademyGroups] ON 

INSERT [dbo].[AcademyGroups] ([Id], [Name]) VALUES (1, N'���-1411')
INSERT [dbo].[AcademyGroups] ([Id], [Name]) VALUES (2, N'���-1421')
INSERT [dbo].[AcademyGroups] ([Id], [Name]) VALUES (3, N'���-1811')
SET IDENTITY_INSERT [dbo].[AcademyGroups] OFF
SET IDENTITY_INSERT [dbo].[Students] ON 

INSERT [dbo].[Students] ([Id], [FirstName], [LastName], [Age], [PointAverage], [AcademyGroupId]) VALUES (1, N'�������', N'�������', 20, 10.5, 1)
INSERT [dbo].[Students] ([Id], [FirstName], [LastName], [Age], [PointAverage], [AcademyGroupId]) VALUES (2, N'���������', N'������', 27, 11.5, 1)
INSERT [dbo].[Students] ([Id], [FirstName], [LastName], [Age], [PointAverage], [AcademyGroupId]) VALUES (3, N'������', N'��������', 23, 12, 2)
INSERT [dbo].[Students] ([Id], [FirstName], [LastName], [Age], [PointAverage], [AcademyGroupId]) VALUES (4, N'����', N'��������', 25, 12, 3)
SET IDENTITY_INSERT [dbo].[Students] OFF