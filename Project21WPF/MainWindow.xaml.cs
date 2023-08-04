using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Project21WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static Contact contactHighlight;
        public MainWindow()
        {
            InitializeComponent();
            AccessCheckMethod();
            UserInformationMethod();

            ContactDataApi context = new ContactDataApi();
            try
            {
                DataGridView.ItemsSource = context.GetContacts().ToObservableCollection();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            RefreshButton.Click += delegate { DataGridView.ItemsSource = context.GetContacts().ToObservableCollection(); };
            AddButton.Click += delegate
            {
                if (context.CheckToken())
                {
                    Contact contact = new Contact()
                    {
                        Name = "",
                        Surname = "",
                        FatherName = "",
                        TelephoneNumber = "",
                        ResidenceAdress = "",
                        Description = ""
                    };
                    AddContactWindow addContactWindow = new AddContactWindow(contact);
                    addContactWindow.ShowDialog();

                    if (addContactWindow != null && addContactWindow.DialogResult.Value)
                    {
                        context.AddContacts(contact);
                        DataGridView.ItemsSource = context.GetContacts().ToObservableCollection();
                    }
                }
                else
                {
                    MessageBox.Show("Токен не валидный");
                    ExitMethod();
                }
                
            };

            DeleteButton.Click += delegate
            {
                if(context.CheckToken())
                {
                    if ((Contact)DataGridView.SelectedItem != null)
                    {
                        contactHighlight = (Contact)DataGridView.SelectedItem;
                        context.DeleteContact(contactHighlight.ID);
                        DataGridView.ItemsSource = context.GetContacts().ToObservableCollection();
                        DataGridView.Items.Refresh();
                    }
                    else
                    {
                        MessageBox.Show("Контакт не выбран");
                    }
                }
                else
                {
                    MessageBox.Show("Токен не валидный");
                    ExitMethod();
                }
            };

            DetailsButton.Click += delegate
            {
                if (context.CheckToken())
                {
                    if ((Contact)DataGridView.SelectedItem != null)
                    {
                        contactHighlight = (Contact)DataGridView.SelectedItem;
                        Contact contact = new Contact()
                        {
                            Name = "",
                            Surname = "",
                            FatherName = "",
                            TelephoneNumber = "",
                            ResidenceAdress = "",
                            Description = ""
                        };
                        contact = context.FindContactById(contactHighlight.ID);
                        DetailsWindow detailsWindow = new DetailsWindow(contact);
                        detailsWindow.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("Контакт не выбран");
                    }
                }
                else
                {
                    MessageBox.Show("Токен не валидный");
                    ExitMethod();
                }
                
            };

            ContactChangeButton.Click += delegate
            {
                if (context.CheckToken())
                {
                    if ((Contact)DataGridView.SelectedItem != null)
                    {
                        contactHighlight = (Contact)DataGridView.SelectedItem;
                        Contact contact = new Contact()
                        {
                            Name = contactHighlight.Name,
                            Surname = contactHighlight.Surname,
                            FatherName = contactHighlight.FatherName,
                            TelephoneNumber = contactHighlight.TelephoneNumber,
                            ResidenceAdress = contactHighlight.ResidenceAdress,
                            Description = contactHighlight.Description
                        };
                        try
                        {
                            ChangeContactWindow changeContactWindow = new ChangeContactWindow(contact);
                            changeContactWindow.ShowDialog();
                            if (changeContactWindow.DialogResult.Value)
                            {
                                context.ChangeContact(contact.Name, contact.Surname,
                                    contact.FatherName, contact.TelephoneNumber,
                                    contact.ResidenceAdress, contact.Description,
                                    contactHighlight.ID);
                                DataGridView.ItemsSource = context.GetContacts().ToObservableCollection();
                                DataGridView.Items.Refresh();
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Контакт не выбран");
                    }
                }
                else
                {
                    MessageBox.Show("Токен не валидный");
                    ExitMethod();
                }
                
            };
            ExitButton.Click += delegate
            {
                ExitMethod();
            };
        }

        private void ExitMethod()
        {
            GlobalVariables.token = string.Empty;
            GlobalVariables.userRole = string.Empty;
            GlobalVariables.userName = string.Empty;
            ContactDataApi contactDataApi = new ContactDataApi();
            AuthenticateWindow authenticateWindow = new AuthenticateWindow(contactDataApi);
            authenticateWindow.Show();
            this.Close();
        }

        private void AccessCheckMethod()
        {
            if (GlobalVariables.userRole == "Admin")
            {
                DeleteButton.Visibility = Visibility.Visible;
                ContactChangeButton.Visibility = Visibility.Visible;
            }
            else
            {
                DeleteButton.Visibility = Visibility.Hidden;
                ContactChangeButton.Visibility = Visibility.Hidden;
            }
        }

        private void UserInformationMethod()
        {
            UserName.Text = $"Добро пожаловать, {GlobalVariables.userName}";
            if (GlobalVariables.userRole == "Admin")
            {
                UserRole.Text = $"У Вас права администратора";
            }
            else
            {
                UserRole.Text = $"Права доступа - пользователь";
            }
        }
    }
}