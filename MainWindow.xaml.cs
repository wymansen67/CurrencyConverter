using System;
using System.Collections.Generic;
using System.IO;
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
using System.Net.Http;

namespace WpfCurrencyConverter
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static private List<Valute> valutes;
        static string path = "valutes.xml";

        public MainWindow()
        {
            InitializeComponent();
            string text = "";
            DateTime today = DateTime.Today;
            try
            {
                string xmlText = File.ReadAllText(path);
                DateTime dt = Data.ValuteLoader.LoadDate(xmlText);
                if (today != dt)
                {
                    HttpClient client = new HttpClient();
                    var respose =
                        client.GetAsync("https://www.cbr.ru/scripts/XML_daily.asp")
                    .GetAwaiter().GetResult();
                    text = respose.Content.ReadAsStringAsync().GetAwaiter().GetResult();

                    StreamWriter sw = new StreamWriter(path, false, Encoding.UTF8);
                    sw.Write(text);
                    sw.Close();
                }
                else
                {
                    text = xmlText;
                }
            }
            catch
            {
               MessageBox.Show("Не удалось обновить данные", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }

            valutes = Data.ValuteLoader.LoadValutes(text);
            valutes.Add(new Valute(0, "RUR", 1, "Российский рубль", 1));
            FromComboBox.ItemsSource = valutes;
            ToComboBox.ItemsSource = valutes;
        }

        private void FilterText(object sender, TextCompositionEventArgs e)
        {
            if (!int.TryParse(e.Text, out int x))
            {
                e.Handled = true;
            }

            if (InputBox.Text.Length > 6)
            {
                e.Handled = true;
            }
        }

        private void SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Calculate(this, null);
        }

        private void Calculate(object sender, TextChangedEventArgs e)
        {
            Valute inValute = FromComboBox.SelectedItem as Valute;
            Valute outValute = ToComboBox.SelectedItem as Valute;

            if (inValute == null || outValute == null)
            {
                return;
            }

            int value;
            bool succ = int.TryParse(InputBox.Text, out value);
            if (!succ) return;

            double rubles = value * inValute.Value;
            double result = rubles / outValute.Value;

            OutputBox.Text = Convert.ToString(Math.Round(result, 2));
        }
    }
}
