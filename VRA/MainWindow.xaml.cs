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
using System.Windows.Navigation;
using System.Windows.Shapes;
using VRA.BusinessLayer;
using VRA.Dto;

namespace VRA
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string status;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void dgArtists_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            //AddArtistWindow window = new AddArtistWindow(); 
            //window.ShowDialog();

            //IList<ArtistDto> items = ProcessFactory.GetArtistProcess().GetList();
            //dgArtists.ItemsSource = items;
            switch(status)
            {
                case "Artist":
                    this.btnAddA_Click();
                    break;
                case "Work":
                    //this.btnAddW_Click();
                    break;
                case "Customer":
                    //this.btnAddC_Click();
                    break;
                case "Nations":
                    //this.btnAddN_Click();
                    break;
                case "Interests":
                    //this.btnAddI_Click();
                    break;
                default:
                    MessageBox.Show("Необходимо выбрать таблицу, в которую добавляется элемент!");
                    return;
            }
        }

        private void btnAddA_Click()
        {
            AddArtistWindow window = new AddArtistWindow();
            window.ShowDialog();

            IList<ArtistDto> items = ProcessFactory.GetArtistProcess().GetList();
            dgArtists.ItemsSource = items;
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            switch (status)
            {
                case "Artist":
                    this.RefreshA_Click();
                    break;
                case "Work":
                    //this.RefreshW_Click();
                    break;
                case "Customer":
                    //this.RefreshC_Click();
                    break;
                case "Nations":
                    //this.RefreshN_Click();
                    break;
                case "Interests":
                    //this.RefreshI_Click();
                    break;
                default:
                    MessageBox.Show("Необходимо выбрать таблицу, в которую добавляется элемент!");
                    return;
            }   
        }

        private void RefreshA_Click()
        {
            dgArtists.ItemsSource = ProcessFactory.GetArtistProcess().GetList();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            switch (status)
            {
                case "Artist":
                    this.btnDeleteA_Click(sender, e);
                    break;
                case "Work":
                    //this.btnDeleteW_Click(sender, e);
                    break;
                case "Customer":
                    //this.btnDeleteC_Click(sender, e);
                    break;
                case "Nations":
                    //this.btnDeleteN_Click(sender, e);
                    break;
                case "Interests":
                    //this.btnDeleteI_Click(sender, e);
                    break;
                default:
                    MessageBox.Show("Необходимо выбрать таблицу, из которой удаляется элемент!");
                    return;
            }
        }

        private void btnDeleteA_Click(object sender, RoutedEventArgs e)
        {
            ArtistDto item = dgArtists.SelectedItem as ArtistDto;

            if (item == null)
            {
                MessageBox.Show("Выберите запись для удаления", "Удаление художника");
                return;
            }

            MessageBoxResult result = MessageBox.Show("Удалить художника " + item.Name + "?", "Удаление художника", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (result != MessageBoxResult.Yes)
            {
                return;
            }

            ProcessFactory.GetArtistProcess().Delete(item.Id);

            Refresh_Click(sender, e);
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            switch (status)
            {
                case "Artist":
                    this.btnEditA_Click(sender, e);
                    break;
                case "Work":
                    //this.btnEditW_Click(sender, e);
                    break;
                case "Customer":
                    //this.btnEditC_Click(sender, e);
                    break;
                case "Nations":
                    //this.btnEditN_Click(sender, e);
                    break;
                case "Interests":
                    //this.btnEditI_Click(sender, e);
                    break;
                default:
                    MessageBox.Show("Необходимо выбрать таблицу, в которой редактируется элемент!");
                    return;
            }
        }

        private void btnEditA_Click(object sender, RoutedEventArgs e)
        {
            ArtistDto item = dgArtists.SelectedItem as ArtistDto;

            if (item == null)
            {
                MessageBox.Show("Выберите запись для редактирования", "Редактирование");
                return;
            }

            AddArtistWindow window = new AddArtistWindow();
            window.Load(item);
            window.ShowDialog();
            Refresh_Click(sender, e);
        }

        private void btnDatabase_Click(object sender, RoutedEventArgs e)
        {
            SettingsWindow window = new SettingsWindow();
            window.ShowDialog();
        }

        private void btnArtists_Click(object sender, RoutedEventArgs e)
        {
            //switch(status)
            //{
            //    case "Customer":
            //        this.dgCustomer.Visibility = Visibility.Hidden;
            //        break;
            //    case "Work": 
            //        this.dgWork.Visibility = Visibility.Hidden;
            //        break;
            //    case "Trans":
            //        this.dgTrans.Visibility = Visibility.Hidden;
            //        break;
            //    case "Interests":
            //        this.dgInterests.Visibility = Visibility.Hidden;
            //        break;
            //    case "Nations":
            //        this.dgNations.Visibility = Visibility.Hidden;
            //        break;
            //}
            this.dgArtists.Visibility = Visibility.Visible;
            status = "Artist";
            this.statusLabel.Content = "Работа с таблицей: Художники";
            this.btnAdd.Visibility = Visibility.Visible;
            //this.btnPurchase.Visibility = Visibility.Collapsed;
            //this.btnSale.Visibility = Visibility.Collapsed;
            this.btnEdit.Visibility = Visibility.Visible;
            this.btnDelete.Visibility = Visibility.Visible;
            this.btnRefresh.Visibility = Visibility.Visible;
            //this.btnSearch.Visibility = Visibility.Visible;
            Refresh_Click(sender, e);
        }
    }
}
