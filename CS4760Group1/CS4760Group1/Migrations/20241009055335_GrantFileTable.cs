using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CS4760Group1.Migrations
{
    /// <inheritdoc />
    public partial class GrantFileTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropColumn(
            //    name: "College",
            //    table: "Department");

            //migrationBuilder.AddColumn<int>(
            //    name: "CollegeID",
            //    table: "Department",
            //    type: "int",
            //    nullable: true);

            //migrationBuilder.CreateTable(
            //    name: "Grant",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        PI = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
            //        Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Grant", x => x.Id);
            //    });

            //        migrationBuilder.CreateTable(
            //            name: "GrantFile",
            //            columns: table => new
            //            {
            //                Id = table.Column<int>(type: "int", nullable: false)
            //                    .Annotation("SqlServer:Identity", "1, 1"),
            //                GrantId = table.Column<int>(type: "int", nullable: false)
            //            },
            //            constraints: table =>
            //            {
            //                table.PrimaryKey("PK_GrantFile", x => x.Id);
            //                table.ForeignKey(
            //                    name: "FK_GrantFile_Grant_GrantId",
            //                    column: x => x.GrantId,
            //                    principalTable: "Grant",
            //                    principalColumn: "Id",
            //                    onDelete: ReferentialAction.Cascade);
            //            });

            //        migrationBuilder.CreateIndex(
            //            name: "IX_GrantFile_GrantId",
            //            table: "GrantFile",
            //            column: "GrantId");
            //    }

            //    /// <inheritdoc />
            //    protected override void Down(MigrationBuilder migrationBuilder)
            //    {
            //        migrationBuilder.DropTable(
            //            name: "GrantFile");

            //        migrationBuilder.DropTable(
            //            name: "Grant");

            //        migrationBuilder.DropColumn(
            //            name: "CollegeID",
            //            table: "Department");

            //        migrationBuilder.AddColumn<string>(
            //            name: "College",
            //            table: "Department",
            //            type: "nvarchar(60)",
            //            maxLength: 60,
            //            nullable: false,
            //            defaultValue: "");
    }        }

}
