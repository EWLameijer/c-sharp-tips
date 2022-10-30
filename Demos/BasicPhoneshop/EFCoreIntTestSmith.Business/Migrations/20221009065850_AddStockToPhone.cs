using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFCoreIntTestSmith.Business.Migrations;

public partial class AddStockToPhone : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<int>(
            name: "Stock",
            table: "Phones",
            type: "int",
            nullable: false,
            defaultValue: 0);

        migrationBuilder.UpdateData(
            table: "Phones",
            keyColumn: "Id",
            keyValue: 1,
            columns: new[] { "Price", "Stock" },
            values: new object[] { 100m, 20 });

        migrationBuilder.UpdateData(
            table: "Phones",
            keyColumn: "Id",
            keyValue: 2,
            columns: new[] { "Price", "Stock" },
            values: new object[] { 200m, 25 });

        migrationBuilder.UpdateData(
            table: "Phones",
            keyColumn: "Id",
            keyValue: 3,
            column: "Price",
            value: 500m);

        migrationBuilder.UpdateData(
            table: "Phones",
            keyColumn: "Id",
            keyValue: 4,
            columns: new[] { "Price", "Stock" },
            values: new object[] { 400m, 15 });

        migrationBuilder.UpdateData(
            table: "Phones",
            keyColumn: "Id",
            keyValue: 5,
            columns: new[] { "Price", "Stock" },
            values: new object[] { 300m, 3 });
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "Stock",
            table: "Phones");

        migrationBuilder.UpdateData(
            table: "Phones",
            keyColumn: "Id",
            keyValue: 1,
            column: "Price",
            value: 0m);

        migrationBuilder.UpdateData(
            table: "Phones",
            keyColumn: "Id",
            keyValue: 2,
            column: "Price",
            value: 0m);

        migrationBuilder.UpdateData(
            table: "Phones",
            keyColumn: "Id",
            keyValue: 3,
            column: "Price",
            value: 0m);

        migrationBuilder.UpdateData(
            table: "Phones",
            keyColumn: "Id",
            keyValue: 4,
            column: "Price",
            value: 0m);

        migrationBuilder.UpdateData(
            table: "Phones",
            keyColumn: "Id",
            keyValue: 5,
            column: "Price",
            value: 0m);
    }
}