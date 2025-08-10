using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using WebAPI_Hortifruti.Utils.Cache;

namespace WebAPI_Hortifruti.Repositories.SQLServer
{
    public class Fruta : IRepository<Models.Fruta>
    {
        readonly SqlConnection conn;
        readonly SqlCommand cmd;
        readonly ICacheService cacheService;
        readonly string KeyCache;

        public int CacheExpirationTime { get; set; }
        public Fruta(string connectionString)
        {
            conn = new SqlConnection(connectionString);
            cmd = new SqlCommand();
            cmd.Connection = conn;
            cacheService = new MemoryCacheService();
            KeyCache = "frutas";
            CacheExpirationTime = 30;
        }
        

        public async Task AddASync(Models.Fruta fruta)
        {
            using (conn)
            {
                await conn.OpenAsync();

                using (cmd)
                {
                    cmd.CommandText = "insert into Frutas (Nome, DataVenc) values (@nome, @datavenc); select scope_identity();";
                    cmd.Parameters.Add(new SqlParameter("@nome", SqlDbType.VarChar)).Value = fruta.Nome;
                    cmd.Parameters.Add(new SqlParameter("@datavenc", SqlDbType.Date)).Value = fruta.DataVenc;

                    fruta.Id = Convert.ToInt32(await cmd.ExecuteScalarAsync());

                    cacheService.Remove(KeyCache);
                }
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            int linhasAfetadas = 0;

            using (conn)
            {
                await conn.OpenAsync();

                using (cmd)
                {
                    cmd.CommandText = "delete from Frutas where Id = @id";
                    cmd.Parameters.Add(new SqlParameter("@id", SqlDbType.Int)).Value = id;

                    linhasAfetadas = await cmd.ExecuteNonQueryAsync();


                }
            }
            if (linhasAfetadas == 0)
                return false;
            cacheService.Remove(KeyCache);
            return true;
        }

        public async Task<List<Models.Fruta>> GetAllAsync()
        {
            List<Models.Fruta> frutas;
            frutas = cacheService.Get<List<Models.Fruta>>(KeyCache);

            if (frutas != null)
                return frutas;

            frutas= new List<Models.Fruta>();

            using (conn)
            {
                await conn.OpenAsync();
                using (cmd)
                {
                    cmd.CommandText = "select Id, Nome, DataVenc from Frutas;";
                    SqlDataReader dr = await cmd.ExecuteReaderAsync();
                    while (await dr.ReadAsync())
                    {
                        Models.Fruta fruta = new Models.Fruta();
                        Mapper(fruta, dr);
                        frutas.Add(fruta);
                    }
                }
            }
            cacheService.Set(KeyCache, frutas, CacheExpirationTime);
            return frutas;
        }

        public async Task<Models.Fruta> GetByIdAsync(int id)
        {
            Models.Fruta fruta = new Models.Fruta();
            using (conn)
            {
                await conn.OpenAsync();
                using (cmd)
                {
                    cmd.CommandText = "select Id, Nome, DataVenc from Frutas where Id = @id;";
                    cmd.Parameters.Add(new SqlParameter("@id", SqlDbType.Int)).Value = id;
                    SqlDataReader dr = await cmd.ExecuteReaderAsync();
                    if (await dr.ReadAsync())
                    {
                        Mapper(fruta, dr);                     
                    }
                }
            }
            return fruta;
        }

        public async Task<List<Models.Fruta>> GetByNameAsync(string nome)
        {
            List<Models.Fruta> frutas = new List<Models.Fruta>();

            using (conn)
            {
                await conn.OpenAsync();
                using (cmd)
                {
                    cmd.CommandText = "select Id, Nome, DataVenc from Frutas where Nome like @nome;";
                    cmd.Parameters.Add(new SqlParameter("@nome", SqlDbType.VarChar)).Value = $"%{nome}%";
                    SqlDataReader dr = await cmd.ExecuteReaderAsync();
                    while (await dr.ReadAsync())
                    {
                        Models.Fruta fruta = new Models.Fruta();
                        Mapper(fruta, dr);
                        frutas.Add(fruta);
                    }
                }
            }
            
            return frutas;
        }

        public async Task<bool> UpdateAsync(Models.Fruta fruta)
        {
            int linhasAfetadas = 0;

            using (conn)
            {
                await conn.OpenAsync();

                using (cmd)
                {
                    cmd.CommandText = "update Frutas set Nome = @nome, DataVenc = @datavenc where Id = @id";
                    cmd.Parameters.Add(new SqlParameter("@nome", SqlDbType.VarChar)).Value = fruta.Nome;
                    cmd.Parameters.Add(new SqlParameter("@datavenc", SqlDbType.Date)).Value = fruta.DataVenc;
                    cmd.Parameters.Add(new SqlParameter("@id", SqlDbType.Int)).Value = fruta.Id;

                    linhasAfetadas = await cmd.ExecuteNonQueryAsync();

                    
                }
            }
            if (linhasAfetadas == 0)
                return false;
            cacheService.Remove(KeyCache);
            return true;
        }

        private void Mapper(Models.Fruta fruta, SqlDataReader dr)
        {
            fruta.Id = (int)dr["Id"];
            fruta.Nome = (string)dr["Nome"];
            fruta.DataVenc = (DateTime)dr["DataVenc"];
        }


    }
}