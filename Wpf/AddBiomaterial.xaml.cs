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
using WebApplication1.Models;

namespace Wpf
{
    /// <summary>
    /// Логика взаимодействия для AddBiomaterial.xaml
    /// </summary>
    public partial class AddBiomaterial : Window
    {
        clinicEntities2 entities = new clinicEntities2();
        public AddBiomaterial()
        {
            InitializeComponent();
            var idP = (entities.orders.Select(x => x.id).ToList().LastOrDefault())+1;
            tbNumPr.Text = idP.ToString();
        }

        private void tbBarcode_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (tbBarcode.Text.Length > 6)
            {
                tbBarcode.Text = "";
                MessageBox.Show("Слишком длинный штрих-код", "Внимание");
            }
        }

        private void addB_Click(object sender, RoutedEventArgs e)
        {
            orders orders = new orders()
            {
                id = int.Parse(tbNumPr.Text),
                patient = int.Parse(tbNumPat.Text),
                barcode = int.Parse(tbBarcode.Text),
                date = DateTime.Now
            };
            entities.orders.Add(orders);
            entities.SaveChanges();
        }
    }
}
