using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Tasker
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            TaskerStore.MainGrid = MainGrid;
            TaskerStore.StackPanelsContainerGrid = StackPanelsContainer;

            if (!TaskerConfigurator.isinitialised)
            {
                TaskerConfigurator.TaskerSetup();
            }

            TaskerStore.SetIdNumber();
            TaskerStore.SetProfile();

            TaskerStore.ActiveElements.Add(MainGrid);
            TaskerStore.ActiveElements.Add(AddButton);
            TaskerStore.ActiveElements.Add(RemoveButton);
            TaskerStore.ActiveElements.Add(SaveTButton);
            TaskerStore.ActiveElements.Add(SavePButton);
            TaskerStore.ActiveElements.Add(LoadButton);
            TaskerStore.ActiveElements.Add(SettingsButton);
            TaskerStore.ActiveElements.Add(StackPanelsContainer);
        }
        
        
        

        private void About_me(object sender, RoutedEventArgs e)
        {

        }

        private void Add(object sender, RoutedEventArgs e)
        {
            SelectAddWindow SelectAddWindow = new SelectAddWindow();
            SelectAddWindow.Show();


        }

        private void Remove(object sender, RoutedEventArgs e)
        {
            RemoveWindow removeWindow = new RemoveWindow();
            removeWindow.Show();
        }

        private void Settings(object sender, RoutedEventArgs e)
        {
            SettingsWindow settingsWindow = new SettingsWindow();
            settingsWindow.Show();
        }

        private void SaveTasklets(object sender, RoutedEventArgs e)
        {
            TaskerConfigurator.SaveTasklets();
        }

        private void SaveProfile(object sender, RoutedEventArgs e)
        {
            TaskerConfigurator.SaveProfile(TaskerStore.Profile);
        }

        private void Load(object sender, RoutedEventArgs e)
        {
            SelectLoadWindow selectLoadWindow = new SelectLoadWindow();
            selectLoadWindow.Show();

            
        }
    }

    
}
