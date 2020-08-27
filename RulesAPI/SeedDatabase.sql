

INSERT INTO [dbo].[Actions]
           ([Name]
           ,[Description]
           ,[Parameters]
           ,[ActionProcessorTypeName]
           ,[Version]
           ,[IsPublished]
           ,[DisplayName])
     VALUES
           ('InitiateDocusignSigningCeremony'
           ,'Will intitiate the Docusign signing ceremony'
           ,'{["DocusignTemplateName","Sender"]}'
           ,'InitiateDocusignSigningCeremonyAction'
           ,1
           ,1
           ,'Action to initiate the Docusign Signing Ceremony, with a specific Docusign Template')
declare @intAction1 int  = SCOPE_IDENTITY();

INSERT INTO [dbo].[Actions]
           ([Name]
           ,[Description]
           ,[Parameters]
           ,[ActionProcessorTypeName]
           ,[Version]
           ,[IsPublished]
           ,[DisplayName])
     VALUES
           ('SendEmail'
           ,'Will Send an email to the configured Recipient'
           ,'{["Recipient"]}'
           ,'SendEmailAction'
           ,1
           ,1
           ,'Action to send an email to the configured Recipient')
declare @intAction2 int  = SCOPE_IDENTITY();

INSERT INTO [dbo].[Conditions]
           ([Name]
           ,[Description]
           ,[Parameters]
           ,[ConditionProcessorTypeName]
           ,[Version]
           ,[IsPublished]
           ,[DisplayName])
     VALUES
           ('CheckForStatusChangeCondition'
           ,'Will check to determine if the was a status change'
           ,'{["Statuses"]}'
           ,'CheckForStatusChangeCondition'
           ,1
           ,1
           ,'Condition to check for status change, to one of the configured statuses')
declare @intCondition1 int  = SCOPE_IDENTITY();

INSERT INTO [dbo].[Conditions]
           ([Name]
           ,[Description]
           ,[Parameters]
           ,[ConditionProcessorTypeName]
           ,[Version]
           ,[IsPublished]
           ,[DisplayName])
     VALUES
           ('CheckForTagsAddedCondition'
           ,'Will check to determine if there were tags added'
           ,'{["tags"]}'
           ,'CheckForTagsAddedCondition'
           ,1
           ,1
           ,'Condition to check if a tag was added, to one of the configured tags')
declare @intCondition2 int  = SCOPE_IDENTITY();

INSERT INTO [dbo].[Rules]
           ([Name]
           ,[CreatedBy]
           ,[EntityType]
           ,[IsPublished]
           ,[DeliveryPartner]
           ,[ActionParameterValues]
           ,[ConditionParameterValues]
		   ,[ConditionBooleanOperatorType])
     VALUES
           ('Rule For Status change and Tag Change'
           ,'Mary'
           ,'SubsidizedPlacement'
           ,1
           ,'Magnet'
           ,''
           ,''
		   ,0)
declare @intRule1 int  = SCOPE_IDENTITY();

Insert into RuleActions  values (@intRule1,@intAction1)
Insert into RuleActions  values (@intRule1,@intAction2)

Insert into RuleConditions  values (@intRule1,@intCondition1)
Insert into RuleConditions  values (@intRule1,@intCondition2)

go