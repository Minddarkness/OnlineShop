using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace OnlineShopDesktop
{
    public partial class MainWindow : Window
    {
        private ClientTable _clientTable;
        
        public MainWindow()
        {
            InitializeComponent();
            _clientTable = new ClientTable(SetClientDataCallback);
        }

        private void SetClientDataCallback(DataView dateDataView)
        {
            clientGridView.DataContext = dateDataView;
        }
        
        private void ClientMenuItemAddClick(object? sender, EventArgs e)
        {
            DataRow dataRow = _clientTable.GetNewRow();
            AddClientWindow addWindow = new AddClientWindow(dataRow);
            addWindow.ShowDialog();

            if (addWindow.DialogResult == null)
            {
                MessageBox.Show("DialogResult = null");
                return;
            }
            
            if (addWindow.DialogResult.Value)
            {
                _clientTable.AddRow(dataRow);
            }
        }
        
        private void ClientMenuItemDeleteClick(object? sender, EventArgs e)
        {
            var dataRowView = (DataRowView)clientGridView.SelectedItem;
            
            try
            {
                _clientTable.Delete(dataRowView);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }
        
        private void ClientGridViewOnCellEditEnding(object? sender, DataGridCellEditEndingEventArgs e)
        {
            var dataRowView = (DataRowView)clientGridView.SelectedItem;
            _clientTable.BeginEdit(dataRowView);
        }
        
        private void ClientGridViewOnCurrentCellChanged(object? sender, EventArgs e)
        {
            _clientTable.EndEdit();
        }
    }
}