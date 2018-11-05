namespace Tambaqui.Models
{    
    public abstract class Registro
    {
        public int Id { get; set; }
        public bool Ativo { get; set; } = true;        
        public void InverterAtivo() => Ativo = !Ativo;
    }
}