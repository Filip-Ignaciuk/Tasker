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





            //Definitions of Stackpanels
            //StackPanel[] stackPanelsLive = new StackPanel[4];
            //stackPanelsLive[0] = Urgent;
            //stackPanelsLive[1] = Required;
            //stackPanelsLive[2] = Optional;
            //stackPanelsLive[3] = Other;
            //TaskerStore.CurrentStackpanels = stackPanelsLive;


            

        }
        
        
        

        private void About_me(object sender, RoutedEventArgs e)
        {

        }

        private void Add(object sender, RoutedEventArgs e)
        {
            AddWindow addWindow = new AddWindow();
            addWindow.Show();
            
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

        private void Save(object sender, RoutedEventArgs e)
        {
            TaskerConfigurator.SaveTasklets();
        }

        private void Load(object sender, RoutedEventArgs e)
        {
            SelectLoadWindow selectLoadWindow = new SelectLoadWindow();
            selectLoadWindow.Show();

            //Tasklet[] tasklets = TaskerConfigurator.LoadTasklets();
            //foreach (Tasklet tasklet in tasklets)
            //{
            //    tasklet.stackPanels = TaskerStore.CurrentStackpanels;
            //    Tasklet.DisplayTask(tasklet);
            //}
        }
    }

    
}
