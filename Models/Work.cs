using System.ComponentModel.DataAnnotations;

namespace AdminProyectos.Models
{
    public class Work
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="El campo {0} es requerido")]
        [Display(Name="Nombre")]
        public string Name { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Descripción")]
        public string Description { get; set; }
        public bool State { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Fecha de Entrega")]
        [DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:dd MMM yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DeliverDate { get; set;}
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Prioridad")]
        public Priority Priority { get; set;}
        public int ProyectId { get; set; }
        public Proyect Proyect { get; set; }
    }
}
