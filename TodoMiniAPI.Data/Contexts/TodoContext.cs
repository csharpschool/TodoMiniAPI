using Microsoft.EntityFrameworkCore;
using TodoMiniAPI.Data.Entities;

namespace TodoMiniAPI.Data.Contexts;

public class TodoContext : DbContext
{
    public DbSet<Todo> Todos => Set<Todo>();
    public DbSet<Status> Statuses => Set<Status>();
    public DbSet<Tag> Tags => Set<Tag>();
    public DbSet<TodoTag> TodoTags => Set<TodoTag>();

    public TodoContext(DbContextOptions<TodoContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
       base.OnModelCreating(builder);

        builder.Entity<Todo>()
            .HasMany(t => t.Tags)
            .WithMany(t => t.Todos)
            .UsingEntity<TodoTag>(
                j => j.HasOne(tt => tt.Tag).WithMany().HasForeignKey(tt => tt.TagId),
                j => j.HasOne(tt => tt.Todo).WithMany().HasForeignKey(tt => tt.TodoId)
            );

        builder.Entity<Tag>()
            .HasMany(t => t.Todos)
            .WithMany(t => t.Tags)
            .UsingEntity<TodoTag>(
                j => j.HasOne(tt => tt.Todo).WithMany().HasForeignKey(tt => tt.TodoId),
                j => j.HasOne(tt => tt.Tag).WithMany().HasForeignKey(tt => tt.TagId)
            );
    }
}
