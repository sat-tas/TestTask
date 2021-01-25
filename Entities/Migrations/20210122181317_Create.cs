using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Entities.Migrations
{
    public partial class Create : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateAdded = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateChangeInfo = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Positions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Positions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Patronymic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PositionId = table.Column<int>(type: "int", nullable: false, defaultValueSql: "1"),
                    DepartmentId = table.Column<int>(type: "int", nullable: false, defaultValueSql: "1"),
                    DateAdded = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateChangeInfo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateOfHiring = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employees_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Employees_Positions_PositionId",
                        column: x => x.PositionId,
                        principalTable: "Positions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "Id", "DateAdded", "DateChangeInfo", "Name" },
                values: new object[] { 1, DateTime.Now, null, "Не назначен" });

            migrationBuilder.InsertData(
                table: "Positions",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "Не назначен" });

            migrationBuilder.CreateIndex(
                name: "IX_Employees_DepartmentId",
                table: "Employees",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_PositionId",
                table: "Employees",
                column: "PositionId");

            //migrationBuilder.Sql(@"CREATE TRIGGER DeleteDepartment ON Departments for Delete AS Update Employees Set DepartmentId=1,PositionId=1 where DepartmentId=(select Id from deleted)");
           // migrationBuilder.Sql(@"CREATE TRIGGER DeletePostion ON Positions for Delete AS Update Employees Set PositionId=1 where PositionId=(select Id from deleted)");
            migrationBuilder.Sql(@"CREATE TRIGGER UpdateEmployee ON Employees AFTER Update AS DECLARE @InsertedPosition int; DECLARE @DeletedPosition int; set @InsertedPosition = (select PositionId from inserted); set @DeletedPosition = (select PositionId from deleted); if @DeletedPosition = 1 and @InsertedPosition !=1  Update Employees Set DateOfHiring=FORMAT (getdate(), 'dd-MM-yy hh:mm:ss') where Employees.Id=(select Id from inserted) else Update Employees Set DateChangeInfo=FORMAT (getdate(), 'dd-MM-yy hh:mm:ss') where Employees.Id=(select Id from inserted)");
            migrationBuilder.Sql(@"CREATE TRIGGER UpdateDepartment ON Departments AFTER Update AS  Update Departments Set DateChangeInfo =FORMAT (getdate(), 'dd-MM-yy hh:mm:ss') where Departments.Id=(select Id from inserted)");
     //       migrationBuilder.Sql(@"CREATE TRIGGER AddEmployee ON Employees AFTER insert as Update Employees Set DateAdded = FORMAT(getdate(), 'dd-MM-yy hh:mm:ss') where Employees.Id = (select Id from inserted)");
     //       migrationBuilder.Sql(@"CREATE TRIGGER AddDepartment ON Departments AFTER insert  as  Update Departments Set DateAdded=FORMAT (getdate(), 'dd-MM-yy hh:mm:ss') where Departments.Id=(select Id from inserted)"); 

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropTable(
                name: "Positions");
        }
    }
}
