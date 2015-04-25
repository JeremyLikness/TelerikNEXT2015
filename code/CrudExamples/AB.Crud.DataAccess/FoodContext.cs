using System.Data.Entity;
using AA.Crud.Domain;

namespace AB.Crud.DataAccess
{
    public class FoodContext : DbContext
    {
        public FoodContext() : base("FoodDb")
        {
            
        }

        public DbSet<FoodDescription> FoodDescriptions { get; set; }
    }
}
