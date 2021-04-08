using Microsoft.EntityFrameworkCore.Migrations;

namespace EF_Core_Transactions.Migrations
{
    public partial class Third : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "payment_Type",
                table: "Orders",
                newName: "Payment_Type");

            migrationBuilder.RenameColumn(
                name: "payment_Id",
                table: "Orders",
                newName: "Payment_Id");

            migrationBuilder.RenameColumn(
                name: "payment_Card",
                table: "Orders",
                newName: "Payment_Card");

            migrationBuilder.AddColumn<int>(
                name: "PaymentId",
                table: "Orders",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Payment_Id", "Payment_Type" },
                values: new object[] { 2, "Card" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaymentId",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "Payment_Type",
                table: "Orders",
                newName: "payment_Type");

            migrationBuilder.RenameColumn(
                name: "Payment_Id",
                table: "Orders",
                newName: "payment_Id");

            migrationBuilder.RenameColumn(
                name: "Payment_Card",
                table: "Orders",
                newName: "payment_Card");
        }
    }
}
