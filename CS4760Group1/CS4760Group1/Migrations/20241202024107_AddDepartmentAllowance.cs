using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CS4760Group1.Migrations
{
    /// <inheritdoc />
    public partial class AddDepartmentAllowance : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AddColumn<decimal>(
                name: "Allowance",
                table: "Department",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Allowance",
                table: "Department");
        }
    }
}
