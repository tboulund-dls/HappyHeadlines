using Microsoft.EntityFrameworkCore;

namespace DraftService.Models;

public class DraftContext : DbContext
{
    public DraftContext(DbContextOptions<DraftContext> options) : base(options)
    {
    }
    
    public DbSet<Draft> Drafts { get; set; }
}