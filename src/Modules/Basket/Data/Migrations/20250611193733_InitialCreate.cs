 
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Basket.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "basket");

            migrationBuilder.CreateTable(
                name: "ShoppingCart",
                schema: "basket",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    _createdDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    _lastModifiedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    _createdBy = table.Column<string>(type: "text", nullable: true),
                    _lastModifiedBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppingCart", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ShoppingCartItem",
                schema: "basket",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ShoppingCartId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductId = table.Column<Guid>(type: "uuid", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false),
                    _createdDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    _lastModifiedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    _createdBy = table.Column<string>(type: "text", nullable: true),
                    _lastModifiedBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppingCartItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShoppingCartItem_ShoppingCart_ShoppingCartId",
                        column: x => x.ShoppingCartId,
                        principalSchema: "basket",
                        principalTable: "ShoppingCart",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCartItem_ShoppingCartId",
                schema: "basket",
                table: "ShoppingCartItem",
                column: "ShoppingCartId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ShoppingCartItem",
                schema: "basket");

            migrationBuilder.DropTable(
                name: "ShoppingCart",
                schema: "basket");
        }
    }
}
