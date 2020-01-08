using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace sssHMS.Data.Migrations
{
    public partial class dbAll : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ambulances",
                columns: table => new
                {
                    Amb_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Amb_num = table.Column<int>(nullable: false),
                    Capacity = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ambulances", x => x.Amb_id);
                });

            migrationBuilder.CreateTable(
                name: "Bloods",
                columns: table => new
                {
                    BloodID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BloodType = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bloods", x => x.BloodID);
                });

            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    CountryId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CountryName = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.CountryId);
                });

            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    DepartmentID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DepartmentName = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.DepartmentID);
                });

            migrationBuilder.CreateTable(
                name: "Designations",
                columns: table => new
                {
                    DesignationID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DesignationName = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Designations", x => x.DesignationID);
                });

            migrationBuilder.CreateTable(
                name: "DoctorSchedules",
                columns: table => new
                {
                    DoctorScheduleId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DoctorID = table.Column<int>(nullable: false),
                    DoctorName = table.Column<string>(nullable: true),
                    DepartmentID = table.Column<int>(nullable: false),
                    DepartmentName = table.Column<string>(nullable: true),
                    UnitID = table.Column<int>(nullable: false),
                    UnitName = table.Column<string>(nullable: true),
                    WeekDay = table.Column<string>(nullable: true),
                    StartTime = table.Column<DateTime>(nullable: false),
                    EndTime = table.Column<DateTime>(nullable: false),
                    PerPatientTime = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoctorSchedules", x => x.DoctorScheduleId);
                });

            migrationBuilder.CreateTable(
                name: "Drivers",
                columns: table => new
                {
                    Dr_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Dr_name = table.Column<string>(nullable: true),
                    MOB = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    Shift = table.Column<string>(nullable: true),
                    Salary = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drivers", x => x.Dr_id);
                });

            migrationBuilder.CreateTable(
                name: "Genders",
                columns: table => new
                {
                    GenderID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    GenderName = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genders", x => x.GenderID);
                });

            migrationBuilder.CreateTable(
                name: "IdentityTypes",
                columns: table => new
                {
                    IdentityTypeID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IdentityTypeName = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityTypes", x => x.IdentityTypeID);
                });

            migrationBuilder.CreateTable(
                name: "MaritalStatuses",
                columns: table => new
                {
                    MaritalStatusID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MaritalStatusName = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaritalStatuses", x => x.MaritalStatusID);
                });

            migrationBuilder.CreateTable(
                name: "Medicines",
                columns: table => new
                {
                    Mdcn_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Mdcn_name = table.Column<string>(nullable: true),
                    company = table.Column<string>(nullable: true),
                    m_date = table.Column<DateTime>(nullable: false),
                    e_date = table.Column<DateTime>(nullable: false),
                    price = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medicines", x => x.Mdcn_id);
                });

            migrationBuilder.CreateTable(
                name: "Nationalities",
                columns: table => new
                {
                    NationalityID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    NationalityName = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nationalities", x => x.NationalityID);
                });

            migrationBuilder.CreateTable(
                name: "PatientTypes",
                columns: table => new
                {
                    PatientTypeID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PatientTypeName = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientTypes", x => x.PatientTypeID);
                });

            migrationBuilder.CreateTable(
                name: "Prefixes",
                columns: table => new
                {
                    PrefixID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PrefixName = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prefixes", x => x.PrefixID);
                });

            migrationBuilder.CreateTable(
                name: "Services",
                columns: table => new
                {
                    ServiceID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ServiceName = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Services", x => x.ServiceID);
                });

            migrationBuilder.CreateTable(
                name: "sssHMSInfos",
                columns: table => new
                {
                    sssHMSId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    sssHMSName = table.Column<string>(nullable: false),
                    sssHMSCode = table.Column<string>(nullable: false),
                    sssHMSAddress = table.Column<string>(nullable: false),
                    sssHMSPhoneNo1 = table.Column<string>(nullable: true),
                    sssHMSPhoneNo2 = table.Column<string>(nullable: true),
                    sssHMSPhoneNo3 = table.Column<string>(nullable: true),
                    sssHMSPhoneNo4 = table.Column<string>(nullable: true),
                    sssHMSEmail = table.Column<string>(nullable: true),
                    sssHMSWebsite = table.Column<string>(nullable: true),
                    sssHMSImage = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sssHMSInfos", x => x.sssHMSId);
                });

            migrationBuilder.CreateTable(
                name: "Statuses",
                columns: table => new
                {
                    StatusID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    StatusName = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Statuses", x => x.StatusID);
                });

            migrationBuilder.CreateTable(
                name: "Tests",
                columns: table => new
                {
                    CustomerId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Country = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tests", x => x.CustomerId);
                });

            migrationBuilder.CreateTable(
                name: "States",
                columns: table => new
                {
                    StateId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    StateName = table.Column<string>(nullable: false),
                    CountryId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_States", x => x.StateId);
                    table.ForeignKey(
                        name: "FK_States_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "CountryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Units",
                columns: table => new
                {
                    UnitID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UnitName = table.Column<string>(nullable: false),
                    DepartmentID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Units", x => x.UnitID);
                    table.ForeignKey(
                        name: "FK_Units_Departments_DepartmentID",
                        column: x => x.DepartmentID,
                        principalTable: "Departments",
                        principalColumn: "DepartmentID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ServicePrices",
                columns: table => new
                {
                    ServicePriceID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ServiceID = table.Column<int>(nullable: false),
                    LocalPrice = table.Column<double>(nullable: true),
                    ForeignerFormulaPrice = table.Column<double>(nullable: true),
                    ForeignerFixedPrice = table.Column<double>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServicePrices", x => x.ServicePriceID);
                    table.ForeignKey(
                        name: "FK_ServicePrices_Services_ServiceID",
                        column: x => x.ServiceID,
                        principalTable: "Services",
                        principalColumn: "ServiceID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Doctors",
                columns: table => new
                {
                    DoctorID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    GenCode = table.Column<string>(nullable: true),
                    FirstName = table.Column<string>(nullable: false),
                    PhoneNumber1 = table.Column<string>(nullable: false),
                    PhoneNumber2 = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    Image = table.Column<string>(nullable: true),
                    DOB = table.Column<DateTime>(nullable: false),
                    JobStart = table.Column<DateTime>(nullable: true),
                    JobEnd = table.Column<DateTime>(nullable: true),
                    DescriptionforLeave = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    Degree = table.Column<string>(nullable: true),
                    DesignationID = table.Column<int>(nullable: false),
                    UnitName = table.Column<string>(nullable: true),
                    StatusID = table.Column<int>(nullable: false),
                    BloodID = table.Column<int>(nullable: false),
                    Gender = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doctors", x => x.DoctorID);
                    table.ForeignKey(
                        name: "FK_Doctors_Bloods_BloodID",
                        column: x => x.BloodID,
                        principalTable: "Bloods",
                        principalColumn: "BloodID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Doctors_Designations_DesignationID",
                        column: x => x.DesignationID,
                        principalTable: "Designations",
                        principalColumn: "DesignationID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Doctors_Statuses_StatusID",
                        column: x => x.StatusID,
                        principalTable: "Statuses",
                        principalColumn: "StatusID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Townships",
                columns: table => new
                {
                    TownshipId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TownshipName = table.Column<string>(nullable: false),
                    StateId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Townships", x => x.TownshipId);
                    table.ForeignKey(
                        name: "FK_Townships_States_StateId",
                        column: x => x.StateId,
                        principalTable: "States",
                        principalColumn: "StateId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DoctorDeptUnits",
                columns: table => new
                {
                    DoctorDeptUnitID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DoctorID = table.Column<int>(nullable: false),
                    DepartmentID = table.Column<int>(nullable: false),
                    UnitID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoctorDeptUnits", x => x.DoctorDeptUnitID);
                    table.ForeignKey(
                        name: "FK_DoctorDeptUnits_Departments_DepartmentID",
                        column: x => x.DepartmentID,
                        principalTable: "Departments",
                        principalColumn: "DepartmentID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DoctorDeptUnits_Doctors_DoctorID",
                        column: x => x.DoctorID,
                        principalTable: "Doctors",
                        principalColumn: "DoctorID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DoctorDeptUnits_Units_UnitID",
                        column: x => x.UnitID,
                        principalTable: "Units",
                        principalColumn: "UnitID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "DoctorUnits",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DoctorID = table.Column<int>(nullable: false),
                    UnitID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoctorUnits", x => x.ID);
                    table.ForeignKey(
                        name: "FK_DoctorUnits_Doctors_DoctorID",
                        column: x => x.DoctorID,
                        principalTable: "Doctors",
                        principalColumn: "DoctorID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DoctorUnits_Units_UnitID",
                        column: x => x.UnitID,
                        principalTable: "Units",
                        principalColumn: "UnitID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DoctorDeptUnits_DepartmentID",
                table: "DoctorDeptUnits",
                column: "DepartmentID");

            migrationBuilder.CreateIndex(
                name: "IX_DoctorDeptUnits_DoctorID",
                table: "DoctorDeptUnits",
                column: "DoctorID");

            migrationBuilder.CreateIndex(
                name: "IX_DoctorDeptUnits_UnitID",
                table: "DoctorDeptUnits",
                column: "UnitID");

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_BloodID",
                table: "Doctors",
                column: "BloodID");

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_DesignationID",
                table: "Doctors",
                column: "DesignationID");

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_StatusID",
                table: "Doctors",
                column: "StatusID");

            migrationBuilder.CreateIndex(
                name: "IX_DoctorUnits_DoctorID",
                table: "DoctorUnits",
                column: "DoctorID");

            migrationBuilder.CreateIndex(
                name: "IX_DoctorUnits_UnitID",
                table: "DoctorUnits",
                column: "UnitID");

            migrationBuilder.CreateIndex(
                name: "IX_ServicePrices_ServiceID",
                table: "ServicePrices",
                column: "ServiceID");

            migrationBuilder.CreateIndex(
                name: "IX_States_CountryId",
                table: "States",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Townships_StateId",
                table: "Townships",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_Units_DepartmentID",
                table: "Units",
                column: "DepartmentID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ambulances");

            migrationBuilder.DropTable(
                name: "DoctorDeptUnits");

            migrationBuilder.DropTable(
                name: "DoctorSchedules");

            migrationBuilder.DropTable(
                name: "DoctorUnits");

            migrationBuilder.DropTable(
                name: "Drivers");

            migrationBuilder.DropTable(
                name: "Genders");

            migrationBuilder.DropTable(
                name: "IdentityTypes");

            migrationBuilder.DropTable(
                name: "MaritalStatuses");

            migrationBuilder.DropTable(
                name: "Medicines");

            migrationBuilder.DropTable(
                name: "Nationalities");

            migrationBuilder.DropTable(
                name: "PatientTypes");

            migrationBuilder.DropTable(
                name: "Prefixes");

            migrationBuilder.DropTable(
                name: "ServicePrices");

            migrationBuilder.DropTable(
                name: "sssHMSInfos");

            migrationBuilder.DropTable(
                name: "Tests");

            migrationBuilder.DropTable(
                name: "Townships");

            migrationBuilder.DropTable(
                name: "Doctors");

            migrationBuilder.DropTable(
                name: "Units");

            migrationBuilder.DropTable(
                name: "Services");

            migrationBuilder.DropTable(
                name: "States");

            migrationBuilder.DropTable(
                name: "Bloods");

            migrationBuilder.DropTable(
                name: "Designations");

            migrationBuilder.DropTable(
                name: "Statuses");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropTable(
                name: "Countries");
        }
    }
}
