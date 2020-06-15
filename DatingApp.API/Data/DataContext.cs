using DatingApp.API.Models;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.API.Data
{
  public class DataContext : DbContext
  {
    public DbSet<Value> Values { get; set; } // Values is used to represent the table name

    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }
  }
}