using AdminProyectos.Models;

namespace AdminProyectos.services
{
    public interface ITaskRepository
    {
        Task Create(Work work);
        Task Update(Work word);
        Task<IEnumerable<Work>> Search(int userId, int proyectId, PaginationViewModel pagination);
        Task<IEnumerable<Work>> GetAll(int userId, int proyectId);
        Task<Work> GetById(int id);
        Task<bool> Delete(int id,int proyectId);
        Task<int> Count(int proyectId);
    }
}
