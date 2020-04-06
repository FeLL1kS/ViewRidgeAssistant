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
using VRA.Dto;
using VRA.BusinessLayer;

namespace VRA
{
    /// <summary>
    /// Логика взаимодействия для AddArtistWindow.xaml
    /// </summary>
    public partial class AddArtistWindow : Window
    {
        private static readonly string[] Nationalities = { "Русский", "Немец", "Испанец", "Итальянец" };
        private int _id;

        public AddArtistWindow()
        {
            InitializeComponent();
            cbNationality.ItemsSource = Nationalities;
            cbNationality.SelectedIndex = 0;
        }

        public void Load(ArtistDto artist)
        {
            if (artist == null || !Nationalities.Contains(artist.Nationality))
                return;

            _id = artist.Id;

            tbName.Text = artist.Name;
            tbBirth.Text = artist.BirthYear.ToString();
            tbDeath.Text = artist?.DeceaseYear.ToString();
            cbNationality.SelectedItem = artist.Nationality;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            int birth;
            int? death = null;

            if(string.IsNullOrEmpty(tbName.Text))
            {
                MessageBox.Show("Имя художника не должно быть пустым", "Проверка");
                return;
            }

            if(!int.TryParse(tbBirth.Text, out birth))
            {
                MessageBox.Show("Год рождения должен быть целым числом", "Проверка");
                return;
            }

            if(!string.IsNullOrEmpty(tbDeath.Text))
            {
                int intDeath;

                if(!int.TryParse(tbDeath.Text, out intDeath))
                {
                    MessageBox.Show("Год смерти должен быть целым числом", "Проверка");
                    return;
                }

                if(intDeath < birth)
                {
                    MessageBox.Show("Год смерти должен быть больше года рождения", "Проверка");
                    return;
                }
                death = intDeath;
            }

            ArtistDto artist = new ArtistDto();

            artist.Name = tbName.Text;
            artist.BirthYear = birth;
            artist.DeceaseYear = death;
            artist.Nationality = cbNationality.SelectedItem.ToString();

            IArtistProcess artistProcess = ProcessFactory.GetArtistProcess();

            if(_id == 0)
            {
                artistProcess.Add(artist);
            }
            else
            {
                artist.Id = _id;
                artistProcess.Update(artist);
            }
            Close();
        }
    }
}
