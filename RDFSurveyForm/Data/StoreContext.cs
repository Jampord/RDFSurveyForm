using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using RDFSurveyForm.Model;
using RDFSurveyForm.Setup;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

namespace RDFSurveyForm.Data
{
    public class StoreContext : DbContext
    {
        public StoreContext(DbContextOptions<StoreContext> options) : base(options) { }

        public virtual DbSet<User> Customer { get; set; }
        public virtual DbSet<Role> CRole { get; set; }
        public virtual DbSet<Department> Department { get; set; }
        public virtual DbSet<Branch> Branches { get; set; }
        public virtual DbSet<Groups> Groups { get; set; }
        public virtual DbSet<Category> Category { get; set; }

        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserRoleConfiguration());
        }

    }





}
