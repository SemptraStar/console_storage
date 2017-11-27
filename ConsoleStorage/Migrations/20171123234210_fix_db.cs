using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ConsoleStorage.Migrations
{
    public partial class fix_db : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_Units_ProductUnitId",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductBatch_Batch_BatchId",
                table: "ProductBatch");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductBatch_Product_ProductId",
                table: "ProductBatch");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductBatch",
                table: "ProductBatch");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Product",
                table: "Product");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Batch",
                table: "Batch");

            migrationBuilder.RenameTable(
                name: "ProductBatch",
                newName: "ProductBatches");

            migrationBuilder.RenameTable(
                name: "Product",
                newName: "Products");

            migrationBuilder.RenameTable(
                name: "Batch",
                newName: "Batches");

            migrationBuilder.RenameIndex(
                name: "IX_ProductBatch_BatchId",
                table: "ProductBatches",
                newName: "IX_ProductBatches_BatchId");

            migrationBuilder.RenameIndex(
                name: "IX_Product_ProductUnitId",
                table: "Products",
                newName: "IX_Products_ProductUnitId");

            migrationBuilder.RenameIndex(
                name: "IX_Product_Name",
                table: "Products",
                newName: "IX_Products_Name");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductBatches",
                table: "ProductBatches",
                columns: new[] { "ProductId", "BatchId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Products",
                table: "Products",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Batches",
                table: "Batches",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductBatches_Batches_BatchId",
                table: "ProductBatches",
                column: "BatchId",
                principalTable: "Batches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductBatches_Products_ProductId",
                table: "ProductBatches",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Units_ProductUnitId",
                table: "Products",
                column: "ProductUnitId",
                principalTable: "Units",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductBatches_Batches_BatchId",
                table: "ProductBatches");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductBatches_Products_ProductId",
                table: "ProductBatches");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Units_ProductUnitId",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Products",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductBatches",
                table: "ProductBatches");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Batches",
                table: "Batches");

            migrationBuilder.RenameTable(
                name: "Products",
                newName: "Product");

            migrationBuilder.RenameTable(
                name: "ProductBatches",
                newName: "ProductBatch");

            migrationBuilder.RenameTable(
                name: "Batches",
                newName: "Batch");

            migrationBuilder.RenameIndex(
                name: "IX_Products_ProductUnitId",
                table: "Product",
                newName: "IX_Product_ProductUnitId");

            migrationBuilder.RenameIndex(
                name: "IX_Products_Name",
                table: "Product",
                newName: "IX_Product_Name");

            migrationBuilder.RenameIndex(
                name: "IX_ProductBatches_BatchId",
                table: "ProductBatch",
                newName: "IX_ProductBatch_BatchId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Product",
                table: "Product",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductBatch",
                table: "ProductBatch",
                columns: new[] { "ProductId", "BatchId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Batch",
                table: "Batch",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Units_ProductUnitId",
                table: "Product",
                column: "ProductUnitId",
                principalTable: "Units",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductBatch_Batch_BatchId",
                table: "ProductBatch",
                column: "BatchId",
                principalTable: "Batch",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductBatch_Product_ProductId",
                table: "ProductBatch",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
