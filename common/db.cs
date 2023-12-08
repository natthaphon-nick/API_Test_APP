using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System;

namespace WebApplication2.Common
{
    public class db
    {
        SqlConnection con;
        public db()
        {
            var configuation = GetConfiguration();
            con = new SqlConnection(configuation.GetSection("ConnectionStrings").GetSection("db").Value);
            con.Open();
        }
        public static IConfigurationRoot GetConfiguration()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: false);
            return builder.Build();
        }
        public SqlDataReader ExecQuery(string sql)
        {
            SqlCommand command = con.CreateCommand();
            command.CommandText = sql;
            return command.ExecuteReader();
        }
        public SqlDataReader ExecQuery(string sql, List<SqlParam> SqlParams)
        {
            SqlCommand command = con.CreateCommand();
            command.CommandText = sql;

            foreach (SqlParam param in SqlParams)
            {
                command.Parameters.Add(param.name, SqlDbType.VarChar);
                command.Parameters[param.name].Value = param.value;
            }
            return command.ExecuteReader();
        }
        public int ExecNotQuery(string sql, List<SqlParam> SqlParams)
        {
            SqlCommand command = con.CreateCommand();

            command.CommandText = sql;
            foreach (SqlParam param in SqlParams)
            {
                command.Parameters.Add(param.name, SqlDbType.VarChar);
                command.Parameters[param.name].Value = param.value;
            }
            return command.ExecuteNonQuery();
        }

        public int ExecNotQuery(string sql)
        {
            SqlCommand command = con.CreateCommand();
            command.CommandText = sql;
            return command.ExecuteNonQuery();
        }

        public string DBNULL(int? NulldbB)
        {
            if (!DBNull.Value.Equals(NulldbB))
            {
                return (int?)NulldbB + " ";
            }
            else
                return "null";
        }

        public void Close()
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
                con.Dispose();
            }
        }


        public class SqlParam
        {
            public string name { get; set; }
            public object value { get; set; }
        }
    }
}
