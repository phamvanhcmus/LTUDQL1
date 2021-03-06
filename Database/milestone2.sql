USE [master]
GO
/****** Object:  Database [MS02]    Script Date: 1/25/2021 11:10:37 AM ******/
CREATE DATABASE [MS02]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'MS02', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\MS02.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'MS02_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\MS02_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [MS02] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [MS02].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [MS02] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [MS02] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [MS02] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [MS02] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [MS02] SET ARITHABORT OFF 
GO
ALTER DATABASE [MS02] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [MS02] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [MS02] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [MS02] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [MS02] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [MS02] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [MS02] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [MS02] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [MS02] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [MS02] SET  ENABLE_BROKER 
GO
ALTER DATABASE [MS02] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [MS02] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [MS02] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [MS02] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [MS02] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [MS02] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [MS02] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [MS02] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [MS02] SET  MULTI_USER 
GO
ALTER DATABASE [MS02] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [MS02] SET DB_CHAINING OFF 
GO
ALTER DATABASE [MS02] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [MS02] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [MS02] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [MS02] SET QUERY_STORE = OFF
GO
USE [MS02]
GO
/****** Object:  Table [dbo].[Category]    Script Date: 1/25/2021 11:10:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Category](
	[CategoryID] [int] IDENTITY(1,1) NOT NULL,
	[CategoryName] [nvarchar](100) NULL,
	[CategoryImage] [varchar](100) NULL,
	[CategoryDescription] [nvarchar](100) NULL,
	[Category_CreateAt] [date] NULL,
	[Category_UpdateAt] [date] NULL,
 CONSTRAINT [PK_Category] PRIMARY KEY CLUSTERED 
(
	[CategoryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Customer]    Script Date: 1/25/2021 11:10:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customer](
	[Customer_ID] [int] IDENTITY(1,1) NOT NULL,
	[Customer_Name] [nvarchar](100) NULL,
	[Customer_Tel] [varchar](12) NOT NULL,
	[Customer_Address] [nvarchar](100) NULL,
	[Customer_Email] [varchar](50) NULL,
 CONSTRAINT [PK_Customer] PRIMARY KEY CLUSTERED 
(
	[Customer_Tel] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Login]    Script Date: 1/25/2021 11:10:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Login](
	[UserName] [varchar](100) NOT NULL,
	[Password] [varchar](100) NULL,
 CONSTRAINT [PK_Login] PRIMARY KEY CLUSTERED 
(
	[UserName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Permission]    Script Date: 1/25/2021 11:10:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Permission](
	[UserName] [varchar](100) NULL,
	[Role] [varchar](100) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Product]    Script Date: 1/25/2021 11:10:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
	[ProductID] [int] IDENTITY(1,1) NOT NULL,
	[CateID] [int] NULL,
	[ProductName] [nvarchar](200) NULL,
	[ProductImage] [varchar](100) NULL,
	[ProductPrice] [int] NULL,
	[Product_isFavor] [bit] NULL,
	[Product_Quantity] [int] NULL,
	[ProductDescription] [nvarchar](200) NULL,
	[Product_createAt] [datetime] NULL,
	[Product_updateAt] [datetime] NULL,
 CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED 
(
	[ProductID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Purchase]    Script Date: 1/25/2021 11:10:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Purchase](
	[PurchaseID] [int] IDENTITY(1,1) NOT NULL,
	[Total] [int] NULL,
	[CreateAt] [datetime] NULL,
	[Status] [int] NULL,
	[Customer_Tel] [varchar](12) NULL,
 CONSTRAINT [PK_Purchase] PRIMARY KEY CLUSTERED 
(
	[PurchaseID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PurchaseDetail]    Script Date: 1/25/2021 11:10:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PurchaseDetail](
	[PurchaseDetail_ID] [int] IDENTITY(1,1) NOT NULL,
	[PurchaseID] [int] NULL,
	[ProductID] [int] NULL,
	[Price] [int] NULL,
	[Quantity] [int] NULL,
	[Total] [int] NULL,
 CONSTRAINT [PK_PurchaseDetail] PRIMARY KEY CLUSTERED 
(
	[PurchaseDetail_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PurchaseStateEnum]    Script Date: 1/25/2021 11:10:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PurchaseStateEnum](
	[EnumKey] [nvarchar](100) NOT NULL,
	[Value] [int] NOT NULL,
	[Description] [nvarchar](100) NULL,
 CONSTRAINT [PK_PurchaseStateEnum] PRIMARY KEY CLUSTERED 
(
	[Value] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Category] ON 

INSERT [dbo].[Category] ([CategoryID], [CategoryName], [CategoryImage], [CategoryDescription], [Category_CreateAt], [Category_UpdateAt]) VALUES (1, N'Asus ', N'asus00.jpg', N'Các sản phẩm đến từ Asus', CAST(N'2020-12-07' AS Date), CAST(N'2020-12-07' AS Date))
INSERT [dbo].[Category] ([CategoryID], [CategoryName], [CategoryImage], [CategoryDescription], [Category_CreateAt], [Category_UpdateAt]) VALUES (2, N'Microsoft', N'microsoft00.png', N'Các sản phẩm đến từ Microsoft', CAST(N'2020-12-07' AS Date), CAST(N'2020-12-07' AS Date))
INSERT [dbo].[Category] ([CategoryID], [CategoryName], [CategoryImage], [CategoryDescription], [Category_CreateAt], [Category_UpdateAt]) VALUES (3, N'Apple', N'apple00.png', N'Các sản phẩm đến từ Apple', CAST(N'2020-12-08' AS Date), CAST(N'2020-12-08' AS Date))
SET IDENTITY_INSERT [dbo].[Category] OFF
GO
SET IDENTITY_INSERT [dbo].[Customer] ON 

INSERT [dbo].[Customer] ([Customer_ID], [Customer_Name], [Customer_Tel], [Customer_Address], [Customer_Email]) VALUES (1, N'Nguyễn Thị A', N'0147852369', N'123 Nguyễn Văn Trỗi, Phường 12, Quận 11', N'nta@gmail.com')
INSERT [dbo].[Customer] ([Customer_ID], [Customer_Name], [Customer_Tel], [Customer_Address], [Customer_Email]) VALUES (2, N'Trần Văn B', N'123456789', N'234 Lê Đại Hành, Phường 2, Quận Tân Bình', N'tvb@gmail.com')
INSERT [dbo].[Customer] ([Customer_ID], [Customer_Name], [Customer_Tel], [Customer_Address], [Customer_Email]) VALUES (3, N'Miu Lê', N'789456123', N'22 Trần Hưng Đạo, Phường Cầu Ông Lãnh, Quận 1', N'ml@gmail.com')
SET IDENTITY_INSERT [dbo].[Customer] OFF
GO
INSERT [dbo].[Login] ([UserName], [Password]) VALUES (N'admin', N'admin')
INSERT [dbo].[Login] ([UserName], [Password]) VALUES (N'tri123', N'123')
GO
INSERT [dbo].[Permission] ([UserName], [Role]) VALUES (N'admin', N'admin')
INSERT [dbo].[Permission] ([UserName], [Role]) VALUES (N'tri123', N'sale')
GO
SET IDENTITY_INSERT [dbo].[Product] ON 

INSERT [dbo].[Product] ([ProductID], [CateID], [ProductName], [ProductImage], [ProductPrice], [Product_isFavor], [Product_Quantity], [ProductDescription], [Product_createAt], [Product_updateAt]) VALUES (1, 1, N'Asus', N'asus01.jpg', 15000000, 0, 12, N'ASUS ZenBook Edition 30 UX334FL -30- A4057T I7 8565U MX250 RAM 8GB SSD 512GB 13.3" FHD', CAST(N'2020-11-26T00:00:00.000' AS DateTime), CAST(N'2020-11-26T00:00:00.000' AS DateTime))
INSERT [dbo].[Product] ([ProductID], [CateID], [ProductName], [ProductImage], [ProductPrice], [Product_isFavor], [Product_Quantity], [ProductDescription], [Product_createAt], [Product_updateAt]) VALUES (2, 1, N'Asus', N'asus02.jpg', 67000000, 0, 4, N'ASUS A412DA - EK346T AMD Ryzen 3 3200U RAM 4GB SSD 512GB 14" FHD WINDOW 10', CAST(N'2020-11-26T00:00:00.000' AS DateTime), CAST(N'2020-11-26T00:00:00.000' AS DateTime))
INSERT [dbo].[Product] ([ProductID], [CateID], [ProductName], [ProductImage], [ProductPrice], [Product_isFavor], [Product_Quantity], [ProductDescription], [Product_createAt], [Product_updateAt]) VALUES (3, 1, N'Asus', N'asus03.jpg', 82000000, 0, 5, N'ASUS A412FA - EK153T I5 8265U RAM 8GB HDD 1TB 14" FHD WINDOW 10', CAST(N'2020-11-27T00:00:00.000' AS DateTime), CAST(N'2020-11-27T00:00:00.000' AS DateTime))
INSERT [dbo].[Product] ([ProductID], [CateID], [ProductName], [ProductImage], [ProductPrice], [Product_isFavor], [Product_Quantity], [ProductDescription], [Product_createAt], [Product_updateAt]) VALUES (4, 1, N'Asus', N'asus04.jpg', 67000000, 0, 7, N'ASUS A412FA - EK155T I3 8145U RAM 4GB HDD 1TB 14" FHD WINDOW 10', CAST(N'2020-11-28T00:00:00.000' AS DateTime), CAST(N'2020-11-28T00:00:00.000' AS DateTime))
INSERT [dbo].[Product] ([ProductID], [CateID], [ProductName], [ProductImage], [ProductPrice], [Product_isFavor], [Product_Quantity], [ProductDescription], [Product_createAt], [Product_updateAt]) VALUES (5, 1, N'Asus', N'asus05.jpg', 27000000, 0, 26, N'ASUS F560UD-BQ327T I5 8250U GTX 1050 RAM 8GB 1TB HDD 15.6" FHD', CAST(N'2020-11-29T00:00:00.000' AS DateTime), CAST(N'2020-11-29T00:00:00.000' AS DateTime))
INSERT [dbo].[Product] ([ProductID], [CateID], [ProductName], [ProductImage], [ProductPrice], [Product_isFavor], [Product_Quantity], [ProductDescription], [Product_createAt], [Product_updateAt]) VALUES (6, 1, N'Asus', N'asus06.jpg', 20000000, 1, 18, N'ASUS F570ZD-E4297T Ryzen 5-2500U GTX 1050 RAM 4GB 1TB HDD 15.6" FHD', CAST(N'2020-11-30T00:00:00.000' AS DateTime), CAST(N'2020-11-30T00:00:00.000' AS DateTime))
INSERT [dbo].[Product] ([ProductID], [CateID], [ProductName], [ProductImage], [ProductPrice], [Product_isFavor], [Product_Quantity], [ProductDescription], [Product_createAt], [Product_updateAt]) VALUES (7, 1, N'Asus', N'asus07.jpg', 27000000, 1, 2, N'Asus F571GD-BQ319T (Black) I5-9300H GTX 1050 RAM 8GB 512GB SSD 15.6" FHD', CAST(N'2020-12-01T00:00:00.000' AS DateTime), CAST(N'2020-12-01T00:00:00.000' AS DateTime))
INSERT [dbo].[Product] ([ProductID], [CateID], [ProductName], [ProductImage], [ProductPrice], [Product_isFavor], [Product_Quantity], [ProductDescription], [Product_createAt], [Product_updateAt]) VALUES (8, 1, N'Asus', N'asus08.jpg', 79000000, 0, 6, N'ASUS Laptop ASUS ROG Strix SCAR III G731G_N-WH100T I7 9750H RTX 2070 RAM 16GB 1TB SSD 17.3" FHD', CAST(N'2020-12-02T00:00:00.000' AS DateTime), CAST(N'2020-12-02T00:00:00.000' AS DateTime))
INSERT [dbo].[Product] ([ProductID], [CateID], [ProductName], [ProductImage], [ProductPrice], [Product_isFavor], [Product_Quantity], [ProductDescription], [Product_createAt], [Product_updateAt]) VALUES (9, 1, N'Asus', N'asus09.jpg', 90000000, 0, 7, N'ASUS ProArt StudioBook Pro 15 I7 9750H Quadro RTX 5000 RAM 48GB 2TB SSD ( W500G5T XS77 )', CAST(N'2020-12-03T00:00:00.000' AS DateTime), CAST(N'2020-12-03T00:00:00.000' AS DateTime))
INSERT [dbo].[Product] ([ProductID], [CateID], [ProductName], [ProductImage], [ProductPrice], [Product_isFavor], [Product_Quantity], [ProductDescription], [Product_createAt], [Product_updateAt]) VALUES (10, 1, N'Asus', N'asus10.jpg', 86000000, 0, 12, N'ASUS ProArt StudioBook Pro 17 I7 9750H Quadro RTX 3000 RAM 16GB 1TB SSD ( W700G3T XH77 )', CAST(N'2020-12-04T00:00:00.000' AS DateTime), CAST(N'2020-12-04T00:00:00.000' AS DateTime))
INSERT [dbo].[Product] ([ProductID], [CateID], [ProductName], [ProductImage], [ProductPrice], [Product_isFavor], [Product_Quantity], [ProductDescription], [Product_createAt], [Product_updateAt]) VALUES (11, 1, N'Asus', N'asus11.jpg', 41000000, 0, 9, N'ASUS ProArt StudioBook Pro 17 Xeon E-2276M Quadro RTX 3000 RAM 32GB 2TB SSD ( W700G3T XH99 )', CAST(N'2020-12-05T00:00:00.000' AS DateTime), CAST(N'2020-12-05T00:00:00.000' AS DateTime))
INSERT [dbo].[Product] ([ProductID], [CateID], [ProductName], [ProductImage], [ProductPrice], [Product_isFavor], [Product_Quantity], [ProductDescription], [Product_createAt], [Product_updateAt]) VALUES (12, 1, N'Asus', N'asus12.jpg', 67000000, 0, 10, N'ASUS ROG Strix SCAR II GL504G I7 8750H GTX 1060 RAM 16GB 512GB SSD 15.6" FHD 144 Hz', CAST(N'2020-12-06T00:00:00.000' AS DateTime), CAST(N'2020-12-06T00:00:00.000' AS DateTime))
INSERT [dbo].[Product] ([ProductID], [CateID], [ProductName], [ProductImage], [ProductPrice], [Product_isFavor], [Product_Quantity], [ProductDescription], [Product_createAt], [Product_updateAt]) VALUES (13, 2, N'Microsoft', N'microsoft01.jpg                                ', 32000000, 1, 12, N'Surface Laptop 1 - i7-8GB-256GB', CAST(N'2020-11-25T00:00:00.000' AS DateTime), CAST(N'2020-11-25T00:00:00.000' AS DateTime))
INSERT [dbo].[Product] ([ProductID], [CateID], [ProductName], [ProductImage], [ProductPrice], [Product_isFavor], [Product_Quantity], [ProductDescription], [Product_createAt], [Product_updateAt]) VALUES (14, 2, N'Microsoft', N'microsoft02.jpg                                ', 16000000, 0, 6, N'Surface Laptop 2 - i5-8GB-256GB', CAST(N'2020-11-26T00:00:00.000' AS DateTime), CAST(N'2020-11-26T00:00:00.000' AS DateTime))
INSERT [dbo].[Product] ([ProductID], [CateID], [ProductName], [ProductImage], [ProductPrice], [Product_isFavor], [Product_Quantity], [ProductDescription], [Product_createAt], [Product_updateAt]) VALUES (15, 2, N'Microsoft', N'microsoft03.jpg                                ', 28000000, 0, 4, N'Surface Laptop 3 13" - i5 1035G7 8GB 128GB', CAST(N'2020-11-27T00:00:00.000' AS DateTime), CAST(N'2020-11-27T00:00:00.000' AS DateTime))
INSERT [dbo].[Product] ([ProductID], [CateID], [ProductName], [ProductImage], [ProductPrice], [Product_isFavor], [Product_Quantity], [ProductDescription], [Product_createAt], [Product_updateAt]) VALUES (16, 2, N'Microsoft', N'microsoft04.jpg                                ', 32000000, 0, 2, N'Surface Laptop 3 13" - i7 1065G7 16GB 1TB', CAST(N'2020-11-28T00:00:00.000' AS DateTime), CAST(N'2020-11-28T00:00:00.000' AS DateTime))
INSERT [dbo].[Product] ([ProductID], [CateID], [ProductName], [ProductImage], [ProductPrice], [Product_isFavor], [Product_Quantity], [ProductDescription], [Product_createAt], [Product_updateAt]) VALUES (17, 2, N'Microsoft', N'microsoft05.jpg                                ', 20000000, 1, 5, N'Surface Laptop 3 15" - AMD Ryzen 5 3850U 8GB 128GB', CAST(N'2020-11-29T00:00:00.000' AS DateTime), CAST(N'2020-11-29T00:00:00.000' AS DateTime))
INSERT [dbo].[Product] ([ProductID], [CateID], [ProductName], [ProductImage], [ProductPrice], [Product_isFavor], [Product_Quantity], [ProductDescription], [Product_createAt], [Product_updateAt]) VALUES (18, 2, N'Microsoft', N'microsoft06.jpg                                ', 54000000, 0, 13, N'Surface Laptop 3 15" - AMD Ryzen 7 3780U 16GB 512GB', CAST(N'2020-11-30T00:00:00.000' AS DateTime), CAST(N'2020-11-30T00:00:00.000' AS DateTime))
INSERT [dbo].[Product] ([ProductID], [CateID], [ProductName], [ProductImage], [ProductPrice], [Product_isFavor], [Product_Quantity], [ProductDescription], [Product_createAt], [Product_updateAt]) VALUES (19, 2, N'Microsoft', N'microsoft07.jpg                                ', 70000000, 0, 9, N'Surface Pro 6 - i5-16GB-256GB', CAST(N'2020-12-01T00:00:00.000' AS DateTime), CAST(N'2020-12-01T00:00:00.000' AS DateTime))
INSERT [dbo].[Product] ([ProductID], [CateID], [ProductName], [ProductImage], [ProductPrice], [Product_isFavor], [Product_Quantity], [ProductDescription], [Product_createAt], [Product_updateAt]) VALUES (20, 2, N'Microsoft', N'microsoft08.jpg                                ', 68000000, 0, 10, N'Surface Pro 6 - i5-8GB-256GB with Type Cover', CAST(N'2020-12-02T00:00:00.000' AS DateTime), CAST(N'2020-12-02T00:00:00.000' AS DateTime))
INSERT [dbo].[Product] ([ProductID], [CateID], [ProductName], [ProductImage], [ProductPrice], [Product_isFavor], [Product_Quantity], [ProductDescription], [Product_createAt], [Product_updateAt]) VALUES (21, 2, N'Microsoft', N'microsoft09.jpg                                ', 59000000, 0, 4, N'Surface Pro 6 - i7-8GB-256GB', CAST(N'2020-12-03T00:00:00.000' AS DateTime), CAST(N'2020-12-03T00:00:00.000' AS DateTime))
INSERT [dbo].[Product] ([ProductID], [CateID], [ProductName], [ProductImage], [ProductPrice], [Product_isFavor], [Product_Quantity], [ProductDescription], [Product_createAt], [Product_updateAt]) VALUES (22, 2, N'Microsoft', N'microsoft10.jpg                                ', 68000000, 1, 17, N'Surface Pro 7 - i3 4GB 128GB', CAST(N'2020-12-04T00:00:00.000' AS DateTime), CAST(N'2020-12-04T00:00:00.000' AS DateTime))
INSERT [dbo].[Product] ([ProductID], [CateID], [ProductName], [ProductImage], [ProductPrice], [Product_isFavor], [Product_Quantity], [ProductDescription], [Product_createAt], [Product_updateAt]) VALUES (23, 2, N'Microsoft', N'microsoft11.jpg                                ', 80000000, 0, 24, N'Surface Pro 7 - i5 16GB 256GB', CAST(N'2020-12-05T00:00:00.000' AS DateTime), CAST(N'2020-12-05T00:00:00.000' AS DateTime))
INSERT [dbo].[Product] ([ProductID], [CateID], [ProductName], [ProductImage], [ProductPrice], [Product_isFavor], [Product_Quantity], [ProductDescription], [Product_createAt], [Product_updateAt]) VALUES (24, 2, N'Microsoft', N'microsoft12.jpg                                ', 82000000, 1, 14, N'Surface Pro 7 - i7 16GB 256GB', CAST(N'2020-12-06T00:00:00.000' AS DateTime), CAST(N'2020-12-06T00:00:00.000' AS DateTime))
INSERT [dbo].[Product] ([ProductID], [CateID], [ProductName], [ProductImage], [ProductPrice], [Product_isFavor], [Product_Quantity], [ProductDescription], [Product_createAt], [Product_updateAt]) VALUES (25, 3, N'Apple', N'apple01.jpg', 69000000, 1, 12, N'Macbook Air 13" 2019 MVFH2 128GB', CAST(N'2020-11-26T00:00:00.000' AS DateTime), CAST(N'2020-11-26T00:00:00.000' AS DateTime))
INSERT [dbo].[Product] ([ProductID], [CateID], [ProductName], [ProductImage], [ProductPrice], [Product_isFavor], [Product_Quantity], [ProductDescription], [Product_createAt], [Product_updateAt]) VALUES (26, 3, N'Apple', N'apple02.jpg', 74000000, 0, 3, N'Macbook Air 13" 2019 MVFK2 128GB Silver', CAST(N'2020-11-27T00:00:00.000' AS DateTime), CAST(N'2020-11-27T00:00:00.000' AS DateTime))
INSERT [dbo].[Product] ([ProductID], [CateID], [ProductName], [ProductImage], [ProductPrice], [Product_isFavor], [Product_Quantity], [ProductDescription], [Product_createAt], [Product_updateAt]) VALUES (27, 3, N'Apple', N'apple03.jpg', 13000000, 0, 5, N'Macbook Air 13" 2019 MVFL2 256GB Silver', CAST(N'2020-11-28T00:00:00.000' AS DateTime), CAST(N'2020-11-28T00:00:00.000' AS DateTime))
INSERT [dbo].[Product] ([ProductID], [CateID], [ProductName], [ProductImage], [ProductPrice], [Product_isFavor], [Product_Quantity], [ProductDescription], [Product_createAt], [Product_updateAt]) VALUES (28, 3, N'Apple', N'apple04.jpg', 53000000, 0, 7, N'Macbook Air 13" 2019 MVFM2 128GB Gold', CAST(N'2020-11-29T00:00:00.000' AS DateTime), CAST(N'2020-11-29T00:00:00.000' AS DateTime))
INSERT [dbo].[Product] ([ProductID], [CateID], [ProductName], [ProductImage], [ProductPrice], [Product_isFavor], [Product_Quantity], [ProductDescription], [Product_createAt], [Product_updateAt]) VALUES (29, 3, N'Apple', N'apple05.jpg', 88000000, 0, 6, N'Macbook Air 13" 2019 MVFN2 256GB Gold', CAST(N'2020-11-30T00:00:00.000' AS DateTime), CAST(N'2020-11-30T00:00:00.000' AS DateTime))
INSERT [dbo].[Product] ([ProductID], [CateID], [ProductName], [ProductImage], [ProductPrice], [Product_isFavor], [Product_Quantity], [ProductDescription], [Product_createAt], [Product_updateAt]) VALUES (30, 3, N'Apple', N'apple06.jpg', 59000000, 0, 2, N'MacBook Pro 15" 2018 Gray 512GB MR932', CAST(N'2020-12-01T00:00:00.000' AS DateTime), CAST(N'2020-12-01T00:00:00.000' AS DateTime))
INSERT [dbo].[Product] ([ProductID], [CateID], [ProductName], [ProductImage], [ProductPrice], [Product_isFavor], [Product_Quantity], [ProductDescription], [Product_createAt], [Product_updateAt]) VALUES (31, 3, N'Apple', N'apple07.jpg', 19000000, 1, 14, N'MacBook Pro 15" 2019 Silver 256GB MV922', CAST(N'2020-12-02T00:00:00.000' AS DateTime), CAST(N'2020-12-02T00:00:00.000' AS DateTime))
INSERT [dbo].[Product] ([ProductID], [CateID], [ProductName], [ProductImage], [ProductPrice], [Product_isFavor], [Product_Quantity], [ProductDescription], [Product_createAt], [Product_updateAt]) VALUES (32, 3, N'Apple', N'apple08.jpg', 44000000, 0, 16, N'MacBook Pro 16" 2019 Silver 512GB MVVL2', CAST(N'2020-12-03T00:00:00.000' AS DateTime), CAST(N'2020-12-03T00:00:00.000' AS DateTime))
INSERT [dbo].[Product] ([ProductID], [CateID], [ProductName], [ProductImage], [ProductPrice], [Product_isFavor], [Product_Quantity], [ProductDescription], [Product_createAt], [Product_updateAt]) VALUES (33, 3, N'Apple', N'apple09.jpg', 71000000, 0, 3, N'MacBook Pro 16" 2019 Silver 1TB MVVM2', CAST(N'2020-12-04T00:00:00.000' AS DateTime), CAST(N'2020-12-04T00:00:00.000' AS DateTime))
INSERT [dbo].[Product] ([ProductID], [CateID], [ProductName], [ProductImage], [ProductPrice], [Product_isFavor], [Product_Quantity], [ProductDescription], [Product_createAt], [Product_updateAt]) VALUES (34, 3, N'Apple', N'apple10.jpg', 21000000, 1, 5, N'MacBook Pro 16" 2019 Gray 512GB MVVJ2', CAST(N'2020-12-05T00:00:00.000' AS DateTime), CAST(N'2020-12-05T00:00:00.000' AS DateTime))
INSERT [dbo].[Product] ([ProductID], [CateID], [ProductName], [ProductImage], [ProductPrice], [Product_isFavor], [Product_Quantity], [ProductDescription], [Product_createAt], [Product_updateAt]) VALUES (35, 3, N'Apple', N'apple11.jpg', 30000000, 0, 8, N'MacBook Pro 15" 2019 Silver 256GB MV922', CAST(N'2020-12-06T00:00:00.000' AS DateTime), CAST(N'2020-12-06T00:00:00.000' AS DateTime))
INSERT [dbo].[Product] ([ProductID], [CateID], [ProductName], [ProductImage], [ProductPrice], [Product_isFavor], [Product_Quantity], [ProductDescription], [Product_createAt], [Product_updateAt]) VALUES (36, 3, N'Apple', N'apple12.jpg', 15000000, 1, 19, N'MacBook Pro 15" 2019 Gray 512GB MV912', CAST(N'2020-12-07T00:00:00.000' AS DateTime), CAST(N'2020-12-07T00:00:00.000' AS DateTime))
SET IDENTITY_INSERT [dbo].[Product] OFF
GO
SET IDENTITY_INSERT [dbo].[Purchase] ON 

INSERT [dbo].[Purchase] ([PurchaseID], [Total], [CreateAt], [Status], [Customer_Tel]) VALUES (1, 30000000, CAST(N'2020-10-01T00:00:00.000' AS DateTime), 1, N'0147852369')
INSERT [dbo].[Purchase] ([PurchaseID], [Total], [CreateAt], [Status], [Customer_Tel]) VALUES (2, 67000000, CAST(N'2020-12-12T00:00:00.000' AS DateTime), 1, N'123456789')
INSERT [dbo].[Purchase] ([PurchaseID], [Total], [CreateAt], [Status], [Customer_Tel]) VALUES (3, 67000000, CAST(N'2020-02-01T00:00:00.000' AS DateTime), 2, N'789456123')
INSERT [dbo].[Purchase] ([PurchaseID], [Total], [CreateAt], [Status], [Customer_Tel]) VALUES (4, 90000000, CAST(N'2020-11-01T00:00:00.000' AS DateTime), 3, N'0147852369')
SET IDENTITY_INSERT [dbo].[Purchase] OFF
GO
SET IDENTITY_INSERT [dbo].[PurchaseDetail] ON 

INSERT [dbo].[PurchaseDetail] ([PurchaseDetail_ID], [PurchaseID], [ProductID], [Price], [Quantity], [Total]) VALUES (1, 1, 1, 15000000, 2, 30000000)
INSERT [dbo].[PurchaseDetail] ([PurchaseDetail_ID], [PurchaseID], [ProductID], [Price], [Quantity], [Total]) VALUES (2, 2, 4, 67000000, 1, 67000000)
INSERT [dbo].[PurchaseDetail] ([PurchaseDetail_ID], [PurchaseID], [ProductID], [Price], [Quantity], [Total]) VALUES (3, 3, 12, 67000000, 1, 67000000)
INSERT [dbo].[PurchaseDetail] ([PurchaseDetail_ID], [PurchaseID], [ProductID], [Price], [Quantity], [Total]) VALUES (4, 4, 35, 30000000, 3, 90000000)
SET IDENTITY_INSERT [dbo].[PurchaseDetail] OFF
GO
INSERT [dbo].[PurchaseStateEnum] ([EnumKey], [Value], [Description]) VALUES (N'All', -1, N'Tất cả đơn hàng')
INSERT [dbo].[PurchaseStateEnum] ([EnumKey], [Value], [Description]) VALUES (N'New', 1, N'Những đơn hàng mới')
INSERT [dbo].[PurchaseStateEnum] ([EnumKey], [Value], [Description]) VALUES (N'Cancelled', 2, N'Những đơn hàng bị hủy')
INSERT [dbo].[PurchaseStateEnum] ([EnumKey], [Value], [Description]) VALUES (N'Completed', 3, N'Những đơn hàng đã hoàn thành')
INSERT [dbo].[PurchaseStateEnum] ([EnumKey], [Value], [Description]) VALUES (N'Shipping', 4, N'Những đơn hàng đang giao')
GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_Category] FOREIGN KEY([CateID])
REFERENCES [dbo].[Category] ([CategoryID])
GO
ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [FK_Product_Category]
GO
ALTER TABLE [dbo].[Purchase]  WITH CHECK ADD  CONSTRAINT [FK_Purchase_Customer] FOREIGN KEY([Customer_Tel])
REFERENCES [dbo].[Customer] ([Customer_Tel])
GO
ALTER TABLE [dbo].[Purchase] CHECK CONSTRAINT [FK_Purchase_Customer]
GO
ALTER TABLE [dbo].[Purchase]  WITH CHECK ADD  CONSTRAINT [FK_Purchase_PurchaseStateEnum] FOREIGN KEY([Status])
REFERENCES [dbo].[PurchaseStateEnum] ([Value])
GO
ALTER TABLE [dbo].[Purchase] CHECK CONSTRAINT [FK_Purchase_PurchaseStateEnum]
GO
ALTER TABLE [dbo].[PurchaseDetail]  WITH CHECK ADD  CONSTRAINT [FK_PurchaseDetail_Product] FOREIGN KEY([ProductID])
REFERENCES [dbo].[Product] ([ProductID])
GO
ALTER TABLE [dbo].[PurchaseDetail] CHECK CONSTRAINT [FK_PurchaseDetail_Product]
GO
ALTER TABLE [dbo].[PurchaseDetail]  WITH CHECK ADD  CONSTRAINT [FK_PurchaseDetail_Purchase] FOREIGN KEY([PurchaseID])
REFERENCES [dbo].[Purchase] ([PurchaseID])
GO
ALTER TABLE [dbo].[PurchaseDetail] CHECK CONSTRAINT [FK_PurchaseDetail_Purchase]
GO
USE [master]
GO
ALTER DATABASE [MS02] SET  READ_WRITE 
GO
