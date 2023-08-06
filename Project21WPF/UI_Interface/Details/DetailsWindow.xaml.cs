using Project21WPF_ClassLibrary.ContactModel;
using System.Windows;
using Project21WPF_ClassLibrary.Extensions;

namespace Project21WPF.UI_Interface.Details
{
    /// <summary>
    /// Логика взаимодействия для DetailsWindow.xaml
    /// </summary>
    public partial class DetailsWindow : Window
    {
        private DetailsWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Публичный конструктор, принимающий экземпляр контакта
        /// contact, в котором происходит биндинг полей контакта
        /// в DataGridView. Контакт записывается в ObservableCollection
        /// с помощью второй перегрузки метода ToObservableCollection.
        /// </summary>
        /// <param name="contact"></param>
        public DetailsWindow(Contact contact) : this()
        {
            DetailsDataGridView.ItemsSource = contact.ToObservableCollection();
            BackToMenuButton.Click += delegate { this.DialogResult = false; };
        }
    }
}
