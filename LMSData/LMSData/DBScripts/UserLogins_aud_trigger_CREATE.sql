SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Flowers, Robert
-- Create date: 5/24/2018
-- Description:	Implements the Auditing Trigger functions
--					For the PaRequests Table
-- =============================================
create trigger UserLogins_aud_trigger
on UserLogins
after UPDATE, INSERT, DELETE
as
declare	 @recordId				int
		,@user					varchar(20)
		,@action				varchar(20)
        ,@passwordHash			nvarchar(max)
        ,@lockoutEnabled		bit
        ,@lockoutEnd			datetime
        ,@accessFailedCount		int
        ,@isAdmin				bit
		,@created				datetime
        ,@createdBy				nvarchar(max)
        ,@lastModified			datetime
        ,@lastModifiedBy		nvarchar(max)
        ,@actionDate			datetime
		,@login					nvarchar(max)
		,@refreshId				bigint

if exists(SELECT * from inserted) and exists (SELECT * from deleted)
begin
	SET NOCOUNT ON;
    SET @action = 'UPDATE';
    SET @user = SYSTEM_USER;
    SELECT	 @recordId = Id
			,@passwordHash = PasswordHash
			,@lockoutEnabled = LockoutEnabled
			,@lockoutEnd = LockoutEnd
			,@accessFailedCount = AccessFailedCount
			,@isAdmin = IsAdmin
			,@created = Created
			,@createdBy = CreatedBy
			,@lastModified = LastModified
			,@lastModifiedBy = LastModifiedBy
			,@login = Login
			,@refreshId = RefreshId
	FROM inserted i;
    INSERT INTO [dbo].[UserLoginAudits]
           ([RecordId]
           ,[PasswordHash]
		   ,[LockoutEnabled]
		   ,[LockoutEnd]
		   ,[AccessFailedCount]
		   ,[isAdmin]
           ,[Created]
           ,[CreatedBy]
           ,[LastModified]
           ,[LastModifiedBy]
           ,[ActionDate]
		   ,[Action]
		   ,[Login]
		   ,[RefreshId])
     VALUES
           (@recordId
           ,@passwordHash
		   ,@lockoutEnabled
		   ,@lockoutEnd
		   ,@accessFailedCount
		   ,@isAdmin
           ,@created
           ,@createdBy
           ,@lastModified
           ,@lastModifiedBy
           ,CURRENT_TIMESTAMP
		   ,@action
		   ,@login
		   ,@refreshId)
end

If exists (Select * from inserted) and not exists(Select * from deleted)
begin
	SET NOCOUNT ON;
    SET @action = 'INSERT';
    SET @user = SYSTEM_USER;
    SELECT	 @recordId = Id
			,@passwordHash = PasswordHash
			,@lockoutEnabled = LockoutEnabled
			,@lockoutEnd = LockoutEnd
			,@accessFailedCount = AccessFailedCount
			,@isAdmin = IsAdmin
			,@created = Created
			,@createdBy = CreatedBy
			,@lastModified = LastModified
			,@lastModifiedBy = LastModifiedBy
			,@login = Login
			,@refreshId = RefreshId
	FROM inserted i;
    INSERT INTO [dbo].[UserLoginAudits]
           ([RecordId]
           ,[PasswordHash]
		   ,[LockoutEnabled]
		   ,[LockoutEnd]
		   ,[AccessFailedCount]
		   ,[isAdmin]
           ,[Created]
           ,[CreatedBy]
           ,[LastModified]
           ,[LastModifiedBy]
           ,[ActionDate]
		   ,[Action]
		   ,[Login]
		   ,[RefreshId])
     VALUES
           (@recordId
           ,@passwordHash
		   ,@lockoutEnabled
		   ,@lockoutEnd
		   ,@accessFailedCount
		   ,@isAdmin
           ,@created
           ,@createdBy
           ,@lastModified
           ,@lastModifiedBy
           ,CURRENT_TIMESTAMP
		   ,@action
		   ,@login
		   ,@refreshId)
end

If exists(select * from deleted) and not exists(Select * from inserted)
begin
	SET NOCOUNT ON;
    SET @action = 'DELETE';
    SET @user = SYSTEM_USER;
    SELECT	 @recordId = Id
			,@passwordHash = PasswordHash
			,@lockoutEnabled = LockoutEnabled
			,@lockoutEnd = LockoutEnd
			,@accessFailedCount = AccessFailedCount
			,@isAdmin = IsAdmin
			,@created = Created
			,@createdBy = CreatedBy
			,@lastModified = LastModified
			,@lastModifiedBy = LastModifiedBy
			,@login = Login
			,@refreshId = RefreshId
	FROM deleted i;
    INSERT INTO [dbo].[UserLoginAudits]
           ([RecordId]
           ,[PasswordHash]
		   ,[LockoutEnabled]
		   ,[LockoutEnd]
		   ,[AccessFailedCount]
		   ,[isAdmin]
           ,[Created]
           ,[CreatedBy]
           ,[LastModified]
           ,[LastModifiedBy]
           ,[ActionDate]
		   ,[Action]
		   ,[Login]
		   ,[RefreshId])
     VALUES
           (@recordId
           ,@passwordHash
		   ,@lockoutEnabled
		   ,@lockoutEnd
		   ,@accessFailedCount
		   ,@isAdmin
           ,@created
           ,@createdBy
           ,@lastModified
           ,@lastModifiedBy
           ,CURRENT_TIMESTAMP
		   ,@action
		   ,@login
		   ,@refreshId)
end
GO
