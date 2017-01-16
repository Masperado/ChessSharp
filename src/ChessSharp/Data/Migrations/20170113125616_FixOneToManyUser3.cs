using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ChessSharp.Data.Migrations
{
    public partial class FixOneToManyUser3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Request_AspNetUsers_RequestingUserId",
                table: "Request");

            migrationBuilder.DropForeignKey(
                name: "FK_Request_AspNetUsers_UserSentToId",
                table: "Request");

            migrationBuilder.DropIndex(
                name: "IX_Request_RequestingUserId",
                table: "Request");

            migrationBuilder.DropIndex(
                name: "IX_Request_UserSentToId",
                table: "Request");

            migrationBuilder.DropColumn(
                name: "RequestingUserId",
                table: "Request");

            migrationBuilder.DropColumn(
                name: "UserSentToId",
                table: "Request");

            migrationBuilder.DropTable(
                name: "Game");

            migrationBuilder.AddColumn<string>(
                name: "ReceiverId",
                table: "Request",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SenderId",
                table: "Request",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Request_ReceiverId",
                table: "Request",
                column: "ReceiverId");

            migrationBuilder.AddForeignKey(
                name: "FK_Request_AspNetUsers_ReceiverId",
                table: "Request",
                column: "ReceiverId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Request_AspNetUsers_ReceiverId",
                table: "Request");

            migrationBuilder.DropIndex(
                name: "IX_Request_ReceiverId",
                table: "Request");

            migrationBuilder.DropColumn(
                name: "ReceiverId",
                table: "Request");

            migrationBuilder.DropColumn(
                name: "SenderId",
                table: "Request");

            migrationBuilder.CreateTable(
                name: "Game",
                columns: table => new
                {
                    GameId = table.Column<Guid>(nullable: false),
                    ApplicationUserId = table.Column<string>(nullable: true),
                    BlackPlayer = table.Column<Guid>(nullable: false),
                    CurrentGameState = table.Column<int>(nullable: false),
                    DrawOffered = table.Column<bool>(nullable: false),
                    FEN = table.Column<string>(nullable: true),
                    GameDate = table.Column<DateTime>(nullable: false),
                    PGN = table.Column<string>(nullable: true),
                    WhitePlayer = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Game", x => x.GameId);
                    table.ForeignKey(
                        name: "FK_Game_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.AddColumn<string>(
                name: "RequestingUserId",
                table: "Request",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserSentToId",
                table: "Request",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Request_RequestingUserId",
                table: "Request",
                column: "RequestingUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Request_UserSentToId",
                table: "Request",
                column: "UserSentToId");

            migrationBuilder.CreateIndex(
                name: "IX_Game_ApplicationUserId",
                table: "Game",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Request_AspNetUsers_RequestingUserId",
                table: "Request",
                column: "RequestingUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Request_AspNetUsers_UserSentToId",
                table: "Request",
                column: "UserSentToId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
