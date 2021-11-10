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

namespace Wpf
{
    /// <summary>
    /// Логика взаимодействия для Kapcha.xaml
    /// </summary>
    public partial class Kapcha : Window
    {
        public Kapcha()
        {
            InitializeComponent();
            Random rnd = new Random();
            kapchBlock.Text = rnd.Next(1000, 9999).ToString();
        }

        private void kapchAcc_Click(object sender, RoutedEventArgs e)
        {
            int numbK = int.Parse(kapchBlock.Text);
            int numTb = int.Parse(tbkapch.Text);

            if (numbK == numTb)
            {
                UserWindow next = new UserWindow();
                next.Show();
                this.Close();
            }
        }
    }
}
