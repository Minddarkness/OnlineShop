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
        private SqlDataAdapter _sqlDataAdapter;
        private DataTable _dataTable;
        private DataRowView _dateRowView;
        
        public MainWindow()
        {
            InitializeComponent();
        }

        private void GridView_OnCurrentCellChanged(object? sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void GridView_OnCellEditEnding(object? sender, DataGridCellEditEndingEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}