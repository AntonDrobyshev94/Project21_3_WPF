using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Project21WPF
{
    public class ContactDataApi
    {
        private HttpClient httpClient { get; set; }
        public ContactDataApi()
        {
            httpClient = new HttpClient();
        }

        /// <summary>
        /// Запрос для проверки валидности токена. Если нет ошибки
        /// в блоке try, то токен валидный. Если возникает ошибка,
        /// то происходит переход в catch (токен не валидный).
        /// </summary>
        /// <returns></returns>
        public bool CheckToken()
        {
            string response = string.Empty;
            string url = @"https://localhost:7037/api/values/CheckToken";
            try
            {
                AddTokenHeaderMethod();
                response = httpClient.GetStringAsync(url).Result;
                if (response == "true")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Запрос на получение всех контактов, передающийся на API 
        /// сервер. Запрос возвращает результат в виде коллекции
        /// объектов типа Contact.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Contact> GetContacts()
        {
            string url = @"https://localhost:7037/api/values";

            string json = httpClient.GetStringAsync(url).Result;
            return JsonConvert.DeserializeObject<IEnumerable<Contact>>(json);
        }

        /// <summary>
        /// Запрос на создание нового контакта, передающийся на API 
        /// сервер. Данный запрос принимает экземпляр контакта и текущий 
        /// Http-контекст, который позволяет обратиться к куки,
        /// в которых хранится токен. Запрос является невозвратным.
        /// </summary>
        /// <param name="contact"></param>
        public void AddContacts(Contact contact)
        {
            string url = @"https://localhost:7037/api/values";
            AddTokenHeaderMethod();
            var r = httpClient.PostAsync(
                requestUri: url,
                content: new StringContent(JsonConvert.SerializeObject(contact), Encoding.UTF8,
                mediaType: "application/json")
                ).Result;
        }

        /// <summary>
        /// Запрос на удаление контакта по указанному id, передающийся 
        /// на API сервер. Данный запрос принимает id контакта. 
        /// Запрос является невозвратным.
        /// </summary>
        /// <param name="id"></param>
        public void DeleteContact(int id)
        {
            string url = $"https://localhost:7037/api/values/{id}";
            AddTokenHeaderMethod();
            var r = httpClient.DeleteAsync(
                requestUri: url);
        }

        /// <summary>
        /// Запрос на поиск контакта по указанному id, передающийся 
        /// на API сервер. Данный запрос принимает id контакта. 
        /// Запрос возвращает результат в виде экземпляра Contact.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Contact FindContactById(int id)
        {
            string url = $"https://localhost:7037/api/values/Details/{id}";
            string json = httpClient.GetStringAsync(url).Result;
            return JsonConvert.DeserializeObject<Contact>(json);
        }

        /// <summary>
        /// Запрос на изменение контакта, передающийся 
        /// на API сервер. Данный запрос принимает строковые переменные,
        /// которые используются для создания нового экземпляра Contact
        /// с дальнейшей передачей этого экземпляра в API.
        /// Данный метод является невозвратным.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="surname"></param>
        /// <param name="fatherName"></param>
        /// <param name="telephoneNumber"></param>
        /// <param name="residenceAdress"></param>
        /// <param name="description"></param>
        /// <param name="id"></param>
        public void ChangeContact(string name, string surname,
            string fatherName, string telephoneNumber, string residenceAdress, string description, int id)
        {
            Contact contact = new Contact()
            {
                Name = name,
                Surname = surname,
                FatherName = fatherName,
                TelephoneNumber = telephoneNumber,
                ResidenceAdress = residenceAdress,
                Description = description
            };

            string url = $"https://localhost:7037/api/values/ChangeContactById/{id}";
            
            AddTokenHeaderMethod();

            var r = httpClient.PostAsync(
                requestUri: url,
                content: new StringContent(JsonConvert.SerializeObject(contact), Encoding.UTF8,
                mediaType: "application/json")
                ).Result;
        }

        /// <summary>
        /// Запрос на вход пользователя, передающийся в API
        /// Данный запрос принимает модель UserLoginProp
        /// и возвращает строковую переменную с ответом,
        /// который будет включать в себя токен при удачном
        /// входе или пустую строку при неудачном входе.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public string IsLogin(UserLoginProp model)
        {
            string url = $"https://localhost:7037/api/values/Authenticate/";

            var r = httpClient.PostAsync(
                requestUri: url,
                content: new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8,
                mediaType: "application/json")
                ).Result;

            string token = "";
            if (r.IsSuccessStatusCode)
            {
                string json = r.Content.ReadAsStringAsync().Result;
                var tokenResponse = JsonConvert.DeserializeObject<TokenResponse>(json);
                string tokenAuth = tokenResponse.access_token;

                token = tokenAuth;
            }
            else
            {
                token = string.Empty;
            }
            return token;
        }

        /// <summary>
        /// Запрос на регистрацию, передающийся в API сервер.
        /// Данный запрос принимает модель регистрации и возвращает
        /// результат запроса в виде строки, содержащей токен.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public string IsRegistration(UserRegistration model)
        {
            string url = $"https://localhost:7037/api/values/Registration/";

            var r = httpClient.PostAsync(
                requestUri: url,
                content: new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8,
                mediaType: "application/json")
                ).Result;
            string token = "";
            if (r.IsSuccessStatusCode)
            {
                string json = r.Content.ReadAsStringAsync().Result;
                var tokenResponse = JsonConvert.DeserializeObject<TokenResponse>(json);
                string tokenAuth = tokenResponse.access_token;
                token = tokenAuth;
            }
            else
            {
                token = string.Empty;
            }
            return token;
        }

        /// <summary>
        /// Метод добавления токена в заголовок запроса. Данный метод
        /// принимает в себя текущий HTTP-контекст (то есть текущий
        /// запрос), что в свою очередь позволяет обратиться к методу
        /// Request для вызова токена, сохраненного в куки.
        /// В методе происходит запись токена из куки и дальнейшее
        /// добавление токена в заголовок запроса с помощью 
        /// создания нового экземпляра заголовка аутентификации
        /// AuthenticationHeaderValue.
        /// </summary>
        private void AddTokenHeaderMethod()
        {
            string tokenValue = GlobalVariables.token;
            if (!string.IsNullOrEmpty(tokenValue))
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenValue);
            }
        }
    }
}
