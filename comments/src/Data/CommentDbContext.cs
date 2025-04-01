using HappyHeadlines.Comments.Models;
using Microsoft.EntityFrameworkCore;

namespace HappyHeadlines.Comments.Data
{
    public class CommentDbContext : DbContext
    {
        public CommentDbContext(DbContextOptions<CommentDbContext> options) : base(options) { }

        public DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Comment>()
                .ToTable("comments")
                .HasKey(c => c.Id);

            modelBuilder.Entity<Comment>()
                .Property(c => c.Content).HasColumnName("content").IsRequired();
            
            modelBuilder.Entity<Comment>()
                .Property(c => c.ArticleId).HasColumnName("articleId");
            
            modelBuilder.Entity<Comment>()
                .Property(c => c.AuthorId).HasColumnName("authorId");   

            modelBuilder.Entity<Comment>()
                .Property(c => c.CreatedAt).HasColumnName("createdAt")
                .HasDefaultValueSql("CURRENT_TIMESTAMP");
            
            modelBuilder.Entity<Comment>()
                .Property(c => c.UpdatedAt).HasColumnName("updatedAt")
                .HasDefaultValueSql("CURRENT_TIMESTAMP");
        }
    }
}
