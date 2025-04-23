using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StarColonies.Infrastructures.Migrations
{
    /// <inheritdoc />
    public partial class update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Colonies_AspNetUsers_OwnerId",
                table: "Colonies");

            migrationBuilder.DropForeignKey(
                name: "FK_ColoniesMembers_AspNetUsers_ColonistId",
                table: "ColoniesMembers");

            migrationBuilder.DropForeignKey(
                name: "FK_ColoniesMembers_Colonies_ColonyId",
                table: "ColoniesMembers");

            migrationBuilder.DropForeignKey(
                name: "FK_Enemies_Types_TypeId",
                table: "Enemies");

            migrationBuilder.DropForeignKey(
                name: "FK_EnemyEntityMissionEntity_Enemies_EnemiesId",
                table: "EnemyEntityMissionEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_EnemyEntityMissionEntity_Missions_MissionsId",
                table: "EnemyEntityMissionEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_Inventory_Items_ItemId",
                table: "Inventory");

            migrationBuilder.DropForeignKey(
                name: "FK_Items_Effects_EffectId",
                table: "Items");

            migrationBuilder.DropForeignKey(
                name: "FK_MissionExecutions_Colonies_ColonieId",
                table: "MissionExecutions");

            migrationBuilder.DropForeignKey(
                name: "FK_MissionExecutions_Missions_MissionId",
                table: "MissionExecutions");

            migrationBuilder.DropForeignKey(
                name: "FK_Missions_Planets_PlanetId",
                table: "Missions");

            migrationBuilder.DropForeignKey(
                name: "FK_Rewarded_Items_ItemId",
                table: "Rewarded");

            migrationBuilder.DropForeignKey(
                name: "FK_Rewarded_Missions_MissionId",
                table: "Rewarded");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Types",
                table: "Types");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Planets",
                table: "Planets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Missions",
                table: "Missions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MissionExecutions",
                table: "MissionExecutions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Items",
                table: "Items");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Enemies",
                table: "Enemies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Effects",
                table: "Effects");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ColoniesMembers",
                table: "ColoniesMembers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Colonies",
                table: "Colonies");

            migrationBuilder.RenameTable(
                name: "Types",
                newName: "Type");

            migrationBuilder.RenameTable(
                name: "Planets",
                newName: "Planet");

            migrationBuilder.RenameTable(
                name: "Missions",
                newName: "Mission");

            migrationBuilder.RenameTable(
                name: "MissionExecutions",
                newName: "MissionExecution");

            migrationBuilder.RenameTable(
                name: "Items",
                newName: "Item");

            migrationBuilder.RenameTable(
                name: "Enemies",
                newName: "Enemy");

            migrationBuilder.RenameTable(
                name: "Effects",
                newName: "Effect");

            migrationBuilder.RenameTable(
                name: "ColoniesMembers",
                newName: "ColonyMember");

            migrationBuilder.RenameTable(
                name: "Colonies",
                newName: "Colony");

            migrationBuilder.RenameIndex(
                name: "IX_Planets_Name",
                table: "Planet",
                newName: "IX_Planet_Name");

            migrationBuilder.RenameIndex(
                name: "IX_Missions_PlanetId",
                table: "Mission",
                newName: "IX_Mission_PlanetId");

            migrationBuilder.RenameIndex(
                name: "IX_MissionExecutions_MissionId",
                table: "MissionExecution",
                newName: "IX_MissionExecution_MissionId");

            migrationBuilder.RenameIndex(
                name: "IX_MissionExecutions_ColonieId",
                table: "MissionExecution",
                newName: "IX_MissionExecution_ColonieId");

            migrationBuilder.RenameIndex(
                name: "IX_Items_Name",
                table: "Item",
                newName: "IX_Item_Name");

            migrationBuilder.RenameIndex(
                name: "IX_Items_EffectId",
                table: "Item",
                newName: "IX_Item_EffectId");

            migrationBuilder.RenameIndex(
                name: "IX_Enemies_TypeId",
                table: "Enemy",
                newName: "IX_Enemy_TypeId");

            migrationBuilder.RenameIndex(
                name: "IX_ColoniesMembers_ColonistId",
                table: "ColonyMember",
                newName: "IX_ColonyMember_ColonistId");

            migrationBuilder.RenameIndex(
                name: "IX_Colonies_OwnerId",
                table: "Colony",
                newName: "IX_Colony_OwnerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Type",
                table: "Type",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Planet",
                table: "Planet",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Mission",
                table: "Mission",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MissionExecution",
                table: "MissionExecution",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Item",
                table: "Item",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Enemy",
                table: "Enemy",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Effect",
                table: "Effect",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ColonyMember",
                table: "ColonyMember",
                columns: new[] { "ColonyId", "ColonistId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Colony",
                table: "Colony",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Colony_AspNetUsers_OwnerId",
                table: "Colony",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ColonyMember_AspNetUsers_ColonistId",
                table: "ColonyMember",
                column: "ColonistId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ColonyMember_Colony_ColonyId",
                table: "ColonyMember",
                column: "ColonyId",
                principalTable: "Colony",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Enemy_Type_TypeId",
                table: "Enemy",
                column: "TypeId",
                principalTable: "Type",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EnemyEntityMissionEntity_Enemy_EnemiesId",
                table: "EnemyEntityMissionEntity",
                column: "EnemiesId",
                principalTable: "Enemy",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EnemyEntityMissionEntity_Mission_MissionsId",
                table: "EnemyEntityMissionEntity",
                column: "MissionsId",
                principalTable: "Mission",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Inventory_Item_ItemId",
                table: "Inventory",
                column: "ItemId",
                principalTable: "Item",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Item_Effect_EffectId",
                table: "Item",
                column: "EffectId",
                principalTable: "Effect",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Mission_Planet_PlanetId",
                table: "Mission",
                column: "PlanetId",
                principalTable: "Planet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MissionExecution_Colony_ColonieId",
                table: "MissionExecution",
                column: "ColonieId",
                principalTable: "Colony",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MissionExecution_Mission_MissionId",
                table: "MissionExecution",
                column: "MissionId",
                principalTable: "Mission",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Rewarded_Item_ItemId",
                table: "Rewarded",
                column: "ItemId",
                principalTable: "Item",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Rewarded_Mission_MissionId",
                table: "Rewarded",
                column: "MissionId",
                principalTable: "Mission",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Colony_AspNetUsers_OwnerId",
                table: "Colony");

            migrationBuilder.DropForeignKey(
                name: "FK_ColonyMember_AspNetUsers_ColonistId",
                table: "ColonyMember");

            migrationBuilder.DropForeignKey(
                name: "FK_ColonyMember_Colony_ColonyId",
                table: "ColonyMember");

            migrationBuilder.DropForeignKey(
                name: "FK_Enemy_Type_TypeId",
                table: "Enemy");

            migrationBuilder.DropForeignKey(
                name: "FK_EnemyEntityMissionEntity_Enemy_EnemiesId",
                table: "EnemyEntityMissionEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_EnemyEntityMissionEntity_Mission_MissionsId",
                table: "EnemyEntityMissionEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_Inventory_Item_ItemId",
                table: "Inventory");

            migrationBuilder.DropForeignKey(
                name: "FK_Item_Effect_EffectId",
                table: "Item");

            migrationBuilder.DropForeignKey(
                name: "FK_Mission_Planet_PlanetId",
                table: "Mission");

            migrationBuilder.DropForeignKey(
                name: "FK_MissionExecution_Colony_ColonieId",
                table: "MissionExecution");

            migrationBuilder.DropForeignKey(
                name: "FK_MissionExecution_Mission_MissionId",
                table: "MissionExecution");

            migrationBuilder.DropForeignKey(
                name: "FK_Rewarded_Item_ItemId",
                table: "Rewarded");

            migrationBuilder.DropForeignKey(
                name: "FK_Rewarded_Mission_MissionId",
                table: "Rewarded");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Type",
                table: "Type");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Planet",
                table: "Planet");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MissionExecution",
                table: "MissionExecution");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Mission",
                table: "Mission");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Item",
                table: "Item");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Enemy",
                table: "Enemy");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Effect",
                table: "Effect");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ColonyMember",
                table: "ColonyMember");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Colony",
                table: "Colony");

            migrationBuilder.RenameTable(
                name: "Type",
                newName: "Types");

            migrationBuilder.RenameTable(
                name: "Planet",
                newName: "Planets");

            migrationBuilder.RenameTable(
                name: "MissionExecution",
                newName: "MissionExecutions");

            migrationBuilder.RenameTable(
                name: "Mission",
                newName: "Missions");

            migrationBuilder.RenameTable(
                name: "Item",
                newName: "Items");

            migrationBuilder.RenameTable(
                name: "Enemy",
                newName: "Enemies");

            migrationBuilder.RenameTable(
                name: "Effect",
                newName: "Effects");

            migrationBuilder.RenameTable(
                name: "ColonyMember",
                newName: "ColoniesMembers");

            migrationBuilder.RenameTable(
                name: "Colony",
                newName: "Colonies");

            migrationBuilder.RenameIndex(
                name: "IX_Planet_Name",
                table: "Planets",
                newName: "IX_Planets_Name");

            migrationBuilder.RenameIndex(
                name: "IX_MissionExecution_MissionId",
                table: "MissionExecutions",
                newName: "IX_MissionExecutions_MissionId");

            migrationBuilder.RenameIndex(
                name: "IX_MissionExecution_ColonieId",
                table: "MissionExecutions",
                newName: "IX_MissionExecutions_ColonieId");

            migrationBuilder.RenameIndex(
                name: "IX_Mission_PlanetId",
                table: "Missions",
                newName: "IX_Missions_PlanetId");

            migrationBuilder.RenameIndex(
                name: "IX_Item_Name",
                table: "Items",
                newName: "IX_Items_Name");

            migrationBuilder.RenameIndex(
                name: "IX_Item_EffectId",
                table: "Items",
                newName: "IX_Items_EffectId");

            migrationBuilder.RenameIndex(
                name: "IX_Enemy_TypeId",
                table: "Enemies",
                newName: "IX_Enemies_TypeId");

            migrationBuilder.RenameIndex(
                name: "IX_ColonyMember_ColonistId",
                table: "ColoniesMembers",
                newName: "IX_ColoniesMembers_ColonistId");

            migrationBuilder.RenameIndex(
                name: "IX_Colony_OwnerId",
                table: "Colonies",
                newName: "IX_Colonies_OwnerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Types",
                table: "Types",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Planets",
                table: "Planets",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MissionExecutions",
                table: "MissionExecutions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Missions",
                table: "Missions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Items",
                table: "Items",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Enemies",
                table: "Enemies",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Effects",
                table: "Effects",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ColoniesMembers",
                table: "ColoniesMembers",
                columns: new[] { "ColonyId", "ColonistId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Colonies",
                table: "Colonies",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Colonies_AspNetUsers_OwnerId",
                table: "Colonies",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ColoniesMembers_AspNetUsers_ColonistId",
                table: "ColoniesMembers",
                column: "ColonistId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ColoniesMembers_Colonies_ColonyId",
                table: "ColoniesMembers",
                column: "ColonyId",
                principalTable: "Colonies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Enemies_Types_TypeId",
                table: "Enemies",
                column: "TypeId",
                principalTable: "Types",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EnemyEntityMissionEntity_Enemies_EnemiesId",
                table: "EnemyEntityMissionEntity",
                column: "EnemiesId",
                principalTable: "Enemies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EnemyEntityMissionEntity_Missions_MissionsId",
                table: "EnemyEntityMissionEntity",
                column: "MissionsId",
                principalTable: "Missions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Inventory_Items_ItemId",
                table: "Inventory",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Effects_EffectId",
                table: "Items",
                column: "EffectId",
                principalTable: "Effects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MissionExecutions_Colonies_ColonieId",
                table: "MissionExecutions",
                column: "ColonieId",
                principalTable: "Colonies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MissionExecutions_Missions_MissionId",
                table: "MissionExecutions",
                column: "MissionId",
                principalTable: "Missions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Missions_Planets_PlanetId",
                table: "Missions",
                column: "PlanetId",
                principalTable: "Planets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Rewarded_Items_ItemId",
                table: "Rewarded",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Rewarded_Missions_MissionId",
                table: "Rewarded",
                column: "MissionId",
                principalTable: "Missions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
