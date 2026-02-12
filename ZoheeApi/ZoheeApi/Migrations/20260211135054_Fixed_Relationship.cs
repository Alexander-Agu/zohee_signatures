using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZoheeApi.Migrations
{
    /// <inheritdoc />
    public partial class Fixed_Relationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_users_documents_UserId",
                table: "users");

            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "users",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "users",
                newName: "DocumentId");

            migrationBuilder.RenameIndex(
                name: "IX_users_UserId",
                table: "users",
                newName: "IX_users_DocumentId");

            migrationBuilder.AddForeignKey(
                name: "FK_users_documents_DocumentId",
                table: "users",
                column: "DocumentId",
                principalTable: "documents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_users_documents_DocumentId",
                table: "users");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "users",
                newName: "UserName");

            migrationBuilder.RenameColumn(
                name: "DocumentId",
                table: "users",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_users_DocumentId",
                table: "users",
                newName: "IX_users_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_users_documents_UserId",
                table: "users",
                column: "UserId",
                principalTable: "documents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
