using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperProject.Services
{
    public class Dapperr<T> : IDapperr<T>
    {
        //ConnectionString
        string Connection = @"Server=.;Database=DapperTest;Trusted_Connection=True;";

        public async Task<List<T>> Get(string where, int pageid = 1, bool IsDesc = false)
        {
            List<T> list;
            //query
            string query = $"SELECT * FROM [{typeof(T).Name}] ";
            //Filter
            if (!string.IsNullOrEmpty(where))
            {
                query += $"WHERE ${where} ";
            }
            //Order By
            query += $"ORDER BY Id {(IsDesc ? "DESC" : string.Empty)} ";
            //Paging
            query += $"OFFSET {((pageid - 1) * 30)} ROWS ";
            query += "FETCH NEXT 30 ROWS ONLY ";

            //Execute
            using (IDbConnection db = new SqlConnection(Connection))
            {
                list = db.Query<T>(query).ToList();
            }
            //Return
            return await Task.FromResult(list);
        }

        public async Task<T> GetById(int id)
        {
            T Record;
            using (IDbConnection db = new SqlConnection(Connection))
            {
                Record = db.Query<T>($"Select * From [{typeof(T).Name}] Where Id=@Id", new { Id = id }).Single();
            }
            return await Task.FromResult(Record);
        }

        public async Task<bool> Add(T entity)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(Connection))
                {
                    string query =
                 $"Insert Into [{typeof(T).Name}] ({ string.Join(",", typeof(T).GetProperties().Select(f => f.Name).ToList()) }) Values ({ string.Join(",", typeof(T).GetProperties().Select(f => "@" + f.Name).ToList()) })";
                    db.Execute(query, entity);
                }
                return await Task.FromResult(true);
            }
            catch (Exception ex)
            {
                var error = ex;
                return await Task.FromResult(false);
            }
        }

        public async Task<bool> Update(T entity)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(Connection))
                {
                    string query =
                   $"Update [{typeof(T).Name}] Set { string.Join(",", typeof(T).GetProperties().Select(f => f.Name + "=@" + f.Name).ToList()) } Where ID=@ID";
                    db.Execute(query, entity);
                }
                return await Task.FromResult(true);
            }
            catch
            {
                return await Task.FromResult(false);
            }
        }

        public async Task<bool> Delete(T entity,int id)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(Connection))
                {
                    string query = $"Delete From [{typeof(T).Name}] Where ID=@ID";
                    db.Execute(query, new { ID = id });
                }
                return await Task.FromResult(true);
            }
            catch
            {
                return await Task.FromResult(false);
            }
        }

        public async Task<int> Count(T entity, string where)
        {
            int count = 0;
            string query = $"SELECT COUNT(*) FROM [{typeof(T).Name}] ";
            if (!string.IsNullOrEmpty(where))
            {
                query += $"WHERE ${where} ";
            }
            using (IDbConnection db = new SqlConnection(Connection))
            {
                count = Convert.ToInt32(db.Execute(query));
            }
            return await Task.FromResult(count);
        }

        public void Dispose()
        {
            Dispose();
        }
    }
}
