using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HR.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddMonthAndYearToPerformanceReview : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropIndex(
            //    name: "IX_PerformanceReviews_EmployeeId_ReviewDate",
            //    table: "PerformanceReviews");

            //migrationBuilder.AddColumn<int>(
            //    name: "Month",
            //    table: "PerformanceReviews",
            //    type: "int",
            //    nullable: false,
            //    defaultValue: 0);

            //migrationBuilder.AddColumn<int>(
            //    name: "Year",
            //    table: "PerformanceReviews",
            //    type: "int",
            //    nullable: false,
            //    defaultValue: 0);

            //migrationBuilder.CreateIndex(
            //    name: "IX_PerformanceReviews_EmployeeId_Month_Year",
            //    table: "PerformanceReviews",
            //    columns: new[] { "EmployeeId", "Month", "Year" },
            //    unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PerformanceReviews_EmployeeId_Month_Year",
                table: "PerformanceReviews");

            migrationBuilder.DropColumn(
                name: "Month",
                table: "PerformanceReviews");

            migrationBuilder.DropColumn(
                name: "Year",
                table: "PerformanceReviews");

            migrationBuilder.CreateIndex(
                name: "IX_PerformanceReviews_EmployeeId_ReviewDate",
                table: "PerformanceReviews",
                columns: new[] { "EmployeeId", "ReviewDate" });
        }
    }
}
