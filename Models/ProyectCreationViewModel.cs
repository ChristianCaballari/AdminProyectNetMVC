using System.ComponentModel.DataAnnotations;

namespace AdminProyectos.Models
{
    public class ProyectCreationViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Nombre")]
        public string Name { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Descripción")]
        public string Description { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Fecha de Entrega")]
        [DataType(DataType.Date)]
       // [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DeliverDate { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Nombre de Cliente")]
        public string Client { get; set; }
        public int? UserId { get; set; }//Usuario quien crea el proyecto
    }
}
