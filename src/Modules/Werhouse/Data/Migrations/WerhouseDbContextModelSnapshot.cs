﻿

namespace Werhouse.Data.Migrations
{
    [DbContext(typeof(WerhouseDbContext))]
    partial class WerhouseDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("werhouse")
                .HasAnnotation("ProductVersion", "9.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Werhouse.Items.Models.WerhouseItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("InvoiceId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uuid");

                    b.Property<int?>("Werhouse")
                        .HasColumnType("integer");

                    b.Property<string>("_createdBy")
                        .HasColumnType("text");

                    b.Property<DateTime?>("_createdDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("_lastModifiedBy")
                        .HasColumnType("text");

                    b.Property<DateTime?>("_lastModifiedDate")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("WerhouseItem", "werhouse");
                });

            modelBuilder.Entity("Werhouse.Items.Models.WerhouseItemHistory", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int?>("In")
                        .HasColumnType("integer");

                    b.Property<int?>("Out")
                        .HasColumnType("integer");

                    b.Property<Guid>("WerhouseItemId")
                        .HasColumnType("uuid");

                    b.Property<string>("_createdBy")
                        .HasColumnType("text");

                    b.Property<DateTime>("_createdDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("_lastModifiedBy")
                        .HasColumnType("text");

                    b.Property<DateTime?>("_lastModifiedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("description")
                        .HasColumnType("text");

                    b.Property<string>("operation")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("WerhouseItemId");

                    b.ToTable("WerhouseItemHistory", "werhouse");
                });

            modelBuilder.Entity("Werhouse.Items.Models.WerhouseItemHistory", b =>
                {
                    b.HasOne("Werhouse.Items.Models.WerhouseItem", "item")
                        .WithMany("checkpoints")
                        .HasForeignKey("WerhouseItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("item");
                });

            modelBuilder.Entity("Werhouse.Items.Models.WerhouseItem", b =>
                {
                    b.Navigation("checkpoints");
                });
#pragma warning restore 612, 618
        }
    }
}
