using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZoheeApi.Migrations
{
    /// <inheritdoc />
    public partial class Added_New_User_Field : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "HasSigned",
                table: "users",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasSigned",
                table: "users");
        }
    }
}
