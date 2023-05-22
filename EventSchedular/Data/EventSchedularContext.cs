using EventSchedularProject.Models;
using Microsoft.EntityFrameworkCore;

namespace EventSchedularProject.Data
{
    public class EventSchedularContext : DbContext
    {
        public EventSchedularContext(DbContextOptions<EventSchedularContext> options) : base(options)
        {
           
        }
        public DbSet<Event> Event { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Event>().ToTable("Event");
            modelBuilder.Entity<Event>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Location)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DateTime)
                    .IsRequired();
                    
            });

           
        }
    }
}
