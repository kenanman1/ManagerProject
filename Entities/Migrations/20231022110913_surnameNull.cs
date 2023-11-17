using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Entities.Migrations
{
    /// <inheritdoc />
    public partial class surnameNull : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("4b3a2d8f-4f9b-4cf9-a8b8-f9792c5f65f5"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("76218353-37f7-4877-a65f-b064d742b83b"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("7e91d66a-380f-4f8f-b51c-d445ac373941"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("887eaf9b-591a-4b2b-8613-855d838e0568"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("b8bd7561-7bd5-4d0a-83b3-c5ceaff5631a"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("d08cacbc-a826-4d80-b875-f57fc500e2b9"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("d59bd064-31a0-4d24-b3ca-7fa6e9ee5d52"));

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "Id",
                keyValue: new Guid("175f8729-91cf-4e81-af13-6211d440c814"));

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "Id",
                keyValue: new Guid("52f2344d-5ab7-4e26-82f1-8e1b172c095f"));

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "Id",
                keyValue: new Guid("736725b9-575f-4147-881a-2e2023e5e201"));

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "Id",
                keyValue: new Guid("f1e421a5-c2c8-4e58-a7d1-a17b4e44d02a"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("0e63a577-29c5-4c94-b7a4-dbf5eb22ad71"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("0e63a577-29c5-4c94-b7a4-dbf5eb22ad72"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("d9b6c0a0-0b0a-4e1a-8f0a-0b9e8f0a0b9e"));

            migrationBuilder.AlterColumn<string>(
                name: "Surname",
                table: "People",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(40)",
                oldMaxLength: 40);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "People",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(40)",
                oldMaxLength: 40,
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Surname",
                table: "People",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(40)",
                oldMaxLength: 40,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "People",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(40)",
                oldMaxLength: 40);

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("0e63a577-29c5-4c94-b7a4-dbf5eb22ad71"), "France" },
                    { new Guid("0e63a577-29c5-4c94-b7a4-dbf5eb22ad72"), "Germany" },
                    { new Guid("4b3a2d8f-4f9b-4cf9-a8b8-f9792c5f65f5"), "Italy" },
                    { new Guid("76218353-37f7-4877-a65f-b064d742b83b"), "USA" },
                    { new Guid("7e91d66a-380f-4f8f-b51c-d445ac373941"), "Spain" },
                    { new Guid("887eaf9b-591a-4b2b-8613-855d838e0568"), "Japan" },
                    { new Guid("b8bd7561-7bd5-4d0a-83b3-c5ceaff5631a"), "Canada" },
                    { new Guid("d08cacbc-a826-4d80-b875-f57fc500e2b9"), "Brazil" },
                    { new Guid("d59bd064-31a0-4d24-b3ca-7fa6e9ee5d52"), "China" },
                    { new Guid("d9b6c0a0-0b0a-4e1a-8f0a-0b9e8f0a0b9e"), "Russia" }
                });

            migrationBuilder.InsertData(
                table: "People",
                columns: new[] { "Id", "Address", "BirthDate", "CountryId", "Email", "Name", "Surname" },
                values: new object[,]
                {
                    { new Guid("175f8729-91cf-4e81-af13-6211d440c814"), "269 new Tomscot Circle", new DateTime(1995, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("0e63a577-29c5-4c94-b7a4-dbf5eb22ad71"), "mymail@mail.ru", "John", "Sidorov" },
                    { new Guid("52f2344d-5ab7-4e26-82f1-8e1b172c095f"), "0110 3rd Plaza", new DateTime(2003, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("0e63a577-29c5-4c94-b7a4-dbf5eb22ad72"), "lol@mail.ru", "Petr", "Petrov" },
                    { new Guid("736725b9-575f-4147-881a-2e2023e5e201"), "265 Tomscot Circle", new DateTime(1999, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("0e63a577-29c5-4c94-b7a4-dbf5eb22ad71"), "keklol@mail.ru", "Sergey", "Sergeev" },
                    { new Guid("f1e421a5-c2c8-4e58-a7d1-a17b4e44d02a"), null, new DateTime(1990, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("d9b6c0a0-0b0a-4e1a-8f0a-0b9e8f0a0b9e"), "kek@mail.ru", "Ivan", "Ivanov" }
                });
        }
    }
}
