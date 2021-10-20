IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Account] (
    [Id] nvarchar(450) NOT NULL,
    [UserName] nvarchar(max) NOT NULL,
    [Password] nvarchar(max) NULL,
    [FirstName] nvarchar(max) NULL,
    [LastName] nvarchar(max) NULL,
    [Email] nvarchar(max) NULL,
    [CreatedDate] nvarchar(max) NULL,
    [ImageUri] nvarchar(max) NULL,
    CONSTRAINT [PK_Account] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Category] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    [Description] nvarchar(max) NULL,
    CONSTRAINT [PK_Category] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Label] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    [Description] nvarchar(max) NULL,
    CONSTRAINT [PK_Label] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Permission] (
    [Id] int NOT NULL IDENTITY,
    [Action] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Permission] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Priority] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    [Description] nvarchar(max) NULL,
    CONSTRAINT [PK_Priority] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Relation] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    [Description] nvarchar(max) NULL,
    CONSTRAINT [PK_Relation] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Role] (
    [Id] nvarchar(450) NOT NULL,
    [Name] nvarchar(max) NOT NULL,
    [Description] nvarchar(max) NULL,
    [MemberId] nvarchar(max) NULL,
    CONSTRAINT [PK_Role] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Status] (
    [Id] nvarchar(450) NOT NULL,
    [Name] nvarchar(max) NOT NULL,
    [Description] nvarchar(max) NULL,
    [Progress] int NOT NULL,
    [TagId] int NOT NULL,
    [AccountId] nvarchar(450) NULL,
    CONSTRAINT [PK_Status] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Status_Account_AccountId] FOREIGN KEY ([AccountId]) REFERENCES [Account] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [Workflow] (
    [Id] nvarchar(450) NOT NULL,
    [Name] nvarchar(max) NOT NULL,
    [AccountId] nvarchar(450) NULL,
    CONSTRAINT [PK_Workflow] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Workflow_Account_AccountId] FOREIGN KEY ([AccountId]) REFERENCES [Account] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [Worklog] (
    [Id] int NOT NULL IDENTITY,
    [SpentTime] int NOT NULL,
    [RemainTime] int NOT NULL,
    [LogDate] datetime2 NOT NULL,
    [LoggerId] nvarchar(450) NULL,
    CONSTRAINT [PK_Worklog] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Worklog_Account_LoggerId] FOREIGN KEY ([LoggerId]) REFERENCES [Account] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [AccountRole] (
    [AccountsId] nvarchar(450) NOT NULL,
    [RolesId] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_AccountRole] PRIMARY KEY ([AccountsId], [RolesId]),
    CONSTRAINT [FK_AccountRole_Account_AccountsId] FOREIGN KEY ([AccountsId]) REFERENCES [Account] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_AccountRole_Role_RolesId] FOREIGN KEY ([RolesId]) REFERENCES [Role] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [PermissionRole] (
    [PermissionsId] int NOT NULL,
    [RolesId] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_PermissionRole] PRIMARY KEY ([PermissionsId], [RolesId]),
    CONSTRAINT [FK_PermissionRole_Permission_PermissionsId] FOREIGN KEY ([PermissionsId]) REFERENCES [Permission] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_PermissionRole_Role_RolesId] FOREIGN KEY ([RolesId]) REFERENCES [Role] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Project] (
    [Id] nvarchar(450) NOT NULL,
    [Name] nvarchar(max) NOT NULL,
    [StartDate] datetime2 NOT NULL,
    [EndDate] datetime2 NOT NULL,
    [Description] nvarchar(max) NULL,
    [CreatorId] nvarchar(450) NULL,
    [WorkflowId] nvarchar(450) NULL,
    CONSTRAINT [PK_Project] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Project_Account_CreatorId] FOREIGN KEY ([CreatorId]) REFERENCES [Account] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Project_Workflow_WorkflowId] FOREIGN KEY ([WorkflowId]) REFERENCES [Workflow] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [StatusWorkflow] (
    [StatusesId] nvarchar(450) NOT NULL,
    [WorkflowsId] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_StatusWorkflow] PRIMARY KEY ([StatusesId], [WorkflowsId]),
    CONSTRAINT [FK_StatusWorkflow_Status_StatusesId] FOREIGN KEY ([StatusesId]) REFERENCES [Status] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_StatusWorkflow_Workflow_WorkflowsId] FOREIGN KEY ([WorkflowsId]) REFERENCES [Workflow] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Transition] (
    [Id] nvarchar(450) NOT NULL,
    [WorkflowId] nvarchar(450) NULL,
    [StartStatusId] nvarchar(450) NULL,
    [EndStatusId] nvarchar(450) NULL,
    CONSTRAINT [PK_Transition] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Transition_Status_EndStatusId] FOREIGN KEY ([EndStatusId]) REFERENCES [Status] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Transition_Status_StartStatusId] FOREIGN KEY ([StartStatusId]) REFERENCES [Status] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Transition_Workflow_WorkflowId] FOREIGN KEY ([WorkflowId]) REFERENCES [Workflow] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [AccountProject] (
    [AccountsId] nvarchar(450) NOT NULL,
    [ProjectsId] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_AccountProject] PRIMARY KEY ([AccountsId], [ProjectsId]),
    CONSTRAINT [FK_AccountProject_Account_AccountsId] FOREIGN KEY ([AccountsId]) REFERENCES [Account] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_AccountProject_Project_ProjectsId] FOREIGN KEY ([ProjectsId]) REFERENCES [Project] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Issue] (
    [Id] nvarchar(450) NOT NULL,
    [Title] nvarchar(max) NOT NULL,
    [Description] nvarchar(max) NULL,
    [TimeLog] datetime2 NOT NULL,
    [CreatedDate] datetime2 NOT NULL,
    [DueDate] datetime2 NOT NULL,
    [OriginEstimateTime] nvarchar(max) NULL,
    [RemainEstimateTime] nvarchar(max) NULL,
    [UriAttachment] nvarchar(max) NULL,
    [Environment] nvarchar(max) NULL,
    [LinkedIssueId] nvarchar(max) NULL,
    [RelationId] int NOT NULL,
    [LinkedStatusId] nvarchar(450) NULL,
    [PriorityId] int NOT NULL,
    [ProjectId] nvarchar(450) NULL,
    [ReporterId] nvarchar(450) NULL,
    [AsigneeId] nvarchar(450) NULL,
    CONSTRAINT [PK_Issue] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Issue_Account_AsigneeId] FOREIGN KEY ([AsigneeId]) REFERENCES [Account] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Issue_Account_ReporterId] FOREIGN KEY ([ReporterId]) REFERENCES [Account] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Issue_Priority_PriorityId] FOREIGN KEY ([PriorityId]) REFERENCES [Priority] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Issue_Project_ProjectId] FOREIGN KEY ([ProjectId]) REFERENCES [Project] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Issue_Relation_RelationId] FOREIGN KEY ([RelationId]) REFERENCES [Relation] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Issue_Status_LinkedStatusId] FOREIGN KEY ([LinkedStatusId]) REFERENCES [Status] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [ProjectRole] (
    [ProjectsId] nvarchar(450) NOT NULL,
    [RolesId] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_ProjectRole] PRIMARY KEY ([ProjectsId], [RolesId]),
    CONSTRAINT [FK_ProjectRole_Project_ProjectsId] FOREIGN KEY ([ProjectsId]) REFERENCES [Project] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_ProjectRole_Role_RolesId] FOREIGN KEY ([RolesId]) REFERENCES [Role] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Tag] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    [Description] nvarchar(max) NULL,
    [CategoryId] int NOT NULL,
    [ProjectId] nvarchar(450) NULL,
    CONSTRAINT [PK_Tag] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Tag_Category_CategoryId] FOREIGN KEY ([CategoryId]) REFERENCES [Category] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Tag_Project_ProjectId] FOREIGN KEY ([ProjectId]) REFERENCES [Project] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [RoleTransition] (
    [RolesId] nvarchar(450) NOT NULL,
    [TransitionsId] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_RoleTransition] PRIMARY KEY ([RolesId], [TransitionsId]),
    CONSTRAINT [FK_RoleTransition_Role_RolesId] FOREIGN KEY ([RolesId]) REFERENCES [Role] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_RoleTransition_Transition_TransitionsId] FOREIGN KEY ([TransitionsId]) REFERENCES [Transition] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Comment] (
    [Id] nvarchar(450) NOT NULL,
    [Content] nvarchar(max) NOT NULL,
    [TimeLog] datetime2 NOT NULL,
    [IssueId] nvarchar(450) NULL,
    [AccountId] nvarchar(450) NULL,
    CONSTRAINT [PK_Comment] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Comment_Account_AccountId] FOREIGN KEY ([AccountId]) REFERENCES [Account] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Comment_Issue_IssueId] FOREIGN KEY ([IssueId]) REFERENCES [Issue] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [Issuelog] (
    [TimeLog] datetime2 NOT NULL,
    [IssueId] nvarchar(450) NULL,
    [ModifierId] nvarchar(450) NULL,
    [PreStatusId] nvarchar(450) NULL,
    [ModStatusId] nvarchar(450) NULL,
    CONSTRAINT [FK_Issuelog_Account_ModifierId] FOREIGN KEY ([ModifierId]) REFERENCES [Account] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Issuelog_Issue_IssueId] FOREIGN KEY ([IssueId]) REFERENCES [Issue] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Issuelog_Status_ModStatusId] FOREIGN KEY ([ModStatusId]) REFERENCES [Status] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Issuelog_Status_PreStatusId] FOREIGN KEY ([PreStatusId]) REFERENCES [Status] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [Vote] (
    [AccountId] nvarchar(450) NULL,
    [IssueId] nvarchar(450) NULL,
    CONSTRAINT [FK_Vote_Account_AccountId] FOREIGN KEY ([AccountId]) REFERENCES [Account] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Vote_Issue_IssueId] FOREIGN KEY ([IssueId]) REFERENCES [Issue] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [Watcher] (
    [AccountId] nvarchar(450) NULL,
    [IssueId] nvarchar(450) NULL,
    CONSTRAINT [FK_Watcher_Account_AccountId] FOREIGN KEY ([AccountId]) REFERENCES [Account] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Watcher_Issue_IssueId] FOREIGN KEY ([IssueId]) REFERENCES [Issue] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [IssueTag] (
    [IssuesId] nvarchar(450) NOT NULL,
    [TagsId] int NOT NULL,
    CONSTRAINT [PK_IssueTag] PRIMARY KEY ([IssuesId], [TagsId]),
    CONSTRAINT [FK_IssueTag_Issue_IssuesId] FOREIGN KEY ([IssuesId]) REFERENCES [Issue] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_IssueTag_Tag_TagsId] FOREIGN KEY ([TagsId]) REFERENCES [Tag] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [StatusTag] (
    [StatusesId] nvarchar(450) NOT NULL,
    [TagsId] int NOT NULL,
    CONSTRAINT [PK_StatusTag] PRIMARY KEY ([StatusesId], [TagsId]),
    CONSTRAINT [FK_StatusTag_Status_StatusesId] FOREIGN KEY ([StatusesId]) REFERENCES [Status] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_StatusTag_Tag_TagsId] FOREIGN KEY ([TagsId]) REFERENCES [Tag] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_AccountProject_ProjectsId] ON [AccountProject] ([ProjectsId]);
