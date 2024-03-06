using System.Reflection;
using Microsoft.EntityFrameworkCore;
using TorcLib.Domain.Aggregates;

namespace TorcLib.Data.Context;

public interface IPlatformContext : IContextBase
{
}

public class LibraryContext : ContextBase, IPlatformContext
{
    public LibraryContext(DbContextOptions<LibraryContext> options ) : base(options)
    {
    }

    //Entities
    public DbSet<Book> Books { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Book>().ToTable(nameof(Book));

        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(builder);
        
    }
}