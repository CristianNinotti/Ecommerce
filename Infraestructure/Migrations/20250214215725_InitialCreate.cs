using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Minoristas",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Minoristas",
                keyColumn: "Id",
                keyValue: 2);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Minoristas",
                columns: new[] { "Id", "Address", "Dni", "Email", "FirstName", "LastName", "NameAccount", "Password", "PhoneNumber", "TypeUser" },
                values: new object[,]
                {
                    { 1, "Santafe 1234", 12323456, "facu@hotmail.com", "Facundo", "Solari", "facu", "facu123", "+543413500300", "Minorista" },
                    { 2, "San Lorenzo 3624", 34732713, "cris@hotmail.com", "Cristian", "Ninotti", "cris", "cris123", "+543415155611", "Minorista" }
                });
        }
    }
}
