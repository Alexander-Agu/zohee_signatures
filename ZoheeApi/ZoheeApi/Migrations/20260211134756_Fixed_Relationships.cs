using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZoheeApi.Migrations
{
    /// <inheritdoc />
    public partial class Fixed_Relationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_documents_users_UserId",
                table: "documents");

            migrationBuilder.DropIndex(
                name: "IX_documents_UserId",
                table: "documents");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "documents");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "users",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_users_UserId",
                table: "users",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_users_documents_UserId",
                table: "users",
                column: "UserId",
                principalTable: "documents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_users_documents_UserId",
                table: "users");

            migrationBuilder.DropIndex(
                name: "IX_users_UserId",
                table: "users");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "users");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "documents",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

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
    }
}
