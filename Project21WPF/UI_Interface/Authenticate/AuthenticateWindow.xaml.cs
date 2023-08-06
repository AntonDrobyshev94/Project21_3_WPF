using Project21WPF.UI_Interface.Registration;
using Project21WPF_ClassLibrary;
using Project21WPF_ClassLibrary.AuthenticationModel;
using System.Windows;
using System.Windows.Controls;
using Project21WPF_ClassLibrary.Extensions;

namespace Project21WPF.UI_Interface.Authenticate
{
    /// <summary>
    /// Логика взаимодействия для AuthenticateWindow.xaml
    /// </summary>
    public partial class AuthenticateWindow : Window
    {
        private readonly ContactDataApi _contactDataApi;

        public AuthenticateWindow()
        {
            InitializeComponent();
        }
        public AuthenticateWindow(ContactDataApi contactDataApi) : this()
        {
            _contactDataApi = contactDataApi;
            EnterButton.Click += delegate
            {
                ProjectLogic projectLogic = new ProjectLogic();
                string password = projectLogic.ConvertPasswordToString(PasswordBox.SecurePassword);
                if (Ext.CheckNullOrEmpty(TxtLogin.Text, password))
                {
                    UserLoginProp userLogin = new UserLoginProp()
                    {
                        UserName = TxtLogin.Text,
                        Password = password
                    };
                    string token = string.Empty;
                    token = _contactDataApi.IsLogin(userLogin);
                    if (projectLogic.TokenDecodingAuthMethod(token))
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
                    MessageBox.Show("Пустое значение имени пользователя или пароля");
                }
            };

            RegistrationButton.Click += delegate
            {
                RegistrationWindow registrationWindow = new RegistrationWindow(_contactDataApi);
                registrationWindow.Show();
                this.Close();
            };
        }

    }
}
