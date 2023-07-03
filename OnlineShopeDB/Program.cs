// See https://aka.ms/new-console-template for more information

using System.Data;
using System.Data.SqlClient;

var sqlConnectionStrBuilder = new SqlConnectionStringBuilder()
{
    DataSource = "(localdb)\\MSSQLLocalDB",
    InitialCatalog = "OnlineShop",
    IntegratedSecurity = true,
    Pooling = false,
    UserID = "anastasiya",
    Password = "fgh5hh34klertfg"
};

Console.WriteLine($"Connection string: {sqlConnectionStrBuilder.ConnectionString}");

try
{
    using (SqlConnection connection = new SqlConnection(sqlConnectionStrBuilder.ConnectionString))
    {
        connection.Open();

        string[] sqls =
        {
            //"INSERT INTO [client] (last_name, name, patronymic, phone_number, email) VALUES (N'Иванов', N'Роман', N'Ивановоич', N'89786785544', N'Ivan_rom_h@bk.ru');",
            //"INSERT INTO [client] (last_name, name, patronymic, phone_number, email) VALUES (N'Соколов', N'Иван', N'Васильевич', N'89136782311', N'FgHTom@bk.ru');"
        };

        SqlCommand sqlCommand;
        for (int i = 0; i < sqls.Length; i++)
        {
            sqlCommand = new SqlCommand(sqls[i], connection);
            sqlCommand.ExecuteNonQuery();
        }
    }
}
catch (Exception e)
{
    Console.WriteLine(e.Message);
}
