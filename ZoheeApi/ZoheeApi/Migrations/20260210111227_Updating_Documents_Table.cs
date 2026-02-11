using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZoheeApi.Migrations
{
    /// <inheritdoc />
    public partial class Updating_Documents_Table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "documents",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsSigned",
                table: "documents",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "documents");

            migrationBuilder.DropColumn(
                name: "IsSigned",
                table: "documents");
        }
    }
}
