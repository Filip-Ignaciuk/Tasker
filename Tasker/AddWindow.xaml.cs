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
                Tasklet task = new Tasklet(TextBoxTitle.Text, 0, TextBoxDescription.Text, ref stackpanels);
                Tasklet.DisplayTask(task);
                TaskerStore.CurrentTasks.Add(task);
            }
            else if (radiobut2.IsChecked == true)
            {
                Tasklet task = new Tasklet(TextBoxTitle.Text, 1, TextBoxDescription.Text, ref stackpanels);
                Tasklet.DisplayTask(task);
                TaskerStore.CurrentTasks.Add(task);
            }
            else if (radiobut3.IsChecked == true)
            {
                Tasklet task = new Tasklet(TextBoxTitle.Text, 2, TextBoxDescription.Text, ref stackpanels);
                Tasklet.DisplayTask(task);
                TaskerStore.CurrentTasks.Add(task);
            }
            else if (radiobut4.IsChecked == true)
            {
                Tasklet task = new Tasklet(TextBoxTitle.Text, 3, TextBoxDescription.Text, ref stackpanels);
                Tasklet.DisplayTask(task);
                TaskerStore.CurrentTasks.Add(task);
            }
            TaskerStore.CurrentStackpanels = stackpanels;
        }
    }
}
