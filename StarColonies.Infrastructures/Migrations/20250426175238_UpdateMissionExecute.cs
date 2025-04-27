using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StarColonies.Infrastructures.Migrations
{
    /// <inheritdoc />
    public partial class UpdateMissionExecute : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MissionExecution_Colony_ColonieId",
                table: "MissionExecution");

            migrationBuilder.RenameColumn(
                name: "ColonieId",
                table: "MissionExecution",
                newName: "ColonyId");

            migrationBuilder.RenameIndex(
                name: "IX_MissionExecution_ColonieId",
                table: "MissionExecution",
                newName: "IX_MissionExecution_ColonyId");

            migrationBuilder.AddColumn<bool>(
                name: "LivingColony",
                table: "MissionExecution",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "OvercomingMission",
                table: "MissionExecution",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_MissionExecution_Colony_ColonyId",
                table: "MissionExecution",
                column: "ColonyId",
                principalTable: "Colony",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MissionExecution_Colony_ColonyId",
                table: "MissionExecution");

            migrationBuilder.DropColumn(
                name: "LivingColony",
                table: "MissionExecution");

            migrationBuilder.DropColumn(
                name: "OvercomingMission",
                table: "MissionExecution");

            migrationBuilder.RenameColumn(
                name: "ColonyId",
                table: "MissionExecution",
                newName: "ColonieId");

            migrationBuilder.RenameIndex(
                name: "IX_MissionExecution_ColonyId",
                table: "MissionExecution",
                newName: "IX_MissionExecution_ColonieId");

            migrationBuilder.AddForeignKey(
                name: "FK_MissionExecution_Colony_ColonieId",
                table: "MissionExecution",
                column: "ColonieId",
                principalTable: "Colony",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
