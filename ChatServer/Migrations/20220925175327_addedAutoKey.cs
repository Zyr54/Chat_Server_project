using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChatServer.Migrations
{
    public partial class addedAutoKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Users_UserForeignKey",
                table: "Messages");

            migrationBuilder.DropIndex(
                name: "IX_Messages_UserForeignKey",
                table: "Messages");

            migrationBuilder.AddColumn<int>(
                name: "User_userID",
                table: "Messages",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Messages_User_userID",
                table: "Messages",
                column: "User_userID");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Users_User_userID",
                table: "Messages",
                column: "User_userID",
                principalTable: "Users",
                principalColumn: "_userID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Users_User_userID",
                table: "Messages");

            migrationBuilder.DropIndex(
                name: "IX_Messages_User_userID",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "User_userID",
                table: "Messages");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_UserForeignKey",
                table: "Messages",
                column: "UserForeignKey");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Users_UserForeignKey",
                table: "Messages",
                column: "UserForeignKey",
                principalTable: "Users",
                principalColumn: "_userID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
