using TorcLib.Domain.BaseDefinitions;

namespace TorcLib.Domain.Aggregates;

public class Book : Entity
{
    public Book()
    {
        
    }
    public Book( string title, string firstName, string lastName, int totalCopies, int copiesInUse, string type, string isbn, string category)
    {
        Title = title;
        FirstName = firstName;
        LastName = lastName;
        TotalCopies = totalCopies;
        CopiesInUse = copiesInUse;
        Type = type;
        ISBN = isbn;
        Category = category;
    }
    
    public string Title { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int TotalCopies { get; set; }
    public int CopiesInUse { get; set; }
    public string Type { get; set; }
    public string ISBN { get; set; }
    public string Category { get; set; }
}