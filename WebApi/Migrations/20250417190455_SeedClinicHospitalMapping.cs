using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi.Migrations
{
    /// <inheritdoc />
    public partial class SeedClinicHospitalMapping : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
        DELETE FROM ClinicHospital;

        INSERT INTO ClinicHospital (ClinicId, HospitalId)
        SELECT c.Id, h.Id
        FROM Clinics c
        JOIN Hospitals h ON c.DistrictId = h.DistrictId
    ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM ClinicHospital;");
        }
    }
}
