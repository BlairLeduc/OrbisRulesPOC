using Microsoft.EntityFrameworkCore.Migrations;

namespace RulesAPI.Migrations
{
    public partial class AddConditionBooleanOperatorType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ConditionBooleanOperatorType",
                table: "Rules",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConditionBooleanOperatorType",
                table: "Rules");
        }
    }
}
