using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Minoristas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FirstName = table.Column<string>(type: "TEXT", nullable: false),
                    LastName = table.Column<string>(type: "TEXT", nullable: false),
                    NameAccount = table.Column<string>(type: "TEXT", nullable: false),
                    Password = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    Dni = table.Column<int>(type: "INTEGER", nullable: false),
                    PhoneNumber = table.Column<string>(type: "TEXT", nullable: false),
                    Address = table.Column<string>(type: "TEXT", nullable: false),
                    TypeUser = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Minoristas", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Minoristas",
                columns: new[] { "Id", "Address", "Dni", "Email", "FirstName", "LastName", "NameAccount", "Password", "PhoneNumber", "TypeUser" },
                values: new object[,]
                {
                    { 1, "Santafe 1234", 12323456, "facu@hotmail.com", "Facundo", "Solari", "facu", "facu123", "+543413500300", "Minorista" },
                    { 2, "San Lorenzo 3624", 34732713, "cris@hotmail.com", "Cristian", "Ninotti", "cris", "cris123", "+543415155611", "Minorista" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Minoristas");
        }
    }
}
