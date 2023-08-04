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
                string password = ConvertPasswordToString(PasswordBox.SecurePassword);
                if (!string.IsNullOrEmpty(TxtLogin.Text) &&
                !string.IsNullOrEmpty(password))
                {
                    UserLoginProp userLogin = new UserLoginProp()
                    {
                        UserName = TxtLogin.Text,
                        Password = password
                    };
                    string token = string.Empty;
                    token = _contactDataApi.IsLogin(userLogin);
                    if (token != string.Empty)
                    {
                        GlobalVariables.token = token;
                        var tokenHandler = new JwtSecurityTokenHandler();
                        var jwtToken = tokenHandler.ReadJwtToken(token);
                        foreach (var item in jwtToken.Claims)
                        {
                            if (item.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role")
                            {
                                if (item.Value == "Admin")
                                {
                                    GlobalVariables.userRole = item.Value;
                                    break;
                                }
                                else
                                {
                                    GlobalVariables.userRole = item.Value;
                                }
                            }
                        }
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
