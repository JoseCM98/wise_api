using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Data;
using System.Reflection.Emit;
using wise_api.Configurations;
using wise_api.Entities;

namespace wise_api.Context
{
    public class DataContext: DbContext
    {
        public DataContext()
        {
        }
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        { }


        public DbSet<Linea> Linea { get; set; }
        public DbSet<BackGroundTime> BackGroundTime { get; set; }
        public DbSet<BackNames> BackNames { get; set; }
        public DbSet<BackNamesHistory> BackNamesHistories { get; set; }
        public DbSet<BackNamesState> BackNamesState { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new LineaConfigurations());
            modelBuilder.ApplyConfiguration(new BackGroundTimeConfigurations());
            modelBuilder.ApplyConfiguration(new BackNamesConfigurations());
            modelBuilder.ApplyConfiguration(new BackNamesStateConfigurations());
            modelBuilder.ApplyConfiguration(new BackNamesHistoryConfigurations());

        }
    }
}
