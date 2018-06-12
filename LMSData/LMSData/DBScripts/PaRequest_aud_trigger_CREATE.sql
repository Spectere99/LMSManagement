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
create trigger PaRequest_aud_trigger
on PaRequests
after UPDATE, INSERT, DELETE
as
declare	 @recordId				int
		,@user					varchar(20)
		,@action				varchar(20)
        ,@patientName			nvarchar(max)
        ,@doctorName			nvarchar(max)
        ,@drugName				nvarchar(max)
        ,@submitted				datetime
        ,@approval				datetime
        ,@denial				datetime
        ,@approvalDocumentUrl	nvarchar(max)
        ,@note					nvarchar(max)
        ,@assignedTo			nvarchar(max)
        ,@archived				bit
        ,@insuranceCompany_Id	int
        ,@fileUploadLogId		int
        ,@status				int
        ,@assigned				datetime
		,@created				datetime
        ,@createdBy				nvarchar(max)
        ,@lastModified			datetime
        ,@lastModifiedBy		nvarchar(max)
        ,@actionDate			datetime
		,@priority				bit
		,@completed				bit
		,@completedTimeStamp	datetime
		,@billingStatus			int
		,@nonMeds				bit;

if exists(SELECT * from inserted) and exists (SELECT * from deleted)
begin
	SET NOCOUNT ON;
    SET @action = 'UPDATE';
    SET @user = SYSTEM_USER;
    SELECT	 @recordId = Id
			,@patientName = PatientName
			,@doctorName = DoctorName
			,@drugName = DrugName
			,@submitted = Submitted
			,@approval = Approval
			,@denial = Denial
			,@approvalDocumentUrl = ApprovalDocumentUrl
			,@note = Note
			,@assignedTo = AssignedTo
			,@archived = Archived
			,@insuranceCompany_Id = InsuranceCompany_Id
			,@fileUploadLogId = FileUploadLogId
			,@status = Status
			,@assigned = Assigned
			,@created = Created
			,@createdBy = CreatedBy
			,@lastModified = LastModified
			,@lastModifiedBy = LastModifiedBy
			,@priority = Priority
			,@completed = Completed
			,@completedTimeStamp = CompletedTimeStamp
			,@billingStatus = BillingStatus
			,@nonMeds = NonMeds
	FROM inserted i;
    INSERT INTO [dbo].[PaRequestAudits]
           ([RecordId]
           ,[PatientName]
           ,[DoctorName]
           ,[DrugName]
           ,[Submitted]
           ,[Approval]
           ,[Denial]
           ,[ApprovalDocumentUrl]
           ,[Note]
           ,[AssignedTo]
           ,[Archived]
           ,[InsuranceCompany_Id]
           ,[Action]
           ,[FileUploadLogId]
           ,[Status]
           ,[Assigned]
           ,[Created]
           ,[CreatedBy]
           ,[LastModified]
           ,[LastModifiedBy]
           ,[ActionDate]
		   ,[Priority]
		   ,[Completed]
		   ,[CompletedTimeStamp]
		   ,[BillingStatus]
		   ,[NonMeds])
     VALUES
           (@recordId
           ,@patientName
           ,@doctorName
           ,@drugName
           ,@submitted
           ,@approval
           ,@denial
           ,@approvalDocumentUrl
           ,@note
           ,@assignedTo
           ,@archived
           ,@insuranceCompany_Id
           ,@action
           ,@fileUploadLogId
           ,@status
           ,@assigned
           ,@created
           ,@createdBy
           ,@lastModified
           ,@lastModifiedBy
           ,CURRENT_TIMESTAMP
		   ,@priority
		   ,@completed
		   ,@completedTimeStamp
		   ,@billingStatus 
		   ,@nonMeds)
end

