namespace ToDoApp.Models;

using Microsoft.EntityFrameworkCore;

public class ToDoContext(DbContextOptions<ToDoContext> options) : DbContext(options)
{
    public DbSet<Task> Tasks { get; set; }
}