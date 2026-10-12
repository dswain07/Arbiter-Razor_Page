using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Arbiter.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddRelationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ReviewName",
                table: "Reviews",
                newName: "ReviewerName");

            migrationBuilder.RenameColumn(
                name: "OrciId",
                table: "Authors",
                newName: "OrcidId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_PaperId",
                table: "Reviews",
                column: "PaperId");

            migrationBuilder.CreateIndex(
                name: "IX_Papers_TrackId",
                table: "Papers",
                column: "TrackId");

            migrationBuilder.CreateIndex(
                name: "IX_PaperAuthors_AuthorId",
                table: "PaperAuthors",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_PaperAuthors_PaperId",
                table: "PaperAuthors",
                column: "PaperId");

            migrationBuilder.AddForeignKey(
                name: "FK_PaperAuthors_Authors_AuthorId",
                table: "PaperAuthors",
                column: "AuthorId",
                principalTable: "Authors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PaperAuthors_Papers_PaperId",
                table: "PaperAuthors",
                column: "PaperId",
                principalTable: "Papers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Papers_Tracks_TrackId",
                table: "Papers",
                column: "TrackId",
                principalTable: "Tracks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Papers_PaperId",
                table: "Reviews",
                column: "PaperId",
                principalTable: "Papers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PaperAuthors_Authors_AuthorId",
                table: "PaperAuthors");

            migrationBuilder.DropForeignKey(
                name: "FK_PaperAuthors_Papers_PaperId",
                table: "PaperAuthors");

            migrationBuilder.DropForeignKey(
                name: "FK_Papers_Tracks_TrackId",
                table: "Papers");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Papers_PaperId",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_PaperId",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Papers_TrackId",
                table: "Papers");

            migrationBuilder.DropIndex(
                name: "IX_PaperAuthors_AuthorId",
                table: "PaperAuthors");

            migrationBuilder.DropIndex(
                name: "IX_PaperAuthors_PaperId",
                table: "PaperAuthors");

            migrationBuilder.RenameColumn(
                name: "ReviewerName",
                table: "Reviews",
                newName: "ReviewName");

            migrationBuilder.RenameColumn(
                name: "OrcidId",
                table: "Authors",
                newName: "OrciId");
        }
    }
}
