using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// Логика взаимодействия для AddTransactionWindow.xaml
    /// </summary>
    public partial class AddTransactionWindow : Window
    {
        private readonly IList<WorkDto> Works;
        private readonly IList<WorkDto> FreeForSale = ProcessFactory.GetWorkProcess().GetListInGallery();
        private IList<WorkDto> FreeForPurchase = new List<WorkDto>();
        private int id;
        public string status;

        public AddTransactionWindow()
        {
            CultureInfo ci = CultureInfo.CreateSpecificCulture(CultureInfo.CurrentCulture.Name);
            ci.DateTimeFormat.ShortDatePattern = "dd - MM - yyyy";
            Thread.CurrentThread.CurrentCulture = ci;
            InitializeComponent();
            IList<CustomerDto> customers = ProcessFactory.GetCustomerProcess().GetList();
            Works = ProcessFactory.GetWorkProcess().GetList();
            this.cbCustomer.ItemsSource = (from c in customers orderby c.Name select c);
            this.cbWork.ItemsSource = (from w in Works orderby w.Title select w);
            this.dpAcuired.IsTodayHighlighted = true;
        }

        public void Load(TransactionDto Trans)
        {
            this.Title = "Транзакция: Редактирование";
            this.id = Trans.Id;
            if (Trans.Customer != null)
            {
                this.cbCustomer.Text = Trans.Customer.Name;
            }
            if (Trans.Work.Copy != null)
            {
                this.cbCopy.Text = Trans.Work.Copy;
                this.cbWork.Text = Trans.Work.Title;
            }
            this.tbAcquisitionPrice.Text = Trans.AcquisitionPrice.ToString();
            this.tbAskingPrice.Text = Trans.AskingPrice.ToString();
            this.tbSalesPrice.Text = Trans.SalesPrice.ToString();
            this.dpAcuired.Text = Trans.DateAcquired.ToString();
            this.dpPurchase.Text = Trans.PurchaseDate.ToString();

            // Загружаем на форму данные работы участвующей в транзакции.
            this.loadWork(Trans.Work.Title);
        }
        private void loadWork(string Title)
        {
            this.cbCopy.Items.Clear();
            foreach (WorkDto work in Works)
            {
                if (work.Title == Title)
                {
                    this.cbCopy.Items.Add(work);
                }
            }
            this.cbCopy.SelectedIndex = 0;
        }

        private void cbWork_LostFocus(object sender, RoutedEventArgs e)
        {
            this.loadWork(this.cbWork.Text);
        }
        private void cbWork_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.status == "sale")
            {
                WorkDto work = cbWork.SelectedItem as WorkDto;
                if (work != null) this.id = FindTransaction(work.Id);
                loadTransaction(this.id);
            }
        }

        private int FindTransaction(int workId)
        {
            IList<TransactionDto> transes = ProcessFactory.GetTransactionProcess().GetList();
            foreach (TransactionDto t in transes)
            {
                if (t.Work.Id == workId & t.Customer == null)
                {
                    return t.Id;
                }
            }
            return -1;
        }

        private void loadTransaction(int transId)
        {
            TransactionDto trans = ProcessFactory.GetTransactionProcess().Get(transId);
            this.dpAcuired.Text = trans.DateAcquired.ToString();
            this.dpAcuired.IsEnabled = false;
            this.tbAcquisitionPrice.Text = trans.AcquisitionPrice.ToString();
            this.tbAcquisitionPrice.IsEnabled = false;
            this.tbAskingPrice.Text = trans.AskingPrice.ToString();
            this.tbAskingPrice.IsEnabled = false;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (status == "sale")
            {
                if (this.cbCustomer.SelectedIndex < 0)
                {
                    MessageBox.Show("Укажите клиента, которому продается картина!"); return;
                }
            }
            TransactionDto transaction = new TransactionDto();
            WorkDto SelectedWork = selectWork();
            if (SelectedWork == null)
            {
                MessageBox.Show("Картина должна быть выбрана!"); return;
            }
            if (status == "sale")
            {
                if (!workAtGalery(SelectedWork))
                {
                    MessageBox.Show("Запрашиваемая работа уже продана!"); return;
                }
            }
            if (status == "purchase")
            {
                if (workAtGalery(SelectedWork))
                {
                    MessageBox.Show("Запрашиваемая работа уже находится в галерее!"); return;
                }
            }
            transaction.Work = SelectedWork;
            if (!string.IsNullOrEmpty(tbAcquisitionPrice.Text))
            {
                try
                {
                    transaction.AcquisitionPrice = Convert.ToDecimal(tbAcquisitionPrice.Text);
                }
                catch (Exception)
                {
                    MessageBox.Show("Введите корректную цену приобретения"); return;
                }
            }
            if (!string.IsNullOrEmpty(tbAskingPrice.Text))
            {
                try
                {
                    transaction.AskingPrice = Convert.ToDecimal(tbAskingPrice.Text);
                }
                catch (Exception)
                {
                    MessageBox.Show("Введите корректную запрашиваемую цену"); return;
                }
            }
            if (!string.IsNullOrEmpty(this.dpAcuired.Text))
            {
                transaction.DateAcquired = Convert.ToDateTime(this.dpAcuired.Text);
            }
            else
            {
                MessageBox.Show("Дата приобретения должна быть указана!"); return;
            }
            if (!string.IsNullOrEmpty(this.dpPurchase.Text))
            {
                if (Convert.ToDateTime(dpPurchase.Text) > Convert.ToDateTime(dpAcuired.Text))
                    transaction.PurchaseDate = Convert.ToDateTime(this.dpPurchase.Text);
                else
                {
                    MessageBox.Show("Нельзя продать работу раньше, чем её купила галерея! Проверьте правильность ввода данных.");
                    return;
                }

                if(dpPurchase.Text == "")
                {
                    transaction.PurchaseDate = null;
                }
            }
            if (!string.IsNullOrEmpty(cbCustomer.Text))
            {
                CustomerDto SelectedCustomer = (CustomerDto)this.cbCustomer.SelectedItem;
                transaction.Customer = SelectedCustomer;
            }
            if (!string.IsNullOrEmpty(tbSalesPrice.Text))
            {
                try
                {
                    if (Convert.ToDecimal(tbSalesPrice.Text) >= 30000 && Convert.ToDecimal(tbSalesPrice.Text) <= 1500000)
                        transaction.SalesPrice = Convert.ToDecimal(tbSalesPrice.Text);
                    else
                    {
                        MessageBox.Show("Продажа может проходить только в пределах от 30 тыс.у.е.до 1, 5 млн.у.е.");
                        return;
                    }
                    if(tbSalesPrice.Text == "")
                    {
                        transaction.SalesPrice = null;
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Неверный формат данных при операции с ценой продажи"); return;
                }
            }
            ITransactionProcess transProcess = ProcessFactory.GetTransactionProcess();
            if (id == 0)
                transProcess.Add(transaction);
            else
            {
                transaction.Id = id;
                transProcess.Update(transaction);
            }
            this.Close();
        }
        private bool workAtGalery(WorkDto work)
        {
            return FreeForSale.Contains(work);
        }

        private WorkDto selectWork()
        {
            WorkDto work = null;
            foreach (WorkDto w in Works)
            {
                if (w.Copy == this.cbCopy.Text && w.Title == this.cbWork.Text)
                { work = w; break; }
            }
            return work;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (status == "purchase")
        {
                GetWorksWithCustomers();
                this.cbWork.ItemsSource = this.FreeForPurchase;
                this.cbCustomer.IsEnabled = false;
                this.tbSalesPrice.IsEnabled = false;
                this.dpPurchase.IsEnabled = false;
            }
            if (status == "sale")
            {
                this.cbWork.ItemsSource = FreeForSale;
            }
        }

        private void GetWorksWithCustomers()
        {
            IEnumerable<WorkDto> forPurchase = Works.Except(FreeForSale);
            FreeForPurchase = forPurchase.ToList();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
