namespace AdminProyectos.Models
{
    public class PaginationViewModel
    {
        public int page { get; set; } = 1;

        public int recordsByPage { get; set; } = 10;

        private readonly int quantityMaximumRecordsPerPage = 50;

        public int RecordsByPage 
        {
            get 
            { 
                return recordsByPage;
            }

            set 
            {
               recordsByPage = (value > quantityMaximumRecordsPerPage)?
                                quantityMaximumRecordsPerPage : value;
            }
        }
        public int RecordsASkip => recordsByPage * (page - 1);
    }
}
