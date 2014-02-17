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

namespace Words_With_Kinect
{
    /// <summary>
    /// Interaction logic for StartupOptions.xaml
    /// @Author Kevin Fox
    /// </summary>
    public partial class StartupOptions : Window
    {
        public StartupOptions()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            bool nearMode =false;

            if (RadioBtnEnable.IsChecked == true)
            {
                nearMode = true;
            }
            if (RadioBtnDisable.IsChecked == true)
            {
                nearMode = false;
            }
            MainWindow app = new MainWindow(nearMode);
            App.Current.MainWindow = app;
            this.Close();
            app.Show();
           
        }
    }
}
