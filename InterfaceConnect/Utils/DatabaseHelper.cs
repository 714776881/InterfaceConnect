using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using Dapper;
using Oracle.ManagedDataAccess.Client;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;

namespace InterfaceConnect
{
    public class DatabaseHelper
    {
        private readonly IDbConnection _connection;
        public DatabaseHelper(DBType dbType,string connString)
        {
            _connection = DbConnectionFactory.Produce(dbType, connString);
        }
        public dynamic QueryFirst(string sql, object param = null)
        {
            return _connection.QueryFirstOrDefault(sql, param);
        }
        public int Execute(string sql, object param = null)
        {
            return _connection.Execute(sql, param);
        }
        public IEnumerable<dynamic> Query(string sql, object param = null)
        {
            return _connection.Query(sql, param);
        }
        public DynamicParameters ExecuteStoredProcedure(string storedProcedureName, DynamicParameters param = null)
        {
            _connection.Query(storedProcedureName, param, commandType: CommandType.StoredProcedure);
            return param;
        }
    }
    public class DbConnectionFactory
    {
        public static IDbConnection Produce(DBType dbType, string connString)
        {
            if (dbType == DBType.SQLSERVER)
            {
                return new SqlConnection(connString);
            }
            else if (dbType == DBType.MYSQL)
            {
                return new MySqlConnection(connString);
            }
            else if (dbType == DBType.ORACLE)
            {
                return new OracleConnection(connString);
            }
            else
            {
                throw new ArgumentException($"Unsupported database type: {dbType}");
            }
        }
        //// 支持高级用法,低版本的 Oracle.ManagedDataAccess.Client 无法提供支持
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
        //}
    }
}
