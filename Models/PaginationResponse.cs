namespace AdminProyectos.Models
{
    public class PaginationResponse
    {
        public int Page { get; set; }
        public int RecordsByPage { get; set; }

        public int QuantityTotalRecords { get; set; }

        // 100 / 25 => 4 paginas, 100 records y mostrar en 25 en 25, van a 
        //ser 4 paginas de 25 registros.
        public int QuantityTotalOfPage => (int)Math.Ceiling((double)QuantityTotalRecords/RecordsByPage);

        public string BaseURL { get; set; }
    }
    public class PaginationResponse<T>: PaginationResponse
    { 
      public IEnumerable<T> Elements { get; set; }
    }
}
