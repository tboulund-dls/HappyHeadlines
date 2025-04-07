using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SubscriberService.Migrations
{
    /// <inheritdoc />
    public partial class RenameSubscriptionType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subscriptions_SubscriberTypes_TypeId",
                table: "Subscriptions");

            migrationBuilder.DropForeignKey(
                name: "FK_Subscriptions_Subscribers_SubscriberId",
                table: "Subscriptions");

            migrationBuilder.DropColumn(
                name: "SubsriberId",
                table: "Subscriptions");

            migrationBuilder.RenameColumn(
                name: "TypeId",
                table: "Subscriptions",
                newName: "SubscriptionTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Subscriptions_TypeId",
                table: "Subscriptions",
                newName: "IX_Subscriptions_SubscriptionTypeId");

            migrationBuilder.AlterColumn<Guid>(
                name: "SubscriberId",
                table: "Subscriptions",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Subscriptions_SubscriberTypes_SubscriptionTypeId",
                table: "Subscriptions",
                column: "SubscriptionTypeId",
                principalTable: "SubscriberTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Subscriptions_Subscribers_SubscriberId",
                table: "Subscriptions",
                column: "SubscriberId",
                principalTable: "Subscribers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subscriptions_SubscriberTypes_SubscriptionTypeId",
                table: "Subscriptions");

            migrationBuilder.DropForeignKey(
                name: "FK_Subscriptions_Subscribers_SubscriberId",
                table: "Subscriptions");

            migrationBuilder.RenameColumn(
                name: "SubscriptionTypeId",
                table: "Subscriptions",
                newName: "TypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Subscriptions_SubscriptionTypeId",
                table: "Subscriptions",
                newName: "IX_Subscriptions_TypeId");

            migrationBuilder.AlterColumn<Guid>(
                name: "SubscriberId",
                table: "Subscriptions",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "TEXT");

            migrationBuilder.AddColumn<Guid>(
                name: "SubsriberId",
                table: "Subscriptions",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddForeignKey(
                name: "FK_Subscriptions_SubscriberTypes_TypeId",
                table: "Subscriptions",
                column: "TypeId",
                principalTable: "SubscriberTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Subscriptions_Subscribers_SubscriberId",
                table: "Subscriptions",
                column: "SubscriberId",
                principalTable: "Subscribers",
                principalColumn: "Id");
        }
    }
}
