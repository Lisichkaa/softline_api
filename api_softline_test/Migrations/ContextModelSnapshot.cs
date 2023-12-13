﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using api_softline_test;

#nullable disable

namespace api_softline_test.Migrations
{
    [DbContext(typeof(TasksContext))]
    partial class ContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.14");

            modelBuilder.Entity("api_softline_test.Status", b =>
                {
                    b.Property<int>("Status_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Status_name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Status_ID");

                    b.ToTable("Statuses");

                    b.HasData(
                        new
                        {
                            Status_ID = 1,
                            Status_name = "Создана"
                        },
                        new
                        {
                            Status_ID = 2,
                            Status_name = "В работе"
                        },
                        new
                        {
                            Status_ID = 3,
                            Status_name = "Завершена"
                        });
                });

            modelBuilder.Entity("api_softline_test.TaskDb", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Status_ID")
                        .HasColumnType("INTEGER");

                    b.HasKey("ID");

                    b.HasIndex("Status_ID");

                    b.ToTable("Tasks");
                });

            modelBuilder.Entity("api_softline_test.TaskDb", b =>
                {
                    b.HasOne("api_softline_test.Status", "Status")
                        .WithMany("TasksList")
                        .HasForeignKey("Status_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Status");
                });

            modelBuilder.Entity("api_softline_test.Status", b =>
                {
                    b.Navigation("TasksList");
                });
#pragma warning restore 612, 618
        }
    }
}
