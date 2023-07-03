using System;
using System.Data;
using System.Data.SqlClient;

namespace OnlineShopDesktop;

public class ClientTable
{
    private SqlConnection _sqlconnection;
    private SqlDataAdapter _sqlDataAdapter;
    private DataTable _dataTable;
    private DataRowView _dataRowView;
    private SqlConnectionStringBuilder _connectionStringBuilder;
    
    public ClientTable(Action<DataView> setDataCallback)
    {
        ConnectToDateBase();
        _dataTable = new DataTable();
        _sqlDataAdapter = new SqlDataAdapter();
        SetSelectCommand();
        SetInsertCommand();
        SetUpdateCommand();
        SetDeleteComand();
        _sqlDataAdapter.Fill(_dataTable);
        setDataCallback?.Invoke(_dataTable.DefaultView);
    }
    
    public void AddRow(DataRow dataRow)
    {
        _dataTable.Rows.Add(dataRow);
        _sqlDataAdapter.Update(_dataTable);
    }

    public DataRow GetNewRow()
    {
        return _dataTable.NewRow();
    }
    
    public void Delete(DataRowView dataRowView)
    {
        if (dataRowView == null)
        {
            throw new NullReferenceException("Выделите строку для удаления");
        }
            
        dataRowView.Row.Delete();
        _sqlDataAdapter.Update(_dataTable);
    }

    public void BeginEdit(DataRowView dataRowView)
    {
        _dataRowView = dataRowView;
        _dataRowView.BeginEdit();
    }
    
    public void EndEdit()
    {
        if (_dataRowView == null)
        {
            return;
        }
            
        _dataRowView.EndEdit();
        _sqlDataAdapter.Update(_dataTable);
    }

    private string GetConnectionString()
    {
        return _connectionStringBuilder.ConnectionString;
    }
    
    private void ConnectToDateBase()
    {
        _connectionStringBuilder = new SqlConnectionStringBuilder()
        {
            DataSource = "(localdb)\\MSSQLLocalDB",
            InitialCatalog = "OnlineShop",
            IntegratedSecurity = true,
            Pooling = false,
            UserID = "anastasiya",
            Password = "fgh5hh34klertfg"
        };
            
        Console.WriteLine($"Connection string: {_connectionStringBuilder.ConnectionString}");
            
        try
        {
            _sqlconnection = new SqlConnection(_connectionStringBuilder.ConnectionString);
            _sqlconnection.Open();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }
    
    private void SetUpdateCommand()
    {
        string clientId = "client_id";
        string lastName = "last_name";
        string name = "name";
        string patronymic = "patronymic";
        string phone_number = "phone_number";
        string email = "email";
        
        var insert = @"UPDATE client 
                                SET 
                                    last_name = @last_name, 
                                    name = @name, 
                                    patronymic = @patronymic, 
                                    phone_number = @phone_number, 
                                    email = @email
                                WHERE client_id = @client_id";
        _sqlDataAdapter.UpdateCommand = new SqlCommand(insert, _sqlconnection);
        _sqlDataAdapter.UpdateCommand.Parameters.Add("@" + clientId, SqlDbType.Int, 4, clientId).SourceVersion =
            DataRowVersion.Original;
        _sqlDataAdapter.UpdateCommand.Parameters.Add("@" + lastName, SqlDbType.NVarChar, 50, lastName);
        _sqlDataAdapter.UpdateCommand.Parameters.Add("@" + name, SqlDbType.NVarChar, 50, name);
        _sqlDataAdapter.UpdateCommand.Parameters.Add("@" + patronymic, SqlDbType.NVarChar, 50, patronymic);
        _sqlDataAdapter.UpdateCommand.Parameters.Add("@" + phone_number, SqlDbType.NVarChar, 50, phone_number);
        _sqlDataAdapter.UpdateCommand.Parameters.Add("@" + email, SqlDbType.NVarChar, 50, email);
    }

    private void SetDeleteComand()
    {
        string clientId = "client_id";

        //TODO попробовать вставить переменные
        var insert = @"DELETE FROM client WHERE client_id = @client_id";
        _sqlDataAdapter.DeleteCommand = new SqlCommand(insert, _sqlconnection);
        _sqlDataAdapter.DeleteCommand.Parameters.Add("@" + clientId, SqlDbType.Int, 4, clientId);
    }

    private void SetInsertCommand()
    {
        string clientId = "client_id";
        string lastName = "last_name";
        string name = "name";
        string patronymic = "patronymic";
        string phone_number = "phone_number";
        string email = "email";
        
        var insert = @"INSERT INTO client (last_name, name, patronymic, phone_number, email)
                            VALUES (@last_name, @name, @patronymic, @phone_number, @email);
                            SET @client_id = @@IDENTITY";
        _sqlDataAdapter.InsertCommand = new SqlCommand(insert, _sqlconnection);
        _sqlDataAdapter.InsertCommand.Parameters.Add("@" + clientId, SqlDbType.Int, 4, clientId).Direction = ParameterDirection.Output;
        _sqlDataAdapter.InsertCommand.Parameters.Add("@" + lastName, SqlDbType.NVarChar, 50, lastName);
        _sqlDataAdapter.InsertCommand.Parameters.Add("@" + name, SqlDbType.NVarChar, 50, name);
        _sqlDataAdapter.InsertCommand.Parameters.Add("@" + patronymic, SqlDbType.NVarChar, 50, patronymic);
        _sqlDataAdapter.InsertCommand.Parameters.Add("@" + phone_number, SqlDbType.NVarChar, 50, phone_number);
        _sqlDataAdapter.InsertCommand.Parameters.Add("@" + email, SqlDbType.NVarChar, 50, email);
    }

    private void SetSelectCommand()
    {
        var select = @"SELECT * FROM client ORDER BY client_id;";
        _sqlDataAdapter.SelectCommand = new SqlCommand(select, _sqlconnection);
    }
}
