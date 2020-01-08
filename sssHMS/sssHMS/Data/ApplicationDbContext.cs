using System;
using System.Collections.Generic;
using System.Text;
using sssHMS.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using sssHMS.Models;

namespace sssHMS.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Ambulance> Ambulances { get; set; }
        public DbSet<Medicine> Medicines { get; set; }
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<Blood> Bloods { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Designation> Designations { get; set; }
        public DbSet<Gender> Genders { get; set; }
        public DbSet<MaritalStatus> MaritalStatuses { get; set; }
        public DbSet<Nationality> Nationalities { get; set; }
        public DbSet<PatientType> PatientTypes { get; set; }
        public DbSet<Prefix> Prefixes { get; set; }
        public DbSet<Status> Statuses { get; set; }

        public DbSet<Unit> Units { get; set; }

        public DbSet<Doctor> Doctors { get; set; }

        public DbSet<DoctorUnit> DoctorUnits { get; set; }
        public DbSet<DoctorDeptUnit> DoctorDeptUnits { get; set; }

        public DbSet<Country> Countries { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<Township> Townships { get; set; }
        public DbSet<sssHMSInfo> sssHMSInfos { get; set; }
        public DbSet<DoctorSchedule> DoctorSchedules { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<ServicePrice> ServicePrices { get; set; }
        public DbSet<IdentityType> IdentityTypes { get; set; }

        public DbSet<Test> Tests { get; set; }




        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured)
            {

            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);



        }
    }
}
