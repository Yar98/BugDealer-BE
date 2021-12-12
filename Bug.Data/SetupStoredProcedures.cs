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
                    FROM Account as a INNER JOIN [AccountProjectRole] as apr
					ON a.Id = apr.AccountId
					WHERE a.Id NOT IN 
                        (SELECT AccountId 
                        FROM [AccountProjectRole] as apr
						INNER JOIN Account as a ON apr.AccountId = a.Id
						WHERE apr.RoleId = @role AND apr.ProjectId = @pro)
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
            migrationBuilder.Sql(sp1);
            migrationBuilder.Sql(sp2);
            migrationBuilder.Sql(sp3);
            migrationBuilder.Sql(sp4);
        }
    }
}
