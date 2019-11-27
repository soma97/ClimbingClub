using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using ClimbingClub.Library;

namespace ClimbingClub.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20191127193729_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.3");

            modelBuilder.Entity("ClimbingClub.Library.GearItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<int?>("LoaningId");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("LoaningId");

                    b.ToTable("GearItems");
                });

            modelBuilder.Entity("ClimbingClub.Library.Loaning", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Count");

                    b.Property<string>("Description");

                    b.Property<DateTime>("ExpectedReturnDate");

                    b.Property<DateTime>("LoanDate");

                    b.Property<int?>("MemberId");

                    b.Property<string>("Name");

                    b.Property<double>("Price");

                    b.Property<DateTime>("ReturnDate");

                    b.HasKey("Id");

                    b.HasIndex("MemberId");

                    b.ToTable("Loanings");
                });

            modelBuilder.Entity("ClimbingClub.Library.Member", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Level");

                    b.Property<string>("Name");

                    b.Property<string>("Surname");

                    b.HasKey("Id");

                    b.ToTable("Members");
                });

            modelBuilder.Entity("ClimbingClub.Library.MembershipFee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("IsMonthly");

                    b.Property<int?>("MemberId");

                    b.Property<DateTime>("Payment");

                    b.Property<int>("Price");

                    b.HasKey("Id");

                    b.HasIndex("MemberId");

                    b.ToTable("MembershipFees");
                });

            modelBuilder.Entity("ClimbingClub.Library.Training", b =>
                {
                    b.Property<DateTime>("TrainingDate");

                    b.Property<int?>("MemberId");

                    b.HasKey("TrainingDate");

                    b.HasIndex("MemberId");

                    b.ToTable("Trainings");
                });

            modelBuilder.Entity("ClimbingClub.Library.GearItem", b =>
                {
                    b.HasOne("ClimbingClub.Library.Loaning", "Loaning")
                        .WithMany()
                        .HasForeignKey("LoaningId");
                });

            modelBuilder.Entity("ClimbingClub.Library.Loaning", b =>
                {
                    b.HasOne("ClimbingClub.Library.Member", "Member")
                        .WithMany()
                        .HasForeignKey("MemberId");
                });

            modelBuilder.Entity("ClimbingClub.Library.MembershipFee", b =>
                {
                    b.HasOne("ClimbingClub.Library.Member", "Member")
                        .WithMany()
                        .HasForeignKey("MemberId");
                });

            modelBuilder.Entity("ClimbingClub.Library.Training", b =>
                {
                    b.HasOne("ClimbingClub.Library.Member", "Member")
                        .WithMany()
                        .HasForeignKey("MemberId");
                });
        }
    }
}
