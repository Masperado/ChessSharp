using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ChessSharp.Data.Migrations
{
    public partial class FixOneToManyUser6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<string>(
                name: "RecieverId",
                table: "Request",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "SenderId",
                table: "Request",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Request_RecieverId",
                table: "Request",
                column: "RecieverId");

            migrationBuilder.CreateIndex(
                name: "IX_Request_SenderId",
                table: "Request",
                column: "SenderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Request_AspNetUsers_RecieverId",
                table: "Request",
                column: "RecieverId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Request_AspNetUsers_SenderId",
                table: "Request",
                column: "SenderId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Request_AspNetUsers_RecieverId",
                table: "Request");

            migrationBuilder.DropForeignKey(
                name: "FK_Request_AspNetUsers_SenderId",
                table: "Request");

            migrationBuilder.DropIndex(
                name: "IX_Request_RecieverId",
                table: "Request");

            migrationBuilder.DropIndex(
                name: "IX_Request_SenderId",
                table: "Request");

            migrationBuilder.DropColumn(
                name: "RecieverId",
                table: "Request");

            migrationBuilder.AddColumn<string>(
                name: "ReceiverId",
                table: "Request",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
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
    }
}
