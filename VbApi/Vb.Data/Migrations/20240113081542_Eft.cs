using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vb.Data.Migrations
{
    /// <inheritdoc />
    public partial class Eft : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SenderName",
                schema: "dbo",
                table: "EftTransaction",
                newName: "ReceiverName");

            migrationBuilder.RenameColumn(
                name: "SenderIban",
                schema: "dbo",
                table: "EftTransaction",
                newName: "ReceiverIban");

            migrationBuilder.RenameColumn(
                name: "SenderAccount",
                schema: "dbo",
                table: "EftTransaction",
                newName: "ReceiverAccount");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ReceiverName",
                schema: "dbo",
                table: "EftTransaction",
                newName: "SenderName");

            migrationBuilder.RenameColumn(
                name: "ReceiverIban",
                schema: "dbo",
                table: "EftTransaction",
                newName: "SenderIban");

            migrationBuilder.RenameColumn(
                name: "ReceiverAccount",
                schema: "dbo",
                table: "EftTransaction",
                newName: "SenderAccount");
        }
    }
}
