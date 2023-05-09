using System;
using System.Collections.Generic;
using System.Data;
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

namespace Currency_Converter
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ExchangeRate _ExchangeRate;
        public MainWindow()
        {
            InitializeComponent();

            ClearControls();

            GetExchangeRateValues();

            //MakeRequest($"https://mempool.space/api/v1/blocks/{720000}");
        }

        private async void GetExchangeRateValues()
        {
            _ExchangeRate = new ExchangeRate();
            _ExchangeRate = await Rate.GetRates<ExchangeRate>("https://openexchangerates.org/api/latest.json?app_id=");
            BindCurrencies();
        }

        private void Convert_Click(object sender, RoutedEventArgs e)
        {
            double ConvertedValue = 0;

            if (CheckRightInput())
            {
                try
                {
                    if (cmbFromCurrency.Text == cmbToCurrency.Text)
                    {
                        ConvertedValue = double.Parse(txtCurrency.Text);
                        LabelCurrency.Content = cmbToCurrency.Text + " " + ConvertedValue.ToString("N3");
                    }
                    else
                    {
                        // We multiply ToCurrency selected value with the value entered by the user and divide that by FromCurrency selected value.
                        ConvertedValue = (double.Parse(cmbToCurrency.SelectedValue.ToString()) * double.Parse(txtCurrency.Text))
                            / double.Parse(cmbFromCurrency.SelectedValue.ToString());

                        if (cmbToCurrency.SelectedIndex == 4)
                            LabelCurrency.Content = cmbToCurrency.Text + " " + ConvertedValue.ToString("0.00000000");
                        else
                            LabelCurrency.Content = cmbToCurrency.Text + " " + ConvertedValue.ToString("N2");
                    }
                }
                catch (FormatException)
                {
                    MessageBox.Show("False Format", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
        }

        private bool CheckRightInput()
        {
            // Check amount textbox is Null or Blank
            if (txtCurrency.Text == null || txtCurrency.Text.Trim() == "")
            {
                // If amount textbox is Null or Blank it will show the below message box   
                MessageBox.Show("Please Enter The Desired Amount To Convert.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);

                // After clicking on message box OK sets the Focus on amount textbox
                txtCurrency.Focus();
                return false;
            }
            // Else if the currency from is not selected or it is default text SELECT CURRENCY
            else if (cmbFromCurrency.SelectedValue == null || cmbFromCurrency.SelectedIndex == 0)
            {
                // It will show the message
                MessageBox.Show("Please Select a Currency To Calculate the Value From.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);

                // Set focus
                cmbFromCurrency.Focus();
                return false;
            }
            // Else if Currency To is not Selected or Select Default Text SELECT CURRENCY
            else if (cmbToCurrency.SelectedValue == null || cmbToCurrency.SelectedIndex == 0)
            {
                //It will show the message
                MessageBox.Show("Please Select a Currency To Calculate the Value.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);

                //Set focus on To Combobox
                cmbToCurrency.Focus();
                return false;
            }
            return true;
        }
        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            ClearControls();
        }

        private void BindCurrencies()
        {
            DataTable currencyTable = new DataTable();

            // Add display column in DataTable
            currencyTable.Columns.Add("Text");
            // Add value column in DataTable
            currencyTable.Columns.Add("Value");

            // Add the rows with text and value
            currencyTable.Rows.Add("SELECT CURRENCY", 0);
            currencyTable.Rows.Add("US DOLLAR", _ExchangeRate.Rates.USD);
            currencyTable.Rows.Add("EURO", _ExchangeRate.Rates.EUR);
            currencyTable.Rows.Add("GUATEMALAN QUETZAL", _ExchangeRate.Rates.GTQ);
            currencyTable.Rows.Add("BITCOIN", _ExchangeRate.Rates.BTC);
            currencyTable.Rows.Add("JAPANESE YEN", _ExchangeRate.Rates.JPY);
            currencyTable.Rows.Add("BRITISH POUND", _ExchangeRate.Rates.GBP);
            currencyTable.Rows.Add("EGYPTIAN POUND", _ExchangeRate.Rates.EGP);
            currencyTable.Rows.Add("PHILIPPINE PESO", _ExchangeRate.Rates.PHP);
            currencyTable.Rows.Add("DANISH KRONE", _ExchangeRate.Rates.DKK);

            cmbFromCurrency.ItemsSource = currencyTable.DefaultView;

            // Set the from currency combo box values
            cmbFromCurrency.DisplayMemberPath = "Text";
            cmbFromCurrency.SelectedValuePath = "Value";
            cmbFromCurrency.SelectedIndex = 0;

            // Set the to currency combo box values
            cmbToCurrency.ItemsSource = currencyTable.DefaultView;
            cmbToCurrency.DisplayMemberPath = "Text";
            cmbToCurrency.SelectedValuePath = "Value";
            cmbToCurrency.SelectedIndex = 0;
        }

        /// <summary>
        /// Used to clear all controls value
        /// </summary>
        private void ClearControls()
        {
            txtCurrency.Text = string.Empty;
            if (cmbFromCurrency.Items.Count > 0)
                cmbFromCurrency.SelectedIndex = 0;

            if (cmbToCurrency.Items.Count > 0)
                cmbToCurrency.SelectedIndex = 0;

            LabelCurrency.Content = "";
            txtCurrency.Focus();
        }
    }
}
