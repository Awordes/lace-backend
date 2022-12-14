// <auto-generated />
using System;
using Lace.Application.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Lace.Application.Migrations
{
    [DbContext(typeof(LaceDbContext))]
    [Migration("20220910152740_AddProfileEmail")]
    partial class AddProfileEmail
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("lace_schema")
                .HasAnnotation("ProductVersion", "6.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Lace.Domain.Entities.Category", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Categories", "lace_schema");
                });

            modelBuilder.Entity("Lace.Domain.Entities.DictionaryElement", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("CategoryId")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("DictionaryElements", "lace_schema");
                });

            modelBuilder.Entity("Lace.Domain.Entities.Profile", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Profiles", "lace_schema");
                });

            modelBuilder.Entity("Lace.Domain.Entities.ProfileAttribute", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("CategoryId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("DictionaryElementId")
                        .HasColumnType("uuid");

                    b.Property<string>("ExternalValue")
                        .HasColumnType("text");

                    b.Property<Guid>("ProfileId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("DictionaryElementId");

                    b.HasIndex("ProfileId");

                    b.ToTable("ProfileAttributes", "lace_schema");
                });

            modelBuilder.Entity("Lace.Domain.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Users", "lace_schema");
                });

            modelBuilder.Entity("Lace.Domain.Entities.DictionaryElement", b =>
                {
                    b.HasOne("Lace.Domain.Entities.Category", "Category")
                        .WithMany("DictionaryElements")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("Lace.Domain.Entities.Profile", b =>
                {
                    b.HasOne("Lace.Domain.Entities.User", "User")
                        .WithOne("Profile")
                        .HasForeignKey("Lace.Domain.Entities.Profile", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Lace.Domain.Entities.ProfileAttribute", b =>
                {
                    b.HasOne("Lace.Domain.Entities.Category", "Category")
                        .WithMany("ProfileAttributes")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Lace.Domain.Entities.DictionaryElement", "DictionaryElement")
                        .WithMany("ProfileAttributes")
                        .HasForeignKey("DictionaryElementId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Lace.Domain.Entities.Profile", "Profile")
                        .WithMany("ProfileAttributes")
                        .HasForeignKey("ProfileId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("DictionaryElement");

                    b.Navigation("Profile");
                });

            modelBuilder.Entity("Lace.Domain.Entities.Category", b =>
                {
                    b.Navigation("DictionaryElements");

                    b.Navigation("ProfileAttributes");
                });

            modelBuilder.Entity("Lace.Domain.Entities.DictionaryElement", b =>
                {
                    b.Navigation("ProfileAttributes");
                });

            modelBuilder.Entity("Lace.Domain.Entities.Profile", b =>
                {
                    b.Navigation("ProfileAttributes");
                });

            modelBuilder.Entity("Lace.Domain.Entities.User", b =>
                {
                    b.Navigation("Profile")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
