// See https://aka.ms/new-console-template for more information

using System.Data;
using System.Data.SqlClient;

var sqlConnectionStrBuilder = new SqlConnectionStringBuilder()
{
    DataSource = "(localdb)\\MSSQLLocalDB",
    InitialCatalog= "MSSQLLocalDemo",
    IntegratedSecurity = true,
    Pooling = false
};

Console.WriteLine($"Connection string: {sqlConnectionStrBuilder.ConnectionString}");
SqlConnection connection = new SqlConnection(sqlConnectionStrBuilder.ConnectionString);

StateChangeEventHandler stateChangeEventHandler = AtConnectionStateChang;
connection.StateChange += stateChangeEventHandler;
try
{
    connection.Open();
}
catch (Exception e)
{
    Console.WriteLine(e.Message);
}
finally
{
    connection.Close();
}

// using (SqlConnection connection = new SqlConnection(sqlConnectionStrBuilder.ConnectionString))
// {
//     connection.Open();
// }


void AtConnectionStateChang(object sender, StateChangeEventArgs e)
{
    Console.WriteLine($"Connection state: {(sender as SqlConnection).State}");
}