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
using System.Windows.Shapes;
using System.Windows.Threading;
using WebApplication1.Models;

namespace Wpf
{
    /// <summary>
    /// Логика взаимодействия для UserWindow2.xaml
    /// </summary>
    public partial class UserWindow2 : Window
    {
        int sec;
        bool logout = false;
        public UserWindow2()
        {
            InitializeComponent();
            clinicEntities2 entities = new clinicEntities2();
            dgpac.ItemsSource = entities.insurances.ToList();
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
    }
}
