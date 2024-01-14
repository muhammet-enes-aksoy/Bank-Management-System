using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vb.Data.Migrations
{
    /// <inheritdoc />
    public partial class DeleteId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Account_Customer_CustomerId",
                schema: "dbo",
                table: "Account");

            migrationBuilder.DropForeignKey(
                name: "FK_AccountTransaction_Account_AccountId",
                schema: "dbo",
                table: "AccountTransaction");

            migrationBuilder.DropForeignKey(
                name: "FK_Address_Customer_CustomerId",
                schema: "dbo",
                table: "Address");

            migrationBuilder.DropForeignKey(
                name: "FK_Contact_Customer_CustomerId",
                schema: "dbo",
                table: "Contact");

            migrationBuilder.DropForeignKey(
                name: "FK_EftTransaction_Account_AccountId",
                schema: "dbo",
                table: "EftTransaction");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Customer",
                schema: "dbo",
                table: "Customer");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Account",
                schema: "dbo",
                table: "Account");

            migrationBuilder.DropColumn(
                name: "Id",
                schema: "dbo",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "Id",
                schema: "dbo",
                table: "Account");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Customer",
                schema: "dbo",
                table: "Customer",
                column: "CustomerNumber");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Account",
                schema: "dbo",
                table: "Account",
                column: "AccountNumber");

            migrationBuilder.AddForeignKey(
                name: "FK_Account_Customer_CustomerId",
                schema: "dbo",
                table: "Account",
                column: "CustomerId",
                principalSchema: "dbo",
                principalTable: "Customer",
                principalColumn: "CustomerNumber",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AccountTransaction_Account_AccountId",
                schema: "dbo",
                table: "AccountTransaction",
                column: "AccountId",
                principalSchema: "dbo",
                principalTable: "Account",
                principalColumn: "AccountNumber",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Address_Customer_CustomerId",
                schema: "dbo",
                table: "Address",
                column: "CustomerId",
                principalSchema: "dbo",
                principalTable: "Customer",
                principalColumn: "CustomerNumber",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Contact_Customer_CustomerId",
                schema: "dbo",
                table: "Contact",
                column: "CustomerId",
                principalSchema: "dbo",
                principalTable: "Customer",
                principalColumn: "CustomerNumber",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EftTransaction_Account_AccountId",
                schema: "dbo",
                table: "EftTransaction",
                column: "AccountId",
                principalSchema: "dbo",
                principalTable: "Account",
                principalColumn: "AccountNumber",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Account_Customer_CustomerId",
                schema: "dbo",
                table: "Account");

            migrationBuilder.DropForeignKey(
                name: "FK_AccountTransaction_Account_AccountId",
                schema: "dbo",
                table: "AccountTransaction");

            migrationBuilder.DropForeignKey(
                name: "FK_Address_Customer_CustomerId",
                schema: "dbo",
                table: "Address");

            migrationBuilder.DropForeignKey(
                name: "FK_Contact_Customer_CustomerId",
                schema: "dbo",
                table: "Contact");

            migrationBuilder.DropForeignKey(
                name: "FK_EftTransaction_Account_AccountId",
                schema: "dbo",
                table: "EftTransaction");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Customer",
                schema: "dbo",
                table: "Customer");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Account",
                schema: "dbo",
                table: "Account");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                schema: "dbo",
                table: "Customer",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                schema: "dbo",
                table: "Account",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Customer",
                schema: "dbo",
                table: "Customer",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Account",
                schema: "dbo",
                table: "Account",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Account_Customer_CustomerId",
                schema: "dbo",
                table: "Account",
                column: "CustomerId",
                principalSchema: "dbo",
                principalTable: "Customer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AccountTransaction_Account_AccountId",
                schema: "dbo",
                table: "AccountTransaction",
                column: "AccountId",
                principalSchema: "dbo",
                principalTable: "Account",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Address_Customer_CustomerId",
                schema: "dbo",
                table: "Address",
                column: "CustomerId",
                principalSchema: "dbo",
                principalTable: "Customer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Contact_Customer_CustomerId",
                schema: "dbo",
                table: "Contact",
                column: "CustomerId",
                principalSchema: "dbo",
                principalTable: "Customer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EftTransaction_Account_AccountId",
                schema: "dbo",
                table: "EftTransaction",
                column: "AccountId",
                principalSchema: "dbo",
                principalTable: "Account",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
