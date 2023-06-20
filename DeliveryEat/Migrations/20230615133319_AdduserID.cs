using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DeliveryEat.Migrations
{
    /// <inheritdoc />
    public partial class AdduserID : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "userId",
                table: "Pessoas",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "userId",
                table: "Pessoas");
        }
    }
}
