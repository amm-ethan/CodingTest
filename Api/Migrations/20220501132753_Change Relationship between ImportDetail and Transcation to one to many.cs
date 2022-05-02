using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Api.Migrations
{
    public partial class ChangeRelationshipbetweenImportDetailandTranscationtoonetomany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ImportDetails_Transactions_TransactionGuid",
                table: "ImportDetails");

            migrationBuilder.DropIndex(
                name: "IX_ImportDetails_TransactionGuid",
                table: "ImportDetails");

            migrationBuilder.DropColumn(
                name: "TransactionGuid",
                table: "ImportDetails");

            migrationBuilder.AddColumn<Guid>(
                name: "ImportDetailGuid",
                table: "Transactions",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_ImportDetailGuid",
                table: "Transactions",
                column: "ImportDetailGuid");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_ImportDetails_ImportDetailGuid",
                table: "Transactions",
                column: "ImportDetailGuid",
                principalTable: "ImportDetails",
                principalColumn: "Guid",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_ImportDetails_ImportDetailGuid",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_ImportDetailGuid",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "ImportDetailGuid",
                table: "Transactions");

            migrationBuilder.AddColumn<Guid>(
                name: "TransactionGuid",
                table: "ImportDetails",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_ImportDetails_TransactionGuid",
                table: "ImportDetails",
                column: "TransactionGuid",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ImportDetails_Transactions_TransactionGuid",
                table: "ImportDetails",
                column: "TransactionGuid",
                principalTable: "Transactions",
                principalColumn: "Guid",
                onDelete: ReferentialAction.Cascade);
        }
    }
}