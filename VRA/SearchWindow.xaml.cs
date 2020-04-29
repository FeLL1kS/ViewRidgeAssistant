using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using VRA.BusinessLayer;
using VRA.Dto;

namespace VRA
{
    /// <summary>
    /// Логика взаимодействия для SearchWindow.xaml
    /// </summary>
    public partial class SearchWindow : Window
    {
        private readonly IList<ArtistDto> AllowArtists;
        private readonly IList<NationDto> AllowNations;
        private readonly IList<CustomerDto> AllowCustomers;
        public IList<ArtistDto> FindedArtists;
        public IList<TransactionDto> FindedTransactions;
        public bool exec;

        public SearchWindow(string status)
        {
            InitializeComponent();
            AllowArtists = ProcessFactory.GetArtistProcess().GetList();
            AllowNations = ProcessFactory.GetNationProcess().GetList();
            AllowCustomers = ProcessFactory.GetCustomerProcess().GetList();

            this.cbArtistCountry.ItemsSource = AllowNations;
            this.cbCustomers.ItemsSource = AllowCustomers;

            switch( status )
            {
                case "Artist":
                    this.SearchTab.SelectedIndex = 0;
                    //this.sCustomer.Visibility = false;
                    //this.sWork.Visibility = false;
                    //this.tiInterests.Visibility = false;
                    this.sTransaction.Visibility = Visibility.Collapsed;
                    break;
                case "Trans":
                    this.SearchTab.SelectedIndex = 1;
                    this.sArtist.Visibility = Visibility.Collapsed;
                    break;
            }
        }

        private void CloseForm(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void SearchArtist(object sender, RoutedEventArgs e)
        {
            this.FindedArtists = ProcessFactory.GetArtistProcess().SearchArtists(this.ArtistName.Text, this.cbArtistCountry.Text);
            this.exec = true;
            this.Close();
        }

        private void SearchTransaction(object sender, RoutedEventArgs e)
        {
            if(dpDateAcquiredFrom.Text == "" && dpDateAcquiredTo.Text != "")
            {
                MessageBox.Show("Необходимо так же заполнить поле \"Дата приобретения от\"!", "Ошибка");
                return;
            }

            string CustomerID = "";
            foreach(CustomerDto customer in AllowCustomers)
            {
                if(customer.Name == cbCustomers.Text)
                {
                    CustomerID = customer.Id.ToString();
                    break;
                }
            }

            DateTime? DateAcquiredFrom = null;
            DateTime? DateAcquiredTo = null;

            if (dpDateAcquiredFrom.Text != "")
            {
                DateAcquiredFrom = DateTime.Parse(dpDateAcquiredFrom.Text);
            }
            if (dpDateAcquiredTo.Text != "")
            {
                DateAcquiredTo = DateTime.Parse(dpDateAcquiredTo.Text);
            }

            this.FindedTransactions = ProcessFactory.GetTransactionProcess().SearchTransaction(CustomerID, this.tbSalesPrice.Text, DateAcquiredFrom, DateAcquiredTo);
            this.exec = true;
            this.Close();
        }
    }
}
