using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
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
using System.Windows.Threading;
using WebApplication1.Models;

namespace Wpf
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        clinicEntities2 entities = new clinicEntities2();
        int view = 0;
        int minute = 0;
        bool inUser = true;
        //private List<users> user;
        DispatcherTimer timer = new DispatcherTimer();
        public MainWindow()
        {
            InitializeComponent();           
            timer.Tick += new EventHandler(timer_tick);
            timer.Interval = new TimeSpan(0, 0, 1);
            
        }

        private void timer_tick(object sender, EventArgs e)
        {
            if (minute <= 60)
            {
                minute -= 1;
            }
            if (minute == 0)
            {
                MessageBox.Show("Можете входить");
                inUser = true;
            }
        }

        private void btnIn_Click(object sender, RoutedEventArgs e)
        {
            if (inUser)
            {
                if (view == 0)
                {
                   
                    var user = entities.users.Select(x => x.login + x.password).Where(x => x.Contains(tbLogin.Text + pbPass.Password)).FirstOrDefault();

                    var type_us = entities.users.Select(x => x.login + " "+x.type).Where(x => x.Contains(tbLogin.Text)).FirstOrDefault();
                    string[] id = type_us.Split(' ');    
                    switch(id[1])
                    {
                        // Администратор
                        case "1":
                            UserWindow userWin = new UserWindow();
                            this.Close();
                            userWin.Show();
                            break;
                        // Лаборант
                       case "2":
                            UserWindow2 userWin2 = new UserWindow2();
                            this.Close();
                            userWin2.Show();
                            break;

                    }
                    //MessageBox.Show(id[1]);
                    //if (user is null)
                    //{
                    //    MessageBox.Show("Логин или пароль неверны");
                    //    user_in us = new user_in
                    //    {
                    //        userLogin = tbLogin.Text,
                    //        userPassword = pbPass.Password,
                    //        dateIn = DateTime.Now,
                    //        tryIn = "Неудачно"
                    //    };
                    //    entities.user_in.Add(us);
                    //    entities.SaveChanges();
                    //}
                    //else
                    //{
                    //    user_in us = new user_in
                    //    {
                    //        userLogin = tbLogin.Text,
                    //        userPassword = pbPass.Password,
                    //        dateIn = DateTime.Now,
                    //        tryIn = "Удачно"
                    //    };
                    //    entities.user_in.Add(us);
                    //    entities.SaveChanges();
                    //    Kapcha next = new Kapcha();
                    //    next.Show();
                    //    this.Hide();
                    //}
                }
                else
                {
                    var user = entities.users.Select(x => x.login + x.password).Where(x => x.Contains(tbLogin.Text + tbPass.Text)).FirstOrDefault();
                    if (user is null)
                    {
                        MessageBox.Show("Логин или пароль неверны");
                        user_in us = new user_in
                        {
                            userLogin = tbLogin.Text,
                            userPassword = tbPass.Text,
                            dateIn = DateTime.Now,
                            tryIn = "Не удачно"
                        };
                        entities.user_in.Add(us);
                        entities.SaveChanges();
                    }
                    else
                    {
                        user_in us = new user_in
                        {
                            userLogin = tbLogin.Text,
                            userPassword = tbPass.Text,
                            dateIn = DateTime.Now,
                            tryIn = "Удачно"
                        };
                        entities.user_in.Add(us);
                        entities.SaveChanges();
                        Kapcha next = new Kapcha();
                        next.Show();
                        this.Close();
                    }
                }
            }
            else
            {
                MessageBox.Show("До входа осталось: " +minute+ " секунд", "Невозможно");
            }
        }

        private void btnReg_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ViewPass_Click(object sender, RoutedEventArgs e)
        {
            if (view == 0)
            {
                string password = pbPass.Password;
                pbPass.Visibility = Visibility.Hidden;
                tbPass.Visibility = Visibility.Visible;
                tbPass.Text = password;
                view = 1;
            }
            else
            {
                string password = tbPass.Text;
                tbPass.Visibility = Visibility.Hidden;
                pbPass.Visibility = Visibility.Visible;
                pbPass.Password = password;
                view = 0;

            }
        }
        public void timerWait(int min)
        {
            minute = min;
            timer.Start();
        }
    }
}
