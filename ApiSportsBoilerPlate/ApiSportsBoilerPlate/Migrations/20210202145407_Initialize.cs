using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ApiSportsBoilerPlate.Migrations
{
    public partial class Initialize : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Club",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RowCreatedById = table.Column<long>(nullable: false),
                    RowModifiedById = table.Column<long>(nullable: false),
                    RowCreatedDateTimeUtc = table.Column<DateTime>(nullable: false),
                    RowModifiedDateTimeUtc = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Image = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Rate = table.Column<int>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Club", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Person",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RowCreatedById = table.Column<long>(nullable: false),
                    RowModifiedById = table.Column<long>(nullable: false),
                    RowCreatedDateTimeUtc = table.Column<DateTime>(nullable: false),
                    RowModifiedDateTimeUtc = table.Column<DateTime>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    DateOfBirth = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Person", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PersonClub",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RowCreatedById = table.Column<long>(nullable: false),
                    RowModifiedById = table.Column<long>(nullable: false),
                    RowCreatedDateTimeUtc = table.Column<DateTime>(nullable: false),
                    RowModifiedDateTimeUtc = table.Column<DateTime>(nullable: false),
                    ClubId = table.Column<int>(nullable: false),
                    PersonId = table.Column<int>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonClub", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PersonClub_Club_ClubId",
                        column: x => x.ClubId,
                        principalTable: "Club",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonClub_Person_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Person",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PersonClub_ClubId",
                table: "PersonClub",
                column: "ClubId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonClub_PersonId",
                table: "PersonClub",
                column: "PersonId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PersonClub");

            migrationBuilder.DropTable(
                name: "Club");

            migrationBuilder.DropTable(
                name: "Person");
        }
    }
}
