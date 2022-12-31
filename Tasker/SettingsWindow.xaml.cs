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
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        public SettingsWindow()
        {
            InitializeComponent();
        }

        private void Appearance_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Customise_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Dark_Mode_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Dark_Mode.IsChecked == true)
            {

                


            }

            if(Dark_Mode.IsChecked == false)
            {
                foreach (var stackPanel in TaskerStore.CurrentStackpanels)
                {
                    stackPanel.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));
                }
            }
        }
    }
}
