using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;

namespace InterfaceConnect
{
    public class Orm
    {
        private readonly IDbConnection _connection;
        public Orm(DBType dbType, string connString)
        {
            _connection = DbConnectionFactory.Produce(dbType, connString);
        }
        public int Execute(string sql, object param = null)
        {
            return _connection.Execute(sql, param);
        }
        public T QueryFirst<T>(string sql, object param = null)
        {
            return _connection.QueryFirstOrDefault<T>(sql, param);
        }
        public IEnumerable<T> Query<T>(string sql, object param = null)
        {
            return _connection.Query<T>(sql, param);
        }
        public int Insert<T>(T entity, string tableName = "",string key = "")
        {
            if (tableName == "") tableName = typeof(T).Name;
            var sql = $"INSERT INTO {tableName} ({string.Join(",", GetProperties<T>(key).Select(p => p.Name))}) VALUES (:{string.Join(", :", GetProperties<T>().Select(p => p.Name))})";
            return _connection.Execute(sql, entity);
        }
        public int Update<T>(T entity, string tableName = "",string key = "Id")
        {
            if (tableName == "") tableName = typeof(T).Name;
            var sql = $"UPDATE {tableName} SET {string.Join(", ", GetProperties<T>().Select(p => $"{p.Name} = :{p.Name}"))} WHERE {key} = :{key}";
            return _connection.Execute(sql, entity);
        }
        public int Delete<T>(int id, string tableName = "",string key = "Id")
        {
            if (tableName == "") tableName = typeof(T).Name;
            var sql = $"DELETE FROM {tableName} WHERE {key} = :Id";
            return _connection.Execute(sql, new { Id = key });
        }
        private static IEnumerable<System.Reflection.PropertyInfo> GetProperties<T>(string key = "Id")
        {
            return typeof(T).GetProperties().Where(p => p.Name != key);
        }
        public IEnumerable<T> ExecuteStoredProcedure<T>(string storedProcedureName, object param = null)
        {
            return _connection.Query<T>(storedProcedureName, param, commandType: CommandType.StoredProcedure);
        }
    }
}
