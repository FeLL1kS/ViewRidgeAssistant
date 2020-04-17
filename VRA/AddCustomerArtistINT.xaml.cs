using System.Collections.Generic;
using System.Linq;
using System.Windows;
using VRA.BusinessLayer;
using VRA.Dto;

namespace VRA
{
    /// <summary>
    /// Логика взаимодействия для AddCustomerArtistINT.xaml
    /// </summary>
    public partial class AddCustomerArtistINT : Window
    {
        private readonly IList<ArtistDto> artistDtos = ProcessFactory.GetArtistProcess().GetList();
        private readonly IList<CustomerDto> customerDtos = ProcessFactory.GetCustomerProcess().GetList();

        public AddCustomerArtistINT()
        {
            InitializeComponent();
            cbArtists.ItemsSource = (from A in artistDtos orderby A.Name select A);
            cbCustomers.ItemsSource = (from C in customerDtos orderby C.Name select C);
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            CustomerArtistINTDto customerArtistINTDto = new CustomerArtistINTDto
            {
                ArtistID = cbArtists.SelectedItem as ArtistDto,
                CustomerID = cbCustomers.SelectedItem as CustomerDto
            };
            ProcessFactory.GetCustomerArtistINTProcess().Add(customerArtistINTDto);
            Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
