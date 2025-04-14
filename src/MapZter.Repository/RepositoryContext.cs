using MapZter.Entity.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MapZter.Repository;

public class RepositoryContext : IdentityDbContext
{
    public RepositoryContext(DbContextOptions options) : base(options)
    {}

    public DbSet<Place> Places { get; set; }
    
    public DbSet<GeoPoint> GeoPoints { get; set; }

    public DbSet<PlaceTag> PlaceTags { get; set; }
}