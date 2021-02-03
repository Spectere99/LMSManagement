SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Flowers, Robert
-- Create date: 2/2/2021
-- Description:	Implements the Auditing Trigger functions
--					For the PaRequestNotes Table
-- =============================================
create trigger PaRequestNote_aud_trigger
on PaRequests
after UPDATE, INSERT, DELETE
as
declare	 @recordId				int
		,@user					varchar(20)
		,@action				varchar(20)
        ,@isPublic  			bit
        ,@paRequestId           int
        ,@NoteText              nvarchar(max)
        ,@archived				bit
		,@created				datetime
        ,@createdBy				nvarchar(max)
        ,@lastModified			datetime
        ,@lastModifiedBy		nvarchar(max)
        ,@actionDate			datetime;
		

if exists(SELECT * from inserted) and exists (SELECT * from deleted)
begin
	SET NOCOUNT ON;
    SET @action = 'UPDATE';
    SET @user = SYSTEM_USER;
    SELECT	 @recordId = Id								
            ,@isPublic = IsPublic			
            ,@paRequestId = PaRequestId           
            ,@noteText = NoteText              
            ,@archived = Archived				
		    ,@created = Created				
            ,@createdBy	= CreatedBy			
            ,@lastModified = LastModified			
            ,@lastModifiedBy = LastModifiedBy					
	FROM inserted i;
    INSERT INTO [dbo].[PaRequestNoteAudits]
           ([RecordId]
           ,[PaRequestId]
           ,[NoteText]
           ,[IsPublic]
           ,[Archived]
           ,[Action]
           ,[Created]
           ,[CreatedBy]
           ,[LastModified]
           ,[LastModifiedBy]
           ,[ActionDate])
     VALUES
           (@recordId
           ,@paRequestId
           ,@noteText
           ,@isPublic
           ,@archived
           ,@action
           ,@created
           ,@createdBy
           ,@lastModified
           ,@lastModifiedBy
           ,CURRENT_TIMESTAMP)
end

If exists (Select * from inserted) and not exists(Select * from deleted)
begin
	SET NOCOUNT ON;
    SET @action = 'INSERT';
    SET @user = SYSTEM_USER;
    SELECT	 @recordId = Id								
            ,@isPublic = IsPublic			
            ,@paRequestId = PaRequestId           
            ,@noteText = NoteText              
            ,@archived = Archived				
		    ,@created = Created				
            ,@createdBy	= CreatedBy			
            ,@lastModified = LastModified			
            ,@lastModifiedBy = LastModifiedBy					
	FROM inserted i;
    INSERT INTO [dbo].[PaRequestNoteAudits]
           ([RecordId]
           ,[PaRequestId]
           ,[NoteText]
           ,[IsPublic]
           ,[Archived]
           ,[Action]
           ,[Created]
           ,[CreatedBy]
           ,[LastModified]
           ,[LastModifiedBy]
           ,[ActionDate])
     VALUES
           (@recordId
           ,@paRequestId
           ,@noteText
           ,@isPublic
           ,@archived
           ,@action
           ,@created
           ,@createdBy
           ,@lastModified
           ,@lastModifiedBy
           ,CURRENT_TIMESTAMP)
end

If exists(select * from deleted) and not exists(Select * from inserted)
begin
	SET NOCOUNT ON; 
    SET @action = 'DELETE';
    SET @user = SYSTEM_USER;
    SELECT	 @recordId = Id								
            ,@isPublic = IsPublic			
            ,@paRequestId = PaRequestId           
            ,@noteText = NoteText              
            ,@archived = Archived				
		    ,@created = Created				
            ,@createdBy	= CreatedBy			
            ,@lastModified = LastModified			
            ,@lastModifiedBy = LastModifiedBy					
	FROM inserted i;
    INSERT INTO [dbo].[PaRequestNoteAudits]
           ([RecordId]
           ,[PaRequestId]
           ,[NoteText]
           ,[IsPublic]
           ,[Archived]
           ,[Action]
           ,[Created]
           ,[CreatedBy]
           ,[LastModified]
           ,[LastModifiedBy]
           ,[ActionDate])
     VALUES
           (@recordId
           ,@paRequestId
           ,@noteText
           ,@isPublic
           ,@archived
           ,@action
           ,@created
           ,@createdBy
           ,@lastModified
           ,@lastModifiedBy
           ,CURRENT_TIMESTAMP)
end
GO
