﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using RDFSurveyForm.Model;
using RDFSurveyForm.Model.Setup;
using RDFSurveyForm.Model.Unit_SubUnit;
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
        public virtual DbSet<GroupSurvey> GroupSurvey { get; set; }
        public virtual DbSet<SurveyGenerator> SurveyGenerator { get; set; }
        public virtual DbSet<SurveyScore> SurveyScores { get; set; }
        public virtual DbSet<Unit> Units { get; set; }
        public virtual DbSet<Subunit> Subunits { get; set; }
        
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserRoleConfiguration());
        }

    }


    //Select* from Customer
    //Select* from CRole
    //Select* from Department

    //Select* from Branches
    //Select* from Groups
    //Select* from Category

    //Select* from GroupSurvey
    //Select* from SurveyGenerator
    //Select* from SurveyScores

    //Select* from Units
    //Select* from Subunits

}
