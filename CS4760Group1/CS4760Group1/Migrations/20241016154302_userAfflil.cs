using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CS4760Group1.Migrations
{
    /// <inheritdoc />
    public partial class userAfflil : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "tempField",
                table: "UserAffiliations",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "tempField",
                table: "UserAffiliations");
        }
    }
}
