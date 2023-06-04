using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MagicVilla_API.Migrations
{
    /// <inheritdoc />
    public partial class AddDataToVillaTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Villas",
                columns: new[] { "Id", "Amenidad", "Area", "CreatedDate", "Detail", "ImageUrl", "Name", "Occupants", "Rate", "UpdateDate" },
                values: new object[,]
                {
                    { 1, "", 100, new DateTime(2023, 6, 3, 21, 55, 38, 686, DateTimeKind.Local).AddTicks(2521), "Villa con vista al mar", "", "Villa Real", 5, 15.0, new DateTime(2023, 6, 3, 21, 55, 38, 686, DateTimeKind.Local).AddTicks(2532) },
                    { 2, "", 100, new DateTime(2023, 6, 3, 21, 55, 38, 686, DateTimeKind.Local).AddTicks(2534), "Villa en la cordillera de los Andes", "", "Villa Alta Cumbre", 5, 15.0, new DateTime(2023, 6, 3, 21, 55, 38, 686, DateTimeKind.Local).AddTicks(2535) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
