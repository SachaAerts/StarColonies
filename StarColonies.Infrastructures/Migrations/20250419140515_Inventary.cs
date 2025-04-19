using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StarColonies.Infrastructures.Migrations
{
    /// <inheritdoc />
    public partial class Inventary : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ColonistItems");

            migrationBuilder.CreateTable(
                name: "Inventory",
                columns: table => new
                {
                    ColonistId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ItemId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inventory", x => new { x.ColonistId, x.ItemId });
                    table.ForeignKey(
                        name: "FK_Inventory_AspNetUsers_ColonistId",
                        column: x => x.ColonistId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Inventory_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Inventory_ItemId",
                table: "Inventory",
                column: "ItemId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Inventory");

            migrationBuilder.CreateTable(
                name: "ColonistItems",
                columns: table => new
                {
                    ColonistId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ItemId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ColonistItems", x => new { x.ColonistId, x.ItemId });
                    table.ForeignKey(
                        name: "FK_ColonistItems_AspNetUsers_ColonistId",
                        column: x => x.ColonistId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ColonistItems_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ColonistItems_ItemId",
                table: "ColonistItems",
                column: "ItemId");
        }
    }
}
