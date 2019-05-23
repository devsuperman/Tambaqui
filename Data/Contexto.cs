using Microsoft.EntityFrameworkCore;

namespace Tambaqui.Models
{
    public class Contexto : DbContext
    {   
        public Contexto(DbContextOptions<Contexto> options) : base(options)
        {

        }
                
        public DbSet<Pessoa> Pessoas {get;set;}
    }
}