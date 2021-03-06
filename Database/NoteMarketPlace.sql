USE [master]
GO
/****** Object:  Database [NotesMarketplace]    Script Date: 10-04-2021 05:54:13 PM ******/
CREATE DATABASE [NotesMarketplace]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'NotesMarketplace', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\NotesMarketplace.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'NotesMarketplace_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\NotesMarketplace_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [NotesMarketplace] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [NotesMarketplace].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [NotesMarketplace] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [NotesMarketplace] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [NotesMarketplace] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [NotesMarketplace] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [NotesMarketplace] SET ARITHABORT OFF 
GO
ALTER DATABASE [NotesMarketplace] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [NotesMarketplace] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [NotesMarketplace] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [NotesMarketplace] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [NotesMarketplace] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [NotesMarketplace] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [NotesMarketplace] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [NotesMarketplace] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [NotesMarketplace] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [NotesMarketplace] SET  DISABLE_BROKER 
GO
ALTER DATABASE [NotesMarketplace] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [NotesMarketplace] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [NotesMarketplace] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [NotesMarketplace] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [NotesMarketplace] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [NotesMarketplace] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [NotesMarketplace] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [NotesMarketplace] SET RECOVERY FULL 
GO
ALTER DATABASE [NotesMarketplace] SET  MULTI_USER 
GO
ALTER DATABASE [NotesMarketplace] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [NotesMarketplace] SET DB_CHAINING OFF 
GO
ALTER DATABASE [NotesMarketplace] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [NotesMarketplace] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [NotesMarketplace] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [NotesMarketplace] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'NotesMarketplace', N'ON'
GO
ALTER DATABASE [NotesMarketplace] SET QUERY_STORE = OFF
GO
USE [NotesMarketplace]
GO
/****** Object:  Table [dbo].[ContactUs]    Script Date: 10-04-2021 05:54:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ContactUs](
	[ContactUsID] [int] IDENTITY(1,1) NOT NULL,
	[FullName] [varchar](50) NOT NULL,
	[EmailID] [varchar](50) NOT NULL,
	[Subject] [varchar](200) NOT NULL,
	[Comments] [varchar](max) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [int] NULL,
 CONSTRAINT [PK_ContactUs] PRIMARY KEY CLUSTERED 
(
	[ContactUsID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Countries]    Script Date: 10-04-2021 05:54:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Countries](
	[CountryID] [int] IDENTITY(1,1) NOT NULL,
	[CountryName] [varchar](100) NOT NULL,
	[CountryCode] [varchar](100) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [int] NULL,
 CONSTRAINT [PK_Countries] PRIMARY KEY CLUSTERED 
(
	[CountryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DownloadNotes]    Script Date: 10-04-2021 05:54:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DownloadNotes](
	[DownloadNoteID] [int] IDENTITY(1,1) NOT NULL,
	[NoteID] [int] NOT NULL,
	[SellerID] [int] NOT NULL,
	[BuyerID] [int] NOT NULL,
	[IsSellerHasAllowedDownload] [bit] NOT NULL,
	[AttachmentPath] [varchar](max) NULL,
	[IsAttachmentDownloaded] [bit] NOT NULL,
	[AttachmentDownloadDate] [datetime] NULL,
	[IsPaid] [bit] NOT NULL,
	[PurchasePrice] [decimal](10, 2) NULL,
	[NoteTitle] [varchar](100) NOT NULL,
	[NoteCategory] [varchar](100) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [int] NULL,
 CONSTRAINT [PK_DownloadNotes] PRIMARY KEY CLUSTERED 
(
	[DownloadNoteID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NoteCategories]    Script Date: 10-04-2021 05:54:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NoteCategories](
	[NoteCategoryID] [int] IDENTITY(1,1) NOT NULL,
	[CategoryName] [varchar](100) NOT NULL,
	[Description] [varchar](max) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [int] NULL,
 CONSTRAINT [PK_NoteCategories] PRIMARY KEY CLUSTERED 
(
	[NoteCategoryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NoteDetails]    Script Date: 10-04-2021 05:54:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NoteDetails](
	[NoteID] [int] IDENTITY(1,1) NOT NULL,
	[SellerID] [int] NOT NULL,
	[Status] [int] NOT NULL,
	[ActionBy] [int] NULL,
	[AdminRemarks] [varchar](max) NULL,
	[PublishedDate] [datetime] NULL,
	[NoteTitle] [varchar](100) NOT NULL,
	[NoteCategoryID] [int] NOT NULL,
	[DisplayPicture] [varchar](max) NULL,
	[NoteTypeID] [int] NOT NULL,
	[NumberOfPages] [int] NULL,
	[NoteDescription] [varchar](max) NOT NULL,
	[UniversityInformation] [varchar](200) NULL,
	[CountryID] [int] NULL,
	[Course] [varchar](100) NULL,
	[CourseCode] [varchar](100) NULL,
	[ProfessorName] [varchar](100) NULL,
	[SellType] [varchar](50) NOT NULL,
	[SellPrice] [decimal](10, 2) NOT NULL,
	[PreviewUpload] [nvarchar](max) NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [int] NULL,
 CONSTRAINT [PK_NoteDetails] PRIMARY KEY CLUSTERED 
(
	[NoteID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NoteReviews]    Script Date: 10-04-2021 05:54:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NoteReviews](
	[NoteReviewID] [int] IDENTITY(1,1) NOT NULL,
	[ReviewByID] [int] NOT NULL,
	[NoteID] [int] NOT NULL,
	[AgainstDownloadID] [int] NOT NULL,
	[Ratings] [decimal](2, 1) NOT NULL,
	[Comments] [varchar](max) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [int] NULL,
 CONSTRAINT [PK_NoteReviews] PRIMARY KEY CLUSTERED 
(
	[NoteReviewID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NoteStatus]    Script Date: 10-04-2021 05:54:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NoteStatus](
	[NoteStatusID] [int] IDENTITY(1,1) NOT NULL,
	[Status] [varchar](20) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [int] NULL,
 CONSTRAINT [PK_NoteStatus] PRIMARY KEY CLUSTERED 
(
	[NoteStatusID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NoteType]    Script Date: 10-04-2021 05:54:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NoteType](
	[NoteTypeID] [int] IDENTITY(1,1) NOT NULL,
	[TypeName] [varchar](100) NOT NULL,
	[Description] [varchar](max) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [int] NULL,
 CONSTRAINT [PK_NoteType] PRIMARY KEY CLUSTERED 
(
	[NoteTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SellerNoteAttachment]    Script Date: 10-04-2021 05:54:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SellerNoteAttachment](
	[NoteAttachmentID] [int] IDENTITY(1,1) NOT NULL,
	[NoteID] [int] NOT NULL,
	[FileName] [varchar](100) NOT NULL,
	[FilePath] [varchar](max) NOT NULL,
	[FilesSize] [float] NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [int] NULL,
 CONSTRAINT [PK_SellerNoteAttachment] PRIMARY KEY CLUSTERED 
(
	[NoteAttachmentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SpamReports]    Script Date: 10-04-2021 05:54:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SpamReports](
	[SpamReportID] [int] IDENTITY(1,1) NOT NULL,
	[NoteID] [int] NOT NULL,
	[ReportByID] [int] NOT NULL,
	[AgainstDownloadID] [int] NOT NULL,
	[Remark] [varchar](max) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [int] NULL,
 CONSTRAINT [PK_SpamReports] PRIMARY KEY CLUSTERED 
(
	[SpamReportID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SystemConfigurations]    Script Date: 10-04-2021 05:54:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SystemConfigurations](
	[SystemConfigID] [int] IDENTITY(1,1) NOT NULL,
	[ConfigKey] [varchar](100) NOT NULL,
	[Value] [varchar](max) NOT NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [int] NULL,
 CONSTRAINT [PK_SystemConfigurations] PRIMARY KEY CLUSTERED 
(
	[SystemConfigID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserProfile]    Script Date: 10-04-2021 05:54:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserProfile](
	[UserProfileID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NOT NULL,
	[DateOfBirth] [datetime] NULL,
	[Gender] [varchar](10) NULL,
	[SecondaryEmailAddress] [varchar](100) NULL,
	[CountryCode] [int] NOT NULL,
	[PhoneNumber] [varchar](20) NOT NULL,
	[ProfilePicture] [varchar](max) NULL,
	[AddressLine1] [varchar](100) NOT NULL,
	[AddressLine2] [varchar](100) NULL,
	[City] [varchar](50) NOT NULL,
	[State] [varchar](50) NOT NULL,
	[ZipCode] [varchar](50) NOT NULL,
	[CountryID] [int] NOT NULL,
	[University] [varchar](100) NULL,
	[College] [varchar](100) NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [int] NULL,
 CONSTRAINT [PK_UserProfile] PRIMARY KEY CLUSTERED 
(
	[UserProfileID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserRoles]    Script Date: 10-04-2021 05:54:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserRoles](
	[UserRoleID] [int] IDENTITY(1,1) NOT NULL,
	[Role] [varchar](50) NOT NULL,
	[Description] [varchar](max) NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [int] NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_UserRoles] PRIMARY KEY CLUSTERED 
(
	[UserRoleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 10-04-2021 05:54:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[UserID] [int] IDENTITY(1,1) NOT NULL,
	[UserRoleID] [int] NOT NULL,
	[FirstName] [varchar](50) NOT NULL,
	[LastName] [varchar](50) NOT NULL,
	[EmailID] [varchar](100) NOT NULL,
	[Password] [varchar](24) NOT NULL,
	[IsEmailVerified] [bit] NOT NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [int] NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Countries] ON 

INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [IsActive], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (1, N'India', N'+91', 1, CAST(N'2021-03-09T18:55:05.220' AS DateTime), 80, NULL, NULL)
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [IsActive], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (2, N'Afghanistan', N'+93', 1, CAST(N'2021-03-09T18:55:05.220' AS DateTime), 80, NULL, NULL)
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [IsActive], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (3, N'Australia', N'+43', 1, CAST(N'2021-03-09T18:55:05.220' AS DateTime), 80, NULL, NULL)
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [IsActive], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (4, N'Belgium', N'+32', 1, CAST(N'2021-03-09T18:55:05.220' AS DateTime), 80, NULL, NULL)
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [IsActive], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (5, N'China', N'+86', 1, CAST(N'2021-03-09T18:55:05.220' AS DateTime), 80, NULL, NULL)
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [IsActive], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (6, N'Colombia', N'+57', 1, CAST(N'2021-03-09T18:55:05.220' AS DateTime), 80, NULL, NULL)
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [IsActive], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (7, N'Denmark', N'+45', 1, CAST(N'2021-03-09T18:55:05.220' AS DateTime), 80, NULL, NULL)
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [IsActive], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (8, N'Egypt', N'+20', 1, CAST(N'2021-03-09T18:55:05.220' AS DateTime), 80, NULL, NULL)
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [IsActive], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (9, N'France', N'+33', 1, CAST(N'2021-03-09T18:55:05.220' AS DateTime), 80, NULL, NULL)
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [IsActive], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (10, N'Germany', N'+49', 1, CAST(N'2021-03-09T18:55:05.220' AS DateTime), 80, NULL, NULL)
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [IsActive], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (11, N'Greece', N'+30', 1, CAST(N'2021-03-09T18:55:05.220' AS DateTime), 80, NULL, NULL)
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [IsActive], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (12, N'Hong Kong', N'+852', 1, CAST(N'2021-03-09T18:55:05.220' AS DateTime), 80, NULL, NULL)
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [IsActive], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (13, N'Italy', N'+39', 1, CAST(N'2021-03-09T18:55:05.220' AS DateTime), 80, NULL, NULL)
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [IsActive], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (14, N'Japan', N'+81', 1, CAST(N'2021-03-09T18:55:05.220' AS DateTime), 80, NULL, NULL)
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [IsActive], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (15, N'Malasiya', N'+60', 1, CAST(N'2021-03-09T18:55:05.220' AS DateTime), 80, NULL, NULL)
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [IsActive], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (16, N'Mexico', N'+52', 1, CAST(N'2021-03-09T18:55:05.220' AS DateTime), 80, NULL, NULL)
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [IsActive], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (17, N'New Zealand', N'+64', 1, CAST(N'2021-03-09T18:55:05.220' AS DateTime), 80, NULL, NULL)
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [IsActive], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (18, N'Philippines', N'+63', 1, CAST(N'2021-03-09T18:55:05.220' AS DateTime), 80, NULL, NULL)
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [IsActive], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (19, N'Russia', N'+7', 1, CAST(N'2021-03-09T18:55:05.220' AS DateTime), 80, NULL, NULL)
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [IsActive], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (20, N'South Africa', N'+27', 1, CAST(N'2021-03-09T18:55:05.220' AS DateTime), 80, NULL, NULL)
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [IsActive], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (21, N'Sri Lanka', N'+94', 1, CAST(N'2021-03-09T18:55:05.220' AS DateTime), 80, NULL, NULL)
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [IsActive], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (22, N'Spain', N'+34', 1, CAST(N'2021-03-09T18:55:05.220' AS DateTime), 80, NULL, NULL)
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [IsActive], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (23, N'Switzerland', N'+41', 1, CAST(N'2021-03-09T18:55:05.220' AS DateTime), 80, NULL, NULL)
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [IsActive], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (24, N'Thailand', N'+66', 1, CAST(N'2021-03-09T18:55:05.220' AS DateTime), 80, NULL, NULL)
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [IsActive], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (25, N'Turkey', N'+90', 1, CAST(N'2021-03-09T18:55:05.220' AS DateTime), 80, NULL, NULL)
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [IsActive], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (26, N'Ukraine', N'+380', 1, CAST(N'2021-03-09T18:55:05.220' AS DateTime), 80, NULL, NULL)
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [IsActive], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (27, N'Vietnam', N'+84', 1, CAST(N'2021-03-09T18:55:05.220' AS DateTime), 80, NULL, NULL)
INSERT [dbo].[Countries] ([CountryID], [CountryName], [CountryCode], [IsActive], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (28, N'Zimbabwe', N'+263', 0, CAST(N'2021-03-09T18:55:05.220' AS DateTime), 80, NULL, NULL)
SET IDENTITY_INSERT [dbo].[Countries] OFF
GO
SET IDENTITY_INSERT [dbo].[NoteCategories] ON 

INSERT [dbo].[NoteCategories] ([NoteCategoryID], [CategoryName], [Description], [IsActive], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (1, N'Agrology', N'This category is related to agrology branch', 1, CAST(N'2021-03-09T18:55:05.220' AS DateTime), 80, NULL, NULL)
INSERT [dbo].[NoteCategories] ([NoteCategoryID], [CategoryName], [Description], [IsActive], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (2, N'MBA', N'This category is related to MBA branch ', 1, CAST(N'2021-03-09T18:55:05.220' AS DateTime), 80, NULL, NULL)
INSERT [dbo].[NoteCategories] ([NoteCategoryID], [CategoryName], [Description], [IsActive], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (3, N'Science', N'This category is related to Science stream', 1, CAST(N'2021-03-09T18:55:05.220' AS DateTime), 80, NULL, NULL)
INSERT [dbo].[NoteCategories] ([NoteCategoryID], [CategoryName], [Description], [IsActive], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (4, N'CA', N'This category is related to CA branch', 1, CAST(N'2021-03-09T18:55:05.220' AS DateTime), 80, NULL, NULL)
INSERT [dbo].[NoteCategories] ([NoteCategoryID], [CategoryName], [Description], [IsActive], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (5, N'MBBS', N'This category is related to MBBS branch', 1, CAST(N'2021-03-09T18:55:05.220' AS DateTime), 80, NULL, NULL)
INSERT [dbo].[NoteCategories] ([NoteCategoryID], [CategoryName], [Description], [IsActive], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (6, N'Physiotherapy', N'This category is related to physiotherapy branch', 1, CAST(N'2021-03-09T18:55:05.220' AS DateTime), 80, NULL, NULL)
INSERT [dbo].[NoteCategories] ([NoteCategoryID], [CategoryName], [Description], [IsActive], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (7, N'IT', N'This category is related to branch', 1, CAST(N'2021-03-09T18:55:05.220' AS DateTime), 80, NULL, NULL)
INSERT [dbo].[NoteCategories] ([NoteCategoryID], [CategoryName], [Description], [IsActive], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (8, N'Dermatology', N'This category is related to Dermatology branch', 1, CAST(N'2021-03-09T18:55:05.220' AS DateTime), 80, NULL, NULL)
INSERT [dbo].[NoteCategories] ([NoteCategoryID], [CategoryName], [Description], [IsActive], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (9, N'Zymology', N'This category is related to Zymology branch', 1, CAST(N'2021-03-09T18:55:05.220' AS DateTime), 80, NULL, NULL)
INSERT [dbo].[NoteCategories] ([NoteCategoryID], [CategoryName], [Description], [IsActive], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (10, N'BCA', N'This is bca branch', 1, CAST(N'2021-03-09T18:55:05.220' AS DateTime), 80, NULL, NULL)
SET IDENTITY_INSERT [dbo].[NoteCategories] OFF
GO
SET IDENTITY_INSERT [dbo].[NoteDetails] ON 

INSERT [dbo].[NoteDetails] ([NoteID], [SellerID], [Status], [ActionBy], [AdminRemarks], [PublishedDate], [NoteTitle], [NoteCategoryID], [DisplayPicture], [NoteTypeID], [NumberOfPages], [NoteDescription], [UniversityInformation], [CountryID], [Course], [CourseCode], [ProfessorName], [SellType], [SellPrice], [PreviewUpload], [IsActive], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (25, 91, 4, NULL, NULL, NULL, N'C++', 7, N'~/Member/91/25/DP_10042021.jpg', 3, 5, N'related to c++ concept.', N'Gec', 1, N'IT', N'05', N'C M Kapadiya', N'Free', CAST(0.00 AS Decimal(10, 2)), NULL, 1, CAST(N'2021-04-10T15:02:46.337' AS DateTime), 91, CAST(N'2021-04-10T15:02:46.340' AS DateTime), 91)
SET IDENTITY_INSERT [dbo].[NoteDetails] OFF
GO
SET IDENTITY_INSERT [dbo].[NoteStatus] ON 

INSERT [dbo].[NoteStatus] ([NoteStatusID], [Status], [IsActive], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (1, N'Draft', 1, NULL, NULL, NULL, NULL)
INSERT [dbo].[NoteStatus] ([NoteStatusID], [Status], [IsActive], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (2, N'Published', 1, NULL, NULL, NULL, NULL)
INSERT [dbo].[NoteStatus] ([NoteStatusID], [Status], [IsActive], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (3, N'Rejected', 1, NULL, NULL, NULL, NULL)
INSERT [dbo].[NoteStatus] ([NoteStatusID], [Status], [IsActive], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (4, N'Under Review', 1, NULL, NULL, NULL, NULL)
INSERT [dbo].[NoteStatus] ([NoteStatusID], [Status], [IsActive], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (5, N'In Review', 1, NULL, NULL, NULL, NULL)
INSERT [dbo].[NoteStatus] ([NoteStatusID], [Status], [IsActive], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (6, N'Removed', 1, NULL, NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[NoteStatus] OFF
GO
SET IDENTITY_INSERT [dbo].[NoteType] ON 

INSERT [dbo].[NoteType] ([NoteTypeID], [TypeName], [Description], [IsActive], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (1, N'Hand Written', N'This notes are written by hand by any person.', 1, CAST(N'2021-03-09T18:55:05.220' AS DateTime), 80, NULL, NULL)
INSERT [dbo].[NoteType] ([NoteTypeID], [TypeName], [Description], [IsActive], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (2, N'Story Books', N'This notes are depend on story books', 1, CAST(N'2021-03-09T18:55:05.220' AS DateTime), 80, NULL, NULL)
INSERT [dbo].[NoteType] ([NoteTypeID], [TypeName], [Description], [IsActive], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (3, N'University', N'This notes are published or created by universities', 1, CAST(N'2021-03-09T18:55:05.220' AS DateTime), 80, NULL, NULL)
INSERT [dbo].[NoteType] ([NoteTypeID], [TypeName], [Description], [IsActive], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (4, N'Books Note', N'This note are books note', 1, CAST(N'2021-03-09T18:55:05.220' AS DateTime), 80, NULL, NULL)
SET IDENTITY_INSERT [dbo].[NoteType] OFF
GO
SET IDENTITY_INSERT [dbo].[SellerNoteAttachment] ON 

INSERT [dbo].[SellerNoteAttachment] ([NoteAttachmentID], [NoteID], [FileName], [FilePath], [FilesSize], [IsActive], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (1, 25, N'Attachment_1_10042021.pdf;', N'/Member/91/25/attachment/Attachment_1_10042021.pdf;', 9, 1, CAST(N'2021-04-10T15:02:46.487' AS DateTime), 91, NULL, NULL)
SET IDENTITY_INSERT [dbo].[SellerNoteAttachment] OFF
GO
SET IDENTITY_INSERT [dbo].[SystemConfigurations] ON 

INSERT [dbo].[SystemConfigurations] ([SystemConfigID], [ConfigKey], [Value], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (1, N'Support Email Address', N'suppport@gmail.com', CAST(N'2021-03-09T18:55:05.220' AS DateTime), 68, NULL, NULL)
INSERT [dbo].[SystemConfigurations] ([SystemConfigID], [ConfigKey], [Value], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (2, N'Support Phone Number', N'0000000000', CAST(N'2021-03-09T18:55:05.220' AS DateTime), 65, NULL, NULL)
INSERT [dbo].[SystemConfigurations] ([SystemConfigID], [ConfigKey], [Value], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (3, N'Email Addresses', N'other@gmail.com', CAST(N'2021-03-09T18:55:05.220' AS DateTime), 65, NULL, NULL)
INSERT [dbo].[SystemConfigurations] ([SystemConfigID], [ConfigKey], [Value], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (4, N'Facebook URL', N'http://facebook.com/NoteMarketPlace', CAST(N'2021-03-09T18:55:05.220' AS DateTime), 68, NULL, NULL)
INSERT [dbo].[SystemConfigurations] ([SystemConfigID], [ConfigKey], [Value], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (5, N'Twitter URL', N'http://twitter.com/NoteMarketPLace', CAST(N'2021-03-09T18:55:05.220' AS DateTime), 68, NULL, NULL)
INSERT [dbo].[SystemConfigurations] ([SystemConfigID], [ConfigKey], [Value], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (6, N'Linkedin URL', N'http://linkedin.com/NoteMarketPlace', CAST(N'2021-03-09T18:55:05.220' AS DateTime), 68, NULL, NULL)
INSERT [dbo].[SystemConfigurations] ([SystemConfigID], [ConfigKey], [Value], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (11, N'Default Image For Notes', N'~/Member/56/1/DP_06032021.jpg', CAST(N'2021-03-09T18:55:05.220' AS DateTime), 65, NULL, NULL)
INSERT [dbo].[SystemConfigurations] ([SystemConfigID], [ConfigKey], [Value], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (13, N'Default Profile Picture', N'~/Member/56/1/DP_06032021.jpg', CAST(N'2021-03-09T18:55:05.220' AS DateTime), 65, NULL, NULL)
SET IDENTITY_INSERT [dbo].[SystemConfigurations] OFF
GO
SET IDENTITY_INSERT [dbo].[UserProfile] ON 

INSERT [dbo].[UserProfile] ([UserProfileID], [UserID], [DateOfBirth], [Gender], [SecondaryEmailAddress], [CountryCode], [PhoneNumber], [ProfilePicture], [AddressLine1], [AddressLine2], [City], [State], [ZipCode], [CountryID], [University], [College], [IsActive], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (13, 91, CAST(N'2000-05-12T00:00:00.000' AS DateTime), N'male', NULL, 1, N'9876543219', N'/Member/91/DP_10042021.png', N'Lok Nagar', N'Ven Road', N'Bajipura', N'Gujarat', N'394690', 1, N'GTU', N'GEC,Gandhinagar', 1, NULL, NULL, CAST(N'2021-04-10T17:22:19.703' AS DateTime), NULL)
SET IDENTITY_INSERT [dbo].[UserProfile] OFF
GO
SET IDENTITY_INSERT [dbo].[UserRoles] ON 

INSERT [dbo].[UserRoles] ([UserRoleID], [Role], [Description], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (1, N'Super Administrator', NULL, NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[UserRoles] ([UserRoleID], [Role], [Description], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (2, N'Administrator', NULL, NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[UserRoles] ([UserRoleID], [Role], [Description], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (3, N'Member', NULL, NULL, NULL, NULL, NULL, 1)
SET IDENTITY_INSERT [dbo].[UserRoles] OFF
GO
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([UserID], [UserRoleID], [FirstName], [LastName], [EmailID], [Password], [IsEmailVerified], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (80, 1, N'Admin', N'Lorem', N'admin123@gmail.com', N'Admin@123', 1, NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Users] ([UserID], [UserRoleID], [FirstName], [LastName], [EmailID], [Password], [IsEmailVerified], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (91, 3, N'Akshay', N'Prajapati', N'akku67877@gmail.com', N'Akshay2@', 1, CAST(N'2021-04-10T14:12:40.860' AS DateTime), NULL, NULL, NULL, 1)
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
ALTER TABLE [dbo].[DownloadNotes]  WITH CHECK ADD  CONSTRAINT [FK_DownloadNotes_BuyerID] FOREIGN KEY([BuyerID])
REFERENCES [dbo].[Users] ([UserID])
GO
ALTER TABLE [dbo].[DownloadNotes] CHECK CONSTRAINT [FK_DownloadNotes_BuyerID]
GO
ALTER TABLE [dbo].[DownloadNotes]  WITH CHECK ADD  CONSTRAINT [FK_DownloadNotes_NoteID] FOREIGN KEY([NoteID])
REFERENCES [dbo].[NoteDetails] ([NoteID])
GO
ALTER TABLE [dbo].[DownloadNotes] CHECK CONSTRAINT [FK_DownloadNotes_NoteID]
GO
ALTER TABLE [dbo].[DownloadNotes]  WITH CHECK ADD  CONSTRAINT [FK_DownloadNotes_SellerID] FOREIGN KEY([SellerID])
REFERENCES [dbo].[Users] ([UserID])
GO
ALTER TABLE [dbo].[DownloadNotes] CHECK CONSTRAINT [FK_DownloadNotes_SellerID]
GO
ALTER TABLE [dbo].[NoteDetails]  WITH CHECK ADD  CONSTRAINT [FK_NoteDetails_ActionBy] FOREIGN KEY([ActionBy])
REFERENCES [dbo].[Users] ([UserID])
GO
ALTER TABLE [dbo].[NoteDetails] CHECK CONSTRAINT [FK_NoteDetails_ActionBy]
GO
ALTER TABLE [dbo].[NoteDetails]  WITH CHECK ADD  CONSTRAINT [FK_NoteDetails_CountryID] FOREIGN KEY([CountryID])
REFERENCES [dbo].[Countries] ([CountryID])
GO
ALTER TABLE [dbo].[NoteDetails] CHECK CONSTRAINT [FK_NoteDetails_CountryID]
GO
ALTER TABLE [dbo].[NoteDetails]  WITH CHECK ADD  CONSTRAINT [FK_NoteDetails_NoteCategoryID] FOREIGN KEY([NoteCategoryID])
REFERENCES [dbo].[NoteCategories] ([NoteCategoryID])
GO
ALTER TABLE [dbo].[NoteDetails] CHECK CONSTRAINT [FK_NoteDetails_NoteCategoryID]
GO
ALTER TABLE [dbo].[NoteDetails]  WITH CHECK ADD  CONSTRAINT [FK_NoteDetails_NoteType] FOREIGN KEY([NoteTypeID])
REFERENCES [dbo].[NoteType] ([NoteTypeID])
GO
ALTER TABLE [dbo].[NoteDetails] CHECK CONSTRAINT [FK_NoteDetails_NoteType]
GO
ALTER TABLE [dbo].[NoteDetails]  WITH CHECK ADD  CONSTRAINT [FK_NoteDetails_SellerID] FOREIGN KEY([SellerID])
REFERENCES [dbo].[Users] ([UserID])
GO
ALTER TABLE [dbo].[NoteDetails] CHECK CONSTRAINT [FK_NoteDetails_SellerID]
GO
ALTER TABLE [dbo].[NoteDetails]  WITH CHECK ADD  CONSTRAINT [FK_NoteDetails_Status] FOREIGN KEY([Status])
REFERENCES [dbo].[NoteStatus] ([NoteStatusID])
GO
ALTER TABLE [dbo].[NoteDetails] CHECK CONSTRAINT [FK_NoteDetails_Status]
GO
ALTER TABLE [dbo].[NoteReviews]  WITH CHECK ADD  CONSTRAINT [FK_NoteReviews_AgainstDownloadID] FOREIGN KEY([AgainstDownloadID])
REFERENCES [dbo].[DownloadNotes] ([DownloadNoteID])
GO
ALTER TABLE [dbo].[NoteReviews] CHECK CONSTRAINT [FK_NoteReviews_AgainstDownloadID]
GO
ALTER TABLE [dbo].[NoteReviews]  WITH CHECK ADD  CONSTRAINT [FK_NoteReviews_NoteID] FOREIGN KEY([NoteID])
REFERENCES [dbo].[NoteDetails] ([NoteID])
GO
ALTER TABLE [dbo].[NoteReviews] CHECK CONSTRAINT [FK_NoteReviews_NoteID]
GO
ALTER TABLE [dbo].[NoteReviews]  WITH CHECK ADD  CONSTRAINT [FK_NoteReviews_ReviewByID] FOREIGN KEY([ReviewByID])
REFERENCES [dbo].[Users] ([UserID])
GO
ALTER TABLE [dbo].[NoteReviews] CHECK CONSTRAINT [FK_NoteReviews_ReviewByID]
GO
ALTER TABLE [dbo].[SellerNoteAttachment]  WITH CHECK ADD  CONSTRAINT [FK_SellerNoteAttachment_NoteID] FOREIGN KEY([NoteID])
REFERENCES [dbo].[NoteDetails] ([NoteID])
GO
ALTER TABLE [dbo].[SellerNoteAttachment] CHECK CONSTRAINT [FK_SellerNoteAttachment_NoteID]
GO
ALTER TABLE [dbo].[SpamReports]  WITH CHECK ADD  CONSTRAINT [FK_SpamReports_AgainstDownloadID] FOREIGN KEY([AgainstDownloadID])
REFERENCES [dbo].[DownloadNotes] ([DownloadNoteID])
GO
ALTER TABLE [dbo].[SpamReports] CHECK CONSTRAINT [FK_SpamReports_AgainstDownloadID]
GO
ALTER TABLE [dbo].[SpamReports]  WITH CHECK ADD  CONSTRAINT [FK_SpamReports_NoteID] FOREIGN KEY([NoteID])
REFERENCES [dbo].[NoteDetails] ([NoteID])
GO
ALTER TABLE [dbo].[SpamReports] CHECK CONSTRAINT [FK_SpamReports_NoteID]
GO
ALTER TABLE [dbo].[SpamReports]  WITH CHECK ADD  CONSTRAINT [FK_SpamReports_ReportByID] FOREIGN KEY([ReportByID])
REFERENCES [dbo].[Users] ([UserID])
GO
ALTER TABLE [dbo].[SpamReports] CHECK CONSTRAINT [FK_SpamReports_ReportByID]
GO
ALTER TABLE [dbo].[UserProfile]  WITH CHECK ADD  CONSTRAINT [FK_UserProfile_CountryCode] FOREIGN KEY([CountryCode])
REFERENCES [dbo].[Countries] ([CountryID])
GO
ALTER TABLE [dbo].[UserProfile] CHECK CONSTRAINT [FK_UserProfile_CountryCode]
GO
ALTER TABLE [dbo].[UserProfile]  WITH CHECK ADD  CONSTRAINT [FK_UserProfile_CountryID] FOREIGN KEY([CountryID])
REFERENCES [dbo].[Countries] ([CountryID])
GO
ALTER TABLE [dbo].[UserProfile] CHECK CONSTRAINT [FK_UserProfile_CountryID]
GO
ALTER TABLE [dbo].[UserProfile]  WITH CHECK ADD  CONSTRAINT [FK_UserProfile_UserID] FOREIGN KEY([UserID])
REFERENCES [dbo].[Users] ([UserID])
GO
ALTER TABLE [dbo].[UserProfile] CHECK CONSTRAINT [FK_UserProfile_UserID]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_Users_UserRoleID] FOREIGN KEY([UserRoleID])
REFERENCES [dbo].[UserRoles] ([UserRoleID])
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_Users_UserRoleID]
GO
USE [master]
GO
ALTER DATABASE [NotesMarketplace] SET  READ_WRITE 
GO
