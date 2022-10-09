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
using System.Windows.Navigation;

namespace Tasker
{
    /// <summary>
    /// Interaction logic for AddWindow.xaml
    /// </summary>
    public partial class AddWindow : Window
    {
        public AddWindow()
        {
            InitializeComponent();
        }

        private void Create(object sender, RoutedEventArgs e)
        {
            var stackpanels = TaskerStore.CurrentStackpanels;
            if (radiobut1.IsChecked == true)
            {
                Task task = new Task(Title.Text, Level.Urgent, Description.Text, ref stackpanels);
            }
            else if (radiobut2.IsChecked == true)
            {
                Task task = new Task(Title.Text, Level.Required, Description.Text, ref stackpanels);
            }
            else if (radiobut3.IsChecked == true)
            {
                Task task = new Task(Title.Text, Level.Optional, Description.Text, ref stackpanels);
            }
            else if (radiobut4.IsChecked == true)
            {
                Task task = new Task(Title.Text, Level.Other, Description.Text, ref stackpanels);
            }
            TaskerStore.CurrentStackpanels = stackpanels;
        }
    }
}
