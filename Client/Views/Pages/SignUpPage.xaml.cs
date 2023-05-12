using Domain.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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

namespace Client.Views.Pages
{
    /// <summary>
    /// Interaction logic for SignUpPage.xaml
    /// </summary>
    public partial class SignUpPage : Page
    {
        public SignUpPage()
        {
            InitializeComponent();
        }

        private async void SignUp_Click(object sender, RoutedEventArgs e)
        {

            var name = UserNameTextBox.Text;
            var email = EmailTextBox.Text;
            var password = PasswordTextBox.Text;
            var createdAt = DateTime.Now;


            using (var httpClient = new HttpClient())
            {
                var content = new StringContent(JsonConvert.SerializeObject(new User()
                {
                    UserName = name,
                    Email = email,
                    Password = password,
                    CreationTime = createdAt
                }), Encoding.UTF8, "application/json");


                var response = await httpClient.PostAsync("https://localhost:7289/api/User", content);

                await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Запрос успешно выполнен!");
                }
                else
                {
                    MessageBox.Show("Запрос выполнен с ошибкой. Код ошибки: " + response.StatusCode);
                }
            }
        }
    }
}
