﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace BlueLibrary.Migrations
{
    public partial class imageisnullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Book_BookImage_ImageId",
                table: "Book");

            migrationBuilder.AlterColumn<int>(
                name: "ImageId",
                table: "Book",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Book_ImageId",
                table: "Book",
                column: "ImageId",
                unique: true,
                filter: "[ImageId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Book_BookImage_ImageId",
                table: "Book",
                column: "ImageId",
                principalTable: "BookImage",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Book_BookImage_ImageId",
                table: "Book");

            migrationBuilder.DropIndex(
                name: "IX_Book_ImageId",
                table: "Book");

            migrationBuilder.AlterColumn<int>(
                name: "ImageId",
                table: "Book",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Book_ImageId",
                table: "Book",
                column: "ImageId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Book_BookImage_ImageId",
                table: "Book",
                column: "ImageId",
                principalTable: "BookImage",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
