﻿// <auto-generated />
using System;
using Bug.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Bug.Data.Migrations
{
    [DbContext(typeof(BugContext))]
    partial class BugContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.11")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("AccountCustomtype", b =>
                {
                    b.Property<string>("AccountsId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("CustomtypeId")
                        .HasColumnType("int");

                    b.HasKey("AccountsId", "CustomtypeId");

                    b.HasIndex("CustomtypeId");

                    b.ToTable("AccountCustomtype");
                });

            modelBuilder.Entity("AccountIssue", b =>
                {
                    b.Property<string>("WatchIssuesId")
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("WatcherId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("WatchIssuesId", "WatcherId");

                    b.HasIndex("WatcherId");

                    b.ToTable("WatcherIssue");
                });

            modelBuilder.Entity("AccountIssue1", b =>
                {
                    b.Property<string>("VoteIssuesId")
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("VoterId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("VoteIssuesId", "VoterId");

                    b.HasIndex("VoterId");

                    b.ToTable("VoterIssue");
                });

            modelBuilder.Entity("AccountProject", b =>
                {
                    b.Property<string>("AccountsId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProjectsId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("AccountsId", "ProjectsId");

                    b.HasIndex("ProjectsId");

                    b.ToTable("AccountProject");
                });

            modelBuilder.Entity("AccountRole", b =>
                {
                    b.Property<string>("AccountsId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("RolesId")
                        .HasColumnType("int");

                    b.HasKey("AccountsId", "RolesId");

                    b.HasIndex("RolesId");

                    b.ToTable("AccountRole");
                });

            modelBuilder.Entity("Bug.Entities.Model.Account", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTimeOffset>("CreatedDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageUri")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Language")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TimezoneId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("TimezoneId1")
                        .HasColumnType("int");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("TimezoneId1");

                    b.ToTable("Account");
                });

            modelBuilder.Entity("Bug.Entities.Model.Attachment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("IssueId")
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Uri")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("IssueId");

                    b.ToTable("Attachment");
                });

            modelBuilder.Entity("Bug.Entities.Model.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Category");
                });

            modelBuilder.Entity("Bug.Entities.Model.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AccountId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IssueId")
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTimeOffset>("TimeLog")
                        .HasColumnType("datetimeoffset");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.HasIndex("IssueId");

                    b.ToTable("Comment");
                });

            modelBuilder.Entity("Bug.Entities.Model.Customtype", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Customtype");
                });

            modelBuilder.Entity("Bug.Entities.Model.Field", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Field");
                });

            modelBuilder.Entity("Bug.Entities.Model.Issue", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("AssigneeId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTimeOffset>("CreatedDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset>("DueDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Environment")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset>("LogDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("OriginEstimateTime")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PriorityId")
                        .HasColumnType("int");

                    b.Property<string>("ProjectId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RemainEstimateTime")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ReporterId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("StatusId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AssigneeId");

                    b.HasIndex("PriorityId");

                    b.HasIndex("ProjectId");

                    b.HasIndex("ReporterId");

                    b.HasIndex("StatusId");

                    b.ToTable("Issue");
                });

            modelBuilder.Entity("Bug.Entities.Model.Issuelog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("IssueId")
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTimeOffset>("LogDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<int?>("ModPriorityId")
                        .HasColumnType("int");

                    b.Property<string>("ModStatusId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int?>("ModePriorityId")
                        .HasColumnType("int");

                    b.Property<string>("ModifierId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int?>("PrePriorityId")
                        .HasColumnType("int");

                    b.Property<string>("PreStatusId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int?>("TagId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("IssueId");

                    b.HasIndex("ModPriorityId");

                    b.HasIndex("ModStatusId");

                    b.HasIndex("ModifierId");

                    b.HasIndex("PrePriorityId");

                    b.HasIndex("PreStatusId");

                    b.HasIndex("TagId");

                    b.ToTable("Issuelog");
                });

            modelBuilder.Entity("Bug.Entities.Model.Permission", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Action")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Permission");
                });

            modelBuilder.Entity("Bug.Entities.Model.Priority", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Priority");
                });

            modelBuilder.Entity("Bug.Entities.Model.Project", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("AvatarUri")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Code")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CreatorId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("DefaultAssigneeId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset>("EndDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset>("RecentDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset>("StartDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<int?>("TagId")
                        .HasColumnType("int");

                    b.Property<int>("TemplateId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CreatorId");

                    b.HasIndex("DefaultAssigneeId");

                    b.HasIndex("TagId");

                    b.HasIndex("TemplateId");

                    b.ToTable("Project");
                });

            modelBuilder.Entity("Bug.Entities.Model.Relation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FromIssueId")
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("TagId")
                        .HasColumnType("int");

                    b.Property<string>("ToIssueId")
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("FromIssueId");

                    b.HasIndex("TagId");

                    b.HasIndex("ToIssueId");

                    b.ToTable("Relation");
                });

            modelBuilder.Entity("Bug.Entities.Model.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CreatorId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CreatorId");

                    b.ToTable("Role");
                });

            modelBuilder.Entity("Bug.Entities.Model.Status", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CreatorId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Default")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Progress")
                        .HasColumnType("int");

                    b.Property<int>("TagId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CreatorId");

                    b.HasIndex("TagId");

                    b.ToTable("Status");
                });

            modelBuilder.Entity("Bug.Entities.Model.Tag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Color")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Tag");
                });

            modelBuilder.Entity("Bug.Entities.Model.Template", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Template");
                });

            modelBuilder.Entity("Bug.Entities.Model.Timezone", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CountryCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CountryName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("GmtOffset")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Timezone");
                });

            modelBuilder.Entity("Bug.Entities.Model.Worklog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTimeOffset>("LogDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("LoggerId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("RemainTime")
                        .HasColumnType("int");

                    b.Property<int>("SpentTime")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("LoggerId");

                    b.ToTable("Worklog");
                });

            modelBuilder.Entity("CustomtypeField", b =>
                {
                    b.Property<int>("CustomtypesId")
                        .HasColumnType("int");

                    b.Property<int>("FieldsId")
                        .HasColumnType("int");

                    b.HasKey("CustomtypesId", "FieldsId");

                    b.HasIndex("FieldsId");

                    b.ToTable("CustomtypeField");
                });

            modelBuilder.Entity("IssueTag", b =>
                {
                    b.Property<string>("IssuesId")
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("TagsId")
                        .HasColumnType("int");

                    b.HasKey("IssuesId", "TagsId");

                    b.HasIndex("TagsId");

                    b.ToTable("IssueTag");
                });

            modelBuilder.Entity("PermissionRole", b =>
                {
                    b.Property<int>("PermissionsId")
                        .HasColumnType("int");

                    b.Property<int>("RolesId")
                        .HasColumnType("int");

                    b.HasKey("PermissionsId", "RolesId");

                    b.HasIndex("RolesId");

                    b.ToTable("PermissionRole");
                });

            modelBuilder.Entity("ProjectRole", b =>
                {
                    b.Property<string>("ProjectsId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("RolesId")
                        .HasColumnType("int");

                    b.HasKey("ProjectsId", "RolesId");

                    b.HasIndex("RolesId");

                    b.ToTable("ProjectRole");
                });

            modelBuilder.Entity("ProjectStatus", b =>
                {
                    b.Property<string>("ProjectsId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("StatusesId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("ProjectsId", "StatusesId");

                    b.HasIndex("StatusesId");

                    b.ToTable("ProjectStatus");
                });

            modelBuilder.Entity("AccountCustomtype", b =>
                {
                    b.HasOne("Bug.Entities.Model.Account", null)
                        .WithMany()
                        .HasForeignKey("AccountsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Bug.Entities.Model.Customtype", null)
                        .WithMany()
                        .HasForeignKey("CustomtypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AccountIssue", b =>
                {
                    b.HasOne("Bug.Entities.Model.Issue", null)
                        .WithMany()
                        .HasForeignKey("WatchIssuesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Bug.Entities.Model.Account", null)
                        .WithMany()
                        .HasForeignKey("WatcherId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AccountIssue1", b =>
                {
                    b.HasOne("Bug.Entities.Model.Issue", null)
                        .WithMany()
                        .HasForeignKey("VoteIssuesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Bug.Entities.Model.Account", null)
                        .WithMany()
                        .HasForeignKey("VoterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AccountProject", b =>
                {
                    b.HasOne("Bug.Entities.Model.Account", null)
                        .WithMany()
                        .HasForeignKey("AccountsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Bug.Entities.Model.Project", null)
                        .WithMany()
                        .HasForeignKey("ProjectsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AccountRole", b =>
                {
                    b.HasOne("Bug.Entities.Model.Account", null)
                        .WithMany()
                        .HasForeignKey("AccountsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Bug.Entities.Model.Role", null)
                        .WithMany()
                        .HasForeignKey("RolesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Bug.Entities.Model.Account", b =>
                {
                    b.HasOne("Bug.Entities.Model.Timezone", "Timezone")
                        .WithMany()
                        .HasForeignKey("TimezoneId1");

                    b.Navigation("Timezone");
                });

            modelBuilder.Entity("Bug.Entities.Model.Attachment", b =>
                {
                    b.HasOne("Bug.Entities.Model.Issue", "Issue")
                        .WithMany("Attachments")
                        .HasForeignKey("IssueId");

                    b.Navigation("Issue");
                });

            modelBuilder.Entity("Bug.Entities.Model.Comment", b =>
                {
                    b.HasOne("Bug.Entities.Model.Account", "Account")
                        .WithMany()
                        .HasForeignKey("AccountId");

                    b.HasOne("Bug.Entities.Model.Issue", "Issue")
                        .WithMany()
                        .HasForeignKey("IssueId");

                    b.Navigation("Account");

                    b.Navigation("Issue");
                });

            modelBuilder.Entity("Bug.Entities.Model.Issue", b =>
                {
                    b.HasOne("Bug.Entities.Model.Account", "Assignee")
                        .WithMany("AssignIssues")
                        .HasForeignKey("AssigneeId");

                    b.HasOne("Bug.Entities.Model.Priority", "Priority")
                        .WithMany()
                        .HasForeignKey("PriorityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Bug.Entities.Model.Project", "Project")
                        .WithMany("Issues")
                        .HasForeignKey("ProjectId");

                    b.HasOne("Bug.Entities.Model.Account", "Reporter")
                        .WithMany("ReportIssues")
                        .HasForeignKey("ReporterId");

                    b.HasOne("Bug.Entities.Model.Status", "Status")
                        .WithMany()
                        .HasForeignKey("StatusId");

                    b.Navigation("Assignee");

                    b.Navigation("Priority");

                    b.Navigation("Project");

                    b.Navigation("Reporter");

                    b.Navigation("Status");
                });

            modelBuilder.Entity("Bug.Entities.Model.Issuelog", b =>
                {
                    b.HasOne("Bug.Entities.Model.Issue", "Issue")
                        .WithMany()
                        .HasForeignKey("IssueId");

                    b.HasOne("Bug.Entities.Model.Priority", "ModPriority")
                        .WithMany()
                        .HasForeignKey("ModPriorityId");

                    b.HasOne("Bug.Entities.Model.Status", "ModStatus")
                        .WithMany()
                        .HasForeignKey("ModStatusId");

                    b.HasOne("Bug.Entities.Model.Account", "Modifier")
                        .WithMany()
                        .HasForeignKey("ModifierId");

                    b.HasOne("Bug.Entities.Model.Priority", "PrePriority")
                        .WithMany()
                        .HasForeignKey("PrePriorityId");

                    b.HasOne("Bug.Entities.Model.Status", "PreStatus")
                        .WithMany()
                        .HasForeignKey("PreStatusId");

                    b.HasOne("Bug.Entities.Model.Tag", "Tag")
                        .WithMany()
                        .HasForeignKey("TagId");

                    b.Navigation("Issue");

                    b.Navigation("Modifier");

                    b.Navigation("ModPriority");

                    b.Navigation("ModStatus");

                    b.Navigation("PrePriority");

                    b.Navigation("PreStatus");

                    b.Navigation("Tag");
                });

            modelBuilder.Entity("Bug.Entities.Model.Project", b =>
                {
                    b.HasOne("Bug.Entities.Model.Account", "Creator")
                        .WithMany("CreatedProjects")
                        .HasForeignKey("CreatorId");

                    b.HasOne("Bug.Entities.Model.Account", "DefaultAssignee")
                        .WithMany("DefaultAssigneeProjects")
                        .HasForeignKey("DefaultAssigneeId");

                    b.HasOne("Bug.Entities.Model.Tag", null)
                        .WithMany("Projects")
                        .HasForeignKey("TagId");

                    b.HasOne("Bug.Entities.Model.Template", "Template")
                        .WithMany()
                        .HasForeignKey("TemplateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Creator");

                    b.Navigation("DefaultAssignee");

                    b.Navigation("Template");
                });

            modelBuilder.Entity("Bug.Entities.Model.Relation", b =>
                {
                    b.HasOne("Bug.Entities.Model.Issue", "FromIssue")
                        .WithMany("FromRelations")
                        .HasForeignKey("FromIssueId");

                    b.HasOne("Bug.Entities.Model.Tag", "Tag")
                        .WithMany()
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Bug.Entities.Model.Issue", "ToIssue")
                        .WithMany("ToRelations")
                        .HasForeignKey("ToIssueId");

                    b.Navigation("FromIssue");

                    b.Navigation("Tag");

                    b.Navigation("ToIssue");
                });

            modelBuilder.Entity("Bug.Entities.Model.Role", b =>
                {
                    b.HasOne("Bug.Entities.Model.Account", "Creator")
                        .WithMany("CreatedRoles")
                        .HasForeignKey("CreatorId");

                    b.Navigation("Creator");
                });

            modelBuilder.Entity("Bug.Entities.Model.Status", b =>
                {
                    b.HasOne("Bug.Entities.Model.Account", "Creator")
                        .WithMany("CreatedStatuses")
                        .HasForeignKey("CreatorId");

                    b.HasOne("Bug.Entities.Model.Tag", "Tag")
                        .WithMany("Statuses")
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Creator");

                    b.Navigation("Tag");
                });

            modelBuilder.Entity("Bug.Entities.Model.Tag", b =>
                {
                    b.HasOne("Bug.Entities.Model.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("Bug.Entities.Model.Worklog", b =>
                {
                    b.HasOne("Bug.Entities.Model.Account", "Logger")
                        .WithMany()
                        .HasForeignKey("LoggerId");

                    b.Navigation("Logger");
                });

            modelBuilder.Entity("CustomtypeField", b =>
                {
                    b.HasOne("Bug.Entities.Model.Customtype", null)
                        .WithMany()
                        .HasForeignKey("CustomtypesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Bug.Entities.Model.Field", null)
                        .WithMany()
                        .HasForeignKey("FieldsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("IssueTag", b =>
                {
                    b.HasOne("Bug.Entities.Model.Issue", null)
                        .WithMany()
                        .HasForeignKey("IssuesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Bug.Entities.Model.Tag", null)
                        .WithMany()
                        .HasForeignKey("TagsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PermissionRole", b =>
                {
                    b.HasOne("Bug.Entities.Model.Permission", null)
                        .WithMany()
                        .HasForeignKey("PermissionsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Bug.Entities.Model.Role", null)
                        .WithMany()
                        .HasForeignKey("RolesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ProjectRole", b =>
                {
                    b.HasOne("Bug.Entities.Model.Project", null)
                        .WithMany()
                        .HasForeignKey("ProjectsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Bug.Entities.Model.Role", null)
                        .WithMany()
                        .HasForeignKey("RolesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ProjectStatus", b =>
                {
                    b.HasOne("Bug.Entities.Model.Project", null)
                        .WithMany()
                        .HasForeignKey("ProjectsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Bug.Entities.Model.Status", null)
                        .WithMany()
                        .HasForeignKey("StatusesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Bug.Entities.Model.Account", b =>
                {
                    b.Navigation("AssignIssues");

                    b.Navigation("CreatedProjects");

                    b.Navigation("CreatedRoles");

                    b.Navigation("CreatedStatuses");

                    b.Navigation("DefaultAssigneeProjects");

                    b.Navigation("ReportIssues");
                });

            modelBuilder.Entity("Bug.Entities.Model.Issue", b =>
                {
                    b.Navigation("Attachments");

                    b.Navigation("FromRelations");

                    b.Navigation("ToRelations");
                });

            modelBuilder.Entity("Bug.Entities.Model.Project", b =>
                {
                    b.Navigation("Issues");
                });

            modelBuilder.Entity("Bug.Entities.Model.Tag", b =>
                {
                    b.Navigation("Projects");

                    b.Navigation("Statuses");
                });
#pragma warning restore 612, 618
        }
    }
}
