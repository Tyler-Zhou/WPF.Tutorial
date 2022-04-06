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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfBasics
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            cboFinish_SelectionChanged(cboFinish,null);
        }

        private void btnApply_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show($"The description is:{txtDescription.Text}");
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                chbWeld.IsChecked = chbAssembly.IsChecked = chbPlasma.IsChecked = 
                    chbLaser.IsChecked = chbPurchase.IsChecked = chbLathe.IsChecked = 
                    chbDrill.IsChecked = chbFold.IsChecked = chbRoll.IsChecked = 
                    chbSaw.IsChecked = false;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void WorkCentres_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                var check = (CheckBox)sender;
                var checkText = (string)check.Content;
                if (check.IsChecked==true)
                {
                    txtLength.Text += checkText;
                }else
                {
                    txtLength.Text = txtLength.Text.Replace(checkText,"");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cboFinish_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (txtNote==null)
                    return;
                var combo = (ComboBox)sender;
                var value=(ComboBoxItem)combo.SelectedValue;
                txtNote.Text = (string)value.Content;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtSuppliserName_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                txtMass.Text = txtSuppliserName.Text;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
