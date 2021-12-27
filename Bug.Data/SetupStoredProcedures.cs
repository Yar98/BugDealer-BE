using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bug.Data
{
    public static class SetupStoredProcedures
    {
        public static void Setup(MigrationBuilder migrationBuilder)
        {
            var sp1 = @"CREATE PROCEDURE [dbo].[UpdateAccountsHaveDumbRole]
                    @list NVARCHAR(MAX),
                    @pro NVARCHAR(MAX),
					@role int
                AS
                BEGIN				
				    SET NOCOUNT ON;

					INSERT INTO [AccountProjectRole]
					SELECT a.Id, @pro, @role 
                    FROM Account as a JOIN [AccountProjectRole] as apr
					ON a.Id = apr.AccountId AND apr.ProjectId = @pro
					WHERE a.Id NOT IN 
                        (SELECT AccountId 
                        FROM [AccountProjectRole] as apr
						INNER JOIN Account as a ON apr.AccountId = a.Id
						WHERE apr.ProjectId = @pro AND 
							(apr.RoleId IN (SELECT VALUE FROM STRING_SPLIT(@list,',')) OR 
							apr.RoleId = @role))
					DELETE FROM [AccountProjectRole]
                    WHERE ProjectId = @pro AND RoleId NOT IN 
                        (SELECT VALUE FROM STRING_SPLIT(@list,','))
                END";
            var sp2 = @"CREATE PROCEDURE [dbo].[UpdateIssuesHaveDumbStatus]
                    @list NVARCHAR(MAX)
                AS
                BEGIN
                    SET NOCOUNT ON;

                    UPDATE i 
                    SET StatusId = 
                        (SELECT p.DefaultStatusId 
                        FROM Project AS p 
                        WHERE p.Id = i.ProjectId)
					FROM Issue as i
                    WHERE Id IN 
                        (SELECT i.Id FROM Issue AS i WHERE i.StatusId IN 
                            (SELECT VALUE FROM STRING_SPLIT(@list,',')))
                END";
            var sp3 = @"CREATE PROCEDURE [dbo].[UpdateAprBeforeDeleteRole]
                    @role int
                AS
                BEGIN
                    SET NOCOUNT ON;
                    
                    Update apr
                    SET RoleId = (SELECT p.DefaultRoleId FROM Project As p WHERE p.Id = apr.ProjectId)
                    FROM [AccountProjectRole] as apr
                    WHERE apr.RoleId = @role
                END";
            var sp4 = @"CREATE PROCEDURE [dbo].[DeleteMemberFromProject]
                    @project NVARCHAR(MAX),
                    @user NVARCHAR(MAX)
                AS
                BEGIN
                    SET NOCOUNT ON;
                    
                    DELETE FROM [AccountProjectRole]
                    WHERE ProjectId = @project AND AccountId = @user
                END";
            var sp5 = @"CREATE PROCEDURE [dbo].[DeleteRelationByIssue]
                    @issue NVARCHAR(MAX)
                AS
                BEGIN
                    SET NOCOUNT ON;
                    
                    DELETE FROM [Relation]
                    WHERE ToIssueId = @issue OR FromIssueId = @issue
                END";
            var sp6 = @"CREATE PROCEDURE [dbo].[GetActiveFieldsByAccountId]
                    @account NVARCHAR(MAX)
                AS
                BEGIN
                    SET NOCOUNT ON;

                    SELECT * FROM [Field] as f
					JOIN [AccountField] as af
					ON f.Id = af.FieldsId
					WHERE af.AccountsId = @account
                END";
            var sp7 = @"CREATE PROCEDURE [dbo].[GetSuggestIssues]
                    @project NVARCHAR(MAX),
                    @search NVARCHAR(MAX),
                    @order NVARCHAR(MAX)
                AS
                BEGIN
                    SET NOCOUNT ON;

                    SELECT i.* FROM [Issue] as i
                    JOIN [Project] as p ON i.ProjectId = p.Id AND p.Id = @project
                    WHERE p.Code + CAST(i.NumberCode AS NVARCHAR) LIKE @search OR i.Title Like @search
                    ORDER BY 
						CASE @order WHEN 'id' THEN i.Id END,
						CASE @order WHEN 'code' THEN i.NumberCode END,
						CASE @order WHEN 'code_desc' THEN i.NumberCode END DESC
                END";
            var sp8 = @"CREATE PROCEDURE [dbo].[UpdateTagsOfIssue]
					@issue NVARCHAR(MAX),
                    @list NVARCHAR(MAX)
                AS
                BEGIN
                    SET NOCOUNT ON;

					DELETE FROM IssueTag
					WHERE IssuesId = @issue 
                    
					INSERT INTO [IssueTag]
					SELECT @issue, CAST(VALUE AS int) FROM STRING_SPLIT(@list,',')
                END";
            var sp9 = @"CREATE PROCEDURE [dbo].[UpdateAttachmentsOfIssue]
					@issue NVARCHAR(MAX),
                    @listId NVARCHAR(MAX),
					@listUris NVARCHAR(MAX)
                AS
                BEGIN
                    SET NOCOUNT ON;

					DELETE FROM [Attachment]
					WHERE IssueId = @issue AND Id NOT IN (SELECT VALUE FROM STRING_SPLIT(@listId,','))

					IF @listUris != '' AND @listUris != NULL					
						INSERT INTO [Attachment]
						SELECT VALUE AS tes, @issue FROM STRING_SPLIT(@listUris,',');
                END";
            var sp10 = @"CREATE PROCEDURE [dbo].[GetIssuesByFilter]
                    @project NVARCHAR(MAX),
                    @search NVARCHAR(MAX),
                    @sortOrder NVARCHAR(MAX),
					@assignees NVARCHAR(MAX),
					@statuses NVARCHAR(MAX),
					@reporters NVARCHAR(MAX),
					@priorities NVARCHAR(MAX),
					@severities NVARCHAR(MAX)
                AS
                BEGIN
                    SET NOCOUNT ON;

					DECLARE @sql NVARCHAR(MAX)

					SET @assignees = TRIM(NULLIF(TRIM(@assignees),''))
					SET @statuses = TRIM(NULLIF(TRIM(@statuses),''))
					SET @reporters = TRIM(NULLIF(TRIM(@reporters),''))
					SET @priorities = TRIM(NULLIF(TRIM(@priorities),''))
					SET @severities = TRIM(NULLIF(TRIM(@severities),''))

					SET @sql = N'SELECT i.* FROM [Issue] as i
								JOIN [Project] as p ON i.ProjectId = p.Id AND p.Id = @project
								WHERE (p.Code + CAST(i.NumberCode AS NVARCHAR) LIKE @search OR i.Title Like @search)'
								

                    IF @assignees IS NOT NULL 
						SET @sql += N'AND i.AssigneeId IN 
										(CASE @assignees
											WHEN NULL THEN (SELECT i.AssigneeId FROM [Issue])
											ELSE (SELECT VALUE FROM STRING_SPLIT(@assignees,'',''))
										END)'
					IF @statuses IS NOT NULL
						SET @sql += N'AND i.StatusId IN 
										(CASE @statuses
											WHEN NULL THEN (SELECT i.StatusId FROM [Issue])
											ELSE (SELECT VALUE FROM STRING_SPLIT(@statuses,'',''))
										END)'
					IF @reporters IS NOT NULL
						SET @sql += N'AND i.ReporterId IN 
										(CASE @reporters
											WHEN NULL THEN (SELECT i.ReporterId FROM [Issue])
											ELSE (SELECT VALUE FROM STRING_SPLIT(@reporters,'',''))
										END)'
					IF @priorities IS NOT NULL
						SET @sql += N'AND i.PriorityId IN 
										(CASE @priorities
											WHEN NULL THEN (SELECT i.PriorityId FROM [Issue])
											ELSE (SELECT VALUE FROM STRING_SPLIT(@priorities,'',''))
										END)'
					IF @severities IS NOT NULL
						SET @sql += N'AND i.SeverityId IN 
										(CASE @severities
											WHEN NULL THEN (SELECT i.SeverityId FROM [Issue])
											ELSE (SELECT VALUE FROM STRING_SPLIT(@severities,'',''))
										END)'
					SET @sql += N'ORDER BY '
					IF @sortOrder = 'title'
						SET @sql += 'i.Title'
					ELSE IF @sortOrder = 'code'
						SET @sql += 'i.NumberCode'
					ELSE IF @sortOrder = 'id_desc'
						SET @sql += 'i.Id DESC'
					ELSE
						SET @sql += 'i.Id'

					EXECUTE sp_executesql @sql, 
					N'@project NVARCHAR(MAX), @search NVARCHAR(MAX), @assignees NVARCHAR(MAX),
					@statuses NVARCHAR(MAX), @reporters NVARCHAR(MAX), @priorities NVARCHAR(MAX),
					@severities NVARCHAR(MAX)',
					@project, @search, @assignees, @statuses, @reporters, @priorities, @severities
                END";
            var sp11 = @"CREATE PROCEDURE [dbo].[DeleteLogBeforeDelIssue]
					@issue NVARCHAR(MAX)
                AS
                BEGIN
                    SET NOCOUNT ON;

					DELETE FROM [Issuelog]
					WHERE NewToIssueId = @issue OR OldToIssueId = @issue
                END";
            var sp12 = @"CREATE PROCEDURE [dbo].[DeleteProject]
					@issue NVARCHAR(MAX),
                    @project NVARCHAR(MAX)
                AS
                BEGIN
                    SET NOCOUNT ON;

					DELETE FROM [Issuelog]
					WHERE NewToIssueId IN (SELECT il.NewToIssueId 
									FROM [Issuelog] AS il JOIN [Issue] AS i 
									ON il.NewToIssueId = i.Id AND i.ProjectId = @project) OR 
						OldToIssueId IN (SELECT il.OldToIssueId 
									FROM [Issuelog] AS il JOIN [Issue] AS i 
									ON il.OldToIssueId = i.Id AND i.ProjectId = @project) OR 
						IssueId IN (SELECT il.IssueId 
									FROM [Issuelog] AS il JOIN [Issue] AS i 
									ON il.IssueId = i.Id AND i.ProjectId = @project)

                    DELETE FROM [Project]
                    WHERE Id = @project
                END";
            migrationBuilder.Sql(sp1);
            migrationBuilder.Sql(sp2);
            migrationBuilder.Sql(sp3);
            migrationBuilder.Sql(sp4);
            migrationBuilder.Sql(sp5);
            migrationBuilder.Sql(sp6);
            migrationBuilder.Sql(sp7);
            migrationBuilder.Sql(sp8);
            migrationBuilder.Sql(sp9);
            migrationBuilder.Sql(sp10);
            migrationBuilder.Sql(sp11);
            migrationBuilder.Sql(sp12);
        }
    }
}
