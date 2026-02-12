using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZoheeApi.Migrations
{
    /// <inheritdoc />
    public partial class Tables_And_Relationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "documents");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "documents");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "documents");

            migrationBuilder.RenameColumn(
                name: "IsTemplate",
                table: "documents",
                newName: "UserId");

            migrationBuilder.CreateTable(
                name: "templates",
                columns: table => new
                {
                    TemplateId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TemplateTitle = table.Column<string>(type: "TEXT", nullable: false),
                    FileName = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_templates", x => x.TemplateId);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserName = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    Phone = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_documents_UserId",
                table: "documents",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_documents_users_UserId",
                table: "documents",
                column: "UserId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_documents_users_UserId",
                table: "documents");

            migrationBuilder.DropTable(
                name: "templates");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropIndex(
                name: "IX_documents_UserId",
                table: "documents");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "documents",
                newName: "IsTemplate");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "documents",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "documents",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "documents",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }
    }
}
