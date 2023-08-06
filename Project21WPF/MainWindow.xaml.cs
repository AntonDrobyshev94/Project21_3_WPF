using Project21WPF_ClassLibrary;
using Project21WPF_ClassLibrary.ContactModel;
using System;
using System.Windows;
using Project21WPF.UI_Interface.AddContact;
using Project21WPF.UI_Interface.Details;
using Project21WPF.UI_Interface.ChangeContact;
using Project21WPF.UI_Interface.Authenticate;

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

            ProjectLogic projectLogic = new ProjectLogic();
            try
            {
                DataGridView.ItemsSource = projectLogic.GetContactsMethod();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            RefreshButton.Click += delegate { DataGridView.ItemsSource = projectLogic.GetContactsMethod(); };
            AddButton.Click += delegate
            {
                if (projectLogic.CheckTokenMethod())
                {
                    Contact contact = projectLogic.CreateNewContactMethod();
                    AddContactWindow addContactWindow = new AddContactWindow(contact);
                    addContactWindow.ShowDialog();
                    if (addContactWindow != null && addContactWindow.DialogResult.Value)
                    {
                        projectLogic.AddContacts(contact);
                        DataGridView.ItemsSource = projectLogic.GetContactsMethod();
                    }
                }
                else
                {
                    MessageBox.Show("Токен не валидный");
                    AuthenticateWindow authenticateWindow = new AuthenticateWindow(projectLogic.ExitMethod());
                    this.Close();
                }
                
            };

            DeleteButton.Click += delegate
            {
                if (projectLogic.CheckTokenMethod())
                {
                    if ((Contact)DataGridView.SelectedItem != null)
                    {
                        contactHighlight = (Contact)DataGridView.SelectedItem;
                        projectLogic.DeleteContactMethod(contactHighlight.ID);
                        DataGridView.Items.Refresh();
                        DataGridView.ItemsSource = projectLogic.GetContactsMethod();
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
                    AuthenticateWindow authenticateWindow = new AuthenticateWindow(projectLogic.ExitMethod());
                    this.Close();
                }
            };

            DetailsButton.Click += delegate
            {
                if (projectLogic.CheckTokenMethod())
                {
                    if ((Contact)DataGridView.SelectedItem != null)
                    {
                        contactHighlight = (Contact)DataGridView.SelectedItem;
                        Contact contact = projectLogic.CreateNewContactMethod();
                        contact = projectLogic.FindContactByIdMethod(contactHighlight.ID);
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
                    AuthenticateWindow authenticateWindow = new AuthenticateWindow(projectLogic.ExitMethod());
                    this.Close();
                }
            };

            ContactChangeButton.Click += delegate
            {
                if (projectLogic.CheckTokenMethod())
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
                                projectLogic.ChangeContactMethod(contact.Name, contact.Surname,
                                contact.FatherName, contact.TelephoneNumber, contact.ResidenceAdress, 
                                contact.Description, contactHighlight.ID);
                                DataGridView.Items.Refresh();
                                DataGridView.ItemsSource = projectLogic.GetContactsMethod();
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
                    AuthenticateWindow authenticateWindow = new AuthenticateWindow(projectLogic.ExitMethod());
                    this.Close();
                }
            };
            ExitButton.Click += delegate
            {
                AuthenticateWindow authenticateWindow = new AuthenticateWindow(projectLogic.ExitMethod());
                authenticateWindow.Show();
                this.Close();
            };
        }

        /// <summary>
        /// Метод проверки прав доступа, в котором в зависимости от 
        /// возвращаемой логической переменной открывается или
        /// закрывается видимость элементов UI.
        /// </summary>
        private void AccessCheckMethod()
        {
            ProjectLogic projectLogic = new ProjectLogic();
            if (projectLogic.UserRoleAccessInformation())
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

        /// <summary>
        /// Метод предоставления информации о пользователях, в
        /// котором происходит вывод информации на UI.
        /// </summary>
        private void UserInformationMethod()
        {
            ProjectLogic projectLogic = new ProjectLogic();
            UserName.Text = projectLogic.UserNameInformation();
            if(projectLogic.UserRoleAccessInformation())
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