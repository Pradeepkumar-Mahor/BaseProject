using Dapper;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Text;

namespace DataAccess.Context
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly IDbConnection _connection;
        private readonly string connectionString = " ";   //YOUR_SQL_CONNECTION_STRING

        public GenericRepository()
        {
            _connection = new SqlConnection(connectionString);
        }

        public bool Add(T entity)
        {
            int rowsEffected;
            try
            {
                string tableName = GetTableName();
                string columns = GetColumns(excludeKey: true);
                string properties = GetPropertyNames(excludeKey: true);
                string query = $"INSERT INTO {tableName} ({columns}) VALUES ({properties})";
                rowsEffected = _connection.Execute(query, entity);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            return rowsEffected > 0 ? true : false;
        }

        public bool Delete(T entity)
        {
            int rowsEffected;
            try
            {
                string tableName = GetTableName();
                string keyColumn = GetKeyColumnName();
                string keyProperty = GetKeyPropertyName();
                string query = $"DELETE FROM {tableName} WHERE {keyColumn} = @{keyProperty}";
                rowsEffected = _connection.Execute(query, entity);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            return rowsEffected > 0 ? true : false;
        }

        public IEnumerable<T> GetAll()
        {
            IEnumerable<T> result;
            try
            {
                string tableName = GetTableName();
                string query = $"SELECT * FROM {tableName}";
                result = _connection.Query<T>(query);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            return result;
        }

        public T GetById(int Id)
        {
            IEnumerable<T> result;
            try
            {
                string tableName = GetTableName();
                string keyColumn = GetKeyColumnName();
                string query = $"SELECT * FROM {tableName} WHERE {keyColumn} = '{Id}'";
                result = _connection.Query<T>(query);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            return result.FirstOrDefault();
        }

        public bool Update(T entity)
        {
            int rowsEffected;
            try
            {
                string tableName = GetTableName();
                string keyColumn = GetKeyColumnName();
                string keyProperty = GetKeyPropertyName();
                StringBuilder query = new();
                _ = query.Append($"UPDATE {tableName} SET ");
                foreach (PropertyInfo property in GetProperties(true))
                {
                    ColumnAttribute? columnAttr = property.GetCustomAttribute<ColumnAttribute>();
                    string propertyName = property.Name;
                    string columnName = columnAttr.Name;
                    _ = query.Append($"{columnName} = @{propertyName},");
                }
                _ = query.Remove(query.Length - 1, 1);
                _ = query.Append($" WHERE {keyColumn} = @{keyProperty}");
                rowsEffected = _connection.Execute(query.ToString(), entity);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            return rowsEffected > 0 ? true : false;
        }

        private string GetTableName()
        {
            Type type = typeof(T);
            TableAttribute? tableAttr = type.GetCustomAttribute<TableAttribute>();
            if (tableAttr != null)
            {
                string tableName = tableAttr.Name;
                return tableName;
            }
            return type.Name + "s";
        }

        public static string GetKeyColumnName()
        {
            PropertyInfo[] properties = typeof(T).GetProperties();
            foreach (PropertyInfo property in properties)
            {
                object[] keyAttributes = property.GetCustomAttributes(typeof(KeyAttribute), true);
                if (keyAttributes != null && keyAttributes.Length > 0)
                {
                    object[] columnAttributes = property.GetCustomAttributes(typeof(ColumnAttribute), true);
                    if (columnAttributes != null && columnAttributes.Length > 0)
                    {
                        ColumnAttribute columnAttribute = (ColumnAttribute)columnAttributes[0];
                        return columnAttribute.Name;
                    }
                    else
                    {
                        return property.Name;
                    }
                }
            }
            return null;
        }

        private string GetColumns(bool excludeKey = false)
        {
            Type type = typeof(T);
            string columns = string.Join(", ", type.GetProperties()
                .Where(p => !excludeKey || !p.IsDefined(typeof(KeyAttribute)))
                .Select(p =>
                {
                    ColumnAttribute? columnAttr = p.GetCustomAttribute<ColumnAttribute>();
                    return columnAttr != null ? columnAttr.Name : p.Name;
                }));
            return columns;
        }

        protected string GetPropertyNames(bool excludeKey = false)
        {
            IEnumerable<PropertyInfo> properties = typeof(T).GetProperties()
                .Where(p => !excludeKey || p.GetCustomAttribute<KeyAttribute>() == null);
            string values = string.Join(", ", properties.Select(p =>
            {
                return $"@{p.Name}";
            }));
            return values;
        }

        protected IEnumerable<PropertyInfo> GetProperties(bool excludeKey = false)
        {
            IEnumerable<PropertyInfo> properties = typeof(T).GetProperties()
                .Where(p => !excludeKey || p.GetCustomAttribute<KeyAttribute>() == null);
            return properties;
        }

        protected string GetKeyPropertyName()
        {
            IEnumerable<PropertyInfo> properties = typeof(T).GetProperties()
                .Where(p => p.GetCustomAttribute<KeyAttribute>() != null);
            if (properties.Any())
            {
                return properties.FirstOrDefault().Name;
            }
            return null;
        }
    }
}