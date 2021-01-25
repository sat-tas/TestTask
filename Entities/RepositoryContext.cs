using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    public class RepositoryContext : DbContext
    {

        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Position> Positions { get; set; }

        public RepositoryContext(DbContextOptions<RepositoryContext> options) : base(options)
        {
         //   Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().Property(p => p.DepartmentId).HasDefaultValueSql("1");
            modelBuilder.Entity<Employee>().Property(p => p.PositionId).HasDefaultValueSql("1");

            modelBuilder.Entity<Department>().Property(p => p.DateAdded).HasDefaultValueSql("getdate()");
            modelBuilder.Entity<Employee>().Property(p => p.DateAdded).HasDefaultValueSql("getdate()");

            modelBuilder.Entity<Department>().HasData(
                new Department {Id=1, Name = "Не назначен"});

            modelBuilder.Entity<Position>().HasData(
                new Position {Id=1, Name = "Не назначен"});
        }
    }
}
