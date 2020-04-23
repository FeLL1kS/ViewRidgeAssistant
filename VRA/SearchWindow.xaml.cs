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
        public IList<ArtistDto> FindedArtists;
        public bool exec;

        public SearchWindow(string status)
        {
            InitializeComponent();
            AllowArtists = ProcessFactory.GetArtistProcess().GetList();
            AllowNations = ProcessFactory.GetNationProcess().GetList();

            this.cbArtistCountry.ItemsSource = AllowNations;

            switch( status )
            {
                case "Artist":
                    this.SearchTab.SelectedIndex = 1;
                    //this.sCustomer.Visibility = false;
                    //this.sWork.Visibility = false;
                    //this.tiInterests.Visibility = false;
                    //this.sTransaction.Visibility = false;
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
    }
}