GO

CREATE INDEX [IX_AccountRole_RolesId] ON [AccountRole] ([RolesId]);
GO

CREATE INDEX [IX_Comment_AccountId] ON [Comment] ([AccountId]);
GO

CREATE INDEX [IX_Comment_IssueId] ON [Comment] ([IssueId]);
GO

CREATE INDEX [IX_Issue_AsigneeId] ON [Issue] ([AsigneeId]);
GO

CREATE INDEX [IX_Issue_LinkedStatusId] ON [Issue] ([LinkedStatusId]);
GO

CREATE INDEX [IX_Issue_PriorityId] ON [Issue] ([PriorityId]);
GO

CREATE INDEX [IX_Issue_ProjectId] ON [Issue] ([ProjectId]);
GO

CREATE INDEX [IX_Issue_RelationId] ON [Issue] ([RelationId]);
GO

CREATE INDEX [IX_Issue_ReporterId] ON [Issue] ([ReporterId]);
GO

CREATE INDEX [IX_Issuelog_IssueId] ON [Issuelog] ([IssueId]);
GO

CREATE INDEX [IX_Issuelog_ModifierId] ON [Issuelog] ([ModifierId]);
GO

CREATE INDEX [IX_Issuelog_ModStatusId] ON [Issuelog] ([ModStatusId]);
GO

