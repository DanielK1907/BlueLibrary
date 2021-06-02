using Microsoft.EntityFrameworkCore.Migrations;

namespace BlueLibrary.Migrations
{
    public partial class Version51 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Genre_Book_BookId",
                table: "Genre");

            migrationBuilder.DropIndex(
                name: "IX_Genre_BookId",
                table: "Genre");

            migrationBuilder.DropColumn(
                name: "BookId",
                table: "Genre");

            migrationBuilder.CreateTable(
                name: "BookGenre",
                columns: table => new
                {
                    GenresId = table.Column<int>(type: "int", nullable: false),
                    booksId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookGenre", x => new { x.GenresId, x.booksId });
                    table.ForeignKey(
                        name: "FK_BookGenre_Book_booksId",
                        column: x => x.booksId,
                        principalTable: "Book",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookGenre_Genre_GenresId",
                        column: x => x.GenresId,
                        principalTable: "Genre",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookGenre_booksId",
                table: "BookGenre",
                column: "booksId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookGenre");

            migrationBuilder.AddColumn<int>(
                name: "BookId",
                table: "Genre",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Genre_BookId",
                table: "Genre",
                column: "BookId");

            migrationBuilder.AddForeignKey(
                name: "FK_Genre_Book_BookId",
                table: "Genre",
                column: "BookId",
                principalTable: "Book",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
