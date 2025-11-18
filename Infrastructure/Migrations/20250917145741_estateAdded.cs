using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class estateAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EquineEstates",
                columns: table => new
                {
                    EstateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    _estatename = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EstateName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NormalizedEstateName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EstateDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HorseCapacity = table.Column<int>(type: "int", nullable: false),
                    CurrentBalance = table.Column<int>(type: "int", nullable: false),
                    IsSytemEstate = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EquineEstates", x => x.EstateId);
                });

            migrationBuilder.CreateTable(
                name: "EstateOwner",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EquineEstateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsPrimaryOwner = table.Column<bool>(type: "bit", nullable: false),
                    EstateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstateOwner", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EstateOwner_EquineEstates_EstateId",
                        column: x => x.EstateId,
                        principalTable: "EquineEstates",
                        principalColumn: "EstateId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EstateOwner_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EstateOwner_EstateId",
                table: "EstateOwner",
                column: "EstateId");

            migrationBuilder.CreateIndex(
                name: "IX_EstateOwner_UserId",
                table: "EstateOwner",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EstateOwner");

            migrationBuilder.DropTable(
                name: "EquineEstates");
        }
    }
}
