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
        //private static readonly string[] Nationalities = { "Русский", "Немец", "Испанец", "Итальянец" };
        private int _id;

        private readonly IList<NationDto> Nationalities = ProcessFactory.GetNationProcess().GetList();

        public AddArtistWindow()
        {
            InitializeComponent();
            cbNationality.ItemsSource = (from N in Nationalities orderby N.Nationality select N);
        }

        public void Load(ArtistDto artist)
        {
            if (artist == null)
                return;

            _id = artist.Id;

            tbName.Text = artist.Name;
            tbBirth.Text = artist.BirthYear.ToString();
            if(artist.DeceaseYear.HasValue)
                tbDeath.Text = artist.DeceaseYear.Value.ToString();
            
            if(artist.Nation != null)
            {
                foreach(NationDto nation in Nationalities)
                {
                    if(artist.Nation.Id == nation.Id)
                    {
                        this.cbNationality.SelectedItem = nation;
                        break;
                    }
                }
            }
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
            int? birth;
            int? death = null;

            if(string.IsNullOrEmpty(tbName.Text))
            {
                MessageBox.Show("Имя художника не должно быть пустым", "Проверка");
                return;
            }

            if(tbBirth.Text != "")
            {
                try
                {
                    birth = int.Parse(this.tbBirth.Text);
                }
                catch
                {
                    MessageBox.Show("Год рождения должен быть целым числом", "Проверка");
                    return;
                }
                if (birth < 1000 || birth > DateTime.Today.Year)
                {
                    MessageBox.Show("Галерея занимается продажей только произведений художников прошлого тысячелетия", "Проверка");
                    return;
                }
            }
            else
            {
                birth = null;
            }

            if(!string.IsNullOrEmpty(tbDeath.Text))
            {
                int intDeath;

                if(!int.TryParse(tbDeath.Text, out intDeath))
                {
                    MessageBox.Show("Год смерти должен быть целым числом", "Проверка");
                    return;
                }

                if(intDeath < 1000 || intDeath > DateTime.Today.Year)
                {
                    MessageBox.Show("Год смерти введён неверно", "Проверка");
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
            artist.Nation = cbNationality.SelectedItem as NationDto;

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
