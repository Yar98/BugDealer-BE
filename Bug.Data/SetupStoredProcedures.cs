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
            var sp3 = @"CREATE PROCEDURE [dbo].[UpdateAprAfterDeleteRole]
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

                    SELECT * FROM [Issue] as i
                    JOIN [Project] as p ON i.ProjectId = p.Id AND p.Id = @project
                    WHERE p.Code + CAST(i.NumberCode AS NVARCHAR) LIKE @search
                    ORDER BY 
						CASE @order WHEN 'id' THEN i.Id END,
						CASE @order WHEN 'code' THEN i.NumberCode END,
						CASE @order WHEN 'code_desc' THEN i.NumberCode END DESC
                END";
            migrationBuilder.Sql(sp1);
            migrationBuilder.Sql(sp2);
            migrationBuilder.Sql(sp3);
            migrationBuilder.Sql(sp4);
            migrationBuilder.Sql(sp5);
            migrationBuilder.Sql(sp6);
            migrationBuilder.Sql(sp7);
        }
    }
}
