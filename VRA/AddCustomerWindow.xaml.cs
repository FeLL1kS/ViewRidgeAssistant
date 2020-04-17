using System.Windows;
using VRA.BusinessLayer;
using VRA.Dto;

namespace VRA
{
    /// <summary>
    /// Логика взаимодействия для AddCustomerWindow.xaml
    /// </summary>
    public partial class AddCustomerWindow : Window
    {
        private int _id;
        public AddCustomerWindow()
        {
            InitializeComponent();
        }

        public void Load(CustomerDto customer)
        {
            _id = customer.Id;
            tbName.Text = customer.Name;
            tbEmail.Text = customer.EMail;
            tbAreaCode.Text = customer.AreaCode;
            tbCity.Text = customer.City;
            tbCountry.Text = customer.Country;
            tbZipPostalCode.Text = customer.ZipPostalCode;
            tbRegion.Text = customer.Region;
            tbHouseNumber.Text = customer.HouseNumber;
            tbPhoneNumber.Text = customer.PhoneNumber;
            tbStreet.Text = customer.Street;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            CustomerDto customer = new CustomerDto()
            {
                Name = tbName.Text,
                AreaCode = tbAreaCode.Text,
                City = tbCity.Text,
                Country = tbCountry.Text,
                ZipPostalCode = tbZipPostalCode.Text,
                EMail = tbEmail.Text,
                Region = tbRegion.Text,
                HouseNumber = tbHouseNumber.Text,
                PhoneNumber = tbPhoneNumber.Text,
                Street = tbStreet.Text
            };

            if(_id == 0)
            {
                ProcessFactory.GetCustomerProcess().Add(customer);
            }
            else
            {
                customer.Id = _id;
                ProcessFactory.GetCustomerProcess().Update(customer);
            }
            Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
