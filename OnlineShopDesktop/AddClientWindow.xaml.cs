using System.Data;
using System.Windows;

namespace OnlineShopDesktop;

public partial class AddClientWindow : Window
{
    public AddClientWindow(DataRowView dataRowView)
    {
        InitializeComponent();
    }
}