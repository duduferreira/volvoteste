using System.ComponentModel.DataAnnotations;

namespace volvoTest.Models
{
    public class Modelo
    {
        public int ID { get; set; }
        [Required]
        public string Descricao { get; set; }
        [Required]
        public string Nome { get; set; }
        [Required]
        public bool Ativo { get; set; }
    }
}