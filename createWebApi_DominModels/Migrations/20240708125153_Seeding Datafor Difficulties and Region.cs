using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace createWebApi_DominModels.Migrations
{
    /// <inheritdoc />
    public partial class SeedingDataforDifficultiesandRegion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Difficulties",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("19c49e0c-ad0f-40a1-a446-c8baac4e4119"), "Medium" },
                    { new Guid("c6105626-2ed0-4f01-ba66-e9f8943e3c3c"), "Easy" },
                    { new Guid("f03ae466-c08f-4b85-80a9-22c55bcdaec7"), "Hard" }
                });

            migrationBuilder.InsertData(
                table: "Regions",
                columns: new[] { "Id", "Code", "Name", "RegionImageUrl" },
                values: new object[,]
                {
                    { new Guid("2776d88d-b0c7-467c-ad85-1eb730e083f0"), "GSKP", "金孫韓廚義大利麵", "https://upssmile.com/wp-content/uploads/2022/12/20221205-IMG_5577.jpg" },
                    { new Guid("5351b315-43b3-4f61-83ec-c044efd9a650"), "Coco", "Coco Brother 椰兄", "https://leelife.tw/wp-content/uploads/2023/04/S__10117148.jpg" },
                    { new Guid("b09924c5-a86d-4d5c-abd2-ede7587fcf25"), "Parko", "Parko Parco 牛肚包義大利小酒館", "https://angelababy.tw/wp-content/uploads/2022/01/DSC09440.jpg" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("19c49e0c-ad0f-40a1-a446-c8baac4e4119"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("c6105626-2ed0-4f01-ba66-e9f8943e3c3c"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("f03ae466-c08f-4b85-80a9-22c55bcdaec7"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("2776d88d-b0c7-467c-ad85-1eb730e083f0"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("5351b315-43b3-4f61-83ec-c044efd9a650"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("b09924c5-a86d-4d5c-abd2-ede7587fcf25"));
        }
    }
}
