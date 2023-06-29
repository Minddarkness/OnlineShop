using System;
using System.Data;
using System.Windows;
using System.Data.SqlClient;
using System.Windows.Controls;

namespace OnlineShopDesktop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private SqlConnection _sqlconnection;
        private SqlDataAdapter _sqlDataAdapter; // для связывания визуальной части с бд
        private DataTable _dataTable; // связать полученные данные с внешним ui
        private DataRowView _dataRowView; // для отслеживания изменния в ячейках
        
        public MainWindow()
        {
            InitializeComponent();
            ConnectToDateBase();
            _dataTable = new DataTable();
            _sqlDataAdapter = new SqlDataAdapter();

            SetSelectCommand();
            SetInsertCommand();
            SetUpdateCommand();
            SetDeleteComand();
            
            _sqlDataAdapter.Fill(_dataTable);
            gridView.DataContext = _dataTable.DefaultView;
        }

        private void SetUpdateCommand()
        {
            string clientId = "client_id";
            string lastName = "last_name";
            string name = "name";
            string patronymic = "patronymic";
            string phone_number = "phone_number";
            string email = "email";
            
            //TODO попробовать вставить переменные
            //TODO попробовать закоммитить сет
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
            
            //TODO попробовать вставить переменные
            //TODO попробовать закоммитить сет
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

        private void ConnectToDateBase()
        {
            var sqlConnectionStrBuilder = new SqlConnectionStringBuilder()
            {
                DataSource = "(localdb)\\MSSQLLocalDB",
                InitialCatalog= "OnlineShop",
                IntegratedSecurity = true,
                Pooling = false
            };
            
            Console.WriteLine($"Connection string: {sqlConnectionStrBuilder.ConnectionString}");
            
            try
            {
                _sqlconnection = new SqlConnection(sqlConnectionStrBuilder.ConnectionString);
                _sqlconnection.Open();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private void MenuItemAddClick(object? sender, EventArgs e)
        {
            DataRow dataRow = _dataTable.NewRow();
            AddClientWindow addWindow = new AddClientWindow(dataRow);
            addWindow.ShowDialog();

            if (addWindow.DialogResult == null)
            {
                MessageBox.Show("DialogResult = null");
                return;
            }
            
            if (addWindow.DialogResult.Value)
            {
                _dataTable.Rows.Add(dataRow);
                _sqlDataAdapter.Update(_dataTable);
            }
        }
        
        private void MenuItemDeleteClick(object? sender, EventArgs e)
        {
            _dataRowView = (DataRowView)gridView.SelectedItem;
            
            if (_dataRowView == null)
            {
                MessageBox.Show("Выделите строку для удаления");
                return;
            }
            
            _dataRowView.Row.Delete();
            
            try
            {
                _sqlDataAdapter.Update(_dataTable);
            }
            catch (Exception exception)
            {
                MessageBox.Show($"exception da = {exception.Message}");
            }
        }
        
        private void GridViewOnCellEditEnding(object? sender, DataGridCellEditEndingEventArgs e)
        {
            _dataRowView = (DataRowView)gridView.SelectedItem;
            _dataRowView.BeginEdit();
        }
        
        private void GridViewOnCurrentCellChanged(object? sender, EventArgs e)
        {
            if (_dataRowView == null)
            {
                return;
            }
            
            _dataRowView.EndEdit();
            _sqlDataAdapter.Update(_dataTable);
        }
    }
}