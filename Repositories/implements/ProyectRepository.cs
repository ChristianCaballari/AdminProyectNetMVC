using AdminProyectos.Models;
using AdminProyectos.services;
using Dapper;
using Microsoft.Data.SqlClient;

namespace AdminProyectos.Repositories.implements
{
    public class ProyectRepository : IProyectRepository
    {
        private readonly string _connectionString;

        public ProyectRepository(IConfiguration configuration) 
        {
            this._connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<int> Count(int userId)
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.ExecuteScalarAsync<int>($@"SP_Coun_Proyect", new { userId },
                commandType: System.Data.CommandType.StoredProcedure);
        }

        public async Task Create(Proyect proyect)
        {
            using var connection = new SqlConnection(_connectionString);
            var id = await connection.QuerySingleAsync<int>($@"SP_Insert_Proyect",
                new { proyect.Name, proyect.Description, proyect.DeliverDate, proyect.Client, proyect.UserId },
                commandType: System.Data.CommandType.StoredProcedure);
            proyect.Id = id;
        }

        public async Task<bool> Delete(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            var deleted = await connection.ExecuteAsync($@"SP_Delete_Proyect", new { id },
                commandType:System.Data.CommandType.StoredProcedure);
            return deleted == 1;
        }

        public async Task<Proyect> GetById(int id, int userId)
        {
            using var connection = new SqlConnection(_connectionString);
            var proyect =  await connection.QueryFirstOrDefaultAsync<Proyect>($@"SP_SL_Get_Proyect_ById", new { id, userId},
                commandType: System.Data.CommandType.StoredProcedure);
            var no = 2;
            return proyect;
        }

        public async Task<IEnumerable<Proyect>> Search(int userId, PaginationViewModel pagination)
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.QueryAsync<Proyect>($@"SP_SL_Pagination_Proyect", new { userId, pagination.RecordsASkip, pagination.RecordsByPage },
                commandType: System.Data.CommandType.StoredProcedure); 
        }

        public async Task Update(Proyect proyect)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.ExecuteAsync($@"SP_Update_Proyect",
                new { proyect.Id, proyect.Name, proyect.Description, proyect.DeliverDate, proyect.Client, proyect.UserId },
                commandType: System.Data.CommandType.StoredProcedure);
        }
    }
}
