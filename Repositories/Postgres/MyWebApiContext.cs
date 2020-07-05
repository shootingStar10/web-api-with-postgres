using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWebApplication2.Repositories.Postgres
{
    public class MyWebApiContext : DbContext
    {
        public MyWebApiContext(DbContextOptions<MyWebApiContext> options) : base(options) { }

        public DbSet<Student> Students { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {
            modelBuilder.Entity<Student>().ToTable("student");
            modelBuilder.Entity<Student>().Property(p => p.RollNo).HasColumnName("roll_no");
            modelBuilder.Entity<Student>().Property(p => p.Name).HasColumnName("name");
            modelBuilder.Entity<Student>().Property(p => p.Age).HasColumnName("age");
            modelBuilder.Entity<Student>().HasKey(k => new { k.RollNo });
        }
    }
}
