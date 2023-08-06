using Project21WPF_ClassLibrary;
using Project21WPF_ClassLibrary.AuthenticationModel;
using Project21WPF_ClassLibrary.Extensions;
using System.Windows;
using System.Windows.Controls;

namespace Project21WPF.UI_Interface.Registration
{
    /// <summary>
    /// Логика взаимодействия для RegistrationWindow.xaml
    /// </summary>
    public partial class RegistrationWindow : Window
    {
        private readonly ContactDataApi _contactDataApi;
        public RegistrationWindow()
        {
            InitializeComponent();
        }
        public RegistrationWindow(ContactDataApi contactDataApi) : this()
        {
            _contactDataApi = contactDataApi;
            RegistrationButton.Click += delegate
            {
                ProjectLogic projectLogic = new ProjectLogic();
                string password = projectLogic.ConvertPasswordToString(PasswordBox.SecurePassword);
                string passwordConfirmed = projectLogic.ConvertPasswordToString(PasswordBoxConfirmed.SecurePassword);
                if(Ext.CheckNullOrEmpty(TxtLogin.Text, password, passwordConfirmed) && password == passwordConfirmed)
                {
                        UserRegistration userRegistration = new UserRegistration()
                        {
                            LoginProp = TxtLogin.Text,
                            Password = password,
                            ConfirmPassword = passwordConfirmed
                        };
                        string token = string.Empty;
                        token = contactDataApi.IsRegistration(userRegistration);

                        if (projectLogic.TokenDecodingRegistrMethod(token))
                        {
                            MainWindow mainWindow = new MainWindow();
                            mainWindow.Show();
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Не верное имя пользователя или пароль");
                        }
                }
                else
                {
                    MessageBox.Show("Пустое значение имени пользователя или пароля" +
                        "или пароли не совпадают");
                }
            };
        }
    }
}
