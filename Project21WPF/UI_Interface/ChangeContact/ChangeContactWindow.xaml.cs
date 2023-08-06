using Project21WPF_ClassLibrary.ContactModel;
using System.Windows;

namespace Project21WPF.UI_Interface.ChangeContact
{
    /// <summary>
    /// Логика взаимодействия для ChangeContactWindow.xaml
    /// </summary>
    public partial class ChangeContactWindow : Window
    {
        private ChangeContactWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Публичный конструктор, принимающий экземпляр контакта
        /// contact. Поля данного экземпляра записываются в 
        /// элементы TextBox для отображения текущих значений 
        /// параметров контакта. При нажатии на кнопку CancelButton
        /// происходит вызов события отмены изменений и возврат в
        /// меню. При нажатии ChangeButton происходит вызов события
        /// "изменение контакта", в результате которого значения,
        /// записанные в поля TextBox записываются в значения
        /// параметров контакта и DialogResult принимает значение true.
        /// </summary>
        /// <param name="contact"></param>
        public ChangeContactWindow(Contact contact) : this()
        {
            TxtName.Text = contact.Name;
            TxtSurname.Text = contact.Surname;
            TxtFatherName.Text = contact.FatherName;
            TxtTelephoneNumber.Text = contact.TelephoneNumber;
            TxtAdress.Text = contact.ResidenceAdress;
            TxtDescription.Text = contact.Description;
            CancelButton.Click += delegate { this.DialogResult = false; };
            ChangeButton.Click += delegate
            {
                contact.Name = TxtName.Text;
                contact.Surname = TxtSurname.Text;
                contact.FatherName = TxtFatherName.Text;
                contact.TelephoneNumber = TxtTelephoneNumber.Text;
                contact.ResidenceAdress = TxtAdress.Text;
                contact.Description = TxtDescription.Text;
                this.DialogResult = true;
            };
        }
    }
}
