using System;
using System.Data.OleDb;
using System.Data;

namespace OnlineShopDesktop;

public class ProductTable
{
    private OleDbConnection _oleDbConnection;
    private OleDbDataAdapter _oleDbDataAdapter;
    private DataTable _dataTable;
    private DataRowView _dataRowView;
    private OleDbConnectionStringBuilder _connectionStringBuilder;
    
    public ProductTable(Action<DataView> setDataCallback)
    {
        ConnectToDateBase();
        // _dataTable = new DataTable();
        // _oleDbDataAdapter = new OleDbDataAdapter();
        // SetSelectCommand();
        // SetInsertCommand();
        // SetUpdateCommand();
        // SetDeleteComand();
        // _oleDbDataAdapter.Fill(_dataTable);
        // setDataCallback?.Invoke(_dataTable.DefaultView);
    }

    private void ConnectToDateBase()
    {
        _connectionStringBuilder = new OleDbConnectionStringBuilder()
        {
            DataSource = "(localdb)\\MSSQLLocalDB",
            // InitialCatalog = "OnlineShop",
            // IntegratedSecurity = true,
            // Pooling = false,
            // UserID = "anastasiya",
            // Password = "fgh5hh34klertfg"
        };
            
        Console.WriteLine($"Connection string: {_connectionStringBuilder.ConnectionString}");
            
        try
        {
            _oleDbConnection = new OleDbConnection(_connectionStringBuilder.ConnectionString);
            _oleDbConnection.Open();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }
}