using Project21WPF_ClassLibrary.ContactModel;
using Project21WPF_ClassLibrary.Extensions;
using System;
using System.Collections.ObjectModel;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.InteropServices;
using System.Security;

namespace Project21WPF_ClassLibrary
{
    public class ProjectLogic
    {
        ContactDataApi context = new ContactDataApi();
        
        /// <summary>
        /// Метод создания нового "пустого" контакта
        /// </summary>
        /// <returns></returns>
        public Contact CreateNewContactMethod()
        {
            Contact newContact = new Contact()
            {
                Name = "",
                Surname = "",
                FatherName = "",
                TelephoneNumber = "",
                ResidenceAdress = "",
                Description = ""
            };
            return newContact;
        }

        /// <summary>
        /// Метод проверки токена, возвращающий bool переменную
        /// </summary>
        /// <returns></returns>
        public bool CheckTokenMethod()
        {
            if (context.CheckToken())
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Метод поиска контакта по ID, принимающий ID контакта и
        /// возвращающий конкретный экземпляр контакта.
        /// </summary>
        /// <param name="contactId"></param>
        /// <returns></returns>
        public Contact FindContactByIdMethod(int contactId)
        {
            return context.FindContactById(contactId);
        }

        /// <summary>
        /// Метод изменения контакта, принимающий строковые значения параметров
        /// контакта (имени, фамилии и т.д.), а также id конкретного контакта.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="surname"></param>
        /// <param name="fatherName"></param>
        /// <param name="telephoneNumber"></param>
        /// <param name="residenceAdress"></param>
        /// <param name="description"></param>
        /// <param name="id"></param>
        public void ChangeContactMethod(string name, string surname, string fatherName, 
            string telephoneNumber, string residenceAdress, string description, int id)
        {
            context.ChangeContact(name, surname, fatherName, telephoneNumber,
                residenceAdress, description, id);
        }

        /// <summary>
        /// Метод добавления контакта, принимающий экземпляр контакта.
        /// </summary>
        /// <param name="contact"></param>
        public void AddContacts(Contact contact)
        {
            context.AddContacts(contact);
        }

        /// <summary>
        /// Метод удаления контакта, принимающий int значение id контакта
        /// </summary>
        /// <param name="contactId"></param>
        public void DeleteContactMethod(int contactId)
        {
            context.DeleteContact(contactId);
        }

        /// <summary>
        /// Метод, предоставляющий перечень всех контактов (возвращает
        /// ObservableCollection контактов).
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<Contact> GetContactsMethod()
        {
            return context.GetContacts().ToObservableCollection();
        }

        /// <summary>
        /// Метод выхода из пользовательского аккаунта, при котором происходит
        /// присвоение пустых значений для глобальных переменных токена,
        /// имени и роли пользователя, а также создаётся новый экземпляр
        /// ContactDataApi, который возвращается по окончанию ключевым словом
        /// return
        /// </summary>
        /// <returns></returns>
        public ContactDataApi ExitMethod()
        {
            GlobalVariables.token = string.Empty;
            GlobalVariables.userRole = string.Empty;
            GlobalVariables.userName = string.Empty;
            ContactDataApi contactDataApi = new ContactDataApi();
            return contactDataApi;
        }

        /// <summary>
        /// Метод предоставления информации о правах доступа пользователя,
        /// который, в случае если роль пользователя Admin возвращает
        /// значение логической переменной true, в обратном случае - false.
        /// </summary>
        /// <returns></returns>
        public bool UserRoleAccessInformation()
        {
            if (GlobalVariables.userRole == "Admin")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Метод предоставления информации об имени пользователя,
        /// который возвращает строку с приветствием и именем пользователя.
        /// </summary>
        /// <returns></returns>
        public string UserNameInformation()
        {
            return $"Добро пожаловать, {GlobalVariables.userName}";
        }

        /// <summary>
        /// Метод расшифровки засекреченного звёздочками пароля из
        /// PasswordBox
        /// </summary>
        /// <param name="securePassword"></param>
        /// <returns></returns>
        public string ConvertPasswordToString(SecureString securePassword)
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

        /// <summary>
        /// Метод расшифровки токена, принимающий строковое значение токена и возвращающий
        /// логический ответ true или false. В методе происходит проверка на содержимое
        /// переменной и если она не равна пустой строке, то происходит расшифровка токена
        /// и запись значений имени, роли и токена в глобальные переменные.
        /// </summary>
        /// <param name="concreteToken"></param>
        /// <returns></returns>
        public bool TokenDecodingAuthMethod(string concreteToken)
        {
            if (concreteToken == string.Empty)
            {
                return false;
            }
            else
            {
                GlobalVariables.token = concreteToken;
                var tokenHandler = new JwtSecurityTokenHandler();
                var jwtToken = tokenHandler.ReadJwtToken(concreteToken);
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
                return true;
            }
        }

        /// <summary>
        /// Метод расшифровки токена, принимающий строковое значение токена и возвращающий
        /// логический ответ true или false. В методе происходит проверка на содержимое
        /// переменной и если она не равна пустой строке, то происходит расшифровка токена
        /// и запись значений имени, роли и токена в глобальные переменные.
        /// </summary>
        /// <param name="concreteToken"></param>
        /// <returns></returns>
        public bool TokenDecodingRegistrMethod(string concreteToken)
        {
            if (concreteToken == string.Empty)
            {
                return false;
            }
            else
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var jwtToken = tokenHandler.ReadJwtToken(concreteToken);

                GlobalVariables.token = concreteToken;
                GlobalVariables.userRole = "User";
                foreach (var item in jwtToken.Claims)
                {
                    if (item.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name")
                    {
                        GlobalVariables.userName = item.Value;
                    }
                }
                return true;
            }
        }
    }
}
