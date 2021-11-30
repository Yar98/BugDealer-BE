using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Bug.Data.Migrations
{
    public partial class Createdb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Customtype",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customtype", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Field",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Field", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Permission",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Action = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permission", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Priority",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Icon = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Priority", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Template",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Template", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Timezone",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GmtOffset = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CountryCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CountryName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Timezone", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tag",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tag", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tag_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CustomtypeField",
                columns: table => new
                {
                    CustomtypesId = table.Column<int>(type: "int", nullable: false),
                    FieldsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomtypeField", x => new { x.CustomtypesId, x.FieldsId });
                    table.ForeignKey(
                        name: "FK_CustomtypeField_Customtype_CustomtypesId",
                        column: x => x.CustomtypesId,
                        principalTable: "Customtype",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CustomtypeField_Field_FieldsId",
                        column: x => x.FieldsId,
                        principalTable: "Field",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Account",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Language = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ImageUri = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VerifyEmail = table.Column<bool>(type: "bit", nullable: false),
                    TimezoneId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TimezoneId1 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Account", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Account_Timezone_TimezoneId1",
                        column: x => x.TimezoneId1,
                        principalTable: "Timezone",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AccountCustomtype",
                columns: table => new
                {
                    AccountsId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CustomtypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountCustomtype", x => new { x.AccountsId, x.CustomtypeId });
                    table.ForeignKey(
                        name: "FK_AccountCustomtype_Account_AccountsId",
                        column: x => x.AccountsId,
                        principalTable: "Account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AccountCustomtype_Customtype_CustomtypeId",
                        column: x => x.CustomtypeId,
                        principalTable: "Customtype",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Project",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    EndDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    RecentDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AvatarUri = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    DefaultAssigneeId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatorId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    TemplateId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Project", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Project_Account_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "Account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Project_Account_DefaultAssigneeId",
                        column: x => x.DefaultAssigneeId,
                        principalTable: "Account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Project_Template_TemplateId",
                        column: x => x.TemplateId,
                        principalTable: "Template",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatorId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Role_Account_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "Account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Status",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Progress = table.Column<int>(type: "int", nullable: false),
                    Default = table.Column<int>(type: "int", nullable: false),
                    CreatorId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    TagId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Status", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Status_Account_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "Account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Status_Tag_TagId",
                        column: x => x.TagId,
                        principalTable: "Tag",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Worklog",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SpentTime = table.Column<int>(type: "int", nullable: false),
                    RemainTime = table.Column<int>(type: "int", nullable: false),
                    LogDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LoggerId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Worklog", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Worklog_Account_LoggerId",
                        column: x => x.LoggerId,
                        principalTable: "Account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AccountProject",
                columns: table => new
                {
                    AccountsId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProjectsId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountProject", x => new { x.AccountsId, x.ProjectsId });
                    table.ForeignKey(
                        name: "FK_AccountProject_Account_AccountsId",
                        column: x => x.AccountsId,
                        principalTable: "Account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AccountProject_Project_ProjectsId",
                        column: x => x.ProjectsId,
                        principalTable: "Project",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RelatorProject",
                columns: table => new
                {
                    RelateProjectsId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RelatorId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RelatorProject", x => new { x.RelateProjectsId, x.RelatorId });
                    table.ForeignKey(
                        name: "FK_RelatorProject_Account_RelatorId",
                        column: x => x.RelatorId,
                        principalTable: "Account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RelatorProject_Project_RelateProjectsId",
                        column: x => x.RelateProjectsId,
                        principalTable: "Project",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AccountRole",
                columns: table => new
                {
                    AccountsId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RolesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountRole", x => new { x.AccountsId, x.RolesId });
                    table.ForeignKey(
                        name: "FK_AccountRole_Account_AccountsId",
                        column: x => x.AccountsId,
                        principalTable: "Account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AccountRole_Role_RolesId",
                        column: x => x.RolesId,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PermissionRole",
                columns: table => new
                {
                    PermissionsId = table.Column<int>(type: "int", nullable: false),
                    RolesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PermissionRole", x => new { x.PermissionsId, x.RolesId });
                    table.ForeignKey(
                        name: "FK_PermissionRole_Permission_PermissionsId",
                        column: x => x.PermissionsId,
                        principalTable: "Permission",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PermissionRole_Role_RolesId",
                        column: x => x.RolesId,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjectRole",
                columns: table => new
                {
                    ProjectsId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RolesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectRole", x => new { x.ProjectsId, x.RolesId });
                    table.ForeignKey(
                        name: "FK_ProjectRole_Project_ProjectsId",
                        column: x => x.ProjectsId,
                        principalTable: "Project",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectRole_Role_RolesId",
                        column: x => x.RolesId,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Issue",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LogDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    DueDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    WorklogDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    OriginEstimateTime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RemainEstimateTime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Environment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StatusId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    PriorityId = table.Column<int>(type: "int", nullable: false),
                    ProjectId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ReporterId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    AssigneeId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Issue", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Issue_Account_AssigneeId",
                        column: x => x.AssigneeId,
                        principalTable: "Account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Issue_Account_ReporterId",
                        column: x => x.ReporterId,
                        principalTable: "Account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Issue_Priority_PriorityId",
                        column: x => x.PriorityId,
                        principalTable: "Priority",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Issue_Project_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Project",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Issue_Status_StatusId",
                        column: x => x.StatusId,
                        principalTable: "Status",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProjectStatus",
                columns: table => new
                {
                    ProjectsId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    StatusesId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectStatus", x => new { x.ProjectsId, x.StatusesId });
                    table.ForeignKey(
                        name: "FK_ProjectStatus_Project_ProjectsId",
                        column: x => x.ProjectsId,
                        principalTable: "Project",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectStatus_Status_StatusesId",
                        column: x => x.StatusesId,
                        principalTable: "Status",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Attachment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Uri = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IssueId = table.Column<string>(type: "nvarchar(100)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attachment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Attachment_Issue_IssueId",
                        column: x => x.IssueId,
                        principalTable: "Issue",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Comment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TimeLog = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IssueId = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    AccountId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comment_Account_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Comment_Issue_IssueId",
                        column: x => x.IssueId,
                        principalTable: "Issue",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Issuelog",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LogDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IssueId = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    ModifierId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    PreStatusId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ModStatusId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    PrePriorityId = table.Column<int>(type: "int", nullable: true),
                    ModPriorityId = table.Column<int>(type: "int", nullable: true),
                    TagId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Issuelog", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Issuelog_Account_ModifierId",
                        column: x => x.ModifierId,
                        principalTable: "Account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Issuelog_Issue_IssueId",
                        column: x => x.IssueId,
                        principalTable: "Issue",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Issuelog_Priority_ModPriorityId",
                        column: x => x.ModPriorityId,
                        principalTable: "Priority",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Issuelog_Priority_PrePriorityId",
                        column: x => x.PrePriorityId,
                        principalTable: "Priority",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Issuelog_Status_ModStatusId",
                        column: x => x.ModStatusId,
                        principalTable: "Status",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Issuelog_Status_PreStatusId",
                        column: x => x.PreStatusId,
                        principalTable: "Status",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Issuelog_Tag_TagId",
                        column: x => x.TagId,
                        principalTable: "Tag",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "IssueTag",
                columns: table => new
                {
                    IssuesId = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    TagsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IssueTag", x => new { x.IssuesId, x.TagsId });
                    table.ForeignKey(
                        name: "FK_IssueTag_Issue_IssuesId",
                        column: x => x.IssuesId,
                        principalTable: "Issue",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IssueTag_Tag_TagsId",
                        column: x => x.TagsId,
                        principalTable: "Tag",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Relation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TagId = table.Column<int>(type: "int", nullable: false),
                    FromIssueId = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    ToIssueId = table.Column<string>(type: "nvarchar(100)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Relation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Relation_Issue_FromIssueId",
                        column: x => x.FromIssueId,
                        principalTable: "Issue",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Relation_Issue_ToIssueId",
                        column: x => x.ToIssueId,
                        principalTable: "Issue",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Relation_Tag_TagId",
                        column: x => x.TagId,
                        principalTable: "Tag",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VoterIssue",
                columns: table => new
                {
                    VoteIssuesId = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    VoterId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VoterIssue", x => new { x.VoteIssuesId, x.VoterId });
                    table.ForeignKey(
                        name: "FK_VoterIssue_Account_VoterId",
                        column: x => x.VoterId,
                        principalTable: "Account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VoterIssue_Issue_VoteIssuesId",
                        column: x => x.VoteIssuesId,
                        principalTable: "Issue",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WatcherIssue",
                columns: table => new
                {
                    WatchIssuesId = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    WatcherId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WatcherIssue", x => new { x.WatchIssuesId, x.WatcherId });
                    table.ForeignKey(
                        name: "FK_WatcherIssue_Account_WatcherId",
                        column: x => x.WatcherId,
                        principalTable: "Account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WatcherIssue_Issue_WatchIssuesId",
                        column: x => x.WatchIssuesId,
                        principalTable: "Issue",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Account_TimezoneId1",
                table: "Account",
                column: "TimezoneId1");

            migrationBuilder.CreateIndex(
                name: "IX_AccountCustomtype_CustomtypeId",
                table: "AccountCustomtype",
                column: "CustomtypeId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountProject_ProjectsId",
                table: "AccountProject",
                column: "ProjectsId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountRole_RolesId",
                table: "AccountRole",
                column: "RolesId");

            migrationBuilder.CreateIndex(
                name: "IX_Attachment_IssueId",
                table: "Attachment",
                column: "IssueId");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_AccountId",
                table: "Comment",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_IssueId",
                table: "Comment",
                column: "IssueId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomtypeField_FieldsId",
                table: "CustomtypeField",
                column: "FieldsId");

            migrationBuilder.CreateIndex(
                name: "IX_Issue_AssigneeId",
                table: "Issue",
                column: "AssigneeId");

            migrationBuilder.CreateIndex(
                name: "IX_Issue_PriorityId",
                table: "Issue",
                column: "PriorityId");

            migrationBuilder.CreateIndex(
                name: "IX_Issue_ProjectId",
                table: "Issue",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Issue_ReporterId",
                table: "Issue",
                column: "ReporterId");

            migrationBuilder.CreateIndex(
                name: "IX_Issue_StatusId",
                table: "Issue",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Issuelog_IssueId",
                table: "Issuelog",
                column: "IssueId");

            migrationBuilder.CreateIndex(
                name: "IX_Issuelog_ModifierId",
                table: "Issuelog",
                column: "ModifierId");

            migrationBuilder.CreateIndex(
                name: "IX_Issuelog_ModPriorityId",
                table: "Issuelog",
                column: "ModPriorityId");

            migrationBuilder.CreateIndex(
                name: "IX_Issuelog_ModStatusId",
                table: "Issuelog",
                column: "ModStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Issuelog_PrePriorityId",
                table: "Issuelog",
                column: "PrePriorityId");

            migrationBuilder.CreateIndex(
                name: "IX_Issuelog_PreStatusId",
                table: "Issuelog",
                column: "PreStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Issuelog_TagId",
                table: "Issuelog",
                column: "TagId");

            migrationBuilder.CreateIndex(
                name: "IX_IssueTag_TagsId",
                table: "IssueTag",
                column: "TagsId");

            migrationBuilder.CreateIndex(
                name: "IX_PermissionRole_RolesId",
                table: "PermissionRole",
                column: "RolesId");

            migrationBuilder.CreateIndex(
                name: "IX_Project_CreatorId",
                table: "Project",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Project_DefaultAssigneeId",
                table: "Project",
                column: "DefaultAssigneeId");

            migrationBuilder.CreateIndex(
                name: "IX_Project_TemplateId",
                table: "Project",
                column: "TemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectRole_RolesId",
                table: "ProjectRole",
                column: "RolesId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectStatus_StatusesId",
                table: "ProjectStatus",
                column: "StatusesId");

            migrationBuilder.CreateIndex(
                name: "IX_Relation_FromIssueId",
                table: "Relation",
                column: "FromIssueId");

            migrationBuilder.CreateIndex(
                name: "IX_Relation_TagId",
                table: "Relation",
                column: "TagId");

            migrationBuilder.CreateIndex(
                name: "IX_Relation_ToIssueId",
                table: "Relation",
                column: "ToIssueId");

            migrationBuilder.CreateIndex(
                name: "IX_RelatorProject_RelatorId",
                table: "RelatorProject",
                column: "RelatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Role_CreatorId",
                table: "Role",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Status_CreatorId",
                table: "Status",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Status_TagId",
                table: "Status",
                column: "TagId");

            migrationBuilder.CreateIndex(
                name: "IX_Tag_CategoryId",
                table: "Tag",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_VoterIssue_VoterId",
                table: "VoterIssue",
                column: "VoterId");

            migrationBuilder.CreateIndex(
                name: "IX_WatcherIssue_WatcherId",
                table: "WatcherIssue",
                column: "WatcherId");

            migrationBuilder.CreateIndex(
                name: "IX_Worklog_LoggerId",
                table: "Worklog",
                column: "LoggerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountCustomtype");

            migrationBuilder.DropTable(
                name: "AccountProject");

            migrationBuilder.DropTable(
                name: "AccountRole");

            migrationBuilder.DropTable(
                name: "Attachment");

            migrationBuilder.DropTable(
                name: "Comment");

            migrationBuilder.DropTable(
                name: "CustomtypeField");

            migrationBuilder.DropTable(
                name: "Issuelog");

            migrationBuilder.DropTable(
                name: "IssueTag");

            migrationBuilder.DropTable(
                name: "PermissionRole");

            migrationBuilder.DropTable(
                name: "ProjectRole");

            migrationBuilder.DropTable(
                name: "ProjectStatus");

            migrationBuilder.DropTable(
                name: "Relation");

            migrationBuilder.DropTable(
                name: "RelatorProject");

            migrationBuilder.DropTable(
                name: "VoterIssue");

            migrationBuilder.DropTable(
                name: "WatcherIssue");

            migrationBuilder.DropTable(
                name: "Worklog");

            migrationBuilder.DropTable(
                name: "Customtype");

            migrationBuilder.DropTable(
                name: "Field");

            migrationBuilder.DropTable(
                name: "Permission");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "Issue");

            migrationBuilder.DropTable(
                name: "Priority");

            migrationBuilder.DropTable(
                name: "Project");

            migrationBuilder.DropTable(
                name: "Status");

            migrationBuilder.DropTable(
                name: "Template");

            migrationBuilder.DropTable(
                name: "Account");

            migrationBuilder.DropTable(
                name: "Tag");

            migrationBuilder.DropTable(
                name: "Timezone");

            migrationBuilder.DropTable(
                name: "Category");
        }
    }
}
