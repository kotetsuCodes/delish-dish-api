using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DelishDishApi.Data.Migrations
{
    public partial class AddRecipeInstructionsAndImageUrl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Recipes",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "RecipeInstruction",
                columns: table => new
                {
                    RecipeInstructionId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    InstructionOrder = table.Column<int>(nullable: false),
                    Content = table.Column<string>(nullable: true),
                    RecipeId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipeInstruction", x => x.RecipeInstructionId);
                    table.ForeignKey(
                        name: "FK_RecipeInstruction_Recipes_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "Recipes",
                        principalColumn: "RecipeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RecipeInstruction_RecipeId",
                table: "RecipeInstruction",
                column: "RecipeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RecipeInstruction");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Recipes");
        }
    }
}
