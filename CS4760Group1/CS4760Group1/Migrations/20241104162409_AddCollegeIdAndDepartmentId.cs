using Microsoft.EntityFrameworkCore.Migrations;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

#nullable disable

namespace CS4760Group1.Migrations
{
    /// <inheritdoc />
    public partial class AddCollegeIdAndDepartmentId : Migration
    {

        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            //migrationBuilder.AddColumn<int>(
            //    name: "Index",
            //    table: "Grant",
            //    type: "int",
            //    nullable: false,
            //    defaultValue: 0);

            //migrationBuilder.AddColumn<string>(
            //    name: "ProcMethod",
            //    table: "Grant",
            //    type: "nvarchar(max)",
            //    nullable: false,
            //    defaultValue: "");

            //migrationBuilder.AddColumn<bool>(
            //    name: "SubjectNeeded",
            //    table: "Grant",
            //    type: "bit",
            //    nullable: false,
            //    defaultValue: false);

            //migrationBuilder.AddColumn<string>(
            //    name: "Timeline",
            //    table: "Grant",
            //    type: "nvarchar(max)",
            //    nullable: false,
            //    defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "CollegeId",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DepartmentId",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            //migrationBuilder.CreateTable(
            //    name: "GrantReview",
            //    columns: table => new
            //    {
            //        ID = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        GrantId = table.Column<int>(type: "int", nullable: false),
            //        UserId = table.Column<int>(type: "int", nullable: false),
            //        Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        ReviewScore = table.Column<float>(type: "real", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_GrantReview", x => x.ID);
            //    });
        }


        ///// <inheritdoc />
        //protected override void Down(MigrationBuilder migrationBuilder)
        //{
        //    migrationBuilder.DropTable(
        //        name: "GrantReview");

        //    migrationBuilder.DropColumn(
        //        name: "Index",
        //        table: "Grant");

        //    migrationBuilder.DropColumn(
        //        name: "ProcMethod",
        //        table: "Grant");

        //    migrationBuilder.DropColumn(
        //        name: "SubjectNeeded",
        //        table: "Grant");

        //    migrationBuilder.DropColumn(
        //        name: "Timeline",
        //        table: "Grant");

        //    migrationBuilder.DropColumn(
        //        name: "CollegeId",
        //        table: "AspNetUsers");

        //    migrationBuilder.DropColumn(
        //        name: "DepartmentId",
        //        table: "AspNetUsers");

        //    migrationBuilder.CreateTable(
        //        name: "UserAffiliations",
        //        columns: table => new
        //        {
        //            Id = table.Column<int>(type: "int", nullable: false)
        //                .Annotation("SqlServer:Identity", "1, 1"),
        //            CollegeId = table.Column<int>(type: "int", nullable: false),
        //            DepartmentId = table.Column<int>(type: "int", nullable: false),
        //            UserId = table.Column<int>(type: "int", nullable: false),
        //            tempField = table.Column<int>(type: "int", nullable: false)
        //        },
        //        constraints: table =>
        //        {
        //            table.PrimaryKey("PK_UserAffiliations", x => x.Id);
        //        });
        //}
    }
}
