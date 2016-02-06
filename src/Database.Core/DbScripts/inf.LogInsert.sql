Use [SIS]
go

create procedure [inf].[usp_LogInsert]
    @MachineName [nvarchar](200),
    @SiteName [nvarchar](200),
    @Logged [datetime],
    @Level [nvarchar](5),
    @UserName [nvarchar](200),
    @Message [nvarchar](max),
    @Logger [nvarchar](300),
    @Properties [nvarchar](max),
    @ServerName [nvarchar](200),
    @Port [nvarchar](100),
    @Url [nvarchar](2000),
    @Https [bit],
    @ServerAddress [nvarchar](100),
    @RemoteAddress [nvarchar](100),
    @Callstie [nvarchar](300),
    @Exception [nvarchar](max)
AS
BEGIN
    INSERT [inf].[Log]
		(
			[MachineName], 
			[SiteName], 
			[Logged], 
			[Level], 
			[UserName], 
			[Message], 
			[Logger], 
			[Properties], 
			[ServerName], 
			[Port], 
			[Url], 
			[Https], 
			[ServerAddress], 
			[RemoteAddress], 
			[Callstie], 
			[Exception], 
			[SystemCode], 
			[AddUser], 
			[AddDate]
		)
    VALUES 
		(
			@MachineName, 
			@SiteName, 
			@Logged, 
			@Level, 
			@UserName, 
			@Message, 
			@Logger, 
			@Properties, 
			@ServerName, 
			@Port, @Url, @Https, @ServerAddress, 
			@RemoteAddress, 
			@Callstie, 
			@Exception, 
			NULL, 
			'NLogger', 
			GETDATE()
		)
END