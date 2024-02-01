using AdminProyectos.Models;
using AdminProyectos.services;
using Dapper;
using Microsoft.Data.SqlClient;

namespace AdminProyectos.Repositories.implements
{
    public class TaskRepository : ITaskRepository
    {
        private readonly string _connectionString;

        public TaskRepository(IConfiguration configuration) 
        {
            this._connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<int> Count(int proyectId)
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.ExecuteScalarAsync<int>($@"SP_Count_Work_By_Proyect", new { proyectId },
                commandType: System.Data.CommandType.StoredProcedure);
        }

        public async Task Create(Work work)
        {
            using var connection = new SqlConnection(_connectionString);
            var id = await connection.QuerySingleAsync<int>($@"SP_Insert_Work",
                new { work.Name, work.Priority, work.DeliverDate, work.Description, work.ProyectId },
                commandType: System.Data.CommandType.StoredProcedure);
            work.Id = id;
            return;
        }


        public async Task<bool> Delete(int id,int proyectId)
        {
            using var connection = new SqlConnection(_connectionString);
            var deleted = await connection.ExecuteAsync($@"SP_Delete_Work", new { id, proyectId },
                commandType: System.Data.CommandType.StoredProcedure);
            return deleted == 1;

        }

        public async Task<Work> GetById(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.QueryFirstOrDefaultAsync<Work>($@"SP_SL_Get_Task_ById",
                new { id }, commandType: System.Data.CommandType.StoredProcedure);
        }

        public async Task<IEnumerable<Work>> Search(int userId, int proyectId, PaginationViewModel pagination)
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.QueryAsync<Work>($@"SP_SL_Pagination_Work", new { userId, proyectId, pagination.RecordsASkip, pagination.RecordsByPage },
               commandType: System.Data.CommandType.StoredProcedure);
        }

        public async Task<IEnumerable<Work>> GetAll(int userId, int proyectId)
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.QueryAsync<Work>($@"SP_Get_All_Task", new { userId, proyectId },
               commandType: System.Data.CommandType.StoredProcedure);
        }

        public async Task Update(Work work)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.ExecuteAsync($@"SP_Update_Work", 
            new {work.Id, work.Name, work.Priority, work.DeliverDate, work.Description,work.ProyectId },
            commandType: System.Data.CommandType.StoredProcedure);
        }

    }
}
