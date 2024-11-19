using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public interface ISqlDataAccess
    {
        public Task<IEnumerable<T>> GetData<T, P>(string spName, P parameters, string connectionId = "DefaultConnection");

        public Task<bool> SaveData<T>(string spName, T parameters, string connectionId = "DefaultConnection");
    }
}