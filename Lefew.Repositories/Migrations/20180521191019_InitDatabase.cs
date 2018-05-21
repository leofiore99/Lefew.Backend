using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Lefew.Repositories.Migrations
{
    public partial class InitDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Distributor",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Email = table.Column<string>(type: "varchar(150)", nullable: false),
                    Tries = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Distributor", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Prospect",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DistributorId = table.Column<int>(nullable: true),
                    Email = table.Column<string>(type: "varchar(150)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prospect", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Prospect_Distributor_DistributorId",
                        column: x => x.DistributorId,
                        principalTable: "Distributor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Prospect_DistributorId",
                table: "Prospect",
                column: "DistributorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Prospect");

            migrationBuilder.DropTable(
                name: "Distributor");
        }
    }
}
