namespace AdminProyectos.Models
{
    public class IndiceWorksProyectViewModel
    {
        public Proyect Proyect { get; set; }

        //public IEnumerable<Work> Works { get; set; }

        public PaginationResponse<Work> PaginationResponse { get; set; }
    }
}
