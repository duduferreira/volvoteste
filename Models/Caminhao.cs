using System.ComponentModel.DataAnnotations;
namespace Volvo.Models
{
    public class Caminhao
    {
        public int CaminhaoID { get; set; }
        [Required]
        public string Descricao { get; set; }
        [Required]
        [MaxLength(4)]
        public string Ano { get; set; }
        [Required]
        [MaxLength(4)]
        public string AnoModelo { get; set; }
        public int ModeloID { get; set; }
        public Modelo Modelo { get; set; }

    }
}