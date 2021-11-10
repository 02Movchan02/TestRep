using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using WebApplication1.Models;
using Wpf.Models;
using System.IO;
using System.Drawing;
using Color = System.Drawing.Color;
using System.Drawing.Imaging;


namespace Wpf
{
    /// <summary>
    /// Логика взаимодействия для UserWindow.xaml
    /// </summary>
    public partial class UserWindow : Window
    {
        int sec;
        bool logout = false;
        List<Service> listId = new List<Service>();
        public UserWindow()
        {
            InitializeComponent();
            clinicEntities2 entities = new clinicEntities2();
            dgpac.ItemsSource = entities.user_in.ToList();
            dgbiomat.ItemsSource = entities.orders.ToList();
            dgServ.ItemsSource = entities.services.ToList();
            DispatcherTimer timer = new DispatcherTimer();
            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Start();
           

        }

        private void timer_Tick(object sender, EventArgs e)
        {
            sec = int.Parse(timerSecond.Text);
            if (int.Parse(timerMinute.Text) == 05 && int.Parse(timerSecond.Text) == 00)
            {
                MessageBox.Show("Осталось 5 минут");
            }
            if ((int.Parse(timerMinute.Text) == 00) && (int.Parse(timerSecond.Text) == 00) && (int.Parse(timerHour.Text) == 00))
            {
                logout = true;
            }

            if (timerSecond.Text == "00")
            {

                int Min = int.Parse(timerMinute.Text);
                Min -= 1;
                timerMinute.Text = Min.ToString();
                if (timerMinute.Text.Length < 2)
                {
                    timerMinute.Text = "0" + Min.ToString();
                }
                timerSecond.Text = "59";
                sec = int.Parse(timerSecond.Text) + 1;

            }
            if (sec <= 60)
            {
                sec -= 1;
                timerSecond.Text = sec.ToString();
                if (timerSecond.Text.Length < 2)
                {
                    timerSecond.Text = "0" + sec.ToString();
                }

            }
            Logout();


        }
        public void Logout()
        {
            if (logout)
            {
                MainWindow main = new MainWindow();
                main.Show();
                this.Close();
                main.timerWait(60);
                logout = false;
            }
        }

        private void addBio_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private async  Task<PatientDTO<ServiceAnswer>> Test(List<Service> listId, string patId)
        {
            var patientList = new PatientDTO<Service>
            {
                Id = patId,
                Services = listId
            };
 
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string url = "http://localhost:5000/api/analyzer/Biorad";
            var jsonPatients = serializer.Serialize(patientList)
                .ToLower()
                .Replace("code", "serviceCode")
                .Replace("id", "patient");
            StringContent content = new StringContent(jsonPatients, Encoding.UTF8, "application/json");

            PatientDTO<ServiceAnswer> patient;

            using (HttpClient client = new HttpClient())
            {
                var requestResult = client.PostAsync(url, content).Result;

                if (!requestResult.IsSuccessStatusCode)
                    throw new Exception("Ошибка сервера");

                var response = await client.GetAsync(url);

                while (true)
                {
                    response = await client.GetAsync(url);
                    var responseContent = await response.Content.ReadAsStringAsync();
                    if (responseContent.Contains("serviceCode"))
                    {
                        patient = serializer.Deserialize<PatientDTO<ServiceAnswer>>(responseContent
                            .Replace("serviceCode", "code")
                            .Replace("patient", "id"));
                        break;
                    }
                    else if (responseContent.Contains("progress"))
                    {                      
                        loadGf.Visibility = Visibility.Visible;
                    }
                }
                return patient;
            }
        }

        private void sendAnyliz_Click(object sender, RoutedEventArgs e)
        {
            orders item = (orders)dgbiomat.SelectedItem;
            string patId = item.patient.ToString();
            var result = Test(listId, patId);
            MessageBox.Show(result.Result.Id);
        }


        private void cb_Checked(object sender, RoutedEventArgs e)
        {
            var code = (sender as CheckBox).DataContext as services;

            listId.Add(new Service { Code=code.id});

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            

            string barcode = testbar.Text;
            Bitmap bitmap = new Bitmap(barcode.Length * 40, 150);
            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                Font oFont = new System.Drawing.Font("Libre Barcode 128 Text", 20);
                PointF point = new PointF(2f, 2f);
                SolidBrush black = new SolidBrush(Color.Black);
                SolidBrush white = new SolidBrush(Color.White);
                graphics.FillRectangle(white, 0, 0, bitmap.Width, bitmap.Height);
                graphics.DrawString("*" + barcode + "*", oFont, black, point);
            }
            using (MemoryStream ms = new MemoryStream())
            {
                bitmap.Save(ms, ImageFormat.Png);
                ms.Position = 0;
                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = ms;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();

                im.Source = bitmapImage;
                
            }
        }
    }
}




