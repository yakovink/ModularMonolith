
namespace Account.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "account");

            migrationBuilder.CreateTable(
                name: "Users",
                schema: "account",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<string>(type: "character(9)", fixedLength: true, maxLength: 9, nullable: false),
                    UserName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: false),
                    FirstName = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    LastName = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    PhoneNumber = table.Column<string>(type: "character varying(12)", maxLength: 12, nullable: false),
                    address = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    ShoppingCartId = table.Column<Guid>(type: "uuid", nullable: false),
                    _createdDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    _lastModifiedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    _createdBy = table.Column<string>(type: "text", nullable: true),
                    _lastModifiedBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserActivity",
                schema: "account",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    ActivityType = table.Column<string>(type: "text", nullable: false),
                    IpAddress = table.Column<string>(type: "text", nullable: false),
                    isValidated = table.Column<bool>(type: "boolean", nullable: false),
                    _createdDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    _lastModifiedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    _createdBy = table.Column<string>(type: "text", nullable: true),
                    _lastModifiedBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserActivity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserActivity_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "account",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserActivity_UserId",
                schema: "account",
                table: "UserActivity",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                schema: "account",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_PhoneNumber",
                schema: "account",
                table: "Users",
                column: "PhoneNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserId",
                schema: "account",
                table: "Users",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserName",
                schema: "account",
                table: "Users",
                column: "UserName",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserActivity",
                schema: "account");

            migrationBuilder.DropTable(
                name: "Users",
                schema: "account");
        }
    }
}
