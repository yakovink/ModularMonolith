﻿
namespace Basket.Data.Migrations
{
    [DbContext(typeof(BasketDbContext))]
    partial class BasketDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("basket")
                .HasAnnotation("ProductVersion", "9.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Basket.Baskets.Models.ShoppingCart", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("_createdBy")
                        .HasColumnType("text");

                    b.Property<DateTime?>("_createdDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("_lastModifiedBy")
                        .HasColumnType("text");

                    b.Property<DateTime?>("_lastModifiedDate")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("ShoppingCart", "basket");
                });

            modelBuilder.Entity("Basket.Baskets.Models.ShoppingCartItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uuid");

                    b.Property<int>("Quantity")
                        .HasColumnType("integer");

                    b.Property<Guid>("ShoppingCartId")
                        .HasColumnType("uuid");

                    b.Property<string>("_createdBy")
                        .HasColumnType("text");

                    b.Property<DateTime?>("_createdDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("_lastModifiedBy")
                        .HasColumnType("text");

                    b.Property<DateTime?>("_lastModifiedDate")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("ShoppingCartId");

                    b.ToTable("ShoppingCartItem", "basket");
                });

            modelBuilder.Entity("Basket.Baskets.Models.ShoppingCartItem", b =>
                {
                    b.HasOne("Basket.Baskets.Models.ShoppingCart", "ShoppingCart")
                        .WithMany("items")
                        .HasForeignKey("ShoppingCartId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ShoppingCart");
                });

            modelBuilder.Entity("Basket.Baskets.Models.ShoppingCart", b =>
                {
                    b.Navigation("items");
                });
#pragma warning restore 612, 618
        }
    }
}
