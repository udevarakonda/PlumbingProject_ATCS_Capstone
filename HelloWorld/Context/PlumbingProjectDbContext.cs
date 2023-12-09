using Microsoft.EntityFrameworkCore;

public class PlumbingProjectDbContext : DbContext

{
    public DbSet<string> Entities {get; set;}

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        
        string server = "sqlclassdb-instance-1.cqjxl5z5vyvr.us-east-2.rds.amazonaws.com";
        string database = "capstone_2324_pallet"; 
        string user = "pallet";
        string password = "8KFj9WnbfRDS";
        
        
        optionsBuilder.UseMySQL($"server={server};database={database};user={user};password={password}");
    }
}
