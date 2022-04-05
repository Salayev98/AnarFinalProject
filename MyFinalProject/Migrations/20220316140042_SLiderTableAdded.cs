using Microsoft.EntityFrameworkCore.Migrations;

namespace MyFinalProject.Migrations
{
    public partial class SLiderTableAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Sliders",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Image = table.Column<string>(maxLength: 200, nullable: false),
                    Title1 = table.Column<string>(maxLength: 50, nullable: false),
                    Title2 = table.Column<string>(maxLength: 50, nullable: false),
                    Desc1 = table.Column<string>(maxLength: 100, nullable: false),
                    Desc2 = table.Column<string>(maxLength: 100, nullable: false),
                    BtnUrl = table.Column<string>(maxLength: 200, nullable: false),
                    BtnText = table.Column<string>(maxLength: 50, nullable: false),
                    Order = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sliders", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Sliders");
        }
    }
}
