using WebApplication1000.Context;
using WebApplication1000.Entities;

/**
 * The Database_Repository class is a part of the WebApplication1000.Repository namespace.
 * This class is responsible for handling database operations.
 * It includes methods for getting all users.
 * 
 */

namespace WebApplication1000.Repository;

/// <summary>
/// The Database_Repository class is responsible for handling database operations.
/// </summary>
public class Database_Repository
{
    private MyDbContext _context;
    
    /// <summary>
    /// Initializes a new instance of the Database_Repository class.
    /// </summary>
    /// <param name="context">
    /// The database context.
    /// </param>
    public Database_Repository(MyDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// The GetAllUsers method is responsible for getting all users.
    /// </summary>
    /// <returns>
    /// The list of users.
    /// </returns>
    public List<Osoba> GetAllUsers()
    {
        return _context.Osobas.ToList();
    }
}