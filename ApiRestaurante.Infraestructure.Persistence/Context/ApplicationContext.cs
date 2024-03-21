using ApiRestaurante.Core.Domain.Commont;
using ApiRestaurante.Core.Domain.Entities;
using ApiRestaurante.Infraestructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiRestaurante.Infraestructure.Persistence.Context
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Ingredients> Ingredients { get; set; }
        public DbSet<Dishes> Dishes { get; set; }
        public DbSet<DishesIngredients> DishesIngredients { get; set; }
        public DbSet<DishesOrders> DishesOrders { get; set; }
        public DbSet<Tables> Tables { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<AuditableBaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.Created = DateTime.Now;
                        entry.Entity.CreatedBy = "DefauldAppUser";
                        break;

                    case EntityState.Modified:
                        entry.Entity.LasModified = DateTime.Now;
                        entry.Entity.LastModifiedBy = "DefauldAppUser";
                        break;
                }

            }
            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            #region "Tables"
            modelBuilder.Entity<Ingredients>().ToTable("Ingredients");
            modelBuilder.Entity<Dishes>().ToTable("Dishes");
            modelBuilder.Entity<Tables>().ToTable("Tables");
            modelBuilder.Entity<DishesOrders>().ToTable("DishesOrders");
            #endregion

            #region "Primary Key"
            modelBuilder.Entity<Ingredients>().HasKey(i => i.Id);
            modelBuilder.Entity<Dishes>().HasKey(d => d.Id);
            modelBuilder.Entity<Tables>().HasKey(t => t.Id);
            #endregion

            #region "Relations"

            #region "Dishes Ingredients"

            modelBuilder.Entity<DishesIngredients>()
                 .HasKey(di => new { di.IngredientId, di.DishesId });


            modelBuilder.Entity<DishesIngredients>()
                .HasOne(di => di.Ingredients)
                .WithMany(i => i.DishesIngredients)
                .HasForeignKey(di => di.IngredientId);


            modelBuilder.Entity<DishesIngredients>()
                .HasOne(di => di.Dishes)
                .WithMany(d => d.DishesIngredients)
                .HasForeignKey(di => di.DishesId);
            #endregion

            #region "Dishes Orders"
            modelBuilder.Entity<DishesOrders>()
                .HasKey(di => new { di.OrdersId, di.DishesID });


            modelBuilder.Entity<DishesOrders>()
                .HasOne(di => di.Orders)
                .WithMany(i => i.DishesOrders)
                .HasForeignKey(di => di.OrdersId);


            modelBuilder.Entity<DishesOrders>()
                .HasOne(di => di.Dishes)
                .WithMany(d => d.DishesOrders)
                .HasForeignKey(di => di.DishesID);
            #endregion

            #region "Tables"
            modelBuilder.Entity<Tables>().HasMany(t => t.Orders)
                .WithOne(o => o.Tables).HasForeignKey(o => o.TableId)
                .OnDelete(DeleteBehavior.Cascade);
            #endregion

            #endregion

            #region "Property Settings"

            #region "Ingredients"
            modelBuilder.Entity<Ingredients>().Property(i => i.Name).IsRequired();
            #endregion

            #region "Dishes"
            modelBuilder.Entity<Dishes>().Property(i => i.Name).IsRequired();
            modelBuilder.Entity<Dishes>().Property(i => i.Price).IsRequired();
            modelBuilder.Entity<Dishes>().Property(i => i.NumberOfPerson).IsRequired();
            modelBuilder.Entity<Dishes>().Property(i => i.DishCategory).IsRequired();
            #endregion

            #region "Tables"
            modelBuilder.Entity<Tables>().Property(t => t.NumberOfPeoplePerTable).IsRequired();
            modelBuilder.Entity<Tables>().Property(t => t.Description).IsRequired();
            modelBuilder.Entity<Tables>().Property(t => t.State).IsRequired();
            #endregion
            #endregion
        }
    }
}