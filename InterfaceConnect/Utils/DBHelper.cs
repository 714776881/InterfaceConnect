using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using Dapper;
using Oracle.ManagedDataAccess.Client;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;

namespace CommMediator.Utils
{
    public class DatabaseHelper
    {
        private readonly IDbConnection _connection;
        public DatabaseHelper(DBType dbType,string connString)
        {
            if(dbType == DBType.SQLSERVER)
            {
                _connection = new SqlConnection(connString);
            }
            else if(dbType == DBType.MYSQL)
            {
                _connection = new MySqlConnection(connString);
            }
            else if(dbType == DBType.ORACLE)
            {
                _connection = new OracleConnection(connString);
            }
            else
            {
                throw new ArgumentException($"Unsupported database type: {dbType}");
            }
        }

        public IEnumerable<T> Query<T>(string sql, object param = null)
        {
            return _connection.Query<T>(sql, param);
        }

        public T QueryFirstOrDefault<T>(string sql, object param = null)
        {
            return _connection.QueryFirstOrDefault<T>(sql, param);
        }

        public int Execute(string sql, object param = null)
        {
            return _connection.Execute(sql, param);
        }

        public T ExecuteScalar<T>(string sql, object param = null)
        {
            return _connection.ExecuteScalar<T>(sql, param);
        }

        public IEnumerable<T> ExecuteStoredProcedure<T>(string storedProcedureName, object param = null)
        {
            return _connection.Query<T>(storedProcedureName, param, commandType: CommandType.StoredProcedure);
        }

        public int Insert<T>(T entity,string key = "Id")
        {
            // 由程序生成GUID，而不是数据库
            var ID = typeof(T).GetProperties().First(x => x.Name == key);
            if (ID != null)
            {
                ID.SetValue(entity, Guid.NewGuid());
            }
            var sql = $"INSERT INTO {typeof(T).Name} VALUES (@{string.Join(", @", GetProperties<T>(key).Select(p => p.Name))})";
            return _connection.Execute(sql, entity);
        }

        public int Update<T>(T entity,string key = "Id")
        {
            var sql = $"UPDATE {typeof(T).Name} SET {string.Join(", ", GetProperties<T>(key).Select(p => $"{p.Name} = @{p.Name}"))} WHERE {key} = @{key}";
            return _connection.Execute(sql, entity);
        }

        public int Delete<T>(int id)
        {
            var sql = $"DELETE FROM {typeof(T).Name} WHERE Id = @Id";
            return _connection.Execute(sql, new { Id = id });
        }

        private static IEnumerable<System.Reflection.PropertyInfo> GetProperties<T>(string key = "Id")
        {
            return typeof(T).GetProperties().Where(p => p.Name != key);
        }
        //// 高级用法
        //private static void test()
        //{
        //    // 事物处理
        //    using (SqlConnection connection = new SqlConnection(connectionString))
        //    {
        //        connection.Open();

        //        using (var transaction = connection.BeginTransaction())
        //        {
        //            try
        //            {
        //                string query1 = "INSERT INTO Customers (CustomerID, CustomerName, ContactName, Country) VALUES (6, 'Blauer See Delikatessen', 'Hanna Moos', 'Germany')";
        //                string query2 = "UPDATE Customers SET Country = 'USA' WHERE CustomerID = 1";

        //                connection.Execute(query1, transaction: transaction);
        //                connection.Execute(query2, transaction: transaction);

        //                transaction.Commit();
        //            }
        //            catch (Exception ex)
        //            {
        //                transaction.Rollback();
        //            }
        //        }
        //    }
        //    、、 
        //}
    }
}
