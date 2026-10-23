using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SalesSystem.Infra.Migrations
{
    /// <inheritdoc />
    public partial class AddData_OrderStatuses : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
            table: "OrderStatuses",
            columns: ["Id", "Name"],
            values: new object[,]
            {
                { 1, "Pendente" },
                { 2, "Pago" },
                { 3, "Cancelado" }
            });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
           table: "OrderStatuses",
           keyColumn: "Id",
           keyValues: new object[] { 1, 2, 3 });
        }
    }
}
