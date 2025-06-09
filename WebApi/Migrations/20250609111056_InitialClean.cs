using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebApi.Migrations
{
    /// <inheritdoc />
    public partial class InitialClean : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Districts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CityId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Districts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Districts_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Clinics",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DistrictId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clinics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Clinics_Districts_DistrictId",
                        column: x => x.DistrictId,
                        principalTable: "Districts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Hospitals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DistrictId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hospitals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Hospitals_Districts_DistrictId",
                        column: x => x.DistrictId,
                        principalTable: "Districts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Specialization = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClinicId = table.Column<int>(type: "int", nullable: true),
                    HospitalId = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    RefreshToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RefreshTokenExpiryTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_Clinics_ClinicId",
                        column: x => x.ClinicId,
                        principalTable: "Clinics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_Hospitals_HospitalId",
                        column: x => x.HospitalId,
                        principalTable: "Hospitals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ClinicHospital",
                columns: table => new
                {
                    ClinicId = table.Column<int>(type: "int", nullable: false),
                    HospitalId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClinicHospital", x => new { x.ClinicId, x.HospitalId });
                    table.ForeignKey(
                        name: "FK_ClinicHospital_Clinics_ClinicId",
                        column: x => x.ClinicId,
                        principalTable: "Clinics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ClinicHospital_Hospitals_HospitalId",
                        column: x => x.HospitalId,
                        principalTable: "Hospitals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Availabilities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DoctorId = table.Column<int>(type: "int", nullable: false),
                    AvailableDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StartTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    EndTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsBooked = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Availabilities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Availabilities_AspNetUsers_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Appointments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatientId = table.Column<int>(type: "int", nullable: false),
                    DoctorId = table.Column<int>(type: "int", nullable: false),
                    AvailabilityId = table.Column<int>(type: "int", nullable: false),
                    AppointmentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Appointments_AspNetUsers_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Appointments_AspNetUsers_PatientId",
                        column: x => x.PatientId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Appointments_Availabilities_AvailabilityId",
                        column: x => x.AvailabilityId,
                        principalTable: "Availabilities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { 1, null, "Patient", "PATIENT" },
                    { 2, null, "Doctor", "DOCTOR" },
                    { 3, null, "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "İstanbul" },
                    { 2, "Ankara" },
                    { 3, "İzmir" },
                    { 4, "Bursa" },
                    { 5, "Antalya" },
                    { 6, "Adana" },
                    { 7, "Konya" },
                    { 8, "Gaziantep" },
                    { 9, "Şanlıurfa" },
                    { 10, "Mersin" }
                });

            migrationBuilder.InsertData(
                table: "Districts",
                columns: new[] { "Id", "CityId", "Name" },
                values: new object[,]
                {
                    { 1, 1, "Kadıköy" },
                    { 2, 1, "Beşiktaş" },
                    { 3, 1, "Üsküdar" },
                    { 4, 1, "Bakırköy" },
                    { 5, 1, "Şişli" },
                    { 6, 2, "Çankaya" },
                    { 7, 2, "Keçiören" },
                    { 8, 2, "Mamak" },
                    { 9, 2, "Yenimahalle" },
                    { 10, 2, "Altındağ" },
                    { 11, 3, "Konak" },
                    { 12, 3, "Karşıyaka" },
                    { 13, 3, "Bornova" },
                    { 14, 3, "Buca" },
                    { 15, 3, "Gaziemir" },
                    { 16, 4, "Nilüfer" },
                    { 17, 4, "Osmangazi" },
                    { 18, 4, "Yıldırım" },
                    { 19, 4, "Gürsu" },
                    { 20, 4, "Gemlik" },
                    { 21, 5, "Muratpaşa" },
                    { 22, 5, "Konyaaltı" },
                    { 23, 5, "Kepez" },
                    { 24, 5, "Alanya" },
                    { 25, 5, "Manavgat" },
                    { 26, 6, "Seyhan" },
                    { 27, 6, "Yüreğir" },
                    { 28, 6, "Çukurova" },
                    { 29, 6, "Sarıçam" },
                    { 30, 6, "Ceyhan" },
                    { 31, 7, "Selçuklu" },
                    { 32, 7, "Karatay" },
                    { 33, 7, "Meram" },
                    { 34, 7, "Ereğli" },
                    { 35, 7, "Akşehir" },
                    { 36, 8, "Şahinbey" },
                    { 37, 8, "Şehitkamil" },
                    { 38, 8, "Nizip" },
                    { 39, 8, "İslahiye" },
                    { 40, 8, "Karkamış" },
                    { 41, 9, "Haliliye" },
                    { 42, 9, "Eyyübiye" },
                    { 43, 9, "Karaköprü" },
                    { 44, 9, "Siverek" },
                    { 45, 9, "Viranşehir" },
                    { 46, 10, "Akdeniz" },
                    { 47, 10, "Mezitli" },
                    { 48, 10, "Toroslar" },
                    { 49, 10, "Yenişehir" },
                    { 50, 10, "Tarsus" }
                });

            migrationBuilder.InsertData(
                table: "Clinics",
                columns: new[] { "Id", "DistrictId", "IsDeleted", "Name" },
                values: new object[,]
                {
                    { 1, 1, false, "Kardiyoloji" },
                    { 2, 1, false, "Dahiliye" },
                    { 3, 1, false, "Ortopedi" },
                    { 4, 1, false, "Pediatri" },
                    { 5, 1, false, "Dermatoloji" },
                    { 6, 1, false, "Göz" },
                    { 7, 1, false, "KBB" },
                    { 8, 1, false, "Üroloji" },
                    { 9, 1, false, "Kadın Doğum" },
                    { 10, 1, false, "Nöroloji" },
                    { 11, 2, false, "Kardiyoloji" },
                    { 12, 2, false, "Dahiliye" },
                    { 13, 2, false, "Ortopedi" },
                    { 14, 2, false, "Pediatri" },
                    { 15, 2, false, "Dermatoloji" },
                    { 16, 2, false, "Göz" },
                    { 17, 2, false, "KBB" },
                    { 18, 2, false, "Üroloji" },
                    { 19, 2, false, "Kadın Doğum" },
                    { 20, 2, false, "Nöroloji" },
                    { 21, 3, false, "Kardiyoloji" },
                    { 22, 3, false, "Dahiliye" },
                    { 23, 3, false, "Ortopedi" },
                    { 24, 3, false, "Pediatri" },
                    { 25, 3, false, "Dermatoloji" },
                    { 26, 3, false, "Göz" },
                    { 27, 3, false, "KBB" },
                    { 28, 3, false, "Üroloji" },
                    { 29, 3, false, "Kadın Doğum" },
                    { 30, 3, false, "Nöroloji" },
                    { 31, 4, false, "Kardiyoloji" },
                    { 32, 4, false, "Dahiliye" },
                    { 33, 4, false, "Ortopedi" },
                    { 34, 4, false, "Pediatri" },
                    { 35, 4, false, "Dermatoloji" },
                    { 36, 4, false, "Göz" },
                    { 37, 4, false, "KBB" },
                    { 38, 4, false, "Üroloji" },
                    { 39, 4, false, "Kadın Doğum" },
                    { 40, 4, false, "Nöroloji" },
                    { 41, 5, false, "Kardiyoloji" },
                    { 42, 5, false, "Dahiliye" },
                    { 43, 5, false, "Ortopedi" },
                    { 44, 5, false, "Pediatri" },
                    { 45, 5, false, "Dermatoloji" },
                    { 46, 5, false, "Göz" },
                    { 47, 5, false, "KBB" },
                    { 48, 5, false, "Üroloji" },
                    { 49, 5, false, "Kadın Doğum" },
                    { 50, 5, false, "Nöroloji" },
                    { 51, 6, false, "Kardiyoloji" },
                    { 52, 6, false, "Dahiliye" },
                    { 53, 6, false, "Ortopedi" },
                    { 54, 6, false, "Pediatri" },
                    { 55, 6, false, "Dermatoloji" },
                    { 56, 6, false, "Göz" },
                    { 57, 6, false, "KBB" },
                    { 58, 6, false, "Üroloji" },
                    { 59, 6, false, "Kadın Doğum" },
                    { 60, 6, false, "Nöroloji" },
                    { 61, 7, false, "Kardiyoloji" },
                    { 62, 7, false, "Dahiliye" },
                    { 63, 7, false, "Ortopedi" },
                    { 64, 7, false, "Pediatri" },
                    { 65, 7, false, "Dermatoloji" },
                    { 66, 7, false, "Göz" },
                    { 67, 7, false, "KBB" },
                    { 68, 7, false, "Üroloji" },
                    { 69, 7, false, "Kadın Doğum" },
                    { 70, 7, false, "Nöroloji" },
                    { 71, 8, false, "Kardiyoloji" },
                    { 72, 8, false, "Dahiliye" },
                    { 73, 8, false, "Ortopedi" },
                    { 74, 8, false, "Pediatri" },
                    { 75, 8, false, "Dermatoloji" },
                    { 76, 8, false, "Göz" },
                    { 77, 8, false, "KBB" },
                    { 78, 8, false, "Üroloji" },
                    { 79, 8, false, "Kadın Doğum" },
                    { 80, 8, false, "Nöroloji" },
                    { 81, 9, false, "Kardiyoloji" },
                    { 82, 9, false, "Dahiliye" },
                    { 83, 9, false, "Ortopedi" },
                    { 84, 9, false, "Pediatri" },
                    { 85, 9, false, "Dermatoloji" },
                    { 86, 9, false, "Göz" },
                    { 87, 9, false, "KBB" },
                    { 88, 9, false, "Üroloji" },
                    { 89, 9, false, "Kadın Doğum" },
                    { 90, 9, false, "Nöroloji" },
                    { 91, 10, false, "Kardiyoloji" },
                    { 92, 10, false, "Dahiliye" },
                    { 93, 10, false, "Ortopedi" },
                    { 94, 10, false, "Pediatri" },
                    { 95, 10, false, "Dermatoloji" },
                    { 96, 10, false, "Göz" },
                    { 97, 10, false, "KBB" },
                    { 98, 10, false, "Üroloji" },
                    { 99, 10, false, "Kadın Doğum" },
                    { 100, 10, false, "Nöroloji" },
                    { 101, 11, false, "Kardiyoloji" },
                    { 102, 11, false, "Dahiliye" },
                    { 103, 11, false, "Ortopedi" },
                    { 104, 11, false, "Pediatri" },
                    { 105, 11, false, "Dermatoloji" },
                    { 106, 11, false, "Göz" },
                    { 107, 11, false, "KBB" },
                    { 108, 11, false, "Üroloji" },
                    { 109, 11, false, "Kadın Doğum" },
                    { 110, 11, false, "Nöroloji" },
                    { 111, 12, false, "Kardiyoloji" },
                    { 112, 12, false, "Dahiliye" },
                    { 113, 12, false, "Ortopedi" },
                    { 114, 12, false, "Pediatri" },
                    { 115, 12, false, "Dermatoloji" },
                    { 116, 12, false, "Göz" },
                    { 117, 12, false, "KBB" },
                    { 118, 12, false, "Üroloji" },
                    { 119, 12, false, "Kadın Doğum" },
                    { 120, 12, false, "Nöroloji" },
                    { 121, 13, false, "Kardiyoloji" },
                    { 122, 13, false, "Dahiliye" },
                    { 123, 13, false, "Ortopedi" },
                    { 124, 13, false, "Pediatri" },
                    { 125, 13, false, "Dermatoloji" },
                    { 126, 13, false, "Göz" },
                    { 127, 13, false, "KBB" },
                    { 128, 13, false, "Üroloji" },
                    { 129, 13, false, "Kadın Doğum" },
                    { 130, 13, false, "Nöroloji" },
                    { 131, 14, false, "Kardiyoloji" },
                    { 132, 14, false, "Dahiliye" },
                    { 133, 14, false, "Ortopedi" },
                    { 134, 14, false, "Pediatri" },
                    { 135, 14, false, "Dermatoloji" },
                    { 136, 14, false, "Göz" },
                    { 137, 14, false, "KBB" },
                    { 138, 14, false, "Üroloji" },
                    { 139, 14, false, "Kadın Doğum" },
                    { 140, 14, false, "Nöroloji" },
                    { 141, 15, false, "Kardiyoloji" },
                    { 142, 15, false, "Dahiliye" },
                    { 143, 15, false, "Ortopedi" },
                    { 144, 15, false, "Pediatri" },
                    { 145, 15, false, "Dermatoloji" },
                    { 146, 15, false, "Göz" },
                    { 147, 15, false, "KBB" },
                    { 148, 15, false, "Üroloji" },
                    { 149, 15, false, "Kadın Doğum" },
                    { 150, 15, false, "Nöroloji" },
                    { 151, 16, false, "Kardiyoloji" },
                    { 152, 16, false, "Dahiliye" },
                    { 153, 16, false, "Ortopedi" },
                    { 154, 16, false, "Pediatri" },
                    { 155, 16, false, "Dermatoloji" },
                    { 156, 16, false, "Göz" },
                    { 157, 16, false, "KBB" },
                    { 158, 16, false, "Üroloji" },
                    { 159, 16, false, "Kadın Doğum" },
                    { 160, 16, false, "Nöroloji" },
                    { 161, 17, false, "Kardiyoloji" },
                    { 162, 17, false, "Dahiliye" },
                    { 163, 17, false, "Ortopedi" },
                    { 164, 17, false, "Pediatri" },
                    { 165, 17, false, "Dermatoloji" },
                    { 166, 17, false, "Göz" },
                    { 167, 17, false, "KBB" },
                    { 168, 17, false, "Üroloji" },
                    { 169, 17, false, "Kadın Doğum" },
                    { 170, 17, false, "Nöroloji" },
                    { 171, 18, false, "Kardiyoloji" },
                    { 172, 18, false, "Dahiliye" },
                    { 173, 18, false, "Ortopedi" },
                    { 174, 18, false, "Pediatri" },
                    { 175, 18, false, "Dermatoloji" },
                    { 176, 18, false, "Göz" },
                    { 177, 18, false, "KBB" },
                    { 178, 18, false, "Üroloji" },
                    { 179, 18, false, "Kadın Doğum" },
                    { 180, 18, false, "Nöroloji" },
                    { 181, 19, false, "Kardiyoloji" },
                    { 182, 19, false, "Dahiliye" },
                    { 183, 19, false, "Ortopedi" },
                    { 184, 19, false, "Pediatri" },
                    { 185, 19, false, "Dermatoloji" },
                    { 186, 19, false, "Göz" },
                    { 187, 19, false, "KBB" },
                    { 188, 19, false, "Üroloji" },
                    { 189, 19, false, "Kadın Doğum" },
                    { 190, 19, false, "Nöroloji" },
                    { 191, 20, false, "Kardiyoloji" },
                    { 192, 20, false, "Dahiliye" },
                    { 193, 20, false, "Ortopedi" },
                    { 194, 20, false, "Pediatri" },
                    { 195, 20, false, "Dermatoloji" },
                    { 196, 20, false, "Göz" },
                    { 197, 20, false, "KBB" },
                    { 198, 20, false, "Üroloji" },
                    { 199, 20, false, "Kadın Doğum" },
                    { 200, 20, false, "Nöroloji" },
                    { 201, 21, false, "Kardiyoloji" },
                    { 202, 21, false, "Dahiliye" },
                    { 203, 21, false, "Ortopedi" },
                    { 204, 21, false, "Pediatri" },
                    { 205, 21, false, "Dermatoloji" },
                    { 206, 21, false, "Göz" },
                    { 207, 21, false, "KBB" },
                    { 208, 21, false, "Üroloji" },
                    { 209, 21, false, "Kadın Doğum" },
                    { 210, 21, false, "Nöroloji" },
                    { 211, 22, false, "Kardiyoloji" },
                    { 212, 22, false, "Dahiliye" },
                    { 213, 22, false, "Ortopedi" },
                    { 214, 22, false, "Pediatri" },
                    { 215, 22, false, "Dermatoloji" },
                    { 216, 22, false, "Göz" },
                    { 217, 22, false, "KBB" },
                    { 218, 22, false, "Üroloji" },
                    { 219, 22, false, "Kadın Doğum" },
                    { 220, 22, false, "Nöroloji" },
                    { 221, 23, false, "Kardiyoloji" },
                    { 222, 23, false, "Dahiliye" },
                    { 223, 23, false, "Ortopedi" },
                    { 224, 23, false, "Pediatri" },
                    { 225, 23, false, "Dermatoloji" },
                    { 226, 23, false, "Göz" },
                    { 227, 23, false, "KBB" },
                    { 228, 23, false, "Üroloji" },
                    { 229, 23, false, "Kadın Doğum" },
                    { 230, 23, false, "Nöroloji" },
                    { 231, 24, false, "Kardiyoloji" },
                    { 232, 24, false, "Dahiliye" },
                    { 233, 24, false, "Ortopedi" },
                    { 234, 24, false, "Pediatri" },
                    { 235, 24, false, "Dermatoloji" },
                    { 236, 24, false, "Göz" },
                    { 237, 24, false, "KBB" },
                    { 238, 24, false, "Üroloji" },
                    { 239, 24, false, "Kadın Doğum" },
                    { 240, 24, false, "Nöroloji" },
                    { 241, 25, false, "Kardiyoloji" },
                    { 242, 25, false, "Dahiliye" },
                    { 243, 25, false, "Ortopedi" },
                    { 244, 25, false, "Pediatri" },
                    { 245, 25, false, "Dermatoloji" },
                    { 246, 25, false, "Göz" },
                    { 247, 25, false, "KBB" },
                    { 248, 25, false, "Üroloji" },
                    { 249, 25, false, "Kadın Doğum" },
                    { 250, 25, false, "Nöroloji" },
                    { 251, 26, false, "Kardiyoloji" },
                    { 252, 26, false, "Dahiliye" },
                    { 253, 26, false, "Ortopedi" },
                    { 254, 26, false, "Pediatri" },
                    { 255, 26, false, "Dermatoloji" },
                    { 256, 26, false, "Göz" },
                    { 257, 26, false, "KBB" },
                    { 258, 26, false, "Üroloji" },
                    { 259, 26, false, "Kadın Doğum" },
                    { 260, 26, false, "Nöroloji" },
                    { 261, 27, false, "Kardiyoloji" },
                    { 262, 27, false, "Dahiliye" },
                    { 263, 27, false, "Ortopedi" },
                    { 264, 27, false, "Pediatri" },
                    { 265, 27, false, "Dermatoloji" },
                    { 266, 27, false, "Göz" },
                    { 267, 27, false, "KBB" },
                    { 268, 27, false, "Üroloji" },
                    { 269, 27, false, "Kadın Doğum" },
                    { 270, 27, false, "Nöroloji" },
                    { 271, 28, false, "Kardiyoloji" },
                    { 272, 28, false, "Dahiliye" },
                    { 273, 28, false, "Ortopedi" },
                    { 274, 28, false, "Pediatri" },
                    { 275, 28, false, "Dermatoloji" },
                    { 276, 28, false, "Göz" },
                    { 277, 28, false, "KBB" },
                    { 278, 28, false, "Üroloji" },
                    { 279, 28, false, "Kadın Doğum" },
                    { 280, 28, false, "Nöroloji" },
                    { 281, 29, false, "Kardiyoloji" },
                    { 282, 29, false, "Dahiliye" },
                    { 283, 29, false, "Ortopedi" },
                    { 284, 29, false, "Pediatri" },
                    { 285, 29, false, "Dermatoloji" },
                    { 286, 29, false, "Göz" },
                    { 287, 29, false, "KBB" },
                    { 288, 29, false, "Üroloji" },
                    { 289, 29, false, "Kadın Doğum" },
                    { 290, 29, false, "Nöroloji" },
                    { 291, 30, false, "Kardiyoloji" },
                    { 292, 30, false, "Dahiliye" },
                    { 293, 30, false, "Ortopedi" },
                    { 294, 30, false, "Pediatri" },
                    { 295, 30, false, "Dermatoloji" },
                    { 296, 30, false, "Göz" },
                    { 297, 30, false, "KBB" },
                    { 298, 30, false, "Üroloji" },
                    { 299, 30, false, "Kadın Doğum" },
                    { 300, 30, false, "Nöroloji" },
                    { 301, 31, false, "Kardiyoloji" },
                    { 302, 31, false, "Dahiliye" },
                    { 303, 31, false, "Ortopedi" },
                    { 304, 31, false, "Pediatri" },
                    { 305, 31, false, "Dermatoloji" },
                    { 306, 31, false, "Göz" },
                    { 307, 31, false, "KBB" },
                    { 308, 31, false, "Üroloji" },
                    { 309, 31, false, "Kadın Doğum" },
                    { 310, 31, false, "Nöroloji" },
                    { 311, 32, false, "Kardiyoloji" },
                    { 312, 32, false, "Dahiliye" },
                    { 313, 32, false, "Ortopedi" },
                    { 314, 32, false, "Pediatri" },
                    { 315, 32, false, "Dermatoloji" },
                    { 316, 32, false, "Göz" },
                    { 317, 32, false, "KBB" },
                    { 318, 32, false, "Üroloji" },
                    { 319, 32, false, "Kadın Doğum" },
                    { 320, 32, false, "Nöroloji" },
                    { 321, 33, false, "Kardiyoloji" },
                    { 322, 33, false, "Dahiliye" },
                    { 323, 33, false, "Ortopedi" },
                    { 324, 33, false, "Pediatri" },
                    { 325, 33, false, "Dermatoloji" },
                    { 326, 33, false, "Göz" },
                    { 327, 33, false, "KBB" },
                    { 328, 33, false, "Üroloji" },
                    { 329, 33, false, "Kadın Doğum" },
                    { 330, 33, false, "Nöroloji" },
                    { 331, 34, false, "Kardiyoloji" },
                    { 332, 34, false, "Dahiliye" },
                    { 333, 34, false, "Ortopedi" },
                    { 334, 34, false, "Pediatri" },
                    { 335, 34, false, "Dermatoloji" },
                    { 336, 34, false, "Göz" },
                    { 337, 34, false, "KBB" },
                    { 338, 34, false, "Üroloji" },
                    { 339, 34, false, "Kadın Doğum" },
                    { 340, 34, false, "Nöroloji" },
                    { 341, 35, false, "Kardiyoloji" },
                    { 342, 35, false, "Dahiliye" },
                    { 343, 35, false, "Ortopedi" },
                    { 344, 35, false, "Pediatri" },
                    { 345, 35, false, "Dermatoloji" },
                    { 346, 35, false, "Göz" },
                    { 347, 35, false, "KBB" },
                    { 348, 35, false, "Üroloji" },
                    { 349, 35, false, "Kadın Doğum" },
                    { 350, 35, false, "Nöroloji" },
                    { 351, 36, false, "Kardiyoloji" },
                    { 352, 36, false, "Dahiliye" },
                    { 353, 36, false, "Ortopedi" },
                    { 354, 36, false, "Pediatri" },
                    { 355, 36, false, "Dermatoloji" },
                    { 356, 36, false, "Göz" },
                    { 357, 36, false, "KBB" },
                    { 358, 36, false, "Üroloji" },
                    { 359, 36, false, "Kadın Doğum" },
                    { 360, 36, false, "Nöroloji" },
                    { 361, 37, false, "Kardiyoloji" },
                    { 362, 37, false, "Dahiliye" },
                    { 363, 37, false, "Ortopedi" },
                    { 364, 37, false, "Pediatri" },
                    { 365, 37, false, "Dermatoloji" },
                    { 366, 37, false, "Göz" },
                    { 367, 37, false, "KBB" },
                    { 368, 37, false, "Üroloji" },
                    { 369, 37, false, "Kadın Doğum" },
                    { 370, 37, false, "Nöroloji" },
                    { 371, 38, false, "Kardiyoloji" },
                    { 372, 38, false, "Dahiliye" },
                    { 373, 38, false, "Ortopedi" },
                    { 374, 38, false, "Pediatri" },
                    { 375, 38, false, "Dermatoloji" },
                    { 376, 38, false, "Göz" },
                    { 377, 38, false, "KBB" },
                    { 378, 38, false, "Üroloji" },
                    { 379, 38, false, "Kadın Doğum" },
                    { 380, 38, false, "Nöroloji" },
                    { 381, 39, false, "Kardiyoloji" },
                    { 382, 39, false, "Dahiliye" },
                    { 383, 39, false, "Ortopedi" },
                    { 384, 39, false, "Pediatri" },
                    { 385, 39, false, "Dermatoloji" },
                    { 386, 39, false, "Göz" },
                    { 387, 39, false, "KBB" },
                    { 388, 39, false, "Üroloji" },
                    { 389, 39, false, "Kadın Doğum" },
                    { 390, 39, false, "Nöroloji" },
                    { 391, 40, false, "Kardiyoloji" },
                    { 392, 40, false, "Dahiliye" },
                    { 393, 40, false, "Ortopedi" },
                    { 394, 40, false, "Pediatri" },
                    { 395, 40, false, "Dermatoloji" },
                    { 396, 40, false, "Göz" },
                    { 397, 40, false, "KBB" },
                    { 398, 40, false, "Üroloji" },
                    { 399, 40, false, "Kadın Doğum" },
                    { 400, 40, false, "Nöroloji" },
                    { 401, 41, false, "Kardiyoloji" },
                    { 402, 41, false, "Dahiliye" },
                    { 403, 41, false, "Ortopedi" },
                    { 404, 41, false, "Pediatri" },
                    { 405, 41, false, "Dermatoloji" },
                    { 406, 41, false, "Göz" },
                    { 407, 41, false, "KBB" },
                    { 408, 41, false, "Üroloji" },
                    { 409, 41, false, "Kadın Doğum" },
                    { 410, 41, false, "Nöroloji" },
                    { 411, 42, false, "Kardiyoloji" },
                    { 412, 42, false, "Dahiliye" },
                    { 413, 42, false, "Ortopedi" },
                    { 414, 42, false, "Pediatri" },
                    { 415, 42, false, "Dermatoloji" },
                    { 416, 42, false, "Göz" },
                    { 417, 42, false, "KBB" },
                    { 418, 42, false, "Üroloji" },
                    { 419, 42, false, "Kadın Doğum" },
                    { 420, 42, false, "Nöroloji" },
                    { 421, 43, false, "Kardiyoloji" },
                    { 422, 43, false, "Dahiliye" },
                    { 423, 43, false, "Ortopedi" },
                    { 424, 43, false, "Pediatri" },
                    { 425, 43, false, "Dermatoloji" },
                    { 426, 43, false, "Göz" },
                    { 427, 43, false, "KBB" },
                    { 428, 43, false, "Üroloji" },
                    { 429, 43, false, "Kadın Doğum" },
                    { 430, 43, false, "Nöroloji" },
                    { 431, 44, false, "Kardiyoloji" },
                    { 432, 44, false, "Dahiliye" },
                    { 433, 44, false, "Ortopedi" },
                    { 434, 44, false, "Pediatri" },
                    { 435, 44, false, "Dermatoloji" },
                    { 436, 44, false, "Göz" },
                    { 437, 44, false, "KBB" },
                    { 438, 44, false, "Üroloji" },
                    { 439, 44, false, "Kadın Doğum" },
                    { 440, 44, false, "Nöroloji" },
                    { 441, 45, false, "Kardiyoloji" },
                    { 442, 45, false, "Dahiliye" },
                    { 443, 45, false, "Ortopedi" },
                    { 444, 45, false, "Pediatri" },
                    { 445, 45, false, "Dermatoloji" },
                    { 446, 45, false, "Göz" },
                    { 447, 45, false, "KBB" },
                    { 448, 45, false, "Üroloji" },
                    { 449, 45, false, "Kadın Doğum" },
                    { 450, 45, false, "Nöroloji" },
                    { 451, 46, false, "Kardiyoloji" },
                    { 452, 46, false, "Dahiliye" },
                    { 453, 46, false, "Ortopedi" },
                    { 454, 46, false, "Pediatri" },
                    { 455, 46, false, "Dermatoloji" },
                    { 456, 46, false, "Göz" },
                    { 457, 46, false, "KBB" },
                    { 458, 46, false, "Üroloji" },
                    { 459, 46, false, "Kadın Doğum" },
                    { 460, 46, false, "Nöroloji" },
                    { 461, 47, false, "Kardiyoloji" },
                    { 462, 47, false, "Dahiliye" },
                    { 463, 47, false, "Ortopedi" },
                    { 464, 47, false, "Pediatri" },
                    { 465, 47, false, "Dermatoloji" },
                    { 466, 47, false, "Göz" },
                    { 467, 47, false, "KBB" },
                    { 468, 47, false, "Üroloji" },
                    { 469, 47, false, "Kadın Doğum" },
                    { 470, 47, false, "Nöroloji" },
                    { 471, 48, false, "Kardiyoloji" },
                    { 472, 48, false, "Dahiliye" },
                    { 473, 48, false, "Ortopedi" },
                    { 474, 48, false, "Pediatri" },
                    { 475, 48, false, "Dermatoloji" },
                    { 476, 48, false, "Göz" },
                    { 477, 48, false, "KBB" },
                    { 478, 48, false, "Üroloji" },
                    { 479, 48, false, "Kadın Doğum" },
                    { 480, 48, false, "Nöroloji" },
                    { 481, 49, false, "Kardiyoloji" },
                    { 482, 49, false, "Dahiliye" },
                    { 483, 49, false, "Ortopedi" },
                    { 484, 49, false, "Pediatri" },
                    { 485, 49, false, "Dermatoloji" },
                    { 486, 49, false, "Göz" },
                    { 487, 49, false, "KBB" },
                    { 488, 49, false, "Üroloji" },
                    { 489, 49, false, "Kadın Doğum" },
                    { 490, 49, false, "Nöroloji" },
                    { 491, 50, false, "Kardiyoloji" },
                    { 492, 50, false, "Dahiliye" },
                    { 493, 50, false, "Ortopedi" },
                    { 494, 50, false, "Pediatri" },
                    { 495, 50, false, "Dermatoloji" },
                    { 496, 50, false, "Göz" },
                    { 497, 50, false, "KBB" },
                    { 498, 50, false, "Üroloji" },
                    { 499, 50, false, "Kadın Doğum" },
                    { 500, 50, false, "Nöroloji" }
                });

            migrationBuilder.InsertData(
                table: "Hospitals",
                columns: new[] { "Id", "DistrictId", "Name" },
                values: new object[,]
                {
                    { 1, 1, "Kadıköy Şehir Hastanesi" },
                    { 2, 1, "Kadıköy Devlet Hastanesi" },
                    { 3, 1, "Kadıköy Özel Hastanesi" },
                    { 4, 2, "Beşiktaş Şehir Hastanesi" },
                    { 5, 2, "Beşiktaş Devlet Hastanesi" },
                    { 6, 2, "Beşiktaş Özel Hastanesi" },
                    { 7, 3, "Üsküdar Şehir Hastanesi" },
                    { 8, 3, "Üsküdar Devlet Hastanesi" },
                    { 9, 3, "Üsküdar Özel Hastanesi" },
                    { 10, 4, "Bakırköy Şehir Hastanesi" },
                    { 11, 4, "Bakırköy Devlet Hastanesi" },
                    { 12, 4, "Bakırköy Özel Hastanesi" },
                    { 13, 5, "Şişli Şehir Hastanesi" },
                    { 14, 5, "Şişli Devlet Hastanesi" },
                    { 15, 5, "Şişli Özel Hastanesi" },
                    { 16, 6, "Çankaya Şehir Hastanesi" },
                    { 17, 6, "Çankaya Devlet Hastanesi" },
                    { 18, 6, "Çankaya Özel Hastanesi" },
                    { 19, 7, "Keçiören Şehir Hastanesi" },
                    { 20, 7, "Keçiören Devlet Hastanesi" },
                    { 21, 7, "Keçiören Özel Hastanesi" },
                    { 22, 8, "Mamak Şehir Hastanesi" },
                    { 23, 8, "Mamak Devlet Hastanesi" },
                    { 24, 8, "Mamak Özel Hastanesi" },
                    { 25, 9, "Yenimahalle Şehir Hastanesi" },
                    { 26, 9, "Yenimahalle Devlet Hastanesi" },
                    { 27, 9, "Yenimahalle Özel Hastanesi" },
                    { 28, 10, "Altındağ Şehir Hastanesi" },
                    { 29, 10, "Altındağ Devlet Hastanesi" },
                    { 30, 10, "Altındağ Özel Hastanesi" },
                    { 31, 11, "Konak Şehir Hastanesi" },
                    { 32, 11, "Konak Devlet Hastanesi" },
                    { 33, 11, "Konak Özel Hastanesi" },
                    { 34, 12, "Karşıyaka Şehir Hastanesi" },
                    { 35, 12, "Karşıyaka Devlet Hastanesi" },
                    { 36, 12, "Karşıyaka Özel Hastanesi" },
                    { 37, 13, "Bornova Şehir Hastanesi" },
                    { 38, 13, "Bornova Devlet Hastanesi" },
                    { 39, 13, "Bornova Özel Hastanesi" },
                    { 40, 14, "Buca Şehir Hastanesi" },
                    { 41, 14, "Buca Devlet Hastanesi" },
                    { 42, 14, "Buca Özel Hastanesi" },
                    { 43, 15, "Gaziemir Şehir Hastanesi" },
                    { 44, 15, "Gaziemir Devlet Hastanesi" },
                    { 45, 15, "Gaziemir Özel Hastanesi" },
                    { 46, 16, "Nilüfer Şehir Hastanesi" },
                    { 47, 16, "Nilüfer Devlet Hastanesi" },
                    { 48, 16, "Nilüfer Özel Hastanesi" },
                    { 49, 17, "Osmangazi Şehir Hastanesi" },
                    { 50, 17, "Osmangazi Devlet Hastanesi" },
                    { 51, 17, "Osmangazi Özel Hastanesi" },
                    { 52, 18, "Yıldırım Şehir Hastanesi" },
                    { 53, 18, "Yıldırım Devlet Hastanesi" },
                    { 54, 18, "Yıldırım Özel Hastanesi" },
                    { 55, 19, "Gürsu Şehir Hastanesi" },
                    { 56, 19, "Gürsu Devlet Hastanesi" },
                    { 57, 19, "Gürsu Özel Hastanesi" },
                    { 58, 20, "Gemlik Şehir Hastanesi" },
                    { 59, 20, "Gemlik Devlet Hastanesi" },
                    { 60, 20, "Gemlik Özel Hastanesi" },
                    { 61, 21, "Muratpaşa Şehir Hastanesi" },
                    { 62, 21, "Muratpaşa Devlet Hastanesi" },
                    { 63, 21, "Muratpaşa Özel Hastanesi" },
                    { 64, 22, "Konyaaltı Şehir Hastanesi" },
                    { 65, 22, "Konyaaltı Devlet Hastanesi" },
                    { 66, 22, "Konyaaltı Özel Hastanesi" },
                    { 67, 23, "Kepez Şehir Hastanesi" },
                    { 68, 23, "Kepez Devlet Hastanesi" },
                    { 69, 23, "Kepez Özel Hastanesi" },
                    { 70, 24, "Alanya Şehir Hastanesi" },
                    { 71, 24, "Alanya Devlet Hastanesi" },
                    { 72, 24, "Alanya Özel Hastanesi" },
                    { 73, 25, "Manavgat Şehir Hastanesi" },
                    { 74, 25, "Manavgat Devlet Hastanesi" },
                    { 75, 25, "Manavgat Özel Hastanesi" },
                    { 76, 26, "Seyhan Şehir Hastanesi" },
                    { 77, 26, "Seyhan Devlet Hastanesi" },
                    { 78, 26, "Seyhan Özel Hastanesi" },
                    { 79, 27, "Yüreğir Şehir Hastanesi" },
                    { 80, 27, "Yüreğir Devlet Hastanesi" },
                    { 81, 27, "Yüreğir Özel Hastanesi" },
                    { 82, 28, "Çukurova Şehir Hastanesi" },
                    { 83, 28, "Çukurova Devlet Hastanesi" },
                    { 84, 28, "Çukurova Özel Hastanesi" },
                    { 85, 29, "Sarıçam Şehir Hastanesi" },
                    { 86, 29, "Sarıçam Devlet Hastanesi" },
                    { 87, 29, "Sarıçam Özel Hastanesi" },
                    { 88, 30, "Ceyhan Şehir Hastanesi" },
                    { 89, 30, "Ceyhan Devlet Hastanesi" },
                    { 90, 30, "Ceyhan Özel Hastanesi" },
                    { 91, 31, "Selçuklu Şehir Hastanesi" },
                    { 92, 31, "Selçuklu Devlet Hastanesi" },
                    { 93, 31, "Selçuklu Özel Hastanesi" },
                    { 94, 32, "Karatay Şehir Hastanesi" },
                    { 95, 32, "Karatay Devlet Hastanesi" },
                    { 96, 32, "Karatay Özel Hastanesi" },
                    { 97, 33, "Meram Şehir Hastanesi" },
                    { 98, 33, "Meram Devlet Hastanesi" },
                    { 99, 33, "Meram Özel Hastanesi" },
                    { 100, 34, "Ereğli Şehir Hastanesi" },
                    { 101, 34, "Ereğli Devlet Hastanesi" },
                    { 102, 34, "Ereğli Özel Hastanesi" },
                    { 103, 35, "Akşehir Şehir Hastanesi" },
                    { 104, 35, "Akşehir Devlet Hastanesi" },
                    { 105, 35, "Akşehir Özel Hastanesi" },
                    { 106, 36, "Şahinbey Şehir Hastanesi" },
                    { 107, 36, "Şahinbey Devlet Hastanesi" },
                    { 108, 36, "Şahinbey Özel Hastanesi" },
                    { 109, 37, "Şehitkamil Şehir Hastanesi" },
                    { 110, 37, "Şehitkamil Devlet Hastanesi" },
                    { 111, 37, "Şehitkamil Özel Hastanesi" },
                    { 112, 38, "Nizip Şehir Hastanesi" },
                    { 113, 38, "Nizip Devlet Hastanesi" },
                    { 114, 38, "Nizip Özel Hastanesi" },
                    { 115, 39, "İslahiye Şehir Hastanesi" },
                    { 116, 39, "İslahiye Devlet Hastanesi" },
                    { 117, 39, "İslahiye Özel Hastanesi" },
                    { 118, 40, "Karkamış Şehir Hastanesi" },
                    { 119, 40, "Karkamış Devlet Hastanesi" },
                    { 120, 40, "Karkamış Özel Hastanesi" },
                    { 121, 41, "Haliliye Şehir Hastanesi" },
                    { 122, 41, "Haliliye Devlet Hastanesi" },
                    { 123, 41, "Haliliye Özel Hastanesi" },
                    { 124, 42, "Eyyübiye Şehir Hastanesi" },
                    { 125, 42, "Eyyübiye Devlet Hastanesi" },
                    { 126, 42, "Eyyübiye Özel Hastanesi" },
                    { 127, 43, "Karaköprü Şehir Hastanesi" },
                    { 128, 43, "Karaköprü Devlet Hastanesi" },
                    { 129, 43, "Karaköprü Özel Hastanesi" },
                    { 130, 44, "Siverek Şehir Hastanesi" },
                    { 131, 44, "Siverek Devlet Hastanesi" },
                    { 132, 44, "Siverek Özel Hastanesi" },
                    { 133, 45, "Viranşehir Şehir Hastanesi" },
                    { 134, 45, "Viranşehir Devlet Hastanesi" },
                    { 135, 45, "Viranşehir Özel Hastanesi" },
                    { 136, 46, "Akdeniz Şehir Hastanesi" },
                    { 137, 46, "Akdeniz Devlet Hastanesi" },
                    { 138, 46, "Akdeniz Özel Hastanesi" },
                    { 139, 47, "Mezitli Şehir Hastanesi" },
                    { 140, 47, "Mezitli Devlet Hastanesi" },
                    { 141, 47, "Mezitli Özel Hastanesi" },
                    { 142, 48, "Toroslar Şehir Hastanesi" },
                    { 143, 48, "Toroslar Devlet Hastanesi" },
                    { 144, 48, "Toroslar Özel Hastanesi" },
                    { 145, 49, "Yenişehir Şehir Hastanesi" },
                    { 146, 49, "Yenişehir Devlet Hastanesi" },
                    { 147, 49, "Yenişehir Özel Hastanesi" },
                    { 148, 50, "Tarsus Şehir Hastanesi" },
                    { 149, 50, "Tarsus Devlet Hastanesi" },
                    { 150, 50, "Tarsus Özel Hastanesi" }
                });

            migrationBuilder.InsertData(
                table: "ClinicHospital",
                columns: new[] { "ClinicId", "HospitalId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 2 },
                    { 2, 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_AvailabilityId",
                table: "Appointments",
                column: "AvailabilityId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_DoctorId",
                table: "Appointments",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_PatientId",
                table: "Appointments",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ClinicId",
                table: "AspNetUsers",
                column: "ClinicId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_Email",
                table: "AspNetUsers",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_HospitalId",
                table: "AspNetUsers",
                column: "HospitalId");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Availabilities_DoctorId",
                table: "Availabilities",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_ClinicHospital_HospitalId",
                table: "ClinicHospital",
                column: "HospitalId");

            migrationBuilder.CreateIndex(
                name: "IX_Clinics_DistrictId",
                table: "Clinics",
                column: "DistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_Districts_CityId",
                table: "Districts",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Hospitals_DistrictId",
                table: "Hospitals",
                column: "DistrictId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Appointments");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "ClinicHospital");

            migrationBuilder.DropTable(
                name: "Availabilities");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Clinics");

            migrationBuilder.DropTable(
                name: "Hospitals");

            migrationBuilder.DropTable(
                name: "Districts");

            migrationBuilder.DropTable(
                name: "Cities");
        }
    }
}
