using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebScrapData.Migrations
{
    /// <inheritdoc />
    public partial class addreliancetable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Mobile",
                table: "Mobile");

            migrationBuilder.RenameTable(
                name: "Mobile",
                newName: "Mobiles");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Mobiles",
                table: "Mobiles",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Reliance",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MobileName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reliance", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reliance");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Mobiles",
                table: "Mobiles");

            migrationBuilder.RenameTable(
                name: "Mobiles",
                newName: "Mobile");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Mobile",
                table: "Mobile",
                column: "Id");
        }
    }
}
