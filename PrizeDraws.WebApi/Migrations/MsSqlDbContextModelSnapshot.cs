﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PrizeDraws.WebApi.Persistence;

#nullable disable

namespace PrizeDraws.WebApi.Migrations
{
    [DbContext(typeof(MsSqlDbContext))]
    partial class MsSqlDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("PrizeDraws.WebApi.Models.Participant", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PromotionId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PromotionId");

                    b.ToTable("Participants");
                });

            modelBuilder.Entity("PrizeDraws.WebApi.Models.Prize", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PromotionId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PromotionId");

                    b.ToTable("Prizes");
                });

            modelBuilder.Entity("PrizeDraws.WebApi.Models.Promotion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Promotions");
                });

            modelBuilder.Entity("PrizeDraws.WebApi.Models.Raffle", b =>
                {
                    b.Property<int>("ParticipantId")
                        .HasColumnType("int");

                    b.Property<int>("PrizeId")
                        .HasColumnType("int");

                    b.Property<int>("PromotionId")
                        .HasColumnType("int");

                    b.HasKey("ParticipantId", "PrizeId", "PromotionId");

                    b.ToTable("Raffles");
                });

            modelBuilder.Entity("PrizeDraws.WebApi.Models.Participant", b =>
                {
                    b.HasOne("PrizeDraws.WebApi.Models.Promotion", null)
                        .WithMany("Participants")
                        .HasForeignKey("PromotionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PrizeDraws.WebApi.Models.Prize", b =>
                {
                    b.HasOne("PrizeDraws.WebApi.Models.Promotion", null)
                        .WithMany("Prizes")
                        .HasForeignKey("PromotionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PrizeDraws.WebApi.Models.Promotion", b =>
                {
                    b.Navigation("Participants");

                    b.Navigation("Prizes");
                });
#pragma warning restore 612, 618
        }
    }
}
