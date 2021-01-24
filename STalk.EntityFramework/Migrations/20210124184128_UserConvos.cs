using Microsoft.EntityFrameworkCore.Migrations;

namespace EntityFramework.Migrations
{
    public partial class UserConvos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserConversation_AspNetUsers_UserId",
                table: "UserConversation");

            migrationBuilder.DropForeignKey(
                name: "FK_UserConversation_Conversations_ConversationId",
                table: "UserConversation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserConversation",
                table: "UserConversation");

            migrationBuilder.RenameTable(
                name: "UserConversation",
                newName: "UserConversations");

            migrationBuilder.RenameIndex(
                name: "IX_UserConversation_UserId",
                table: "UserConversations",
                newName: "IX_UserConversations_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserConversation_ConversationId",
                table: "UserConversations",
                newName: "IX_UserConversations_ConversationId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserConversations",
                table: "UserConversations",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserConversations_AspNetUsers_UserId",
                table: "UserConversations",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserConversations_Conversations_ConversationId",
                table: "UserConversations",
                column: "ConversationId",
                principalTable: "Conversations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserConversations_AspNetUsers_UserId",
                table: "UserConversations");

            migrationBuilder.DropForeignKey(
                name: "FK_UserConversations_Conversations_ConversationId",
                table: "UserConversations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserConversations",
                table: "UserConversations");

            migrationBuilder.RenameTable(
                name: "UserConversations",
                newName: "UserConversation");

            migrationBuilder.RenameIndex(
                name: "IX_UserConversations_UserId",
                table: "UserConversation",
                newName: "IX_UserConversation_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserConversations_ConversationId",
                table: "UserConversation",
                newName: "IX_UserConversation_ConversationId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserConversation",
                table: "UserConversation",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserConversation_AspNetUsers_UserId",
                table: "UserConversation",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserConversation_Conversations_ConversationId",
                table: "UserConversation",
                column: "ConversationId",
                principalTable: "Conversations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
