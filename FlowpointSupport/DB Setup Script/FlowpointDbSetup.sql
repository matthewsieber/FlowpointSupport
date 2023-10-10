USE [master]
GO
/****** Object:  Database [Flowpoint]    Script Date: 2023-10-10 9:51:50 AM ******/
IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = N'Flowpoint')
BEGIN
CREATE DATABASE [Flowpoint]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Flowpoint', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.LOCAL_DEV\MSSQL\DATA\Flowpoint.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Flowpoint_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.LOCAL_DEV\MSSQL\DATA\Flowpoint_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 COLLATE SQL_Latin1_General_CP1_CI_AS
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
END
GO
ALTER DATABASE [Flowpoint] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Flowpoint].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Flowpoint] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Flowpoint] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Flowpoint] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Flowpoint] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Flowpoint] SET ARITHABORT OFF 
GO
ALTER DATABASE [Flowpoint] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Flowpoint] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Flowpoint] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Flowpoint] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Flowpoint] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Flowpoint] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Flowpoint] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Flowpoint] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Flowpoint] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Flowpoint] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Flowpoint] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Flowpoint] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Flowpoint] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Flowpoint] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Flowpoint] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Flowpoint] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Flowpoint] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Flowpoint] SET RECOVERY FULL 
GO
ALTER DATABASE [Flowpoint] SET  MULTI_USER 
GO
ALTER DATABASE [Flowpoint] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Flowpoint] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Flowpoint] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Flowpoint] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Flowpoint] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Flowpoint] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'Flowpoint', N'ON'
GO
ALTER DATABASE [Flowpoint] SET QUERY_STORE = ON
GO
ALTER DATABASE [Flowpoint] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [Flowpoint]
GO
/****** Object:  Table [dbo].[Flowpoint_Support_Company]    Script Date: 2023-10-10 9:51:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Flowpoint_Support_Company]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Flowpoint_Support_Company](
	[iCompanyID] [int] IDENTITY(1,1) NOT NULL,
	[vCompanyName] [nvarchar](64) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[vStreet1] [nvarchar](128) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[vStreet2] [nvarchar](128) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[vCity] [nvarchar](128) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[vProvince] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[vPostalCode] [nvarchar](32) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[vCountry] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[vContact] [nvarchar](64) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[vPhone] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[vFax] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[vEmail] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[dtCreated] [datetime] NULL,
	[bIsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_Flowpoint_Support_Company] PRIMARY KEY CLUSTERED 
(
	[iCompanyID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Flowpoint_Support_Ticket]    Script Date: 2023-10-10 9:51:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Flowpoint_Support_Ticket]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Flowpoint_Support_Ticket](
	[iTicketID] [int] IDENTITY(1,1) NOT NULL,
	[iVendorID] [int] NOT NULL,
	[vTicketMessage] [nvarchar](3000) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[dtCreatedDate] [datetime] NOT NULL,
	[dtModifiedDate] [datetime] NOT NULL,
	[iCreatedBy] [int] NOT NULL,
	[iModifiedBy] [int] NOT NULL,
	[bIsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_Flowpoint_Support_Ticket] PRIMARY KEY CLUSTERED 
(
	[iTicketID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Flowpoint_Support_Vendor]    Script Date: 2023-10-10 9:51:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Flowpoint_Support_Vendor]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Flowpoint_Support_Vendor](
	[iVendorID] [int] IDENTITY(1,1) NOT NULL,
	[iCompanyID] [int] NOT NULL,
	[vVendorName] [nvarchar](128) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[vStreet1] [nvarchar](128) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[vStreet2] [nvarchar](128) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[vCity] [nvarchar](128) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[vProvince] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[vPostalCode] [nvarchar](32) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[vCountry] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[vContact] [nvarchar](64) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[vPhone] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[vFax] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[vEmail] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[dtCreated] [datetime] NULL,
	[bIsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_Flowpoint_Support_Vendor] PRIMARY KEY CLUSTERED 
(
	[iVendorID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DF_Flowpoint_Support_Company_dtCreated]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Flowpoint_Support_Company] ADD  CONSTRAINT [DF_Flowpoint_Support_Company_dtCreated]  DEFAULT (getdate()) FOR [dtCreated]
END
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DF_Flowpoint_Support_Company_bIsDeleted]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Flowpoint_Support_Company] ADD  CONSTRAINT [DF_Flowpoint_Support_Company_bIsDeleted]  DEFAULT ((0)) FOR [bIsDeleted]
END
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DF_Flowpoint_Support_Ticket_dtCreatedDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Flowpoint_Support_Ticket] ADD  CONSTRAINT [DF_Flowpoint_Support_Ticket_dtCreatedDate]  DEFAULT (getdate()) FOR [dtCreatedDate]
END
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DF_Flowpoint_Support_Ticket_dtModifiedDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Flowpoint_Support_Ticket] ADD  CONSTRAINT [DF_Flowpoint_Support_Ticket_dtModifiedDate]  DEFAULT (getdate()) FOR [dtModifiedDate]
END
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DF_Flowpoint_Support_Ticket_bIsDeleted]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Flowpoint_Support_Ticket] ADD  CONSTRAINT [DF_Flowpoint_Support_Ticket_bIsDeleted]  DEFAULT ((0)) FOR [bIsDeleted]
END
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DF_Flowpoint_Support_Vendor_dtCreated]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Flowpoint_Support_Vendor] ADD  CONSTRAINT [DF_Flowpoint_Support_Vendor_dtCreated]  DEFAULT (getdate()) FOR [dtCreated]
END
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DF_Flowpoint_Support_Vendor_bIsDeleted]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Flowpoint_Support_Vendor] ADD  CONSTRAINT [DF_Flowpoint_Support_Vendor_bIsDeleted]  DEFAULT ((0)) FOR [bIsDeleted]
END
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Flowpoint_Support_Ticket_Flowpoint_Support_Vendor]') AND parent_object_id = OBJECT_ID(N'[dbo].[Flowpoint_Support_Ticket]'))
ALTER TABLE [dbo].[Flowpoint_Support_Ticket]  WITH CHECK ADD  CONSTRAINT [FK_Flowpoint_Support_Ticket_Flowpoint_Support_Vendor] FOREIGN KEY([iVendorID])
REFERENCES [dbo].[Flowpoint_Support_Vendor] ([iVendorID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Flowpoint_Support_Ticket_Flowpoint_Support_Vendor]') AND parent_object_id = OBJECT_ID(N'[dbo].[Flowpoint_Support_Ticket]'))
ALTER TABLE [dbo].[Flowpoint_Support_Ticket] CHECK CONSTRAINT [FK_Flowpoint_Support_Ticket_Flowpoint_Support_Vendor]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Flowpoint_Support_Vendor_Flowpoint_Support_Company]') AND parent_object_id = OBJECT_ID(N'[dbo].[Flowpoint_Support_Vendor]'))
ALTER TABLE [dbo].[Flowpoint_Support_Vendor]  WITH CHECK ADD  CONSTRAINT [FK_Flowpoint_Support_Vendor_Flowpoint_Support_Company] FOREIGN KEY([iCompanyID])
REFERENCES [dbo].[Flowpoint_Support_Company] ([iCompanyID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Flowpoint_Support_Vendor_Flowpoint_Support_Company]') AND parent_object_id = OBJECT_ID(N'[dbo].[Flowpoint_Support_Vendor]'))
ALTER TABLE [dbo].[Flowpoint_Support_Vendor] CHECK CONSTRAINT [FK_Flowpoint_Support_Vendor_Flowpoint_Support_Company]
GO
/****** Object:  Trigger [dbo].[trg_UpdateTicketModifiedDate]    Script Date: 2023-10-10 9:51:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].[trg_UpdateTicketModifiedDate]'))
EXEC dbo.sp_executesql @statement = N'CREATE TRIGGER [dbo].[trg_UpdateTicketModifiedDate]
ON [dbo].[Flowpoint_Support_Ticket]
AFTER UPDATE
AS
	UPDATE	Flowpoint_Support_Ticket
	SET		dtModifiedDate = GETDATE()
	WHERE	iTicketID IN (SELECT DISTINCT iTicketID FROM Inserted)' 
GO
ALTER TABLE [dbo].[Flowpoint_Support_Ticket] ENABLE TRIGGER [trg_UpdateTicketModifiedDate]
GO
USE [master]
GO
ALTER DATABASE [Flowpoint] SET  READ_WRITE 
GO
