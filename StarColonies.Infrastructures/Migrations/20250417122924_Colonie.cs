using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StarColonies.Infrastructures.Migrations
{
    /// <inheritdoc />
    public partial class Colonie : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Colonies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OwnerId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Colonies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Colonies_AspNetUsers_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ColoniesMembers",
                columns: table => new
                {
                    ColonieId = table.Column<int>(type: "int", nullable: false),
                    ColonistId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ColoniesMembers", x => new { x.ColonieId, x.ColonistId });
                    table.ForeignKey(
                        name: "FK_ColoniesMembers_AspNetUsers_ColonistId",
                        column: x => x.ColonistId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ColoniesMembers_Colonies_ColonieId",
                        column: x => x.ColonieId,
                        principalTable: "Colonies",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MissionExecutions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ColonieId = table.Column<int>(type: "int", nullable: false),
                    MissionId = table.Column<int>(type: "int", nullable: false),
                    ExecutedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    IsSuccess = table.Column<bool>(type: "bit", nullable: false),
                    RewardedCoins = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MissionExecutions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MissionExecutions_Colonies_ColonieId",
                        column: x => x.ColonieId,
                        principalTable: "Colonies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MissionExecutions_Missions_MissionId",
                        column: x => x.MissionId,
                        principalTable: "Missions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Colonies_OwnerId",
                table: "Colonies",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_ColoniesMembers_ColonistId",
                table: "ColoniesMembers",
                column: "ColonistId");

            migrationBuilder.CreateIndex(
                name: "IX_MissionExecutions_ColonieId",
                table: "MissionExecutions",
                column: "ColonieId");

            migrationBuilder.CreateIndex(
                name: "IX_MissionExecutions_MissionId",
                table: "MissionExecutions",
                column: "MissionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ColoniesMembers");

            migrationBuilder.DropTable(
                name: "MissionExecutions");

            migrationBuilder.DropTable(
                name: "Colonies");
        }
    }
}
