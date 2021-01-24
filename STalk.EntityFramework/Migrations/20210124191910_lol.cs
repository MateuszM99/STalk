using Microsoft.EntityFrameworkCore.Migrations;

namespace EntityFramework.Migrations
{
    public partial class lol : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContactListUser_AspNetUsers_UserId",
                table: "ContactListUser");

            migrationBuilder.DropForeignKey(
                name: "FK_ContactListUser_ContactLists_ContactListId",
                table: "ContactListUser");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ContactListUser",
                table: "ContactListUser");

            migrationBuilder.RenameTable(
                name: "ContactListUser",
                newName: "ContactListUsers");

            migrationBuilder.RenameIndex(
                name: "IX_ContactListUser_UserId",
                table: "ContactListUsers",
                newName: "IX_ContactListUsers_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_ContactListUser_ContactListId",
                table: "ContactListUsers",
                newName: "IX_ContactListUsers_ContactListId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ContactListUsers",
                table: "ContactListUsers",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ContactListUsers_AspNetUsers_UserId",
                table: "ContactListUsers",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ContactListUsers_ContactLists_ContactListId",
                table: "ContactListUsers",
                column: "ContactListId",
                principalTable: "ContactLists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContactListUsers_AspNetUsers_UserId",
                table: "ContactListUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_ContactListUsers_ContactLists_ContactListId",
                table: "ContactListUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ContactListUsers",
                table: "ContactListUsers");

            migrationBuilder.RenameTable(
                name: "ContactListUsers",
                newName: "ContactListUser");

            migrationBuilder.RenameIndex(
                name: "IX_ContactListUsers_UserId",
                table: "ContactListUser",
                newName: "IX_ContactListUser_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_ContactListUsers_ContactListId",
                table: "ContactListUser",
                newName: "IX_ContactListUser_ContactListId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ContactListUser",
                table: "ContactListUser",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ContactListUser_AspNetUsers_UserId",
                table: "ContactListUser",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ContactListUser_ContactLists_ContactListId",
                table: "ContactListUser",
                column: "ContactListId",
                principalTable: "ContactLists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
