USE [FMA]

GO
/****** Object:  Table [dbo].[Accounts]    Script Date: 8/23/2022 1:39:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Accounts](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](255) NULL,
	[LastName] [nvarchar](255) NULL,
	[Username] [nvarchar](255) NULL,
	[Email] [nvarchar](255) NULL,
	[Role] [int] NULL,
	[PasswordHash] [nvarchar](1024) NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Companies]    Script Date: 8/23/2022 1:39:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Companies](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Address] [nvarchar](50) NULL,
	[Country] [nvarchar](50) NULL,
 CONSTRAINT [PK_Companies] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Department]    Script Date: 8/23/2022 1:39:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Department](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](50) NULL,
 CONSTRAINT [PK_Department] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Employee]    Script Date: 8/23/2022 1:39:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employee](
	[Id] [uniqueidentifier] NOT NULL,
	[FullName] [nvarchar](50) NULL,
	[Birthday] [datetime] NULL,
	[DepartmentId] [uniqueidentifier] NULL,
 CONSTRAINT [PK_Employee] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Employees]    Script Date: 8/23/2022 1:39:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employees](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Age] [int] NULL,
	[Position] [nvarchar](50) NULL,
	[CompanyId] [int] NULL,
 CONSTRAINT [PK_Employees] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Products]    Script Date: 8/23/2022 1:39:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Products](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Barcode] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[Rate] [decimal](18, 2) NOT NULL,
	[AddedOn] [datetime] NOT NULL,
	[ModifiedOn] [datetime] NULL,
 CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TodoItems]    Script Date: 8/23/2022 1:39:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TodoItems](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Content] [nvarchar](max) NULL,
	[IsDone] [bit] NULL,
	[CreatedDate] [datetime] NULL,
	[UpdatedDate] [datetime] NULL,
	[AccountId] [bigint] NULL,
 CONSTRAINT [PK_TodoItems] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Accounts] ON 

INSERT [dbo].[Accounts] ([Id], [FirstName], [LastName], [Username], [Email], [Role], [PasswordHash]) VALUES (23, N'Quang', N'Trần Văn', N'User', NULL, 1, N'$2a$11$NRTMaWWDUZ63K1K1rKNNzO5mId6QrTx5iPoYHsU1btyaauLIyyK.W')
INSERT [dbo].[Accounts] ([Id], [FirstName], [LastName], [Username], [Email], [Role], [PasswordHash]) VALUES (24, N'admin', N'test-admin', N'Admin', NULL, 0, N'$2a$11$Nc6gib6tSdyJR2JMSsHKce9Zz8CUpzFFoZRMET4ppPQe.n6OySfem')
INSERT [dbo].[Accounts] ([Id], [FirstName], [LastName], [Username], [Email], [Role], [PasswordHash]) VALUES (25, N'Quang', N'Tran Van', N'Otto84_1661228495336@gmail.com', NULL, 1, N'$2a$11$fY5jtiMk/R5yLmdX.g4Tke9elEHlwUHMZYhkhhqqeXBkZk6qzHT1q')
INSERT [dbo].[Accounts] ([Id], [FirstName], [LastName], [Username], [Email], [Role], [PasswordHash]) VALUES (26, N'Quang', N'Tran Van', N'Ashton_Altenwerth_1661228520261@gmail.com', NULL, 1, N'$2a$11$08N5l9JNrs.9CDTlkF3vYu2NqMNwOTJAdUSiQ/X6Df5oq8FRTR5QW')
INSERT [dbo].[Accounts] ([Id], [FirstName], [LastName], [Username], [Email], [Role], [PasswordHash]) VALUES (27, N'Lori', N'{{randomLastName}}', N'Jammie.Stoltenberg', NULL, 1, N'$2a$11$sBTRyGQItnvCqtJP6kV74uQbG2ORUcQghlKqKFxBT5Q.wfv2qQl4G')
INSERT [dbo].[Accounts] ([Id], [FirstName], [LastName], [Username], [Email], [Role], [PasswordHash]) VALUES (28, N'Bonita', N'Kuhic', N'Delphia30', NULL, 1, N'$2a$11$kGvtjWWGBMSfV5iqiBiM2u2MNXgvANNwVo0RmvknXweJ7zWLPqkNu')
INSERT [dbo].[Accounts] ([Id], [FirstName], [LastName], [Username], [Email], [Role], [PasswordHash]) VALUES (29, N'Kaitlyn', N'Legros', N'{{$userNameRandom}}', NULL, 1, N'$2a$11$Bx3cFzyxG4V5p3PAyfNbyuh2.o2.S84grmxogBbU3aCGeXnRy7Cj.')
INSERT [dbo].[Accounts] ([Id], [FirstName], [LastName], [Username], [Email], [Role], [PasswordHash]) VALUES (30, N'Mossie', N'Blick', N'fX9o4Iv3Gk6TS2eeFCrSGCUndwHf1tYieFYTYybL1gRU69WWgE', NULL, 1, N'$2a$11$wAJnI/O3fGFgmb4Yq2mzXuKWfrcwJQy28lapzAyBhTOU6.GOumkO6')
SET IDENTITY_INSERT [dbo].[Accounts] OFF
GO
SET IDENTITY_INSERT [dbo].[Companies] ON 

INSERT [dbo].[Companies] ([Id], [Name], [Address], [Country]) VALUES (1, N'VMO', N'Tôn Thất Thuyết', N'Việt Nam')
INSERT [dbo].[Companies] ([Id], [Name], [Address], [Country]) VALUES (2, N'FPT', N'HN', N'VN')
INSERT [dbo].[Companies] ([Id], [Name], [Address], [Country]) VALUES (3, N'FPT1', N'HN', N'VN')
INSERT [dbo].[Companies] ([Id], [Name], [Address], [Country]) VALUES (4, N'string', N'string', N'string')
INSERT [dbo].[Companies] ([Id], [Name], [Address], [Country]) VALUES (5, N'Công ty 123', N'HN', N'VN')
INSERT [dbo].[Companies] ([Id], [Name], [Address], [Country]) VALUES (6, N'Công ty 456', N'HN', N'VN')
INSERT [dbo].[Companies] ([Id], [Name], [Address], [Country]) VALUES (7, N'Công ty 789', NULL, NULL)
SET IDENTITY_INSERT [dbo].[Companies] OFF
GO
INSERT [dbo].[Department] ([Id], [Name]) VALUES (N'ebbfd7d6-76a7-41d4-9525-2968440864be', N'Hành chính nhân sự')
INSERT [dbo].[Department] ([Id], [Name]) VALUES (N'83810804-fd4b-46d0-807f-5010088a6e23', N'Kế toán')
INSERT [dbo].[Department] ([Id], [Name]) VALUES (N'b3ee3db4-0540-4adb-88f9-602c0b34591d', N'Developer')
INSERT [dbo].[Department] ([Id], [Name]) VALUES (N'171cee4a-2cad-4595-9ecd-b80bac0f96a1', N'Testing')
GO
SET IDENTITY_INSERT [dbo].[Employees] ON 

INSERT [dbo].[Employees] ([Id], [Name], [Age], [Position], [CompanyId]) VALUES (1, N'Tran Van Quang', 28, N'Dev', 1)
INSERT [dbo].[Employees] ([Id], [Name], [Age], [Position], [CompanyId]) VALUES (2, N'Nguyen Van Teo', 19, N'Tester', 1)
SET IDENTITY_INSERT [dbo].[Employees] OFF
GO
SET IDENTITY_INSERT [dbo].[Products] ON 

INSERT [dbo].[Products] ([Id], [Name], [Barcode], [Description], [Rate], [AddedOn], [ModifiedOn]) VALUES (1, N'Điện thoại', N'1233', N'This is description', CAST(12.00 AS Decimal(18, 2)), CAST(N'2022-08-01T00:00:00.000' AS DateTime), CAST(N'2022-03-01T00:00:00.000' AS DateTime))
INSERT [dbo].[Products] ([Id], [Name], [Barcode], [Description], [Rate], [AddedOn], [ModifiedOn]) VALUES (2, N'f', N'1', N'b', CAST(251.00 AS Decimal(18, 2)), CAST(N'2022-08-19T10:24:41.590' AS DateTime), NULL)
INSERT [dbo].[Products] ([Id], [Name], [Barcode], [Description], [Rate], [AddedOn], [ModifiedOn]) VALUES (3, N'c', N'2', N'b', CAST(675.00 AS Decimal(18, 2)), CAST(N'2022-08-19T10:24:48.637' AS DateTime), NULL)
INSERT [dbo].[Products] ([Id], [Name], [Barcode], [Description], [Rate], [AddedOn], [ModifiedOn]) VALUES (4, N'3xE5U5J9', N'3xE5U5J9', N'3xE5U5J9', CAST(0.50 AS Decimal(18, 2)), CAST(N'2022-08-19T10:32:23.820' AS DateTime), NULL)
INSERT [dbo].[Products] ([Id], [Name], [Barcode], [Description], [Rate], [AddedOn], [ModifiedOn]) VALUES (5, N'5SEDSRKs', N'5SEDSRKs', N'5SEDSRKs', CAST(0.50 AS Decimal(18, 2)), CAST(N'2022-08-19T10:32:33.590' AS DateTime), NULL)
SET IDENTITY_INSERT [dbo].[Products] OFF
GO
SET IDENTITY_INSERT [dbo].[TodoItems] ON 

INSERT [dbo].[TodoItems] ([Id], [Content], [IsDone], [CreatedDate], [UpdatedDate], [AccountId]) VALUES (26, N'Công việc 1', 0, CAST(N'2022-08-23T06:27:55.953' AS DateTime), NULL, 24)
INSERT [dbo].[TodoItems] ([Id], [Content], [IsDone], [CreatedDate], [UpdatedDate], [AccountId]) VALUES (27, N'Công việc 2', 1, CAST(N'2022-08-23T06:27:56.003' AS DateTime), NULL, 24)
INSERT [dbo].[TodoItems] ([Id], [Content], [IsDone], [CreatedDate], [UpdatedDate], [AccountId]) VALUES (28, N'Công việc 3', 0, CAST(N'2022-08-23T06:27:56.100' AS DateTime), NULL, 24)
INSERT [dbo].[TodoItems] ([Id], [Content], [IsDone], [CreatedDate], [UpdatedDate], [AccountId]) VALUES (29, N'Công việc 4', 0, CAST(N'2022-08-23T06:27:56.223' AS DateTime), NULL, 24)
SET IDENTITY_INSERT [dbo].[TodoItems] OFF
GO
ALTER TABLE [dbo].[Employee]  WITH CHECK ADD  CONSTRAINT [FK_Employee_Department] FOREIGN KEY([DepartmentId])
REFERENCES [dbo].[Department] ([Id])
GO
ALTER TABLE [dbo].[Employee] CHECK CONSTRAINT [FK_Employee_Department]
GO
ALTER TABLE [dbo].[Employees]  WITH CHECK ADD  CONSTRAINT [FK_Employees_Companies] FOREIGN KEY([CompanyId])
REFERENCES [dbo].[Companies] ([Id])
GO
ALTER TABLE [dbo].[Employees] CHECK CONSTRAINT [FK_Employees_Companies]
GO
ALTER TABLE [dbo].[TodoItems]  WITH CHECK ADD  CONSTRAINT [FK_TodoItems_Accounts] FOREIGN KEY([AccountId])
REFERENCES [dbo].[Accounts] ([Id])
GO
ALTER TABLE [dbo].[TodoItems] CHECK CONSTRAINT [FK_TodoItems_Accounts]
GO
/****** Object:  StoredProcedure [dbo].[GetListCompaniesWithPaging]    Script Date: 8/23/2022 1:39:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetListCompaniesWithPaging] @PageNumber INT, @PageSize INT, @SearchStr NVARCHAR(255)
AS
BEGIN
	 DECLARE @maxPagSize INT, @Skip INT, @Take INT;
	 SET @maxPagSize = 50;
	
	IF(@PageSize > 0 AND @PageSize<= @maxPagSize)
		SET @PageSize = @PageSize
	ELSE
		SET @PageSize = @maxPagSize
    
    SET @Skip = (@PageNumber - 1) * @PageSize;
    SET @Take = @PageSize;

	SELECT 
                            COUNT(*)
                            FROM Companies WHERE Name LIKE N'%'+@SearchStr+'%'
 
                            SELECT  * FROM Companies WHERE Name LIKE N'%'+@SearchStr+'%'
                            ORDER BY Id
                            OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY
END
GO
/****** Object:  StoredProcedure [dbo].[ShowCompanyForProvidedEmployeeId]    Script Date: 8/23/2022 1:39:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ShowCompanyForProvidedEmployeeId] @Id int
AS
SELECT c.Id, c.Name, c.Address, c.Country
FROM Companies c JOIN Employees e ON c.Id = e.CompanyId
Where e.Id = @Id
GO
