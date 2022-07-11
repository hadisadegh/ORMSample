using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperProject.Services
{
    public interface IDapperr<T> : IDisposable
    {
        Task<List<T>> Get(string where, int pageid = 1, bool IsDesc = false);
        Task<T> GetById(int id);
        Task<bool> Add(T entity);
        Task<bool> Update(T entity);
        Task<bool> Delete(T entity, int id);
        Task<int> Count(T entity, string where);
    }
}
