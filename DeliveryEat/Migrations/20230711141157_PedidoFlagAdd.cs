﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DeliveryEat.Migrations
{
    /// <inheritdoc />
    public partial class PedidoFlagAdd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Confirmed",
                table: "Pedidos",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Confirmed",
                table: "Pedidos");
        }
    }
}
