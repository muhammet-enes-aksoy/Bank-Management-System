using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vb.Data.Migrations
{
    /// <inheritdoc />
    public partial class Users : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Amount",
                schema: "dbo",
                table: "AccountTransaction",
                newName: "DebitAmount");

            migrationBuilder.AddColumn<decimal>(
                name: "CreditAmount",
                schema: "dbo",
                table: "AccountTransaction",
                type: "decimal(18,4)",
                precision: 18,
                scale: 4,
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreditAmount",
                schema: "dbo",
                table: "AccountTransaction");

            migrationBuilder.RenameColumn(
                name: "DebitAmount",
                schema: "dbo",
                table: "AccountTransaction",
                newName: "Amount");
        }
    }
}
