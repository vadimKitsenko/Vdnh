using Core.Database;
using Core.Models.Entities.System;
using Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using static System.Runtime.InteropServices.Marshalling.IIUnknownCacheStrategy;
using System.Runtime.ConstrainedExecution;
using Data.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Core.Models.Configuration;

namespace Data.Context
{
    public partial class VdnhContext(DbContextOptions<VdnhContext> options)
        : BaseDbContext<
            Param,
            FieldType,
            DataType,
            BaseFile,
            RoleFormView,
            FormView,
            BaseUser,
            BaseUserRole,
            BaseRole
        >(options)

    {
        public DbSet<Image>? Images { get; set; }
        public DbSet<Title>? Titles { get; set; }
        public DbSet<About>? Abouts { get; set; }
        public DbSet<Header>? Headers { get; set; }
        public DbSet<Interval>? Intervals { get; set; }
        public DbSet<Map>? Maps { get; set; }
        public DbSet<Horisontal>? Horisontals { get; set; }
        public DbSet<SecondLevel>? SecondLevels { get; set; }
        public DbSet<Vertical>? Verticals { get; set; }

        /*protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {

                if (!optionsBuilder.IsConfigured)
                    optionsBuilder.UseNpgsql(
                        "Server=localhost;Port=5432;User Id=postgres;Password=qwerty123456;Database=VdnhBd;"
                        
                    );
            }*/

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

/*public class VdnhContextFactory : IDesignTimeDbContextFactory<VdnhContext>
{
    public VdnhContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<VdnhContext>();
        optionsBuilder.UseNpgsql(
            "Server=localhost;Port=5432;User Id=postgres;Password=qwerty123456;Database=VdnhBd;"
        );

        return new VdnhContext(optionsBuilder.Options);
    }
}*/
