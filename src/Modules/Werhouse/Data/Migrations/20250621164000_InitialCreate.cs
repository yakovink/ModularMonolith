
namespace Werhouse.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "werhouse");

            migrationBuilder.CreateTable(
                name: "WerhouseItem",
                schema: "werhouse",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductId = table.Column<Guid>(type: "uuid", nullable: false),
                    InvoiceId = table.Column<Guid>(type: "uuid", nullable: true),
                    Werhouse = table.Column<int>(type: "integer", nullable: true),
                    _createdDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    _lastModifiedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    _createdBy = table.Column<string>(type: "text", nullable: true),
                    _lastModifiedBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WerhouseItem", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WerhouseItemHistory",
                schema: "werhouse",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    In = table.Column<int>(type: "integer", nullable: true),
                    Out = table.Column<int>(type: "integer", nullable: true),
                    operation = table.Column<string>(type: "text", nullable: true),
                    description = table.Column<string>(type: "text", nullable: true),
                    WerhouseItemId = table.Column<Guid>(type: "uuid", nullable: false),
                    _createdDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    _lastModifiedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    _createdBy = table.Column<string>(type: "text", nullable: true),
                    _lastModifiedBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WerhouseItemHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WerhouseItemHistory_WerhouseItem_WerhouseItemId",
                        column: x => x.WerhouseItemId,
                        principalSchema: "werhouse",
                        principalTable: "WerhouseItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WerhouseItemHistory_WerhouseItemId",
                schema: "werhouse",
                table: "WerhouseItemHistory",
                column: "WerhouseItemId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WerhouseItemHistory",
                schema: "werhouse");

            migrationBuilder.DropTable(
                name: "WerhouseItem",
                schema: "werhouse");
        }
    }
}
