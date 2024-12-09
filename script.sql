USE [master]
GO
/****** Object:  Database [10210808]    Script Date: 09.12.2024 13:46:35 ******/
CREATE DATABASE [10210808]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'10210808', FILENAME = N'C:\databases\10210808.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'10210808_log', FILENAME = N'C:\databases\10210808_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [10210808] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [10210808].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [10210808] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [10210808] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [10210808] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [10210808] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [10210808] SET ARITHABORT OFF 
GO
ALTER DATABASE [10210808] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [10210808] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [10210808] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [10210808] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [10210808] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [10210808] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [10210808] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [10210808] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [10210808] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [10210808] SET  ENABLE_BROKER 
GO
ALTER DATABASE [10210808] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [10210808] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [10210808] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [10210808] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [10210808] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [10210808] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [10210808] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [10210808] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [10210808] SET  MULTI_USER 
GO
ALTER DATABASE [10210808] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [10210808] SET DB_CHAINING OFF 
GO
ALTER DATABASE [10210808] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [10210808] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [10210808] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [10210808] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [10210808] SET QUERY_STORE = OFF
GO
USE [10210808]
GO
/****** Object:  User [college\t.spiridonova]    Script Date: 09.12.2024 13:46:35 ******/
CREATE USER [college\t.spiridonova] FOR LOGIN [college\t.spiridonova] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [college\o.kopets]    Script Date: 09.12.2024 13:46:35 ******/
CREATE USER [college\o.kopets] FOR LOGIN [college\o.kopets] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [college\g.glebov]    Script Date: 09.12.2024 13:46:35 ******/
CREATE USER [college\g.glebov] FOR LOGIN [college\g.glebov] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [college\g.fedorova]    Script Date: 09.12.2024 13:46:35 ******/
CREATE USER [college\g.fedorova] FOR LOGIN [college\g.fedorova] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [college\a.r.popova]    Script Date: 09.12.2024 13:46:35 ******/
CREATE USER [college\a.r.popova] FOR LOGIN [college\a.r.popova] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [college\a.berezhkov]    Script Date: 09.12.2024 13:46:35 ******/
CREATE USER [college\a.berezhkov] FOR LOGIN [college\a.berezhkov] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [college\10210808]    Script Date: 09.12.2024 13:46:35 ******/
CREATE USER [college\10210808] FOR LOGIN [college\10210808] WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_owner] ADD MEMBER [college\t.spiridonova]
GO
ALTER ROLE [db_owner] ADD MEMBER [college\o.kopets]
GO
ALTER ROLE [db_owner] ADD MEMBER [college\g.glebov]
GO
ALTER ROLE [db_owner] ADD MEMBER [college\g.fedorova]
GO
ALTER ROLE [db_owner] ADD MEMBER [college\a.r.popova]
GO
ALTER ROLE [db_owner] ADD MEMBER [college\a.berezhkov]
GO
ALTER ROLE [db_owner] ADD MEMBER [college\10210808]
GO
/****** Object:  Table [dbo].[allergens_table]    Script Date: 09.12.2024 13:46:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[allergens_table](
	[id_allergens] [int] IDENTITY(1,1) NOT NULL,
	[allergens] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_allergens_table] PRIMARY KEY CLUSTERED 
(
	[id_allergens] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[cart_table]    Script Date: 09.12.2024 13:46:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[cart_table](
	[id_cart] [int] IDENTITY(1,1) NOT NULL,
	[id_order] [int] NOT NULL,
	[id_product] [int] NOT NULL,
 CONSTRAINT [PK_cart_table] PRIMARY KEY CLUSTERED 
(
	[id_cart] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[kind_product_table]    Script Date: 09.12.2024 13:46:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[kind_product_table](
	[id_kind_product] [int] IDENTITY(1,1) NOT NULL,
	[kind_product] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_kind_product_table] PRIMARY KEY CLUSTERED 
(
	[id_kind_product] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[name_product_table]    Script Date: 09.12.2024 13:46:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[name_product_table](
	[id_name_product] [int] IDENTITY(1,1) NOT NULL,
	[name_product] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_name_product_table] PRIMARY KEY CLUSTERED 
(
	[id_name_product] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[order_manager_table]    Script Date: 09.12.2024 13:46:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[order_manager_table](
	[id_order_manager] [int] IDENTITY(1,1) NOT NULL,
	[id_order] [int] NOT NULL,
	[id_product] [int] NOT NULL,
 CONSTRAINT [PK_order_manager_table] PRIMARY KEY CLUSTERED 
(
	[id_order_manager] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[orders_table]    Script Date: 09.12.2024 13:46:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[orders_table](
	[id_order] [int] IDENTITY(1,1) NOT NULL,
	[id_user] [int] NOT NULL,
	[id_status] [int] NOT NULL,
 CONSTRAINT [PK_orders_table] PRIMARY KEY CLUSTERED 
(
	[id_order] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[products_table]    Script Date: 09.12.2024 13:46:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[products_table](
	[id_product] [int] IDENTITY(1,1) NOT NULL,
	[id_name_product] [int] NOT NULL,
	[id_type_product] [int] NOT NULL,
	[id_kind_product] [int] NOT NULL,
	[weight] [float] NOT NULL,
	[id_unit] [int] NOT NULL,
	[cost] [float] NOT NULL,
	[picture] [nvarchar](max) NULL,
	[description] [nvarchar](max) NULL,
	[id_allergens] [int] NULL,
 CONSTRAINT [PK_products_table] PRIMARY KEY CLUSTERED 
(
	[id_product] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[role_table]    Script Date: 09.12.2024 13:46:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[role_table](
	[id_role] [int] IDENTITY(1,1) NOT NULL,
	[role] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_role_table] PRIMARY KEY CLUSTERED 
(
	[id_role] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[status_table]    Script Date: 09.12.2024 13:46:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[status_table](
	[id_status] [int] IDENTITY(1,1) NOT NULL,
	[status] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_status_table] PRIMARY KEY CLUSTERED 
(
	[id_status] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[type_product_table]    Script Date: 09.12.2024 13:46:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[type_product_table](
	[id_type_product] [int] IDENTITY(1,1) NOT NULL,
	[type_product] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_type_product_table] PRIMARY KEY CLUSTERED 
(
	[id_type_product] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[unit_table]    Script Date: 09.12.2024 13:46:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[unit_table](
	[id_unit] [int] IDENTITY(1,1) NOT NULL,
	[unit] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_unit_table] PRIMARY KEY CLUSTERED 
(
	[id_unit] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[users_table]    Script Date: 09.12.2024 13:46:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[users_table](
	[id_user] [int] IDENTITY(1,1) NOT NULL,
	[login] [nvarchar](max) NOT NULL,
	[password] [nvarchar](max) NOT NULL,
	[phone] [nvarchar](16) NOT NULL,
	[email] [nvarchar](max) NOT NULL,
	[name] [nvarchar](max) NOT NULL,
	[surname] [nvarchar](max) NOT NULL,
	[id_role_user] [int] NOT NULL,
 CONSTRAINT [PK_users_table] PRIMARY KEY CLUSTERED 
(
	[id_user] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[role_table] ON 

INSERT [dbo].[role_table] ([id_role], [role]) VALUES (1, N'Администратор')
INSERT [dbo].[role_table] ([id_role], [role]) VALUES (2, N'Менеджер')
INSERT [dbo].[role_table] ([id_role], [role]) VALUES (3, N'Пользователь')
SET IDENTITY_INSERT [dbo].[role_table] OFF
GO
SET IDENTITY_INSERT [dbo].[users_table] ON 

INSERT [dbo].[users_table] ([id_user], [login], [password], [phone], [email], [name], [surname], [id_role_user]) VALUES (3, N'11', N'11', N'1', N'1', N'admin', N'ad', 1)
INSERT [dbo].[users_table] ([id_user], [login], [password], [phone], [email], [name], [surname], [id_role_user]) VALUES (4, N'22', N'22', N'2', N'2', N'manager', N'man', 2)
INSERT [dbo].[users_table] ([id_user], [login], [password], [phone], [email], [name], [surname], [id_role_user]) VALUES (5, N'33', N'33', N'3', N'3', N'user', N'us', 3)
INSERT [dbo].[users_table] ([id_user], [login], [password], [phone], [email], [name], [surname], [id_role_user]) VALUES (6, N'tt', N'123', N'+711111111111111', N'ghngh@retgf.uj', N'bggbgb', N'fgerr', 3)
SET IDENTITY_INSERT [dbo].[users_table] OFF
GO
ALTER TABLE [dbo].[cart_table]  WITH CHECK ADD  CONSTRAINT [FK_cart_table_orders_table] FOREIGN KEY([id_order])
REFERENCES [dbo].[orders_table] ([id_order])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[cart_table] CHECK CONSTRAINT [FK_cart_table_orders_table]
GO
ALTER TABLE [dbo].[cart_table]  WITH CHECK ADD  CONSTRAINT [FK_cart_table_products_table] FOREIGN KEY([id_product])
REFERENCES [dbo].[products_table] ([id_product])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[cart_table] CHECK CONSTRAINT [FK_cart_table_products_table]
GO
ALTER TABLE [dbo].[order_manager_table]  WITH CHECK ADD  CONSTRAINT [FK_order_manager_table_orders_table] FOREIGN KEY([id_order])
REFERENCES [dbo].[orders_table] ([id_order])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[order_manager_table] CHECK CONSTRAINT [FK_order_manager_table_orders_table]
GO
ALTER TABLE [dbo].[order_manager_table]  WITH CHECK ADD  CONSTRAINT [FK_order_manager_table_products_table] FOREIGN KEY([id_product])
REFERENCES [dbo].[products_table] ([id_product])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[order_manager_table] CHECK CONSTRAINT [FK_order_manager_table_products_table]
GO
ALTER TABLE [dbo].[orders_table]  WITH CHECK ADD  CONSTRAINT [FK_orders_table_status_table] FOREIGN KEY([id_status])
REFERENCES [dbo].[status_table] ([id_status])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[orders_table] CHECK CONSTRAINT [FK_orders_table_status_table]
GO
ALTER TABLE [dbo].[orders_table]  WITH CHECK ADD  CONSTRAINT [FK_orders_table_users_table1] FOREIGN KEY([id_user])
REFERENCES [dbo].[users_table] ([id_user])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[orders_table] CHECK CONSTRAINT [FK_orders_table_users_table1]
GO
ALTER TABLE [dbo].[products_table]  WITH CHECK ADD  CONSTRAINT [FK_products_table_allergens_table] FOREIGN KEY([id_allergens])
REFERENCES [dbo].[allergens_table] ([id_allergens])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[products_table] CHECK CONSTRAINT [FK_products_table_allergens_table]
GO
ALTER TABLE [dbo].[products_table]  WITH CHECK ADD  CONSTRAINT [FK_products_table_kind_product_table] FOREIGN KEY([id_kind_product])
REFERENCES [dbo].[kind_product_table] ([id_kind_product])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[products_table] CHECK CONSTRAINT [FK_products_table_kind_product_table]
GO
ALTER TABLE [dbo].[products_table]  WITH CHECK ADD  CONSTRAINT [FK_products_table_name_product_table] FOREIGN KEY([id_name_product])
REFERENCES [dbo].[name_product_table] ([id_name_product])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[products_table] CHECK CONSTRAINT [FK_products_table_name_product_table]
GO
ALTER TABLE [dbo].[products_table]  WITH CHECK ADD  CONSTRAINT [FK_products_table_type_product_table] FOREIGN KEY([id_type_product])
REFERENCES [dbo].[type_product_table] ([id_type_product])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[products_table] CHECK CONSTRAINT [FK_products_table_type_product_table]
GO
ALTER TABLE [dbo].[products_table]  WITH CHECK ADD  CONSTRAINT [FK_products_table_unit_table] FOREIGN KEY([id_unit])
REFERENCES [dbo].[unit_table] ([id_unit])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[products_table] CHECK CONSTRAINT [FK_products_table_unit_table]
GO
ALTER TABLE [dbo].[users_table]  WITH CHECK ADD  CONSTRAINT [FK_users_table_role_table] FOREIGN KEY([id_role_user])
REFERENCES [dbo].[role_table] ([id_role])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[users_table] CHECK CONSTRAINT [FK_users_table_role_table]
GO
USE [master]
GO
ALTER DATABASE [10210808] SET  READ_WRITE 
GO
