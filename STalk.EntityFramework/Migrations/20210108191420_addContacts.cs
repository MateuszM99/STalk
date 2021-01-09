using Microsoft.EntityFrameworkCore.Migrations;

namespace EntityFramework.Migrations
{
    public partial class addContacts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AddToContactRequest_AspNetUsers_UserToId",
                table: "AddToContactRequest");

            migrationBuilder.DropForeignKey(
                name: "FK_ContactList_AspNetUsers_UserId",
                table: "ContactList");

            migrationBuilder.DropForeignKey(
                name: "FK_ContactListUser_ContactList_ContactListId",
                table: "ContactListUser");

            migrationBuilder.DropForeignKey(
                name: "FK_File_AspNetUsers_UserId",
                table: "File");

            migrationBuilder.DropPrimaryKey(
                name: "PK_File",
                table: "File");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ContactList",
                table: "ContactList");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AddToContactRequest",
                table: "AddToContactRequest");

            migrationBuilder.RenameTable(
                name: "File",
                newName: "Files");

            migrationBuilder.RenameTable(
                name: "ContactList",
                newName: "ContactLists");

            migrationBuilder.RenameTable(
                name: "AddToContactRequest",
                newName: "AddToContactRequests");

            migrationBuilder.RenameIndex(
                name: "IX_File_UserId",
                table: "Files",
                newName: "IX_Files_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_ContactList_UserId",
                table: "ContactLists",
                newName: "IX_ContactLists_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_AddToContactRequest_UserToId",
                table: "AddToContactRequests",
                newName: "IX_AddToContactRequests_UserToId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Files",
                table: "Files",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ContactLists",
                table: "ContactLists",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AddToContactRequests",
                table: "AddToContactRequests",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AddToContactRequests_AspNetUsers_UserToId",
                table: "AddToContactRequests",
                column: "UserToId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ContactLists_AspNetUsers_UserId",
                table: "ContactLists",
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

            migrationBuilder.AddForeignKey(
                name: "FK_Files_AspNetUsers_UserId",
                table: "Files",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AddToContactRequests_AspNetUsers_UserToId",
                table: "AddToContactRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_ContactLists_AspNetUsers_UserId",
                table: "ContactLists");

            migrationBuilder.DropForeignKey(
                name: "FK_ContactListUser_ContactLists_ContactListId",
                table: "ContactListUser");

            migrationBuilder.DropForeignKey(
                name: "FK_Files_AspNetUsers_UserId",
                table: "Files");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Files",
                table: "Files");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ContactLists",
                table: "ContactLists");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AddToContactRequests",
                table: "AddToContactRequests");

            migrationBuilder.RenameTable(
                name: "Files",
                newName: "File");

            migrationBuilder.RenameTable(
                name: "ContactLists",
                newName: "ContactList");

            migrationBuilder.RenameTable(
                name: "AddToContactRequests",
                newName: "AddToContactRequest");

            migrationBuilder.RenameIndex(
                name: "IX_Files_UserId",
                table: "File",
                newName: "IX_File_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_ContactLists_UserId",
                table: "ContactList",
                newName: "IX_ContactList_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_AddToContactRequests_UserToId",
                table: "AddToContactRequest",
                newName: "IX_AddToContactRequest_UserToId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_File",
                table: "File",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ContactList",
                table: "ContactList",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AddToContactRequest",
                table: "AddToContactRequest",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AddToContactRequest_AspNetUsers_UserToId",
                table: "AddToContactRequest",
                column: "UserToId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ContactList_AspNetUsers_UserId",
                table: "ContactList",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ContactListUser_ContactList_ContactListId",
                table: "ContactListUser",
                column: "ContactListId",
                principalTable: "ContactList",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_File_AspNetUsers_UserId",
                table: "File",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
