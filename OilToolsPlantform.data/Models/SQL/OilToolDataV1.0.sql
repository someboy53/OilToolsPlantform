USE [OilToolsData]
GO
/****** Object:  Table [dbo].[tbRole]    Script Date: 01/27/2021 22:34:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbRole](
	[RoleID] [int] IDENTITY(1,1) NOT NULL,
	[RoleName] [nvarchar](50) NULL,
	[RoleType] [char](1) NULL,
 CONSTRAINT [PK_tbRole] PRIMARY KEY CLUSTERED 
(
	[RoleID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbPic]    Script Date: 01/27/2021 22:34:23 ******/
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
/****** Object:  Table [dbo].[tbOrg]    Script Date: 01/27/2021 22:34:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbOrg](
	[OrgID] [int] IDENTITY(1,1) NOT NULL,
	[OrgName] [nvarchar](50) NOT NULL,
	[RealPath] [nvarchar](2000) NULL,
	[FullPath] [nvarchar](2000) NULL,
	[UpdateTime] [datetime] NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[Description] [nvarchar](2000) NULL,
	[ParentID] [int] NOT NULL,
 CONSTRAINT [PK_tbOrg] PRIMARY KEY CLUSTERED 
(
	[OrgID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbOrg', @level2type=N'COLUMN',@level2name=N'UpdateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbOrg', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
/****** Object:  Table [dbo].[tbLog]    Script Date: 01/27/2021 22:34:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbLog](
	[LogID] [int] IDENTITY(1,1) NOT NULL,
	[AccountNumber] [varchar](32) NULL,
	[UserID] [int] NULL,
	[Request] [nvarchar](500) NULL,
	[Response] [nvarchar](500) NULL,
	[Description] [nvarchar](500) NULL,
	[HappenDate] [datetime] NULL,
 CONSTRAINT [PK_tbLog] PRIMARY KEY CLUSTERED 
(
	[LogID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbCountAna]    Script Date: 01/27/2021 22:34:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbCountAna](
	[CountAnaID] [int] NOT NULL,
	[Day] [date] NOT NULL,
	[ToolAdd] [int] NOT NULL,
	[FanAdd] [int] NOT NULL,
	[ViewAdd] [int] NOT NULL,
 CONSTRAINT [PK_tbCountAna] PRIMARY KEY CLUSTERED 
(
	[CountAnaID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbCatF]    Script Date: 01/27/2021 22:34:23 ******/
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
/****** Object:  Table [dbo].[tbAudit]    Script Date: 01/27/2021 22:34:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbAudit](
	[AuditID] [int] IDENTITY(1,1) NOT NULL,
	[TargetTableName] [varchar](50) NOT NULL,
	[TargetTableID] [int] NOT NULL,
	[Status] [int] NOT NULL,
	[Status_Desc] [nvarchar](500) NULL,
	[NextAuditOrgID] [int] NULL,
	[NextAuditStep] [int] NULL,
	[LastAuditName] [nvarchar](50) NULL,
	[LastAuditDate] [datetime] NULL,
	[AuditID1] [int] NULL,
	[AuditName1] [nvarchar](50) NULL,
	[AuditStatus1] [int] NULL,
	[AuditDate1] [datetime] NULL,
	[AuditID2] [int] NULL,
	[AuditName2] [nvarchar](50) NULL,
	[AuditStatus2] [int] NULL,
	[AuditDate2] [datetime] NULL,
	[AuditID3] [int] NULL,
	[AuditName3] [nvarchar](50) NULL,
	[AuditStatus3] [int] NULL,
	[AuditDate3] [datetime] NULL,
	[AuditID4] [int] NULL,
	[AuditName4] [nvarchar](50) NULL,
	[AuditStatus4] [int] NULL,
	[AuditDate4] [datetime] NULL,
	[AuditID5] [int] NULL,
	[AuditName5] [nvarchar](50) NULL,
	[AuditStatus5] [int] NULL,
	[AuditDate5] [datetime] NULL,
	[AuditAdvice1] [nvarchar](500) NULL,
	[AuditAdvice2] [nvarchar](500) NULL,
	[AuditAdvice3] [nvarchar](500) NULL,
	[AuditAdvice4] [nvarchar](500) NULL,
	[AuditAdvice5] [nvarchar](500) NULL,
 CONSTRAINT [PK_tbAudit] PRIMARY KEY CLUSTERED 
(
	[AuditID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'目标表名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbAudit', @level2type=N'COLUMN',@level2name=N'TargetTableName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'目标表ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbAudit', @level2type=N'COLUMN',@level2name=N'TargetTableID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'状态，0-作废1-不通过2-草稿3-审核中4-审核完成' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbAudit', @level2type=N'COLUMN',@level2name=N'Status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'下一审核部门' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbAudit', @level2type=N'COLUMN',@level2name=N'NextAuditOrgID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最后审核人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbAudit', @level2type=N'COLUMN',@level2name=N'LastAuditName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最后审核时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbAudit', @level2type=N'COLUMN',@level2name=N'LastAuditDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'1不通过2-返回修改4通过' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbAudit', @level2type=N'COLUMN',@level2name=N'AuditStatus1'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'1不通过2-返回修改4通过' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbAudit', @level2type=N'COLUMN',@level2name=N'AuditStatus2'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'1不通过2-返回修改4通过' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbAudit', @level2type=N'COLUMN',@level2name=N'AuditStatus3'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'1不通过2-返回修改4通过' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbAudit', @level2type=N'COLUMN',@level2name=N'AuditStatus4'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'1不通过2-返回修改4通过' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbAudit', @level2type=N'COLUMN',@level2name=N'AuditStatus5'
GO
/****** Object:  Table [dbo].[tbWechatFan]    Script Date: 01/27/2021 22:34:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbWechatFan](
	[WechatFanID] [int] IDENTITY(1,1) NOT NULL,
	[openid] [nvarchar](64) NOT NULL,
	[subscribe] [char](1) NULL,
	[nickname] [nvarchar](64) NULL,
	[sex] [char](1) NULL,
	[city] [nvarchar](64) NULL,
	[country] [nvarchar](64) NULL,
	[province] [nvarchar](64) NULL,
	[language] [nvarchar](32) NULL,
	[headimgurl] [varchar](1024) NULL,
	[subscribe_time] [nvarchar](64) NULL,
	[unionid] [nvarchar](64) NULL,
	[remark] [nvarchar](1024) NULL,
	[groupid] [nvarchar](64) NULL,
	[tagid_list] [nvarchar](1024) NULL,
	[subscribe_scene] [nvarchar](64) NULL,
	[qr_scene] [nvarchar](64) NULL,
	[qr_scene_str] [nvarchar](64) NULL,
	[updatetime] [datetime] NOT NULL,
 CONSTRAINT [PK_tbWechatFan] PRIMARY KEY CLUSTERED 
(
	[WechatFanID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户的标识，对当前公众号唯一' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbWechatFan', @level2type=N'COLUMN',@level2name=N'openid'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户是否订阅该公众号标识，值为0时，代表此用户没有关注该公众号，拉取不到其余信息。' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbWechatFan', @level2type=N'COLUMN',@level2name=N'subscribe'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户的昵称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbWechatFan', @level2type=N'COLUMN',@level2name=N'nickname'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户的性别，值为1时是男性，值为2时是女性，值为0时是未知' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbWechatFan', @level2type=N'COLUMN',@level2name=N'sex'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户所在城市' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbWechatFan', @level2type=N'COLUMN',@level2name=N'city'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户所在国家' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbWechatFan', @level2type=N'COLUMN',@level2name=N'country'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户所在省份' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbWechatFan', @level2type=N'COLUMN',@level2name=N'province'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户的语言，简体中文为zh_CN' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbWechatFan', @level2type=N'COLUMN',@level2name=N'language'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户头像，最后一个数值代表正方形头像大小（有0、46、64、96、132数值可选，0代表640*640正方形头像），用户没有头像时该项为空。若用户更换头像，原有头像URL将失效。' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbWechatFan', @level2type=N'COLUMN',@level2name=N'headimgurl'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户关注时间，为时间戳。如果用户曾多次关注，则取最后关注时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbWechatFan', @level2type=N'COLUMN',@level2name=N'subscribe_time'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'只有在用户将公众号绑定到微信开放平台帐号后，才会出现该字段。' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbWechatFan', @level2type=N'COLUMN',@level2name=N'unionid'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'公众号运营者对粉丝的备注，公众号运营者可在微信公众平台用户管理界面对粉丝添加备注' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbWechatFan', @level2type=N'COLUMN',@level2name=N'remark'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户所在的分组ID（暂时兼容用户分组旧接口）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbWechatFan', @level2type=N'COLUMN',@level2name=N'groupid'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户被打上的标签ID列表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbWechatFan', @level2type=N'COLUMN',@level2name=N'tagid_list'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'返回用户关注的渠道来源，ADD_SCENE_SEARCH 公众号搜索，ADD_SCENE_ACCOUNT_MIGRATION 公众号迁移，ADD_SCENE_PROFILE_CARD 名片分享，ADD_SCENE_QR_CODE 扫描二维码，ADD_SCENE_PROFILE_LINK 图文页内名称点击，ADD_SCENE_PROFILE_ITEM 图文页右上角菜单，ADD_SCENE_PAID 支付后关注，ADD_SCENE_WECHAT_ADVERTISEMENT 微信广告，ADD_SCENE_OTHERS 其他' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbWechatFan', @level2type=N'COLUMN',@level2name=N'subscribe_scene'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'二维码扫码场景（开发者自定义）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbWechatFan', @level2type=N'COLUMN',@level2name=N'qr_scene'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'二维码扫码场景描述（开发者自定义）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbWechatFan', @level2type=N'COLUMN',@level2name=N'qr_scene_str'
GO
/****** Object:  Table [dbo].[tbToken]    Script Date: 01/27/2021 22:34:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbToken](
	[TokenID] [int] IDENTITY(1,1) NOT NULL,
	[UserAccount] [varchar](32) NOT NULL,
	[token] [char](64) NOT NULL,
	[CreateDate] [date] NULL,
	[UserID] [int] NOT NULL,
 CONSTRAINT [PK_tbToken] PRIMARY KEY CLUSTERED 
(
	[TokenID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbRoleDetail]    Script Date: 01/27/2021 22:34:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbRoleDetail](
	[RoleDetailID] [int] IDENTITY(1,1) NOT NULL,
	[RoleID] [int] NULL,
	[FunctionCode] [nvarchar](50) NULL,
 CONSTRAINT [PK_tbRoleDetail] PRIMARY KEY CLUSTERED 
(
	[RoleDetailID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbUser]    Script Date: 01/27/2021 22:34:23 ******/
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
	[OrgID] [int] NULL,
	[CellPhone] [varchar](18) NULL,
	[Email] [varchar](200) NULL,
	[WorkNumber] [varchar](20) NULL,
	[RoleID] [int] NULL,
	[UserName] [nvarchar](50) NULL,
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
/****** Object:  Table [dbo].[tbCatS]    Script Date: 01/27/2021 22:34:23 ******/
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
/****** Object:  Table [dbo].[tbCatFPic]    Script Date: 01/27/2021 22:34:23 ******/
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
/****** Object:  Table [dbo].[tbCatSPic]    Script Date: 01/27/2021 22:34:23 ******/
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
/****** Object:  Table [dbo].[tbTool]    Script Date: 01/27/2021 22:34:23 ******/
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
/****** Object:  Table [dbo].[tbToolPic]    Script Date: 01/27/2021 22:34:23 ******/
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
/****** Object:  Table [dbo].[tbToolExt]    Script Date: 01/27/2021 22:34:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbToolExt](
	[ToolExtID] [int] IDENTITY(1,1) NOT NULL,
	[ToolID] [int] NOT NULL,
	[ViewCount] [int] NOT NULL,
	[LikeCount] [int] NOT NULL,
	[FavCount] [int] NOT NULL,
	[CommentCount] [int] NOT NULL,
 CONSTRAINT [PK_tbToolExt] PRIMARY KEY CLUSTERED 
(
	[ToolExtID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbToolExt', @level2type=N'COLUMN',@level2name=N'ToolID'
GO
/****** Object:  Table [dbo].[tbToolDetail]    Script Date: 01/27/2021 22:34:23 ******/
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
/****** Object:  Default [DF_tbOrg_ParentID]    Script Date: 01/27/2021 22:34:23 ******/
ALTER TABLE [dbo].[tbOrg] ADD  CONSTRAINT [DF_tbOrg_ParentID]  DEFAULT ((0)) FOR [ParentID]
GO
/****** Object:  Default [DF_tbWechatFan_updatetime]    Script Date: 01/27/2021 22:34:23 ******/
ALTER TABLE [dbo].[tbWechatFan] ADD  CONSTRAINT [DF_tbWechatFan_updatetime]  DEFAULT (getdate()) FOR [updatetime]
GO
/****** Object:  ForeignKey [FK_002]    Script Date: 01/27/2021 22:34:23 ******/
ALTER TABLE [dbo].[tbCatFPic]  WITH NOCHECK ADD  CONSTRAINT [FK_002] FOREIGN KEY([CatFID])
REFERENCES [dbo].[tbCatF] ([CatFID])
NOT FOR REPLICATION
GO
ALTER TABLE [dbo].[tbCatFPic] NOCHECK CONSTRAINT [FK_002]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'一级目录的图片' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbCatFPic', @level2type=N'CONSTRAINT',@level2name=N'FK_002'
GO
/****** Object:  ForeignKey [FK_003]    Script Date: 01/27/2021 22:34:23 ******/
ALTER TABLE [dbo].[tbCatFPic]  WITH NOCHECK ADD  CONSTRAINT [FK_003] FOREIGN KEY([PicID])
REFERENCES [dbo].[tbPic] ([PicID])
NOT FOR REPLICATION
GO
ALTER TABLE [dbo].[tbCatFPic] NOCHECK CONSTRAINT [FK_003]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'一级目录的图片' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbCatFPic', @level2type=N'CONSTRAINT',@level2name=N'FK_003'
GO
/****** Object:  ForeignKey [FK_001]    Script Date: 01/27/2021 22:34:23 ******/
ALTER TABLE [dbo].[tbCatS]  WITH NOCHECK ADD  CONSTRAINT [FK_001] FOREIGN KEY([CatFID])
REFERENCES [dbo].[tbCatF] ([CatFID])
NOT FOR REPLICATION
GO
ALTER TABLE [dbo].[tbCatS] NOCHECK CONSTRAINT [FK_001]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'一级目录的二级目录' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbCatS', @level2type=N'CONSTRAINT',@level2name=N'FK_001'
GO
/****** Object:  ForeignKey [FK_004]    Script Date: 01/27/2021 22:34:23 ******/
ALTER TABLE [dbo].[tbCatSPic]  WITH NOCHECK ADD  CONSTRAINT [FK_004] FOREIGN KEY([CatSID])
REFERENCES [dbo].[tbCatS] ([CatSID])
NOT FOR REPLICATION
GO
ALTER TABLE [dbo].[tbCatSPic] NOCHECK CONSTRAINT [FK_004]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'二级目录的图片' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbCatSPic', @level2type=N'CONSTRAINT',@level2name=N'FK_004'
GO
/****** Object:  ForeignKey [FK_005]    Script Date: 01/27/2021 22:34:23 ******/
ALTER TABLE [dbo].[tbCatSPic]  WITH NOCHECK ADD  CONSTRAINT [FK_005] FOREIGN KEY([PicID])
REFERENCES [dbo].[tbPic] ([PicID])
NOT FOR REPLICATION
GO
ALTER TABLE [dbo].[tbCatSPic] NOCHECK CONSTRAINT [FK_005]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'二级目录的图片' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbCatSPic', @level2type=N'CONSTRAINT',@level2name=N'FK_005'
GO
/****** Object:  ForeignKey [FK_tbRoleDetail_tbRole]    Script Date: 01/27/2021 22:34:23 ******/
ALTER TABLE [dbo].[tbRoleDetail]  WITH CHECK ADD  CONSTRAINT [FK_tbRoleDetail_tbRole] FOREIGN KEY([RoleID])
REFERENCES [dbo].[tbRole] ([RoleID])
GO
ALTER TABLE [dbo].[tbRoleDetail] CHECK CONSTRAINT [FK_tbRoleDetail_tbRole]
GO
/****** Object:  ForeignKey [FK_009]    Script Date: 01/27/2021 22:34:23 ******/
ALTER TABLE [dbo].[tbTool]  WITH NOCHECK ADD  CONSTRAINT [FK_009] FOREIGN KEY([CatSID])
REFERENCES [dbo].[tbCatS] ([CatSID])
NOT FOR REPLICATION
GO
ALTER TABLE [dbo].[tbTool] NOCHECK CONSTRAINT [FK_009]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工具所属的二级目录' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbTool', @level2type=N'CONSTRAINT',@level2name=N'FK_009'
GO
/****** Object:  ForeignKey [FK_008]    Script Date: 01/27/2021 22:34:23 ******/
ALTER TABLE [dbo].[tbToolDetail]  WITH NOCHECK ADD  CONSTRAINT [FK_008] FOREIGN KEY([ToolID])
REFERENCES [dbo].[tbTool] ([ToolID])
NOT FOR REPLICATION
GO
ALTER TABLE [dbo].[tbToolDetail] NOCHECK CONSTRAINT [FK_008]
GO
/****** Object:  ForeignKey [FK_tbToolExt_tbTool]    Script Date: 01/27/2021 22:34:23 ******/
ALTER TABLE [dbo].[tbToolExt]  WITH CHECK ADD  CONSTRAINT [FK_tbToolExt_tbTool] FOREIGN KEY([ToolID])
REFERENCES [dbo].[tbTool] ([ToolID])
GO
ALTER TABLE [dbo].[tbToolExt] CHECK CONSTRAINT [FK_tbToolExt_tbTool]
GO
/****** Object:  ForeignKey [FK_006]    Script Date: 01/27/2021 22:34:23 ******/
ALTER TABLE [dbo].[tbToolPic]  WITH NOCHECK ADD  CONSTRAINT [FK_006] FOREIGN KEY([ToolID])
REFERENCES [dbo].[tbTool] ([ToolID])
NOT FOR REPLICATION
GO
ALTER TABLE [dbo].[tbToolPic] NOCHECK CONSTRAINT [FK_006]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工具的图片' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbToolPic', @level2type=N'CONSTRAINT',@level2name=N'FK_006'
GO
/****** Object:  ForeignKey [FK_007]    Script Date: 01/27/2021 22:34:23 ******/
ALTER TABLE [dbo].[tbToolPic]  WITH NOCHECK ADD  CONSTRAINT [FK_007] FOREIGN KEY([PicID])
REFERENCES [dbo].[tbPic] ([PicID])
NOT FOR REPLICATION
GO
ALTER TABLE [dbo].[tbToolPic] NOCHECK CONSTRAINT [FK_007]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工具的图片' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbToolPic', @level2type=N'CONSTRAINT',@level2name=N'FK_007'
GO
/****** Object:  ForeignKey [FK_010]    Script Date: 01/27/2021 22:34:23 ******/
ALTER TABLE [dbo].[tbUser]  WITH CHECK ADD  CONSTRAINT [FK_010] FOREIGN KEY([OrgID])
REFERENCES [dbo].[tbOrg] ([OrgID])
GO
ALTER TABLE [dbo].[tbUser] CHECK CONSTRAINT [FK_010]
GO
/****** Object:  ForeignKey [FK_011]    Script Date: 01/27/2021 22:34:23 ******/
ALTER TABLE [dbo].[tbUser]  WITH CHECK ADD  CONSTRAINT [FK_011] FOREIGN KEY([RoleID])
REFERENCES [dbo].[tbRole] ([RoleID])
GO
ALTER TABLE [dbo].[tbUser] CHECK CONSTRAINT [FK_011]
GO
