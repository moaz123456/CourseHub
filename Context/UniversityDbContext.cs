using Microsoft.EntityFrameworkCore;
using UniversityManagement.API.Models;

namespace UniversityManagement.API.Context
{
    public class UniversityDbContext : DbContext
    {
        public UniversityDbContext(DbContextOptions<UniversityDbContext> options)
       : base(options)
        { }

        public DbSet<Student> Students { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // =========================
            // Student
            // =========================

            modelBuilder.Entity<Student>()
                .HasKey(s => s.Id);

            modelBuilder.Entity<Student>()
                .Property(s => s.FullName)
                .IsRequired()
                .HasMaxLength(50);

            modelBuilder.Entity<Student>()
                .Property(s => s.Email)
                .IsRequired()
                .HasMaxLength(100);

            // =========================
            // Student -> Department
            // One Department has Many Students
            // =========================

            modelBuilder.Entity<Student>()
                .HasOne(s => s.Department)
                .WithMany(d => d.Students)
                .HasForeignKey(s => s.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);

            // =========================
            // Student -> Address
            // One-to-One
            // =========================

            modelBuilder.Entity<Student>()
                .HasOne(s => s.Address)
                .WithOne(a => a.Student)
                .HasForeignKey<Address>(a => a.StudentId)
                .OnDelete(DeleteBehavior.Cascade);

            // =========================
            // Student -> Enrollment
            // One Student has Many Enrollments
            // =========================

            modelBuilder.Entity<Student>()
                .HasMany(s => s.Enrollments)
                .WithOne(e => e.Student)
                .HasForeignKey(e => e.StudentId)
                .OnDelete(DeleteBehavior.Cascade);

            // =========================
            // Course -> Enrollment
            // One Course has Many Enrollments
            // =========================

            modelBuilder.Entity<Course>()
                .HasMany(c => c.Enrollments)
                .WithOne(e => e.Course)
                .HasForeignKey(e => e.CourseId)
                .OnDelete(DeleteBehavior.Cascade);

            // =========================
            // Enrollment Composite Key
            // =========================

            modelBuilder.Entity<Enrollment>()
                .HasKey(e => new
                {
                    e.StudentId,
                    e.CourseId
                });

            // =========================
            // Enrollment
            // =========================
            modelBuilder.Entity<Enrollment>()
            .Property(e => e.Grade)
            .HasPrecision(5, 2);
            // =========================
            // Course
            // =========================

            modelBuilder.Entity<Course>()
                .Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<Course>()
                .Property(c => c.Credits)
                .IsRequired();

            // =========================
            // Department
            // =========================

            modelBuilder.Entity<Department>()
                .Property(d => d.Name)
                .IsRequired()
                .HasMaxLength(100);

            // =========================
            // Address
            // =========================

            modelBuilder.Entity<Address>()
                .Property(a => a.City)
                .IsRequired()
                .HasMaxLength(50);

            modelBuilder.Entity<Address>()
                .Property(a => a.Street)
                .IsRequired()
                .HasMaxLength(100);
        }
    }
}