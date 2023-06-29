using System.Data;
using System.Windows;

namespace OnlineShopDesktop;

public partial class AddClientWindow : Window
{
    private readonly DataRow _dataRow;

    public AddClientWindow()
    {
        InitializeComponent();
    }
    
    public AddClientWindow(DataRow dataRowView) : this()
    {
        CancelButton.Click -= AtCancelClicked;
        CancelButton.Click += AtCancelClicked;
        AddButton.Click -= AtAddClicked;
        AddButton.Click += AtAddClicked;
        _dataRow = dataRowView;
    }

    private void AtAddClicked(object sender, RoutedEventArgs e)
    {
        //TODO: вынести в константы
        string lastName = "last_name";
        string name = "name";
        string patronymic = "patronymic";
        string phoneNumber = "phone_number";
        string email = "email";
        
        _dataRow[lastName] = LastName.Text;
        _dataRow[name] = FirstName.Text;
        _dataRow[patronymic] = Patronymic.Text;
        _dataRow[phoneNumber] = Phone.Text;
        _dataRow[email] = Email.Text;
        
        DialogResult = true;
    }

    private void AtCancelClicked(object sender, RoutedEventArgs e)
    {
        DialogResult = false;
    }
}