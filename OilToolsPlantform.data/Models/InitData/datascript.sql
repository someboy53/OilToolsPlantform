USE [master]
GO
/****** Object:  Database [OilToolsData]    Script Date: 08/04/2020 21:22:12 ******/
CREATE DATABASE [OilToolsData] ON  PRIMARY 
( NAME = N'OilToolsData', FILENAME = N'D:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\DATA\OilToolsData.mdf' , SIZE = 3072KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'OilToolsData_log', FILENAME = N'D:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\DATA\OilToolsData_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [OilToolsData] SET COMPATIBILITY_LEVEL = 100
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [OilToolsData].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [OilToolsData] SET ANSI_NULL_DEFAULT OFF
GO
ALTER DATABASE [OilToolsData] SET ANSI_NULLS OFF
GO
ALTER DATABASE [OilToolsData] SET ANSI_PADDING OFF
GO
ALTER DATABASE [OilToolsData] SET ANSI_WARNINGS OFF
GO
ALTER DATABASE [OilToolsData] SET ARITHABORT OFF
GO
ALTER DATABASE [OilToolsData] SET AUTO_CLOSE OFF
GO
ALTER DATABASE [OilToolsData] SET AUTO_CREATE_STATISTICS ON
GO
ALTER DATABASE [OilToolsData] SET AUTO_SHRINK OFF
GO
ALTER DATABASE [OilToolsData] SET AUTO_UPDATE_STATISTICS ON
GO
ALTER DATABASE [OilToolsData] SET CURSOR_CLOSE_ON_COMMIT OFF
GO
ALTER DATABASE [OilToolsData] SET CURSOR_DEFAULT  GLOBAL
GO
ALTER DATABASE [OilToolsData] SET CONCAT_NULL_YIELDS_NULL OFF
GO
ALTER DATABASE [OilToolsData] SET NUMERIC_ROUNDABORT OFF
GO
ALTER DATABASE [OilToolsData] SET QUOTED_IDENTIFIER OFF
GO
ALTER DATABASE [OilToolsData] SET RECURSIVE_TRIGGERS OFF
GO
ALTER DATABASE [OilToolsData] SET  DISABLE_BROKER
GO
ALTER DATABASE [OilToolsData] SET AUTO_UPDATE_STATISTICS_ASYNC OFF
GO
ALTER DATABASE [OilToolsData] SET DATE_CORRELATION_OPTIMIZATION OFF
GO
ALTER DATABASE [OilToolsData] SET TRUSTWORTHY OFF
GO
ALTER DATABASE [OilToolsData] SET ALLOW_SNAPSHOT_ISOLATION OFF
GO
ALTER DATABASE [OilToolsData] SET PARAMETERIZATION SIMPLE
GO
ALTER DATABASE [OilToolsData] SET READ_COMMITTED_SNAPSHOT OFF
GO
ALTER DATABASE [OilToolsData] SET HONOR_BROKER_PRIORITY OFF
GO
ALTER DATABASE [OilToolsData] SET  READ_WRITE
GO
ALTER DATABASE [OilToolsData] SET RECOVERY SIMPLE
GO
ALTER DATABASE [OilToolsData] SET  MULTI_USER
GO
ALTER DATABASE [OilToolsData] SET PAGE_VERIFY CHECKSUM
GO
ALTER DATABASE [OilToolsData] SET DB_CHAINING OFF
GO
EXEC sys.sp_db_vardecimal_storage_format N'OilToolsData', N'ON'
GO
USE [OilToolsData]
GO
/****** Object:  Table [dbo].[tbUser]    Script Date: 08/04/2020 21:22:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbUser](
	[UserID] [int] IDENTITY(1,1) NOT NULL,
	[UserAccount] [varchar](32) NOT NULL,
	[UserPass] [char](64) NOT NULL,
	[Salt] [char](6) NOT NULL,
	[StartDate] [datetime] NOT NULL,
	[EndDate] [datetime] NOT NULL,
	[LastLoginTime] [datetime] NULL,
	[CreateTime] [datetime] NOT NULL,
	[UpdateTime] [datetime] NOT NULL,
	[Enabled] [char](1) NOT NULL,
 CONSTRAINT [PK_TBAUSER] PRIMARY KEY NONCLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbUser', @level2type=N'COLUMN',@level2name=N'UserID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户帐号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbUser', @level2type=N'COLUMN',@level2name=N'UserAccount'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户密码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbUser', @level2type=N'COLUMN',@level2name=N'UserPass'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'随机值' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbUser', @level2type=N'COLUMN',@level2name=N'Salt'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'开始时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbUser', @level2type=N'COLUMN',@level2name=N'StartDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'结束时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbUser', @level2type=N'COLUMN',@level2name=N'EndDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最后一次登陆时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbUser', @level2type=N'COLUMN',@level2name=N'LastLoginTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbUser', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbUser', @level2type=N'COLUMN',@level2name=N'UpdateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否启用' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbUser', @level2type=N'COLUMN',@level2name=N'Enabled'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'需要登陆系统的用户,一般与Emp一一对应,但是如果不登陆管理平台,则不需对应员工' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbUser'
GO
/****** Object:  Table [dbo].[tbCatF]    Script Date: 08/04/2020 21:22:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbCatF](
	[CatFID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](20) NOT NULL,
	[NameJP] [nvarchar](20) NOT NULL,
	[NameQP] [nvarchar](100) NOT NULL,
	[Description] [nvarchar](500) NOT NULL,
	[CreateUser] [varchar](32) NOT NULL,
	[CreateTime] [date] NOT NULL,
	[UpdateUser] [varchar](32) NOT NULL,
	[UpdateTime] [date] NOT NULL,
	[SortOrder] [int] NOT NULL,
	[Enabled] [char](1) NOT NULL,
 CONSTRAINT [PK_tbCatF] PRIMARY KEY CLUSTERED 
(
	[CatFID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否启用' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbCatF', @level2type=N'COLUMN',@level2name=N'Enabled'
GO
/****** Object:  Table [dbo].[tbPic]    Script Date: 08/04/2020 21:22:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbPic](
	[PicID] [int] IDENTITY(1,1) NOT NULL,
	[PicName] [nvarchar](120) NOT NULL,
	[StoreName] [varchar](120) NOT NULL,
	[Path] [varchar](500) NOT NULL,
	[ThambName] [varchar](120) NOT NULL,
	[CreateUser] [varchar](32) NOT NULL,
	[CreateTime] [date] NOT NULL,
 CONSTRAINT [PK_tbPic] PRIMARY KEY CLUSTERED 
(
	[PicID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbCatS]    Script Date: 08/04/2020 21:22:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbCatS](
	[CatSID] [int] IDENTITY(1,1) NOT NULL,
	[CatFID] [int] NOT NULL,
	[Name] [nvarchar](20) NOT NULL,
	[NameJP] [nvarchar](20) NOT NULL,
	[NameQP] [nvarchar](100) NOT NULL,
	[Description] [nvarchar](500) NOT NULL,
	[CreateUser] [varchar](32) NOT NULL,
	[CreateTime] [date] NOT NULL,
	[UpdateUser] [varchar](32) NOT NULL,
	[UpdateTime] [date] NOT NULL,
	[SortOrder] [int] NOT NULL,
	[Enabled] [char](1) NOT NULL,
 CONSTRAINT [PK_tbCatS] PRIMARY KEY CLUSTERED 
(
	[CatSID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否启用' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbCatS', @level2type=N'COLUMN',@level2name=N'Enabled'
GO
/****** Object:  Table [dbo].[tbCatFPic]    Script Date: 08/04/2020 21:22:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbCatFPic](
	[CatFPicID] [int] IDENTITY(1,1) NOT NULL,
	[CatFID] [int] NOT NULL,
	[PicID] [int] NOT NULL,
	[PicType] [char](1) NOT NULL,
	[SortOrder] [int] NOT NULL,
	[Enabled] [char](1) NOT NULL,
 CONSTRAINT [PK_tbCatFPic] PRIMARY KEY CLUSTERED 
(
	[CatFPicID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0-头图' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbCatFPic', @level2type=N'COLUMN',@level2name=N'PicType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否启用' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbCatFPic', @level2type=N'COLUMN',@level2name=N'Enabled'
GO
/****** Object:  Table [dbo].[tbCatSPic]    Script Date: 08/04/2020 21:22:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbCatSPic](
	[CatSPicID] [int] IDENTITY(1,1) NOT NULL,
	[CatSID] [int] NOT NULL,
	[PicID] [int] NOT NULL,
	[PicType] [char](1) NOT NULL,
	[SortOrder] [int] NOT NULL,
	[Enabled] [char](1) NOT NULL,
 CONSTRAINT [PK_tbCatSPic] PRIMARY KEY CLUSTERED 
(
	[CatSPicID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0-头图' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbCatSPic', @level2type=N'COLUMN',@level2name=N'PicType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否启用' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbCatSPic', @level2type=N'COLUMN',@level2name=N'Enabled'
GO
/****** Object:  Table [dbo].[tbTool]    Script Date: 08/04/2020 21:22:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbTool](
	[ToolID] [int] IDENTITY(1,1) NOT NULL,
	[CatSID] [int] NOT NULL,
	[Name] [nvarchar](20) NOT NULL,
	[NameJP] [nvarchar](20) NOT NULL,
	[NameQP] [nvarchar](100) NOT NULL,
	[Description] [nvarchar](500) NOT NULL,
	[CreateUser] [varchar](32) NOT NULL,
	[CreateTime] [date] NOT NULL,
	[UpdateUser] [varchar](32) NOT NULL,
	[UpdateTime] [date] NOT NULL,
	[SortOrder] [int] NOT NULL,
	[Enabled] [char](1) NOT NULL,
	[SearchStr] [nvarchar](500) NOT NULL,
 CONSTRAINT [PK_tbTool] PRIMARY KEY CLUSTERED 
(
	[ToolID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否启用' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbTool', @level2type=N'COLUMN',@level2name=N'Enabled'
GO
/****** Object:  Table [dbo].[tbToolPic]    Script Date: 08/04/2020 21:22:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbToolPic](
	[ToolPicID] [int] IDENTITY(1,1) NOT NULL,
	[ToolID] [int] NOT NULL,
	[PicID] [int] NOT NULL,
	[PicType] [char](1) NOT NULL,
	[SortOrder] [int] NOT NULL,
	[Enabled] [char](1) NOT NULL,
 CONSTRAINT [PK_tbToolPic] PRIMARY KEY CLUSTERED 
(
	[ToolPicID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0-头图' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbToolPic', @level2type=N'COLUMN',@level2name=N'PicType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否启用' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbToolPic', @level2type=N'COLUMN',@level2name=N'Enabled'
GO
/****** Object:  Table [dbo].[tbToolDetail]    Script Date: 08/04/2020 21:22:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbToolDetail](
	[ToolDetailID] [int] IDENTITY(1,1) NOT NULL,
	[ToolID] [int] NOT NULL,
	[DetailIcon] [int] NOT NULL,
	[IconName] [nvarchar](20) NOT NULL,
	[Name] [nvarchar](20) NOT NULL,
	[NameJP] [nvarchar](20) NOT NULL,
	[NameQP] [nvarchar](100) NOT NULL,
	[Description] [nvarchar](500) NOT NULL,
	[CreateUser] [varchar](32) NOT NULL,
	[CreateTime] [date] NOT NULL,
	[UpdateUser] [varchar](32) NOT NULL,
	[UpdateTime] [date] NOT NULL,
	[SortOrder] [int] NOT NULL,
	[Enabled] [char](1) NOT NULL,
 CONSTRAINT [PK_tbToolDetail] PRIMARY KEY CLUSTERED 
(
	[ToolDetailID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'明细的图标，当为0时，为自定义图标' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbToolDetail', @level2type=N'COLUMN',@level2name=N'DetailIcon'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'当为0时，此名称为必输项，其它时，名称为DetailIcon[序号]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbToolDetail', @level2type=N'COLUMN',@level2name=N'IconName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否启用' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbToolDetail', @level2type=N'COLUMN',@level2name=N'Enabled'
GO
/****** Object:  ForeignKey [FK_001]    Script Date: 08/04/2020 21:22:14 ******/
ALTER TABLE [dbo].[tbCatS]  WITH NOCHECK ADD  CONSTRAINT [FK_001] FOREIGN KEY([CatFID])
REFERENCES [dbo].[tbCatF] ([CatFID])
NOT FOR REPLICATION
GO
ALTER TABLE [dbo].[tbCatS] NOCHECK CONSTRAINT [FK_001]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'一级目录的二级目录' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbCatS', @level2type=N'CONSTRAINT',@level2name=N'FK_001'
GO
/****** Object:  ForeignKey [FK_002]    Script Date: 08/04/2020 21:22:14 ******/
ALTER TABLE [dbo].[tbCatFPic]  WITH NOCHECK ADD  CONSTRAINT [FK_002] FOREIGN KEY([CatFID])
REFERENCES [dbo].[tbCatF] ([CatFID])
NOT FOR REPLICATION
GO
ALTER TABLE [dbo].[tbCatFPic] NOCHECK CONSTRAINT [FK_002]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'一级目录的图片' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbCatFPic', @level2type=N'CONSTRAINT',@level2name=N'FK_002'
GO
/****** Object:  ForeignKey [FK_003]    Script Date: 08/04/2020 21:22:14 ******/
ALTER TABLE [dbo].[tbCatFPic]  WITH NOCHECK ADD  CONSTRAINT [FK_003] FOREIGN KEY([PicID])
REFERENCES [dbo].[tbPic] ([PicID])
NOT FOR REPLICATION
GO
ALTER TABLE [dbo].[tbCatFPic] NOCHECK CONSTRAINT [FK_003]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'一级目录的图片' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbCatFPic', @level2type=N'CONSTRAINT',@level2name=N'FK_003'
GO
/****** Object:  ForeignKey [FK_004]    Script Date: 08/04/2020 21:22:14 ******/
ALTER TABLE [dbo].[tbCatSPic]  WITH NOCHECK ADD  CONSTRAINT [FK_004] FOREIGN KEY([CatSID])
REFERENCES [dbo].[tbCatS] ([CatSID])
NOT FOR REPLICATION
GO
ALTER TABLE [dbo].[tbCatSPic] NOCHECK CONSTRAINT [FK_004]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'二级目录的图片' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbCatSPic', @level2type=N'CONSTRAINT',@level2name=N'FK_004'
GO
/****** Object:  ForeignKey [FK_005]    Script Date: 08/04/2020 21:22:14 ******/
ALTER TABLE [dbo].[tbCatSPic]  WITH NOCHECK ADD  CONSTRAINT [FK_005] FOREIGN KEY([PicID])
REFERENCES [dbo].[tbPic] ([PicID])
NOT FOR REPLICATION
GO
ALTER TABLE [dbo].[tbCatSPic] NOCHECK CONSTRAINT [FK_005]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'二级目录的图片' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbCatSPic', @level2type=N'CONSTRAINT',@level2name=N'FK_005'
GO
/****** Object:  ForeignKey [FK_009]    Script Date: 08/04/2020 21:22:14 ******/
ALTER TABLE [dbo].[tbTool]  WITH NOCHECK ADD  CONSTRAINT [FK_009] FOREIGN KEY([CatSID])
REFERENCES [dbo].[tbCatS] ([CatSID])
NOT FOR REPLICATION
GO
ALTER TABLE [dbo].[tbTool] NOCHECK CONSTRAINT [FK_009]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工具所属的二级目录' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbTool', @level2type=N'CONSTRAINT',@level2name=N'FK_009'
GO
/****** Object:  ForeignKey [FK_006]    Script Date: 08/04/2020 21:22:14 ******/
ALTER TABLE [dbo].[tbToolPic]  WITH NOCHECK ADD  CONSTRAINT [FK_006] FOREIGN KEY([ToolID])
REFERENCES [dbo].[tbTool] ([ToolID])
NOT FOR REPLICATION
GO
ALTER TABLE [dbo].[tbToolPic] NOCHECK CONSTRAINT [FK_006]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工具的图片' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbToolPic', @level2type=N'CONSTRAINT',@level2name=N'FK_006'
GO
/****** Object:  ForeignKey [FK_007]    Script Date: 08/04/2020 21:22:14 ******/
ALTER TABLE [dbo].[tbToolPic]  WITH NOCHECK ADD  CONSTRAINT [FK_007] FOREIGN KEY([PicID])
REFERENCES [dbo].[tbPic] ([PicID])
NOT FOR REPLICATION
GO
ALTER TABLE [dbo].[tbToolPic] NOCHECK CONSTRAINT [FK_007]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工具的图片' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbToolPic', @level2type=N'CONSTRAINT',@level2name=N'FK_007'
GO
/****** Object:  ForeignKey [FK_008]    Script Date: 08/04/2020 21:22:14 ******/
ALTER TABLE [dbo].[tbToolDetail]  WITH NOCHECK ADD  CONSTRAINT [FK_008] FOREIGN KEY([ToolID])
REFERENCES [dbo].[tbTool] ([ToolID])
NOT FOR REPLICATION
GO
ALTER TABLE [dbo].[tbToolDetail] NOCHECK CONSTRAINT [FK_008]
GO
