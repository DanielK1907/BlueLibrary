using Microsoft.EntityFrameworkCore.Migrations;

namespace BlueLibrary.Migrations
{
    public partial class xd1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Book_BookImage_BookImageId",
                table: "Book");

            migrationBuilder.RenameColumn(
                name: "BookImageId",
                table: "Book",
                newName: "ImageId");

            migrationBuilder.RenameIndex(
                name: "IX_Book_BookImageId",
                table: "Book",
                newName: "IX_Book_ImageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Book_BookImage_ImageId",
                table: "Book",
                column: "ImageId",
                principalTable: "BookImage",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Book_BookImage_ImageId",
                table: "Book");

            migrationBuilder.RenameColumn(
                name: "ImageId",
                table: "Book",
                newName: "BookImageId");

            migrationBuilder.RenameIndex(
                name: "IX_Book_ImageId",
                table: "Book",
                newName: "IX_Book_BookImageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Book_BookImage_BookImageId",
                table: "Book",
                column: "BookImageId",
                principalTable: "BookImage",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
