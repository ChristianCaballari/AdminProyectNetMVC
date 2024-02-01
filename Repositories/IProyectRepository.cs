using AdminProyectos.Models;

namespace AdminProyectos.services
{
    public interface IProyectRepository
    {
        Task Create(Proyect proyect);
        Task Update (Proyect proyect);
        Task<IEnumerable<Proyect>> Search(int userId,PaginationViewModel pagination);
        Task<Proyect> GetById(int id,int userId);
        Task<bool> Delete(int id);
        Task<int> Count(int userId);
    }
}
