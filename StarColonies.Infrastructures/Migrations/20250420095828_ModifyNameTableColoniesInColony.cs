using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StarColonies.Infrastructures.Migrations
{
    /// <inheritdoc />
    public partial class ModifyNameTableColoniesInColony : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ColoniesMembers_Colonies_ColonieId",
                table: "ColoniesMembers");

            migrationBuilder.RenameColumn(
                name: "ColonieId",
                table: "ColoniesMembers",
                newName: "ColonyId");

            migrationBuilder.AddForeignKey(
                name: "FK_ColoniesMembers_Colonies_ColonyId",
                table: "ColoniesMembers",
                column: "ColonyId",
                principalTable: "Colonies",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ColoniesMembers_Colonies_ColonyId",
                table: "ColoniesMembers");

            migrationBuilder.RenameColumn(
                name: "ColonyId",
                table: "ColoniesMembers",
                newName: "ColonieId");

            migrationBuilder.AddForeignKey(
                name: "FK_ColoniesMembers_Colonies_ColonieId",
                table: "ColoniesMembers",
                column: "ColonieId",
                principalTable: "Colonies",
                principalColumn: "Id");
        }
    }
}
