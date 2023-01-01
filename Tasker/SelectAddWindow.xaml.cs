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

namespace Tasker
{
    /// <summary>
    /// Interaction logic for SelectAddWindow.xaml
    /// </summary>
    public partial class SelectAddWindow : Window
    {
        public SelectAddWindow()
        {
            InitializeComponent();
        }

        private void Tasklet(object sender, RoutedEventArgs e)
        {
            AddWindow addWindow = new AddWindow();
            addWindow.Show();
            this.Close();
        }

        private void Profile(object sender, RoutedEventArgs e)
        {
            AddProfileWindow addProfileWindow = new AddProfileWindow();
            addProfileWindow.Show();
            this.Close();
        }
    }
}
