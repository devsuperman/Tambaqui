using Microsoft.EntityFrameworkCore;

namespace Tambaqui.Models
{
    public class Contexto : DbContext
    {   
        public Contexto(DbContextOptions<Contexto> options) : base(options)
        {

        }
                
        public DbSet<Carro> Carros {get;set;}
        public DbSet<Cor> Cores {get;set;} 
    }
}