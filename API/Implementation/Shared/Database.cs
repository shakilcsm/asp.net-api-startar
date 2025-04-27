using System;
using System.Data;
using API.Framework.Shared;
using API.Framework.Shared.Error;
using Microsoft.Data.SqlClient;

namespace API.Implementation.Shared;

public class DatabaseImpl : Database
{
    SqlConnection _connection;


    public DatabaseImpl(IConfiguration configuration)
    {
        _connection = new SqlConnection(configuration[AppConfig.ConnectionString.Dev]!);
    }

    public void OpenConnection()
    {
        if (_connection?.State != ConnectionState.Open)
        {
            try
            {
                _connection?.Open();
            }
            catch (SqlException ex)
            {

                new DatabaseConnectionException(ex.Message);
            }
        }
    }

    public SqlTransaction BeginTransaction(IsolationLevel iso = IsolationLevel.ReadUncommitted)
    {
        return BeginTransaction(iso);
    }

    public SqlCommand GetCommand(string Query)
    {
        if (_connection == null)
        {
            OpenConnection();
        }
        SqlCommand command = new SqlCommand(Query, _connection);
        command.CommandType = CommandType.Text;
        return command;
    }

    public DataSet GetDataSet(string Query)
    {

        DataSet dataset = new DataSet();
        dataset.Tables.Add(new DataTable());

        SqlDataAdapter dataadapter = new SqlDataAdapter(GetCommand(Query));
        dataadapter.Fill(dataset);

        return dataset;
    }

    public SqlCommand GetSPCommand(string StoredProcedure)
    {
        if (_connection == null)
        {
            OpenConnection();
        }
        SqlCommand command = new SqlCommand(StoredProcedure, _connection);
        command.CommandType = CommandType.StoredProcedure;
        return command;
    }

    public void CloseTransaction(bool success, SqlTransaction transaction)
    {
        if (transaction != null)
        {
            if (success)
                transaction.Commit();
            else
                transaction.Rollback();
            if (transaction.Connection != null)
            {
                transaction.Connection.Close();
                transaction.Connection.Dispose();
            }
            transaction.Dispose();
        }
    }

    public void CloseConnection()
    {
        if (_connection != null && _connection.State != ConnectionState.Closed)
        {
            _connection.Close();
        }
    }
}
