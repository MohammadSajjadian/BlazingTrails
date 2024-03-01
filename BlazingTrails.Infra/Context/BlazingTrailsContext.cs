using BlazingTrails.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BlazingTrails.Infra.Context
{
    public class BlazingTrailsContext : DbContext
    {
        public DbSet<Trail> trails { get; set; }
        public DbSet<Waypoint> waypoints { get; set; }

        public BlazingTrailsContext(DbContextOptions options) : base(options) { }
    }
}
