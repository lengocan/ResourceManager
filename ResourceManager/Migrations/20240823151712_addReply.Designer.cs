﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ResourceManager.Models.Entities;

#nullable disable

namespace ResourceManager.Migrations
{
    [DbContext(typeof(ResourceContext))]
    [Migration("20240823151712_addReply")]
    partial class addReply
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ResourceManager.Models.Entities.AttachFile", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FilePath")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("AttachFiles");
                });

            modelBuilder.Entity("ResourceManager.Models.Entities.Notice", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Content")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TimeCreate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserIdReceivedDM")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserIdSent")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("projectId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("Notices");
                });

            modelBuilder.Entity("ResourceManager.Models.Entities.NoticeCompleteFromUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Content")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TimeCreate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserIdReceived")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserIdSentDM")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("projectId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("NoticeCompleteFromUsers");
                });

            modelBuilder.Entity("ResourceManager.Models.Entities.Project", b =>
                {
                    b.Property<Guid>("ProjectId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Branch")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("createDay")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("dueDay")
                        .HasColumnType("datetime2");

                    b.Property<string>("instruction")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("priority")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("projectName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("projectNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("turntime")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ProjectId");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("ResourceManager.Models.Entities.ProjectAssign", b =>
                {
                    b.Property<Guid>("ProjectId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserEmployeeId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ProjectId", "UserEmployeeId");

                    b.ToTable("ProjectAssigns");
                });

            modelBuilder.Entity("ResourceManager.Models.Entities.ProjectAttachFile", b =>
                {
                    b.Property<Guid>("ProjectId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("attachFileId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ProjectId", "attachFileId");

                    b.ToTable("ProjectAttachFiles");
                });

            modelBuilder.Entity("ResourceManager.Models.Entities.SendProject", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("isAccept")
                        .HasColumnType("bit");

                    b.Property<Guid>("projectId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("timeSend")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("SendProjects");
                });

            modelBuilder.Entity("ResourceManager.Models.Entities.TodoList", b =>
                {
                    b.Property<Guid>("TodoListId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ProjectId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("estimateHour")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("isCompleted")
                        .HasColumnType("bit");

                    b.Property<string>("taskName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TodoListId");

                    b.ToTable("TodoList");
                });
#pragma warning restore 612, 618
        }
    }
}
