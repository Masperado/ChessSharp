using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ChessSharp.Data.Migrations
{
    public partial class FixRequestsGames1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Request_AspNetUsers_UserSentToId",
                table: "Request");

            migrationBuilder.DropIndex(
                name: "IX_Request_UserSentToId",
                table: "Request");

            migrationBuilder.DropColumn(
                name: "UserSentToId",
                table: "Request");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Request",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Request_UserId",
                table: "Request",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Request_AspNetUsers_UserId",
                table: "Request",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Request_AspNetUsers_UserId",
                table: "Request");

            migrationBuilder.DropIndex(
                name: "IX_Request_UserId",
                table: "Request");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Request");

            migrationBuilder.AddColumn<string>(
                name: "UserSentToId",
                table: "Request",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Request_UserSentToId",
                table: "Request",
                column: "UserSentToId");

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
