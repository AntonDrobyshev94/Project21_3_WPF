using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Project21WPF
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
                string password = ConvertPasswordToString(PasswordBox.SecurePassword);
                string passwordConfirmed = ConvertPasswordToString(PasswordBoxConfirmed.SecurePassword);
                if (!string.IsNullOrEmpty(TxtLogin.Text) &&
                !string.IsNullOrEmpty(password) &&
                !string.IsNullOrEmpty(passwordConfirmed) &&
                password == passwordConfirmed)
                {
                    
                    UserRegistration userRegistration = new UserRegistration()
                    {
                        LoginProp = TxtLogin.Text,
                        Password = password,
                        ConfirmPassword = passwordConfirmed
                    };
                    string token = string.Empty;
                    token = contactDataApi.IsRegistration(userRegistration);

                    if (token != string.Empty)
                    {
                        var tokenHandler = new JwtSecurityTokenHandler();
                        var jwtToken = tokenHandler.ReadJwtToken(token);

                        GlobalVariables.token = token;
                        GlobalVariables.userRole = "User";
                        foreach (var item in jwtToken.Claims)
                        {
                            if (item.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name")
                            {
                                GlobalVariables.userName = item.Value;
                            }
                        }
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

        /// <summary>
        /// Метод расшифровки засекреченного звёздочками пароля из
        /// PasswordBox
        /// </summary>
        /// <param name="securePassword"></param>
        /// <returns></returns>
        private string ConvertPasswordToString(SecureString securePassword)
        {
            if (securePassword == null)
                return string.Empty;

            IntPtr unmanagedString = IntPtr.Zero;
            try
            {
                unmanagedString = Marshal.SecureStringToGlobalAllocUnicode(securePassword);
                return Marshal.PtrToStringUni(unmanagedString);
            }
            finally
            {
                Marshal.ZeroFreeGlobalAllocUnicode(unmanagedString);
            }
        }
    }
}
