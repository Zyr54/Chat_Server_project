using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChatServer.Migrations
{
    public partial class FK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Users_User_userID",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "UserForeignKey",
                table: "Messages");

            migrationBuilder.RenameColumn(
                name: "User_userID",
                table: "Messages",
                newName: "_userIDFK");

            migrationBuilder.RenameIndex(
                name: "IX_Messages_User_userID",
                table: "Messages",
                newName: "IX_Messages__userIDFK");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Users__userIDFK",
                table: "Messages",
                column: "_userIDFK",
                principalTable: "Users",
                principalColumn: "_userID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Users__userIDFK",
                table: "Messages");

            migrationBuilder.RenameColumn(
                name: "_userIDFK",
                table: "Messages",
                newName: "User_userID");

            migrationBuilder.RenameIndex(
                name: "IX_Messages__userIDFK",
                table: "Messages",
                newName: "IX_Messages_User_userID");

            migrationBuilder.AddColumn<int>(
                name: "UserForeignKey",
                table: "Messages",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Users_User_userID",
                table: "Messages",
                column: "User_userID",
                principalTable: "Users",
                principalColumn: "_userID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
