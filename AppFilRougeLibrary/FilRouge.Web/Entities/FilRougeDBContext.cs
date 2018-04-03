﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;


namespace FilRouge.Web.Entities
{
    public class FilRougeDBContext : DbContext
    {
        
        public FilRougeDBContext() :base("BDDAppliFilRouge")
        {
          // Database.SetInitializer(new DropCreateDatabaseAlways<FilRougeDBContext>()); //Pour la création de la base
           Database.SetInitializer(new MigrateDatabaseToLatestVersion<FilRougeDBContext, Migrations.Configuration>()); //Pour la migration
        }
        //Documentation http://www.entityframeworktutorial.net/code-first/code-based-migration-in-code-first.aspx


        public DbSet<Contact> Contact { get; set; }
        public DbSet<DifficultyMaster> DifficultyMasters { get; set; }
        public DbSet<Difficulty> Difficulties { get; set; }
        public DbSet<DifficultyRate> DifficultyRates { get; set; }
        public DbSet<Questions> Questions { get; set; }
        public DbSet<Quizz> Quizz { get; set; }
        public DbSet<Reponses> Reponse { get; set; }
        public DbSet<Technology> Technologies { get; set; }
        public DbSet<TypeQuestion> TypeQuestion { get; set; }
        public DbSet<UserReponse> UserReponse { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }
    }
}
