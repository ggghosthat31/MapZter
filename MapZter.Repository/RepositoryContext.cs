using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MapZter.Repository;

public class RepostioryContext : IdentityDbContext
{
    public RepostioryContext(DbContextOptions options) : base(options)
    {
    }
}