If exists (Select * from inserted) and not exists(Select * from deleted)
begin
	SET NOCOUNT ON;
    SET @action = 'INSERT';
    SET @user = SYSTEM_USER;
    SELECT	 @recordId = Id
			,@patientName = PatientName
			,@doctorName = DoctorName
			,@drugName = DrugName
			,@submitted = Submitted
			,@approval = Approval
			,@denial = Denial
			,@approvalDocumentUrl = ApprovalDocumentUrl
			,@note = Note
			,@assignedTo = AssignedTo
			,@archived = Archived
			,@insuranceCompany_Id = InsuranceCompany_Id
			,@fileUploadLogId = FileUploadLogId
			,@status = Status
			,@assigned = Assigned
			,@created = Created
			,@createdBy = CreatedBy
			,@lastModified = LastModified
			,@lastModifiedBy = LastModifiedBy
			,@priority = Priority
			,@completed = Completed
			,@completedTimeStamp = CompletedTimeStamp
			,@billingStatus = BillingStatus
			,@nonMeds = NonMeds
	FROM inserted i;
    INSERT INTO [dbo].[PaRequestAudits]
           ([RecordId]
           ,[PatientName]
           ,[DoctorName]
           ,[DrugName]
           ,[Submitted]
           ,[Approval]
           ,[Denial]
           ,[ApprovalDocumentUrl]
           ,[Note]
           ,[AssignedTo]
           ,[Archived]
           ,[InsuranceCompany_Id]
           ,[Action]
           ,[FileUploadLogId]
           ,[Status]
           ,[Assigned]
           ,[Created]
           ,[CreatedBy]
           ,[LastModified]
           ,[LastModifiedBy]
           ,[ActionDate]
		   ,[Priority]
		   ,[Completed]
		   ,[CompletedTimeStamp]
		   ,[BillingStatus]
		   ,[NonMeds])
     VALUES
           (@recordId
           ,@patientName
           ,@doctorName
           ,@drugName
           ,@submitted
           ,@approval
           ,@denial
           ,@approvalDocumentUrl
           ,@note
           ,@assignedTo
           ,@archived
           ,@insuranceCompany_Id
           ,@action
           ,@fileUploadLogId
           ,@status
           ,@assigned
           ,@created
           ,@createdBy
           ,@lastModified
           ,@lastModifiedBy
           ,CURRENT_TIMESTAMP
		   ,@priority
		   ,@completed
		   ,@completedTimeStamp
		   ,@billingStatus
		   ,@nonMeds)
end

If exists(select * from deleted) and not exists(Select * from inserted)
begin
	SET NOCOUNT ON; 
    SET @action = 'DELETE';
    SET @user = SYSTEM_USER;
    SELECT	 @recordId = Id
			,@patientName = PatientName
			,@doctorName = DoctorName
			,@drugName = DrugName
			,@submitted = Submitted
			,@approval = Approval
			,@denial = Denial
			,@approvalDocumentUrl = ApprovalDocumentUrl
			,@note = Note
			,@assignedTo = AssignedTo
			,@archived = Archived
			,@insuranceCompany_Id = InsuranceCompany_Id
			,@fileUploadLogId = FileUploadLogId
			,@status = Status
			,@assigned = Assigned
			,@created = Created
			,@createdBy = CreatedBy
			,@lastModified = LastModified
			,@lastModifiedBy = LastModifiedBy
			,@priority = Priority
			,@completed = Completed
			,@completedTimeStamp = CompletedTimeStamp
			,@billingStatus = BillingStatus
			,@nonMeds = NonMeds
	FROM deleted i;
    INSERT INTO [dbo].[PaRequestAudits]
           ([RecordId]
           ,[PatientName]
           ,[DoctorName]
           ,[DrugName]
           ,[Submitted]
           ,[Approval]
           ,[Denial]
           ,[ApprovalDocumentUrl]
           ,[Note]
           ,[AssignedTo]
           ,[Archived]
           ,[InsuranceCompany_Id]
           ,[Action]
           ,[FileUploadLogId]
           ,[Status]
           ,[Assigned]
           ,[Created]
           ,[CreatedBy]
           ,[LastModified]
           ,[LastModifiedBy]
           ,[ActionDate]
		   ,[Priority]
		   ,[Completed]
		   ,[CompletedTimeStamp]
		   ,[BillingStatus]
		   ,[NonMeds])
     VALUES
           (@recordId
           ,@patientName
           ,@doctorName
           ,@drugName
           ,@submitted
           ,@approval
           ,@denial
           ,@approvalDocumentUrl
           ,@note
           ,@assignedTo
           ,@archived
           ,@insuranceCompany_Id
           ,@action
           ,@fileUploadLogId
           ,@status
           ,@assigned
           ,@created
           ,@createdBy
           ,@lastModified
           ,@lastModifiedBy
           ,CURRENT_TIMESTAMP
		   ,@priority
		   ,@completed
		   ,@completedTimeStamp
		   ,@billingStatus
		   ,@nonMeds )
end
GO
