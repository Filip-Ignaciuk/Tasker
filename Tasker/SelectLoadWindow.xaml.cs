using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.IO;
using Ookii.Dialogs.Wpf;
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
    /// Interaction logic for SelectLoadWindow.xaml
    /// </summary>
    public partial class SelectLoadWindow : Window
    {
        public SelectLoadWindow()
        {
            InitializeComponent();
        }

        private void Profile_Click(object sender, RoutedEventArgs e)
        {
            VistaOpenFileDialog dialog = new VistaOpenFileDialog();

            var result = dialog.ShowDialog();

            if (result == true && dialog.CheckPathExists)
            {
                string path = dialog.FileName;
                Profile profile = TaskerConfigurator.LoadProfile(path);
                TaskerStore.Profile = profile;
                StreamWriter streamWriter = new StreamWriter(TaskerStore.DocumentDir + @"\Tasker\lp.txt");
                streamWriter.WriteLine(dialog.InitialDirectory);
                streamWriter.Close();
            }
        }

        private void Tasklet_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
