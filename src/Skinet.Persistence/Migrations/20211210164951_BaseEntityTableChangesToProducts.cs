using Microsoft.EntityFrameworkCore.Migrations;

namespace Skinet.Persistence.Migrations
{
    public partial class BaseEntityTableChangesToProducts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_BaseEntity",
                table: "BaseEntity");

            migrationBuilder.DropIndex(
                name: "IX_BaseEntity_Id",
                table: "BaseEntity");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "BaseEntity");

            migrationBuilder.RenameTable(
                name: "BaseEntity",
                newName: "Products");

            migrationBuilder.RenameIndex(
                name: "IX_BaseEntity_ProductName",
                table: "Products",
                newName: "IX_Products_ProductName");

            migrationBuilder.AlterColumn<string>(
                name: "ProductName",
                table: "Products",
                type: "TEXT",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LastModifiedBy",
                table: "Products",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "Products",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Products",
                table: "Products",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Products",
                table: "Products");

            migrationBuilder.RenameTable(
                name: "Products",
                newName: "BaseEntity");

            migrationBuilder.RenameIndex(
                name: "IX_Products_ProductName",
                table: "BaseEntity",
                newName: "IX_BaseEntity_ProductName");

            migrationBuilder.AlterColumn<string>(
                name: "ProductName",
                table: "BaseEntity",
                type: "TEXT",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "LastModifiedBy",
                table: "BaseEntity",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "BaseEntity",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "BaseEntity",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BaseEntity",
                table: "BaseEntity",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_BaseEntity_Id",
                table: "BaseEntity",
                column: "Id",
                unique: true);
        }
    }
}
