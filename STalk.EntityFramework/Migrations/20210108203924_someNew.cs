using Microsoft.EntityFrameworkCore.Migrations;

namespace EntityFramework.Migrations
{
    public partial class someNew : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserFromId",
                table: "AddToContactRequests",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AddToContactRequests_UserFromId",
                table: "AddToContactRequests",
                column: "UserFromId");

            migrationBuilder.AddForeignKey(
                name: "FK_AddToContactRequests_AspNetUsers_UserFromId",
                table: "AddToContactRequests",
                column: "UserFromId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AddToContactRequests_AspNetUsers_UserFromId",
                table: "AddToContactRequests");

            migrationBuilder.DropIndex(
                name: "IX_AddToContactRequests_UserFromId",
                table: "AddToContactRequests");

            migrationBuilder.AlterColumn<string>(
                name: "UserFromId",
                table: "AddToContactRequests",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);
        }
    }
}
