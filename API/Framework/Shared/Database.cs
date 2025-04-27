using System.Data;
using Microsoft.Data.SqlClient;

namespace API.Framework.Shared;

public interface Database
{
    DataSet GetDataSet(string Query);
    SqlCommand GetSPCommand(string StoredProcedure);

    SqlTransaction BeginTransaction(IsolationLevel iso = IsolationLevel.ReadUncommitted);
    void CloseTransaction(bool success, SqlTransaction transaction);

    void OpenConnection();

    void CloseConnection();

}
