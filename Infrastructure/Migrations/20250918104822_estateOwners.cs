using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class estateOwners : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EstateOwner_EquineEstates_EstateId",
                table: "EstateOwner");

            migrationBuilder.DropForeignKey(
                name: "FK_EstateOwner_Users_UserId",
                table: "EstateOwner");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EstateOwner",
                table: "EstateOwner");

            migrationBuilder.RenameTable(
                name: "EstateOwner",
                newName: "EstateOwners");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "EstateOwners",
                newName: "OwnershipId");

            migrationBuilder.RenameIndex(
                name: "IX_EstateOwner_UserId",
                table: "EstateOwners",
                newName: "IX_EstateOwners_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_EstateOwner_EstateId",
                table: "EstateOwners",
                newName: "IX_EstateOwners_EstateId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EstateOwners",
                table: "EstateOwners",
                column: "OwnershipId");

            migrationBuilder.AddForeignKey(
                name: "FK_EstateOwners_EquineEstates_EstateId",
                table: "EstateOwners",
                column: "EstateId",
                principalTable: "EquineEstates",
                principalColumn: "EstateId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EstateOwners_Users_UserId",
                table: "EstateOwners",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EstateOwners_EquineEstates_EstateId",
                table: "EstateOwners");

            migrationBuilder.DropForeignKey(
                name: "FK_EstateOwners_Users_UserId",
                table: "EstateOwners");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EstateOwners",
                table: "EstateOwners");

            migrationBuilder.RenameTable(
                name: "EstateOwners",
                newName: "EstateOwner");

            migrationBuilder.RenameColumn(
                name: "OwnershipId",
                table: "EstateOwner",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_EstateOwners_UserId",
                table: "EstateOwner",
                newName: "IX_EstateOwner_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_EstateOwners_EstateId",
                table: "EstateOwner",
                newName: "IX_EstateOwner_EstateId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EstateOwner",
                table: "EstateOwner",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EstateOwner_EquineEstates_EstateId",
                table: "EstateOwner",
                column: "EstateId",
                principalTable: "EquineEstates",
                principalColumn: "EstateId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EstateOwner_Users_UserId",
                table: "EstateOwner",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
