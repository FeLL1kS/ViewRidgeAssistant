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
    /// Логика взаимодействия для AddNationWindow.xaml
    /// </summary>
    public partial class AddNationWindow : Window
    {
        private int _id;

        public AddNationWindow()
        {
            InitializeComponent();
        }

        public void Load(NationDto nation)
        {
            if (nation == null)
                return;

            _id = nation.Id;

            tbNation.Text = nation.Nationality.ToString();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrEmpty(tbNation.Text))
            {
                MessageBox.Show("Национальность не должна быть пустой", "Проверка");
                return;
            }

            NationDto nation = new NationDto();
            nation.Nationality = tbNation.Text;

            INationProcess nationProcess = ProcessFactory.GetNationProcess();

            if(_id == 0)
            {
                nationProcess.Add(nation);
            }
            else
            {
                nation.Id = _id;
                nationProcess.Add(nation);
            }
            Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
