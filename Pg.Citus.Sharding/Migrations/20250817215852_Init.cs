using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pg.Citus.Sharding.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "orders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CustomerId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Amount = table.Column<decimal>(type: "numeric", nullable: false),
                    Note = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_orders", x => new { x.CustomerId, x.Id });
                });

            migrationBuilder.CreateIndex(
                name: "IX_orders_CustomerId_CreatedAt",
                table: "orders",
                columns: new[] { "CustomerId", "CreatedAt" });

            migrationBuilder.Sql("""
                DO $$
                BEGIN
                  IF EXISTS (SELECT 1 FROM pg_proc WHERE proname='citus_is_coordinator')
                     AND citus_is_coordinator() THEN
                    PERFORM create_distributed_table('public."orders"', 'CustomerId');
                    -- дочерние: colocate_with => 'public."orders"'
                  END IF;
                END$$;
            """);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "orders");
        }
    }
}
