using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Entities.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "People",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    Surname = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    CountryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_People", x => x.Id);
                    table.ForeignKey(
                        name: "FK_People_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("0e63a577-29c5-4c94-b7a4-dbf5eb22ad71"), "France" },
                    { new Guid("0e63a577-29c5-4c94-b7a4-dbf5eb22ad72"), "Germany" },
                    { new Guid("1a1a6b6c-e200-4610-b414-582729b06891"), "Canada" },
                    { new Guid("291a5e65-34cf-4469-a38c-7b894a4cdf3a"), "Italy" },
                    { new Guid("347f163c-84e5-4027-9238-a7c5492e5e51"), "Japan" },
                    { new Guid("597cf1a0-8839-49c2-b22c-0720f4849ea9"), "China" },
                    { new Guid("83e3a561-6a9a-47d0-a36c-3a4be2d2d58c"), "Spain" },
                    { new Guid("d21d5ba9-2944-4c6f-a679-da6559ca57cb"), "Brazil" },
                    { new Guid("d9b6c0a0-0b0a-4e1a-8f0a-0b9e8f0a0b9e"), "Russia" },
                    { new Guid("e2140b54-4f77-4006-9346-9ecb235c25eb"), "USA" }
                });

            migrationBuilder.InsertData(
                table: "People",
                columns: new[] { "Id", "Address", "BirthDate", "CountryId", "Email", "Name", "Surname" },
                values: new object[,]
                {
                    { new Guid("2093a754-b63a-41ec-97f8-044472f31a1f"), "0110 3rd Plaza", new DateTime(2003, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("0e63a577-29c5-4c94-b7a4-dbf5eb22ad72"), "lol@mail.ru", "Petr", "Petrov" },
                    { new Guid("485b9fe7-4d1a-4857-ac46-629ab1afb9b0"), null, new DateTime(1990, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("d9b6c0a0-0b0a-4e1a-8f0a-0b9e8f0a0b9e"), "kek@mail.ru", "Ivan", "Ivanov" },
                    { new Guid("4e0f16b2-5c69-4387-9e28-3d4e33cc9857"), "265 Tomscot Circle", new DateTime(1999, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("0e63a577-29c5-4c94-b7a4-dbf5eb22ad71"), "keklol@mail.ru", "Sergey", "Sergeev" },
                    { new Guid("7b9b1720-fd26-415a-a592-80776cc30b59"), "269 new Tomscot Circle", new DateTime(1995, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("0e63a577-29c5-4c94-b7a4-dbf5eb22ad71"), "mymail@mail.ru", "John", "Sidorov" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_People_CountryId",
                table: "People",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_People_Email",
                table: "People",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "People");

            migrationBuilder.DropTable(
                name: "Countries");
        }
    }
}