CREATE INDEX [IX_Issuelog_PreStatusId] ON [Issuelog] ([PreStatusId]);
GO

CREATE INDEX [IX_IssueTag_TagsId] ON [IssueTag] ([TagsId]);
GO

CREATE INDEX [IX_PermissionRole_RolesId] ON [PermissionRole] ([RolesId]);
GO

CREATE UNIQUE INDEX [IX_Project_CreatorId] ON [Project] ([CreatorId]) WHERE [CreatorId] IS NOT NULL;
GO

CREATE INDEX [IX_Project_WorkflowId] ON [Project] ([WorkflowId]);
GO

CREATE INDEX [IX_ProjectRole_RolesId] ON [ProjectRole] ([RolesId]);
GO

CREATE INDEX [IX_RoleTransition_TransitionsId] ON [RoleTransition] ([TransitionsId]);
GO

CREATE INDEX [IX_Status_AccountId] ON [Status] ([AccountId]);
GO

CREATE INDEX [IX_StatusTag_TagsId] ON [StatusTag] ([TagsId]);
GO

CREATE INDEX [IX_StatusWorkflow_WorkflowsId] ON [StatusWorkflow] ([WorkflowsId]);
GO

CREATE INDEX [IX_Tag_CategoryId] ON [Tag] ([CategoryId]);
GO

CREATE INDEX [IX_Tag_ProjectId] ON [Tag] ([ProjectId]);
GO

CREATE INDEX [IX_Transition_EndStatusId] ON [Transition] ([EndStatusId]);
GO

CREATE INDEX [IX_Transition_StartStatusId] ON [Transition] ([StartStatusId]);
GO

CREATE INDEX [IX_Transition_WorkflowId] ON [Transition] ([WorkflowId]);
GO

CREATE INDEX [IX_Vote_AccountId] ON [Vote] ([AccountId]);
GO

CREATE INDEX [IX_Vote_IssueId] ON [Vote] ([IssueId]);
GO

CREATE INDEX [IX_Watcher_AccountId] ON [Watcher] ([AccountId]);
GO

CREATE INDEX [IX_Watcher_IssueId] ON [Watcher] ([IssueId]);
GO

CREATE INDEX [IX_Workflow_AccountId] ON [Workflow] ([AccountId]);
GO

CREATE INDEX [IX_Worklog_LoggerId] ON [Worklog] ([LoggerId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20211018165116_create-db', N'5.0.10');
GO

COMMIT;
GO

